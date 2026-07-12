using AssignmentRohanback.Dto;
using AssignmentRohanback.Repository;

namespace AssignmentRohanback.Service.LocationService
{
    public class PurchasebillService : IPurchasebillService
    {
        private readonly IPurchasebillRepository _locationRepository;

        public PurchasebillService(IPurchasebillRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }
        public async Task<List<UserLocationDto>> getALlLocationsAsync()
        {
            try
            {
                List<UserLocationDto> locations = await _locationRepository.getALlLocations();
                return locations;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LocationService: {ex.Message}");
                return new List<UserLocationDto>();
            }
        }

        public async Task<List<itemResponse>> getALlItemsAsync()
        {
            try
            {
                List<itemResponse> items = await _locationRepository.getAllItems();
                return items;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LocationService: {ex.Message}");
                return new List<itemResponse>();
            }
        }

        public async Task<List<PurchaseBillResponseDto>> getAllPurchaseBillsAsync()
        {
            try
            {
                List<PurchaseBillResponseDto> bills = await _locationRepository.getAllPurchaseBills();
                return bills;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LocationService: {ex.Message}");
                return new List<PurchaseBillResponseDto>();
            }
        }

        public async Task<bool> insertPurchaseBillAsync(PurchaseBillRequestDto request)
        {
            try
            {
                if (request == null || request.ItemId <= 0 || request.LocationId <= 0 || request.Qty <= 0)
                {
                    return false;
                }

                return await _locationRepository.insertPurchaseBill(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LocationService: {ex.Message}");
                return false;
            }
        }
    }
}
