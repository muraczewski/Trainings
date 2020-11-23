using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessLayer.Models;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace RestApiCoreTrainings.IntegrationTests
{
    [TestFixture]
    public class PersonControllerTests : TestBase
    {
        [Test]
        public async Task GetPeople_When_NoPeople_Then_NoContentReturned()
        {
            // when
            var response = await _client.GetAsync("api/people");

            // then
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test]
        public async Task GetPeople_When_OnePersonExists_Then_OkReturned()
        {
            // given
            var person = new Person(1);
            var content = JsonConvert.SerializeObject(person);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            await _client.PostAsync("api/people", stringContent, new CancellationToken());

            // when
            var response = await _client.GetAsync("api/people");

            // then
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
