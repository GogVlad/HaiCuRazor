using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RazorMvc.Data;
using RazorMvc.Hubs;
using RazorMvc.Services;

namespace RazorMvc
{
    public class Startup
    {
        private string connectionString;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            connectionString = env.IsDevelopment() ? Configuration.GetConnectionString("DefaultConnection") : GetConnectionString();
        }

        public IConfiguration Configuration { get; }

        public static string ConvertDbUrlToConnectionString(string envDbUrl)
        {
            Uri url;
            bool isUrl = Uri.TryCreate(envDbUrl, UriKind.Absolute, out url);
            if (isUrl)
            {
                return $"Server={url.Host};Port={url.Port};Database={url.LocalPath.Substring(1)};User Id={url.UserInfo.Split(':')[0]};Password={url.UserInfo.Split(':')[1]};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
            }

            throw new FormatException($"Database URL connot be converted! Check this {envDbUrl}.");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<InternDbContext>(options =>
                  options.UseNpgsql(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddControllersWithViews();
            services.AddScoped<IInternshipService, InternshipDbService>();
            services.AddScoped<EmployeeDbService>();
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "InternshipClass.WebAPI", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddSignalR();
            services.AddSingleton<MessageService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RazorMVC v1"));
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("MyPolicy");

            app.UseAuthorization();

            app.UseMigrationsEndPoint();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<MessageHub>("/messagehub");
            });
        }

        private string GetConnectionString()
        {
            var envDbUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            var herokuConnectionString = ConvertDbUrlToConnectionString(envDbUrl);
            return herokuConnectionString;
        }
    }
}
