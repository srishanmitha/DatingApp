using System.Security.Claims;
using System.Threading.Tasks;
using DatingApp.API.Dtos;
using DatingApp.API.models;
using DatingApp.API.models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _configuration;
       
        public AuthController(IAuthRepository repository, IConfiguration configuration)
        {
            _configuration = configuration;
            _repository = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {

            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();

            if (await _repository.UserExists(userForRegisterDto.UserName))
                return BadRequest("User already exists");

            var userToCreate = new User
            {
                UserName = userForRegisterDto.UserName
            };


            var createdUser = await _repository.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var user = await _repository.Login(userForLoginDto.UserName.ToLower(), userForLoginDto.Password);

            if (user.UserName == null || string.IsNullOrEmpty(user.UserName))
                return Unauthorized();

            var claims = new[]
            {
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new Claim(ClaimTypes.Name, user.UserName)
             };

            var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
             };

            var tokenHandler = new JwtSecurityTokenHandler();

            var  token   = tokenHandler.CreateToken(tokenDescriptor);

        return Ok (new{
            token = tokenHandler.WriteToken(token)
        });




    }
}



}