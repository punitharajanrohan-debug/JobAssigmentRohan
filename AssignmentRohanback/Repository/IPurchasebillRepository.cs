using AssignmentRohanback.Dto;

namespace AssignmentRohanback.Repository
{
    public interface IPurchasebillRepository
    {
        public Task<bool> saveLocations(List<UserLocationDto> userLocation);
        public Task<List<UserLocationDto>> getALlLocations();
        public Task<List<itemResponse>> getAllItems();

        Task<bool> insertPurchaseBill(PurchaseBillRequestDto request);
        Task<List<PurchaseBillResponseDto>> getAllPurchaseBills();
    }
}
