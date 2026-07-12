// ---------- Request ----------

export interface ApiBody {
  Username: string;
  Pw: string;
}

export interface LoginRequest {
  API_Action: string;
  Device_Id: string;
  Sync_Time: string;
  Company_Code: string;
  API_Body: ApiBody;
}

// ---------- Response ----------

export interface UserLocation {
  Id :number
  Location_Code: string;
  Location_Name: string;
  Stock_Handle: number;
  Address: string;
  Phone: string;
  Status: number;
}

export interface InvoiceType {
  Type_Code: string;
  Type_Name: string;
  Tax_Code: string;
  Tax_Name: string;
  Tax_Value: number;
}

export interface InitialCode {
  Location_Code: string;
  Type: string;
  Current_Count: number;
}

export interface LoginResponseBody {
  User_Code: string;
  User_Display_Name: string;
  Email: string;
  User_Employee_Code: string;
  Company_Code: string;
  User_Locations: UserLocation[];
  Invoice_Types: InvoiceType[];
  Initial_Codes: InitialCode[];
}

export interface LoginResponse {
  Status_Code: number;
  Sync_Time: string | null;
  Message: string | null;
  Response_Body: LoginResponseBody[] | null;
}