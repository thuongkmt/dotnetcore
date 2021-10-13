using HttpClientTest.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to HttpClientTest: ");
            while (true)
            {
                var key = Console.ReadLine();
                switch (key)
                {
                    case "M":
                        Console.WriteLine("Input your path: ");
                        string path = Console.ReadLine();
                        if (string.IsNullOrEmpty(path))
                        {
                            path = "personal-info";
                        }
                        await MakeRequest(path);
                        break;
                    default: 
                        break;
                }
            }
        }

        public static async Task MakeRequest(string path)
        {
            var httpClient = new HttpClientHelper<GitRepoResponseTest>(new HttpClient());
            //1. add header
            var headers = new HeaderDictionary();
            headers.Add("Authorization", "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");//forbidden
            headers.Add("Accept", "application/vnd.github.v3+json");
            //2. add query-string

            //3. make request
            var data = await httpClient.GetAsync(
                url: "https://api.github.com/repos/thuongkmt/dotnetcore",
                queryString: null,
                 header: headers,
                authType: "",
                token: "",
                cancellationToken: CancellationToken.None);

            //
            Console.WriteLine(JsonConvert.SerializeObject(data));
        }
    }
    public class GitRepoResponseTest
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("node_id")]
        public string NodeId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("private")]
        public bool Private { get; set; }
    }
}
