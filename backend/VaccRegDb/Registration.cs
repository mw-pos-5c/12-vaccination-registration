using Newtonsoft.Json;

#nullable disable

namespace VaccRegDb
{
    public partial class Registration
    {
       
        public string FirstName { get; set; }
        public long Id { get; set; }
        public string LastName { get; set; }
        [JsonProperty("pin")]
        public long PinCode { get; set; }
        [JsonProperty("ssn")]
        public long SocialSecurityNumber { get; set; }

        public virtual Vaccination Vaccination { get; set; }
    }
}
