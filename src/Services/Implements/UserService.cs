using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using image_api_netcore.src.Models;
using image_api_netcore.src.DTOs;
using Microsoft.IdentityModel.Tokens;
using image_api_netcore.src.Services.Interfaces;
using image_api_netcore.src.Repositories.Interfaces;
using System.Text;

namespace image_api_netcore.src.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<LoggedUserDTO> userLogin(LoginUserDTO loginUserDTO)
        {
            var user = await _userRepository.GetUserByEmail(loginUserDTO.email);
            if (user == null)
            {
                throw new Exception("Usuario no encontrado");
            }
            var passwordHash = BCrypt.Net.BCrypt.Verify(loginUserDTO.password, user.password);
            if (!passwordHash)
            {
                throw new Exception("Contraseña incorrecta");
            }
            var token = GenerateToken(user);
            var loggedUserDTO = new LoggedUserDTO
            {
                email = user.email,
                token = token
            };
            return loggedUserDTO;
        }

        public async Task<LoggedUserDTO> userRegister(LoginUserDTO registerUserDTO)
        {
            if (_userRepository.VerifyEmail(registerUserDTO.email).Result)
            {
                throw new Exception("El email ya esta registrado");
            }

            var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerUserDTO.password, salt);
            var newUser = new User
            {
                email = registerUserDTO.email,
                password = hashedPassword
            };

            await _userRepository.AddUser(newUser);
            var token = GenerateToken(newUser);
            var loggedUserDTO = new LoggedUserDTO
            {
                email = newUser.email,
                token = token
            };
            return loggedUserDTO;
        }

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>{
                new ("id", user.id.ToString()),
                new ("email", user.email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}