using AssignmentRohanback.Dto;
using AssignmentRohanback.Repository;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;


namespace AssignmentRohanback.Service.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IPurchasebillRepository _locationRepository;
        public AuthService(IConfiguration configuration , HttpClient httpClient , IPurchasebillRepository locationRepository)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _locationRepository = locationRepository;

        }
        public async Task<LoginResponse> loginasync(LoginRequest request)
        {
            try
            {
                List<UserLocationDto> locationDto = new List<UserLocationDto>();

                string loginUrl = _configuration["LoginApi"];
                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                string outboundJson = JsonSerializer.Serialize(request, options);

                var content = new StringContent(outboundJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(loginUrl, content);

                string rawJson = await response.Content.ReadAsStringAsync();

                LoginResponse? loginResponse =
                    System.Text.Json.JsonSerializer.Deserialize<LoginResponse>(rawJson);

                if (loginResponse?.ResponseBody != null && loginResponse.ResponseBody.Count > 0)
                {
                    locationDto = loginResponse.ResponseBody[0].UserLocations ?? new List<UserLocationDto>();
                    bool isLocationsSaved= await _locationRepository.saveLocations(locationDto);
                }

                if (loginResponse == null || loginResponse.StatusCode == 0)
                {
                    return new LoginResponse
                    {
                        StatusCode = 401,
                        Message = $"Login failed. Upstream response: {rawJson}"
                    };
                }

                return loginResponse;
            }
            catch (Exception ex)
            {
                return new LoginResponse
                {
                    StatusCode = 500,
                    Message = $"Error during login: {ex.Message}"
                };
            }
        }
    }
}
