using Exercise.Domain.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Xunit;

namespace WebStudents.Tests.IntegrationTests.Api
{
    public class StudentsApiShould : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        const string apiStudentsRequestUri = "api/students";

        private readonly HttpClient _client;

        public StudentsApiShould(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetStudentsWithOkResponseAndAPagedResultOfStudentData()
        {
            var response = await _client.GetAsync(apiStudentsRequestUri);

            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
            var actual = JsonConvert.DeserializeObject<PagedResult<Student>>(content);
            actual.CurrentPage.Should().Be(1);
        }

        [Fact]
        public async Task ReturnNotFoundIfAStudentDoesNotExist()
        {
            var response = await _client.GetAsync($"{apiStudentsRequestUri}/5cdd3fec0f73e80728ed824f");

            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ReturnCreatedForNewRecordAndNoContentForDeletingTheRecord()
        {
            var student = Student.Create(null, "Mr", 21, "Samuel", "Farah");
            var response = await _client.PostAsync(apiStudentsRequestUri, new JsonContent(student));

            response.IsSuccessStatusCode.Should().BeTrue("Failed to create");
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var content = await response.Content.ReadAsStringAsync();
            var expectedStudent = JsonConvert.DeserializeObject<Student>(content);
            expectedStudent.Should().NotBeNull();
            expectedStudent.Id.Should().NotBeNullOrEmpty();

            if (response.IsSuccessStatusCode)
            {
                response = await _client.DeleteAsync($"{apiStudentsRequestUri}/{expectedStudent.Id}");
                response.IsSuccessStatusCode.Should().BeTrue("Failed to delete");
                response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            }
        }
    }

    public class JsonContent : StringContent
    {
        public JsonContent(object obj) :
            base(JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            }), Encoding.UTF8, "application/json")
        { }
    }
}
