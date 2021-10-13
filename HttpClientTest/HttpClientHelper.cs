using HttpClientTest.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace HttpClientTest
{
    public class HttpClientHelper<T> : IHttpClientHelper<T> where T : class
    {
        private readonly HttpClient _client;

        public HttpClientHelper(HttpClient client)
        {
            _client = client;
        }

        public async Task<CommonResponse<T>> GetAsync(string url, NameValueCollection queryString, HeaderDictionary header, string authType, string token, CancellationToken cancellationToken)
        {
            HttpRequestMessage request = null;
            if (!string.IsNullOrEmpty(authType) && !string.IsNullOrEmpty(token))
            {
                Validation(authType, token);
            }
            if(header != null)
            {
                AddHeader(header);
            }
            string queryStringUrl = "";
            if (queryString != null)
            {
                queryStringUrl = AddQueryString(queryString, url);
            }
            if (string.IsNullOrEmpty(queryStringUrl))
            {
                request = new HttpRequestMessage(HttpMethod.Get, url);
            }
            else
            {
                request = new HttpRequestMessage(HttpMethod.Get, queryStringUrl);
            }

            try
            {
                using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var dataResult = JsonConvert.DeserializeObject<T>(json);

                    var commonRespone = new CommonResponse<T>
                    {
                        Status = response.StatusCode.ToString(),
                        Message = response.ReasonPhrase,
                        Data = dataResult
                    };
                    return commonRespone;
                }
            }
            catch(Exception ex)
            {
                var commonRespone = new CommonResponse<T>
                {
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = ex.Message.ToString(),
                    Data = null
                };
                return commonRespone;
            }
        }

        public void Validation(string authType, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authType, token);
        }

        public void AddHeader(HeaderDictionary headers)
        {
            headers.ToList().ForEach(header => _client.DefaultRequestHeaders.Add(header.Key, header.Value.ToString()));
        }

        public string AddQueryString(NameValueCollection queryString, string baseUrl)
        {
            var builder = new UriBuilder(baseUrl);
            var query = HttpUtility.ParseQueryString(builder.Query);

            foreach(var item in queryString.AllKeys)
            {
                query[item] = queryString[item];
            }

            builder.Query = query.ToString();
            return builder.ToString();
        }
    }
}
