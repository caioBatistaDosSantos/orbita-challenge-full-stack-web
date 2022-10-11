using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using students_db.Tests;
using students_db.Models;
using students_db.Request;

namespace StudentsAPI.Tests
{
    public class ApiStudentsTests
    {
        [Fact]
        public async Task GET_all_students_test_sucess()
        {
            await using var application = new StudentsFactoryTest();

            await StudentsMockData.CreateAPI(application, true);
            var url = "/Students";

            var client = application.CreateClient();

            var result = await client.GetAsync(url);
            var students = await client.GetFromJsonAsync<IEnumerable<Student>>("/Students");

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            students.Count().Should().Be(2);
        }

        [Fact]
        public async Task GET_all_students_test_empty()
        {
            await using var application = new StudentsFactoryTest();

            await StudentsMockData.CreateAPI(application, false);
            var url = "/Students";

            var client = application.CreateClient();

            var result = await client.GetAsync(url);
            var students = await client.GetFromJsonAsync<IEnumerable<Student>>("/Students");

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            students.Count().Should().Be(0);
        }


        [Fact]
        public async Task GET_students_by_PK_test_sucess()
        {
            await using var application = new StudentsFactoryTest();

            var student = new Student 
            {
                RA = 2222,
                Name = "Students 2",
                Email = "student2@email.com",
                CPF = 22222222222,
            };

            await StudentsMockData.CreateAPI(application, true);
            var url = "/Students/2222";

            var client = application.CreateClient();

            var result = await client.GetAsync(url);
            var content = await result.Content.ReadFromJsonAsync<Student>();

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Should().BeEquivalentTo(student);
        }

        [Fact]
        public async Task GET_students_by_PK_test_failed()
        {
            await using var application = new StudentsFactoryTest();

            await StudentsMockData.CreateAPI(application, false);
            var url = "/Students/2222";

            var client = application.CreateClient();

            var result = await client.GetAsync(url);
            var content = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            content.Should().Be("Student not found");
        }

        [Fact]
        public async Task POST_students_test_sucess()
        {
            await using var application = new StudentsFactoryTest();
            await StudentsMockData.CreateAPI(application, false);

            var student = new Student
            {
                RA = 123456,
                Name = "New Student",
                Email = "new@student.com",
                CPF = 12345678910,
            };

            var client = application.CreateClient();

            var result = await client.PostAsJsonAsync("/Students", student);
            var content = await result.Content.ReadFromJsonAsync<Student>();

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Should().BeEquivalentTo(student);
        }

        public async Task POST_students_test_failed()
        {
            await using var application = new StudentsFactoryTest();
            await StudentsMockData.CreateAPI(application, true);

            var student = new Student 
            {
                RA = 2222,
                Name = "Students 2",
                Email = "student2@email.com",
                CPF = 22222222222,
            };

            var client = application.CreateClient();

            var result = await client.PostAsJsonAsync("/Students", student);
            var content = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            content.Should().Be("Student already registered");
        }

        [Fact]
        public async Task PUT_students_test_sucess()
        {
            await using var application = new StudentsFactoryTest();
            await StudentsMockData.CreateAPI(application, true);

            var client = application.CreateClient();
            var url = "/Students/2222";

            var request = new StudentRequest
            {
                Name = "Student updated",
                Email = "student@updated.com",
            };

            var result = await client.PutAsJsonAsync(url, request);
            var content = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            content.Should().Be("Student '2222' updated");
        }

        [Fact]
        public async Task PUT_students_test_failed()
        {
            await using var application = new StudentsFactoryTest();
            await StudentsMockData.CreateAPI(application, false);

            var client = application.CreateClient();
            var url = "/Students/2222";

            var request = new StudentRequest
            {
                Name = "Student updated",
                Email = "student@updated.com",
            };

            var result = await client.PutAsJsonAsync(url, request);
            var content = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            content.Should().Be("Student not found");
        }

        [Fact]
        public async Task DELETE_students_test_sucess()
        {
            await using var application = new StudentsFactoryTest();
            await StudentsMockData.CreateAPI(application, true);

            var client = application.CreateClient();
            var url = "/Students/2222";

            var result = await client.DeleteAsync(url);
            var content = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DELETE_students_test_failed()
        {
            await using var application = new StudentsFactoryTest();
            await StudentsMockData.CreateAPI(application, false);

            var client = application.CreateClient();
            var url = "/Students/2222";

            var result = await client.DeleteAsync(url);
            var content = await result.Content.ReadAsStringAsync();

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            content.Should().Be("Student not found");
        }
    }
}