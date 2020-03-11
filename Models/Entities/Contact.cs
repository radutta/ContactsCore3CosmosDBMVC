//using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ContactsCore3CosmosDBMVC.Models.Entities
{
  public class Contact
  {
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    [JsonProperty(PropertyName = "contactName")]
    public string ContactName { get; set; }
    [JsonProperty(PropertyName = "phone")]
    public string Phone { get; set; }
    [JsonProperty(PropertyName = "contactType")]
    public string ContactType { get; set; }
    //[JsonPropertyName("email")]
    [JsonProperty(PropertyName = "email")]
    public string Email { get; set; }
  }
}