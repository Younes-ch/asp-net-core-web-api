using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects.User;

namespace Service.Contracts;

public interface IAuthenticationService
{
    Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto);
}