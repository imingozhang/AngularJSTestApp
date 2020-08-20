using System;
using System.Net.Http;
using System.Net;
using Xunit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Mingo.NgAppTest.Models;

namespace Mingo.NgAppTest.UnitTests
{
    public class PatternMatchUnitTest
    {
        private static readonly string url = "http://localhost:60307/api/findpattern/check";
        private HttpClient httpClient;

        public PatternMatchUnitTest()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(url);
        }

        /// <summary>
        /// Match single result
        /// </summary>
        [Fact]
        public async void SinglePatternExistedTest()
        {
            JObject payload = new JObject()
            {
                ["text"] = "Test text ST mmu St %$ sT",
                ["subtext"] = "text",
            };

            var stringPayload = JsonConvert.SerializeObject(payload);
            HttpContent content = new StringContent(stringPayload, System.Text.Encoding.UTF8, "application/json");

            var result = await httpClient.PostAsync("", content);
            var returnContent = await result.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(returnContent);

            var patternResult = JsonConvert.DeserializeObject<PatternResult>(returnContent);

            Assert.True(patternResult.Found);
            Assert.Equal("5", patternResult.Positions);

        }

        /// <summary>
        /// Match multiple results
        /// </summary>
        [Fact]
        public async void MultiplePatternExistedTest()
        {
            JObject payload = new JObject()
            {
                ["text"] = "Test text ST mmu St %$ sT",
                ["subtext"] = "st",
            };

            var stringPayload = JsonConvert.SerializeObject(payload);
            HttpContent content = new StringContent(stringPayload, System.Text.Encoding.UTF8, "application/json");

            var result = await httpClient.PostAsync("", content);
            var returnContent = await result.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(returnContent);

            var patternResult = JsonConvert.DeserializeObject<PatternResult>(returnContent);

            Assert.True(patternResult.Found);
            Assert.Equal("2,10,17,23", patternResult.Positions);

        }

        /// <summary>
        /// No match
        /// </summary>
        [Fact]
        public async void PatternNotFoundTest()
        {
            JObject payload = new JObject()
            {
                ["text"] = "Test text ST mmu St %$ sT",
                ["subtext"] = "UQQ",
            };

            var stringPayload = JsonConvert.SerializeObject(payload);
            HttpContent content = new StringContent(stringPayload, System.Text.Encoding.UTF8, "application/json");

            var result = await httpClient.PostAsync("", content);
            var returnContent = await result.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(returnContent);

            var patternResult = JsonConvert.DeserializeObject<PatternResult>(returnContent);

            Assert.False(patternResult.Found);
            Assert.Null(patternResult.Positions);

        }

        /// <summary>
        /// Check input null
        /// </summary>
        [Fact]
        public async void InputTextNullTest()
        {
            JObject payload = new JObject()
            {
                ["subtext"] = "UQQ",
            };

            var stringPayload = JsonConvert.SerializeObject(payload);
            HttpContent content = new StringContent(stringPayload, System.Text.Encoding.UTF8, "application/json");

            var result = await httpClient.PostAsync("", content);
            var returnContent = await result.Content.ReadAsStringAsync();
            Assert.NotEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(returnContent);

            Assert.Contains("The input text should not be null", returnContent);
        }

        /// <summary>
        /// Check input subtext null
        /// </summary>
        [Fact]
        public async void SubtextNullTest()
        {
            JObject payload = new JObject()
            {
                ["text"] = "Test text ST mmu St %$ sT",
                ["sub"] = "UQQ",
            };

            var stringPayload = JsonConvert.SerializeObject(payload);
            HttpContent content = new StringContent(stringPayload, System.Text.Encoding.UTF8, "application/json");

            var result = await httpClient.PostAsync("", content);
            var returnContent = await result.Content.ReadAsStringAsync();
            Assert.NotEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(returnContent);

            Assert.Contains("The input subtext should not be null", returnContent);
        }


    }
}
