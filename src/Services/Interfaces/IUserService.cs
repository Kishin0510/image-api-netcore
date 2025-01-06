using image_api_netcore.src.Models;
using image_api_netcore.src.DTOs;

namespace image_api_netcore.src.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoggedUserDTO> userLogin(LoginUserDTO loginUserDTO);

        Task<LoggedUserDTO> userRegister(LoginUserDTO registerUserDTO);
    }
}