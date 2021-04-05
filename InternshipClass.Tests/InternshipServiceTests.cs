using RazorMvc.Models;
using RazorMvc.Services;
using System.Linq;
using Xunit;

namespace RazorMvc.Tests
{
    public class InternshipServiceTests
    {
        [Fact]
        public void InitiallyContainsThreeMembers()
        {
            // Assume
            var intershipService = new InternshipService();

            // Act

            // Assert
            Assert.Equal(3, intershipService.GetClass().Members.Count);
        }

        [Fact]
        public void WhenAddMemberItShouldBeThere()
        {
            // Assume
            var intershipService = new InternshipService();
            Intern intern = new Intern();

            // Act
            intern.Name = "Marko";
            intershipService.AddMember(intern);

            // Assert
            Assert.Equal(4, intershipService.GetClass().Members.Count);
            Assert.Contains("Marko", intershipService.GetClass().Members.Select(member => member.Name));
        }
    }
}