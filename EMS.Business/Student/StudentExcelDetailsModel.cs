using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EMS.Business
{
    [JsonObject]
    public class StudentExcelDetailsModel
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("FatherInitial")]
        public string FatherInitial { get; set; }

        [JsonProperty("Group")]
        public string Group { get; set; }

        [JsonProperty("Year")]
        public int Year { get; set; }

        [JsonProperty("Specialty")]
        public string Specialty { get; set; }

        [JsonProperty("RegistrationNumber")]
        public string RegistrationNumber { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Courses")]
        public List<string> Courses { get; set; }
    }
}
