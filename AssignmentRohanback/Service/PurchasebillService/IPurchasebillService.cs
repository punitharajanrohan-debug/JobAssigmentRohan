using AssignmentRohanback.Dto;

namespace AssignmentRohanback.Service.LocationService
{
    public interface IPurchasebillService
    {
        public Task<List<UserLocationDto>> getALlLocationsAsync();

        public Task<List<itemResponse>> getALlItemsAsync();
        Task<List<PurchaseBillResponseDto>> getAllPurchaseBillsAsync();
        Task<bool> insertPurchaseBillAsync(PurchaseBillRequestDto request);

    }
}
