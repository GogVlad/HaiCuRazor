using RazorMvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InternshipClass.Tests
{
    public class StartupTests
    {
        [Fact]
        public void ShouldConvertDbUrlToConnectionString()
        {
            //Assume
            var url = "postgres://uxdhdkwzilyzlr:b4209a83facb6c744fd0b947cc5f847761d11c6eb29820fd3c92f1fc4531f04a@ec2-99-80-200-225.eu-west-1.compute.amazonaws.com:5432/dc1nj3ujh52uk7";
            //Act
            var herokuConnectionString = Startup.ConvertDbUrlToConnectionString(url);
            //Assert
            Assert.Equal("Server=ec2-99-80-200-225.eu-west-1.compute.amazonaws.com;Port=5432;Database=dc1nj3ujh52uk7;User Id=uxdhdkwzilyzlr;Password=b4209a83facb6c744fd0b947cc5f847761d11c6eb29820fd3c92f1fc4531f04a;Pooling=true;SSL Mode=Require;Trust Server Certificate=True;", herokuConnectionString);
        }
    }
}
