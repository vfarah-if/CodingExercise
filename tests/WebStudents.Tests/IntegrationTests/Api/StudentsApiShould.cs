using Exercise.Domain.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace WebStudents.Tests.IntegrationTests.Api
{
    public class StudentsApiShould : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public StudentsApiShould(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetStudentsWithOkResponseAndAPagedResultOfStudentData()
        {
            var response = await _client.GetAsync("api/students");

            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
            var actual = JsonConvert.DeserializeObject<PagedResult<Student>>(content);
            actual.CurrentPage.Should().Be(1);
        }
    }
}
