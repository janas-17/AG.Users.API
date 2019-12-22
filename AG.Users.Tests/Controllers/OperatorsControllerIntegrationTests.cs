using AG.Users.API;
using AG.Users.EFCore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AG.Users.Tests.Controllers
{
    public class OperatorsControllerIntegrationTests : IClassFixture<UsersWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public OperatorsControllerIntegrationTests(UsersWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CanGetOperators()
        {
            var httpResponse = await _client.GetAsync("/api/operators");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Operator>>(stringResponse);

            Assert.Contains(result, p => p.FirstName == "John");
            Assert.Contains(result, p => p.FirstName == "Jane");
        }


        [Fact]
        public async Task CanGetOperatorById()
        {
            var httpResponse = await _client.GetAsync("/api/operators/1");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Operator>(stringResponse);

            Assert.Equal(1, result.Id);
            Assert.Equal("John", result.FirstName);
        }

        [Fact]
        public async Task CanInsertOperator()
        {
            var request = new
            {
                Url = "/api/operators",
                Body = new
                {
                    gameName = "byoung",
                    approved = true,
                    id = 0,
                    firstName = "billy",
                    lastName = "young",
                    dateofBirth = DateTime.Today.AddYears(-20)
                }
            };

            var httpResponse = await _client.PostAsync(request.Url,
                new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.Default, "application/json"));

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Operator>(stringResponse);

            Assert.NotEqual(0, result.Id);
        }

        [Fact]
        public async Task CannotUpdateOperatorIfNotApproved()
        {
            var request = new
            {
                Url = "/api/operators/1",
                Body = new
                {
                    gameName = "NotUpdatedJdoe",
                    approved = false,
                    id = 1,
                    firstName = "NotUpdatedJohn",
                    lastName = "NotUpdatedDoe",
                    dateofBirth = new DateTime(2000, 1, 1)
                }
            };

            // Should throw exception
            await Assert.ThrowsAsync<Exception>(() => _client.PutAsync(request.Url,
                 new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.Default, "application/json")));
        }
    }
}
