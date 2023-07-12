using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using universityAPI.DataAccess;
using universityAPI.helpers;
using universityAPI.models.DataModels;
using universityAPI.Models.DataModels;


namespace universityAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UniversityDbContext _context;


        public AccountController(UniversityDbContext context, JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
            _context = context;

        }

        //TODO CHANGE BY REAL USERS IN DB
        private IEnumerable<User> Logins = new List<User>()
        {
            new User()
            {
                Id = 1,
                Email = "alfredo.hdez@hotmail.com",
                UserName = "Admin",
                Password = "Admin"

            },
            new User()
            {
                Id = 2,
                Email = "alfredo2.hdez@hotmail.com",
                UserName = "User1",
                Password = "Admin"

            },
        };

        [HttpPost]
        public IActionResult GetToken(UserLogin userLogin)
        {
            try
            {
                var Token = new UserTokens();

                var Valid = Logins.Any(user => user.UserName.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));
                if (Valid)
                {
                    var user = Logins.FirstOrDefault(user => user.UserName.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));
                    Token = JWTHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = user.UserName,
                        EmailId = user.Email,
                        Id = user.Id,
                        GuidId = Guid.NewGuid()
                    }, _jwtSettings);

                }
                else
                {
                    return BadRequest("Wrong Credentials");
                }

                return Ok(Token);

            }catch (Exception ex)
            {
                throw new Exception("Get Token Error", ex);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public IActionResult GetUsersList()
        {
            return Ok(Logins);
        }



    }
}
