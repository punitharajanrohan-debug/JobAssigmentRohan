namespace AssignmentRohanback.Dto
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class LoginResponse
    {
        [JsonPropertyName("Status_Code")]
        public int StatusCode { get; set; }

        [JsonPropertyName("Sync_Time")]
        public string SyncTime { get; set; }

        [JsonPropertyName("Message")]
        public string Message { get; set; }

        [JsonPropertyName("Response_Body")]
        public List<LoginResponseBodyDto> ResponseBody { get; set; }
    }

    public class LoginResponseBodyDto
    {
        [JsonPropertyName("User_Code")]
        public string UserCode { get; set; }

        [JsonPropertyName("User_Display_Name")]
        public string UserDisplayName { get; set; }

        [JsonPropertyName("Email")]
        public string Email { get; set; }

        [JsonPropertyName("User_Employee_Code")]
        public string UserEmployeeCode { get; set; }

        [JsonPropertyName("Company_Code")]
        public string CompanyCode { get; set; }

        [JsonPropertyName("User_Locations")]
        public List<UserLocationDto> UserLocations { get; set; }

        [JsonPropertyName("Invoice_Types")]
        public List<InvoiceTypeDto> InvoiceTypes { get; set; }

        [JsonPropertyName("Initial_Codes")]
        public List<InitialCodeDto> InitialCodes { get; set; }
    }

    public class UserLocationDto
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Location_Code")]
        public string LocationCode { get; set; }

        [JsonPropertyName("Location_Name")]
        public string LocationName { get; set; }

        [JsonPropertyName("Stock_Handle")]
        public int StockHandle { get; set; }

        [JsonPropertyName("Address")]
        public string Address { get; set; }

        [JsonPropertyName("Phone")]
        public string Phone { get; set; }

        [JsonPropertyName("Status")]
        public int Status { get; set; }
    }

    public class InvoiceTypeDto
    {
        [JsonPropertyName("Type_Code")]
        public string TypeCode { get; set; }

        [JsonPropertyName("Type_Name")]
        public string TypeName { get; set; }

        [JsonPropertyName("Tax_Code")]
        public string TaxCode { get; set; }

        [JsonPropertyName("Tax_Name")]
        public string TaxName { get; set; }

        [JsonPropertyName("Tax_Value")]
        public decimal TaxValue { get; set; }
    }

    public class InitialCodeDto
    {
        [JsonPropertyName("Location_Code")]
        public string LocationCode { get; set; }

        [JsonPropertyName("Type")]
        public string Type { get; set; }

        [JsonPropertyName("Current_Count")]
        public int CurrentCount { get; set; }
    }
}
