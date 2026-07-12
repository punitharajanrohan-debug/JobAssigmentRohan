using AssignmentRohanback.Dto;
using Microsoft.Data.SqlClient;

namespace AssignmentRohanback.Repository
{
    public class PurchasebillRepository : IPurchasebillRepository
    {
        private readonly IConfiguration _configuration;

        public PurchasebillRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<itemResponse>> getAllItems()
        {
            List<itemResponse> items = new List<itemResponse>();

            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                string sql = @"SELECT Id, Item_Name FROM items";

                using SqlConnection connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                using SqlCommand command = new SqlCommand(sql, connection);
                using SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    items.Add(new itemResponse
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        ItemName = reader.GetString(reader.GetOrdinal("Item_Name"))
                    });
                }

                return items;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching items: {ex.Message}");
                return items;   // empty list on failure
            }
        }

        public async Task<List<UserLocationDto>> getALlLocations()
        {
            List<UserLocationDto> locations = new List<UserLocationDto>();

            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                string sql = @"SELECT Id, Location_Code, Location_Name, Stock_Handle, Address, Phone, Status
                       FROM User_Locations";

                using SqlConnection connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                using SqlCommand command = new SqlCommand(sql, connection);
                using SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    locations.Add(new UserLocationDto
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        LocationCode = reader.GetString(reader.GetOrdinal("Location_Code")),
                        LocationName = reader.GetString(reader.GetOrdinal("Location_Name")),
                        StockHandle = reader.GetInt32(reader.GetOrdinal("Stock_Handle")),
                        Address = reader.IsDBNull(reader.GetOrdinal("Address"))
                                         ? "" : reader.GetString(reader.GetOrdinal("Address")),
                        Phone = reader.IsDBNull(reader.GetOrdinal("Phone"))
                                         ? "" : reader.GetString(reader.GetOrdinal("Phone")),
                        Status = reader.GetInt32(reader.GetOrdinal("Status"))
                    });
                }

                return locations;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching locations: {ex.Message}");
                return locations;   // empty list on failure — caller never gets null
            }
        }

        public async Task<List<PurchaseBillResponseDto>> getAllPurchaseBills()
        {
            List<PurchaseBillResponseDto> purchaseBill = new List<PurchaseBillResponseDto>();

            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                string sql = @"
                    SELECT
                        s.Id,
                        i.Item_Name,
                        l.Location_Name,
                        s.Standard_Cost,
                        s.Standard_Price,
                        s.Margin,
                        s.Qty,
                        s.Free_Qty,
                        s.Discount,
                        s.Total_Cost,
                        s.Total_Selling
                    FROM Purchase_Bill s
                    INNER JOIN items i          ON s.Item_Id = i.Id
                    INNER JOIN User_Locations l ON s.Location_Id = l.Id
                    ORDER BY s.Created_At;";

                using SqlConnection connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                using SqlCommand command = new SqlCommand(sql, connection);
                using SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    purchaseBill.Add(new PurchaseBillResponseDto
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        ItemName = reader.GetString(reader.GetOrdinal("Item_Name")),
                        LocationName = reader.GetString(reader.GetOrdinal("Location_Name")),
                        StandardCost = reader.GetDecimal(reader.GetOrdinal("Standard_Cost")),
                        StandardPrice = reader.GetDecimal(reader.GetOrdinal("Standard_Price")),
                        Margin = reader.GetDecimal(reader.GetOrdinal("Margin")),
                        Qty = reader.GetInt32(reader.GetOrdinal("Qty")),
                        FreeQty = reader.GetInt32(reader.GetOrdinal("Free_Qty")),
                        Discount = reader.GetDecimal(reader.GetOrdinal("Discount")),
                        TotalCost = reader.GetDecimal(reader.GetOrdinal("Total_Cost")),
                        TotalSelling = reader.GetDecimal(reader.GetOrdinal("Total_Selling"))
                    });
                }

                return purchaseBill;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching item summaries: {ex.Message}");
                return purchaseBill;
            }
        }

        public  async Task<bool> insertPurchaseBill(PurchaseBillRequestDto request)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                string sql = @"
                    INSERT INTO Purchase_Bill
                        (Item_Id, Location_Id, Standard_Cost, Standard_Price, Margin,
                         Qty, Free_Qty, Discount, Total_Cost, Total_Selling)
                    VALUES
                        (@ItemId, @LocationId, @StandardCost, @StandardPrice, @Margin,
                         @Qty, @FreeQty, @Discount, @TotalCost, @TotalSelling);";

                using SqlConnection connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                using SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ItemId", request.ItemId);
                command.Parameters.AddWithValue("@LocationId", request.LocationId);
                command.Parameters.AddWithValue("@StandardCost", request.StandardCost);
                command.Parameters.AddWithValue("@StandardPrice", request.StandardPrice);
                command.Parameters.AddWithValue("@Margin", request.Margin);
                command.Parameters.AddWithValue("@Qty", request.Qty);
                command.Parameters.AddWithValue("@FreeQty", request.FreeQty);
                command.Parameters.AddWithValue("@Discount", request.Discount);
                command.Parameters.AddWithValue("@TotalCost", request.TotalCost);
                command.Parameters.AddWithValue("@TotalSelling", request.TotalSelling);

                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting item summary: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> saveLocations(List<UserLocationDto> userLocation)
        {
            try
            {
                if (userLocation == null || userLocation.Count == 0)
                {
                    return false;
                }

                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                string sql = @"
                IF NOT EXISTS (SELECT 1 FROM User_Locations WHERE Location_Code = @LocationCode)
                BEGIN
                    INSERT INTO User_Locations (Location_Code, Location_Name, Stock_Handle, Address, Phone, Status)
                    VALUES (@LocationCode, @LocationName, @StockHandle, @Address, @Phone, @Status);
                END
                ELSE
                BEGIN
                    UPDATE User_Locations
                    SET Location_Name = @LocationName,
                        Stock_Handle  = @StockHandle,
                        Address       = @Address,
                        Phone         = @Phone,
                        Status        = @Status
                    WHERE Location_Code = @LocationCode;
                END";

                using SqlConnection connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                
                using SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    foreach (UserLocationDto location in userLocation)
                    {
                        using SqlCommand command = new SqlCommand(sql, connection, transaction);

                        command.Parameters.AddWithValue("@LocationCode", location.LocationCode);
                        command.Parameters.AddWithValue("@LocationName", location.LocationName);
                        command.Parameters.AddWithValue("@StockHandle", location.StockHandle);
                        command.Parameters.AddWithValue("@Address", (object?)location.Address ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Phone", (object?)location.Phone ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Status", location.Status);

                        await command.ExecuteNonQueryAsync();
                    }

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving locations: {ex.Message}");
                return false;
            }
        }
    }
}
