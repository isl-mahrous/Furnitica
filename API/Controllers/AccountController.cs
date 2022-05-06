using Core.Entities;
using JWTAuth.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _config;
        public AccountController(
            UserManager<AppUser> user,
            IConfiguration config)
        {
            _userManager = user;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AppUser newUser = new AppUser();
            newUser.Email = registerDTO.Email;
            newUser.UserName = registerDTO.Username;


            IdentityResult result = await _userManager.CreateAsync(newUser, registerDTO.Password);

            if (result.Succeeded)
            {
                return Ok("Add Success");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return BadRequest(ModelState);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // check
            AppUser user = await _userManager.FindByNameAsync(loginDTO.Username);

            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, loginDTO.Password))
                {
                    // create token based on claims
                    var claims = new List<Claim>();

                    claims.Add(new Claim(ClaimTypes.Name, loginDTO.Username));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    var roles = await _userManager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    // jti 
                    claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]));
                    var token = new JwtSecurityToken(
                        audience: _config["JWT:ValidAudience"],
                        issuer: _config["JWT:ValidIssuer"],
                        expires: DateTime.Now.AddHours(1),
                        claims: claims,
                        signingCredentials:
                            new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                        );
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
                else
                {
                    return Unauthorized();
                }
            }

            return BadRequest();
        }
    }
}
