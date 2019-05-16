using Exercise.Domain.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace WebStudents.Tests.IntegrationTests.Api
{
    public class StudentsApiShould : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        const string apiStudentsRequestUri = "api/students";

        private readonly HttpClient _client;
        private Student currentStudent;

        public StudentsApiShould(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ListStudentsWithOkResponsePaginatingStudentData()
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
        public async Task ReturnCreatedForNewRecord()
        {
            try
            {
                var response = await CreateAndVerifyResponse("Mr", "Joe", "Bloggs", 21);
                var content = await response.Content.ReadAsStringAsync();
                currentStudent = JsonConvert.DeserializeObject<Student>(content);
                currentStudent.Should().NotBeNull();
                currentStudent.Id.Should().NotBeNullOrEmpty();
                await CheckIfExistsAndVerifyResponse(currentStudent);
            }
            finally
            {
                await DeleteAndVerifyResponse(currentStudent);
            }
        }

        [Fact]
        public async Task ReturnBadRequestForCreateWhenAgeIsOver100()
        {
            var badAge = 101;
            var response = await Create("Mr", "Joe", "Bloggs", badAge);
            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
            var result = JsonConvert.DeserializeObject<ErrorResult>(content);
            result.Should().NotBeNull();
            result.HasErrors.Should().BeTrue();
            result.Errors.First().Key.Should().Be("Age");
        }

        [Fact]
        public async Task ReturnOkResponseWhenUpdatingAnExistingRecord()
        {
            try
            {
                // Create
                var response = await CreateAndVerifyResponse("Mrs", "Jane", "Doe", 21);
                var content = await response.Content.ReadAsStringAsync();
                currentStudent = JsonConvert.DeserializeObject<Student>(content);

                // Update
                currentStudent.Age = currentStudent.Age + 1;
                currentStudent.Salutation = "Professor";
                response = await UpdateAndVerifyResponse(currentStudent);
                content = await response.Content.ReadAsStringAsync();
                var updatedStudent = JsonConvert.DeserializeObject<Student>(content);
                currentStudent.Age.Should().Be(updatedStudent.Age);
                currentStudent.Salutation.Should().Be(updatedStudent.Salutation);
            }
            finally
            {
                await DeleteAndVerifyResponse(currentStudent);
            }
        }

        private async Task CheckIfExistsAndVerifyResponse(Student student)
        {
            var response = await Head(student);
            response.IsSuccessStatusCode.Should().BeTrue("Failed to check if exists");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private async Task CheckIfNotFoundExistsAndVerifyResponse(Student student)
        {
            var response = await Head(student);
            response.IsSuccessStatusCode.Should().BeFalse("Record exists");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        private async Task<HttpResponseMessage> UpdateAndVerifyResponse(Student expectedStudent)
        {
            var response = await _client.PutAsync($"{apiStudentsRequestUri}/{expectedStudent.Id}",
                new JsonContent(expectedStudent));
            response.IsSuccessStatusCode.Should().BeTrue("Failed to update");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            return response;
        }

        private async Task DeleteAndVerifyResponse(Student expectedStudent)
        {
            var response = await Delete(expectedStudent);
            response.IsSuccessStatusCode.Should().BeTrue( "Failed to delete");
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            await CheckIfNotFoundExistsAndVerifyResponse(expectedStudent);
        }

        private async Task<HttpResponseMessage> CreateAndVerifyResponse(string salutation, string firstname, string lastname, int age)
        {
            var response = await Create(salutation, firstname, lastname, age);
            response.IsSuccessStatusCode.Should().BeTrue("Failed to create");
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            return response;
        }

        private async Task<HttpResponseMessage> Create(string salutation, string firstname, string lastname, int age)
        {
            var student = Student.Create(null, salutation, age, firstname, lastname);
            var response = await _client.PostAsync(apiStudentsRequestUri, new JsonContent(student));
            return response;
        }

        private async Task<HttpResponseMessage> Delete(Student expectedStudent)
        {
            var response = await _client.DeleteAsync($"{apiStudentsRequestUri}/{expectedStudent.Id}");
            return response;
        }

        private async Task<HttpResponseMessage> Head(Student student)
        {
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Head,
                $"{apiStudentsRequestUri}/{student.Id}");
            var response = await _client.SendAsync(request);
            return response;
        }
    }
}
