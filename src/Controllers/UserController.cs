using image_api_netcore.src.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using image_api_netcore.src.DTOs;

namespace image_api_netcore.src.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<LoggedUserDTO>> Login(LoginUserDTO loginUserDTO){
            try {  
                var result = await _userService.userLogin(loginUserDTO);
                return Ok(result);

            } catch (Exception e) {
                return Unauthorized(e.Message);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<LoggedUserDTO>> Register(LoginUserDTO registerUserDTO){
            try {  
                var result = await _userService.userRegister(registerUserDTO);
                return Ok(result);

            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }
    }
}