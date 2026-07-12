using AssignmentRohanback.Dto;

namespace AssignmentRohanback.Service.AuthService
{
    public interface IAuthService
    {
        public Task<LoginResponse> loginasync(LoginRequest request);
    }
}
