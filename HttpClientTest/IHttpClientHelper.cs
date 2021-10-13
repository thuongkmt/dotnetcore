using HttpClientTest.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientTest
{
    public interface IHttpClientHelper<T> where T : class
    {
        Task<CommonResponse<T>> GetAsync(string url, NameValueCollection queryString, HeaderDictionary header, string authType, string token, CancellationToken cancellationToken);
    }
}
