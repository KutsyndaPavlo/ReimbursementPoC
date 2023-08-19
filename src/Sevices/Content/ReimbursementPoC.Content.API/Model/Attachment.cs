using Newtonsoft.Json;

namespace ReimbursementPoC.Content.API.Model
{
    public class Attachment
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}
