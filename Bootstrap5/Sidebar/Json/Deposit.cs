using System.Text.Json.Serialization;

namespace Sidebar.Json
{
    public class Deposit
    {
        [JsonPropertyName("RegistrationDate")]
        public DateTime RegistrationDate { get; set; }
        [JsonPropertyName("UserID")]
        public required string UserID { get; set; }
        [JsonPropertyName("Name")]
        public required string Name { get; set; }
        [JsonPropertyName("Gender")]
        public string? Gender { get; set; }
        [JsonPropertyName("Age")]
        public int Age { get; set; }
        [JsonPropertyName("Total")]
        public decimal Total { get; set; }
        [JsonPropertyName("Birthday")]
        public DateTime Birthday { get; set; }
    }
}
