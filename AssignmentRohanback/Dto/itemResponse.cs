using System.Text.Json.Serialization;

namespace AssignmentRohanback.Dto
{
    public class itemResponse
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Item_Name")]
        public string ItemName { get; set; }
    }
}
