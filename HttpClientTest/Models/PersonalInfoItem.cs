using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientTest.Models
{
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
