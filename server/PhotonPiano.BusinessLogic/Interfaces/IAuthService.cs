using PhotonPiano.Helper.Dtos.Auth;
using PhotonPiano.Helper.Dtos.Users;
using System.Security.Claims;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Login(LoginDto loginDto);

        Task<GetUserDto> GetUserByClaims(ClaimsPrincipal claims);
    }
}
