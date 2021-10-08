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
            Console.WriteLine("Hello World!");

            var httpClient = new HttpClientHelper<PersonalInfoList>(new HttpClient());
            //1. add header
            var header = new HeaderDictionary();
            header.Add("Type", "payment-history");
            header.Add("Code", "xxp");

            //2. add query string
            var queryString = new NameValueCollection();
            queryString.Add("query", "thuongkmt");

            //3. authentication infor
            var authType = "Bearer";
            var token = "hello";
            
            //4. make request
            var urlCloud = "https://localhost:5001/api/personal-info";
            var data = await httpClient.GetAsync(
                url: urlCloud,
                queryString,
                header: header,
                authType: authType,
                token: token,
                cancellationToken: CancellationToken.None);

            //

            Console.ReadLine();
        }
    }
    public class PersonalInfoList
    {
        [JsonProperty("personalInfo")]
        public List<PersonalInfoItem> PersonalInfo { get; set; }
    }

    public class PersonalInfoItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("residentialAddress")]
        public string ResidentialAddress { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("sameAsResidentialAddress")]
        public bool SameAsResidentialAddress { get; set; }

        [JsonProperty("postalAddress")]
        public string PostalAddress { get; set; }

        [JsonProperty("emailNotification")]
        public bool EmailNotification { get; set; }

        [JsonProperty("mobileTextNotification")]
        public bool MobileTextNotification { get; set; }
    }
}
