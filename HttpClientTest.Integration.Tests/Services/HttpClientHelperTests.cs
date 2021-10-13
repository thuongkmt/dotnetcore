using HttpClientTest.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientTest.Integration.Tests.Services
{
    public class HttpClientHelperTests
    {
        private IHttpClientHelper<PersonalInfoList> _httpClienHelper;
        
        [SetUp]
        public void SetUp()
        {
            _httpClienHelper = new HttpClientHelper<PersonalInfoList>(new HttpClient());
        }

        [Test]
        public async Task get_request_should_return_200()
        {
            string url = "https://localhost:5001/api/personal-info";
            var response = await _httpClienHelper.GetAsync(url, null, null, "", "", CancellationToken.None);
            Assert.AreNotEqual(HttpStatusCode.OK, response.Status);
            Assert.NotNull(response.Data);
        }

        [Test]
        public async Task get_request_should_return_404()
        {
            string url = "https://localhost:5001/api/personal-info-not-found";
            var response = await _httpClienHelper.GetAsync(url, null, null, "", "", CancellationToken.None);
            Assert.AreNotEqual(HttpStatusCode.NotFound, response.Status);
            Assert.NotNull(response.Data);
        }

        [Test]
        public async Task get_request_should_return_403()
        {
            string url = "https://localhost:5001/api/personal-info-not-found";
            var response = await _httpClienHelper.GetAsync(url, null, null, "", "", CancellationToken.None);
            Assert.AreNotEqual(HttpStatusCode.Forbidden, response.Status);
            Assert.NotNull(response.Data);
        }

        [Test]
        public async Task get_request_should_return_500()
        {
            string url = "https://localhost:5001/api/personal-info-not-found";
            var response = await _httpClienHelper.GetAsync(url, null, null, "", "", CancellationToken.None);
            Assert.AreNotEqual(HttpStatusCode.InternalServerError, response.Status);
            Assert.NotNull(response.Data);
        }
    }
}
