// What we POST when adding a row (matches PurchaseBillRequestDto)
export interface PurchaseBillRequest {
  Item_Id: number;
  Location_Id: number;
  Standard_Cost: number;
  Standard_Price: number;
  Margin: number;
  Qty: number;
  Free_Qty: number;
  Discount: number;
  Total_Cost: number;
  Total_Selling: number;
}

// What GET returns per row (matches PurchaseBillResponseDto)
export interface PurchaseBillResponse {
  Id: number;
  Item_Name: string;
  Location_Name: string;
  Standard_Cost: number;
  Standard_Price: number;
  Margin: number;
  Qty: number;
  Free_Qty: number;
  Discount: number;
  Total_Cost: number;
  Total_Selling: number;
}

// What POST returns
export interface AddBillResult {
  Message: string;
}