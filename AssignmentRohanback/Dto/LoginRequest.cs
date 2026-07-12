namespace AssignmentRohanback.Dto
{
    using System.Text.Json.Serialization;

    public class LoginRequest
    {
        [JsonPropertyName("API_Action")]
        public string ApiAction { get; set; } = "GetLoginData";

        [JsonPropertyName("Device_Id")]
        public string DeviceId { get; set; }

        [JsonPropertyName("Sync_Time")]
        public string SyncTime { get; set; } = "";

        [JsonPropertyName("Company_Code")]
        public string CompanyCode { get; set; }

        [JsonPropertyName("API_Body")]
        public ApiBody ApiBody { get; set; }
    }

    public class ApiBody
    {
        [JsonPropertyName("Username")]
        public string Username { get; set; }

        [JsonPropertyName("Pw")]
        public string Pw { get; set; }
    }
}
