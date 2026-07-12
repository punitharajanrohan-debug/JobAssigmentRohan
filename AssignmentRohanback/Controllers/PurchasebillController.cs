using AssignmentRohanback.Dto;
using AssignmentRohanback.Service.LocationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentRohanback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasebillController : ControllerBase
    {
        private readonly IPurchasebillService _locationService;

        public PurchasebillController(IPurchasebillService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("locations")]
        public async Task<ActionResult<List<UserLocationDto>>> GetAll()
        {
            List<UserLocationDto> locations = await _locationService.getALlLocationsAsync();
            return Ok(locations);
        }

        [HttpGet("items")]
        public async Task<ActionResult<List<itemResponse>>> GetAllItems()
        {
            List<itemResponse> items = await _locationService.getALlItemsAsync();
            return Ok(items);
        }

        [HttpGet("purchasebills")]
        public async Task<ActionResult<List<PurchaseBillResponseDto>>> GetAllPurchaseBills()
        {
            List<PurchaseBillResponseDto> bills = await _locationService.getAllPurchaseBillsAsync();
            return Ok(bills);
        }

        [HttpPost("purchasebills")]
        public async Task<IActionResult> AddPurchaseBill([FromBody] PurchaseBillRequestDto request)
        {
            if (request == null || request.ItemId <= 0 || request.LocationId <= 0 || request.Qty <= 0)
            {
                return BadRequest(new { Message = "Item, batch, and a quantity greater than 0 are required." });
            }

            bool saved = await _locationService.insertPurchaseBillAsync(request);

            if (!saved)
            {
                return StatusCode(500, new { Message = "Failed to save the purchase bill item." });
            }

            return Ok(new { Message = "Item added successfully." });
        }
    }
}
