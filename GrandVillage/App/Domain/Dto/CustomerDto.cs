using Newtonsoft.Json;

namespace GrandVillage.App.Domain.Dto
{
    public class CustomerDto
    {
        [JsonProperty("customerId")]
        public string? CustomerId { get; set; }

        [JsonProperty("documentNumber")]
        public string DocumentNumber { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
