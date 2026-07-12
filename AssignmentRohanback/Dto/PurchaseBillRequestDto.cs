using System.Text.Json.Serialization;

namespace AssignmentRohanback.Dto
{
    public class PurchaseBillRequestDto
    {
        [JsonPropertyName("Item_Id")]
        public int ItemId { get; set; }

        [JsonPropertyName("Location_Id")]
        public int LocationId { get; set; }

        [JsonPropertyName("Standard_Cost")]
        public decimal StandardCost { get; set; }

        [JsonPropertyName("Standard_Price")]
        public decimal StandardPrice { get; set; }

        [JsonPropertyName("Margin")]
        public decimal Margin { get; set; }

        [JsonPropertyName("Qty")]
        public int Qty { get; set; }

        [JsonPropertyName("Free_Qty")]
        public int FreeQty { get; set; }

        [JsonPropertyName("Discount")]
        public decimal Discount { get; set; }

        [JsonPropertyName("Total_Cost")]
        public decimal TotalCost { get; set; }

        [JsonPropertyName("Total_Selling")]
        public decimal TotalSelling { get; set; }
    }

    public class PurchaseBillResponseDto
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Item_Name")]
        public string ItemName { get; set; }

        [JsonPropertyName("Location_Name")]
        public string LocationName { get; set; }

        [JsonPropertyName("Standard_Cost")]
        public decimal StandardCost { get; set; }

        [JsonPropertyName("Standard_Price")]
        public decimal StandardPrice { get; set; }

        [JsonPropertyName("Margin")]
        public decimal Margin { get; set; }

        [JsonPropertyName("Qty")]
        public int Qty { get; set; }

        [JsonPropertyName("Free_Qty")]
        public int FreeQty { get; set; }

        [JsonPropertyName("Discount")]
        public decimal Discount { get; set; }

        [JsonPropertyName("Total_Cost")]
        public decimal TotalCost { get; set; }

        [JsonPropertyName("Total_Selling")]
        public decimal TotalSelling { get; set; }
    }
}
