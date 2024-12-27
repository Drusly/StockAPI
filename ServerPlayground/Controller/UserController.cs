using Karluna.Business.ServerSide.Management;
using Karluna.Entities.Entities;
using Karluna.Entities.Models.Requests;
using Karluna.Entities.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static Karluna.Entities.Enums.GeneralEnum;

namespace Karluna.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        
        public UserController(UserManager<User> userManager,
            RoleManager<UserRole> roleManager,
            IConfiguration configuration, IServiceProvider serviceProvider) 
        {
            _serviceProvider = serviceProvider;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] ReqUser model)
        {
            UserManagement userManagement = new UserManagement(_configuration, _serviceProvider, _userManager, _roleManager);
            User user = await _userManager.FindByNameAsync(model.User.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.PasswordInput))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = userManagement.GetToken(authClaims);
                try
                {
                    var user2 = userManagement.GetUser(model).Result.FirstOrDefault().RoleList;
                }
                catch (Exception ex) 
                { 
                    
                }
                
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    user = userManagement.GetUser(model).Result.FirstOrDefault().RoleList
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("AddUser")]
        [Authorize]
        public async Task<ResultObject<User>> AddUser([FromBody] ReqUser model)
        {
            UserManagement userManagement = new UserManagement(_configuration, _serviceProvider, _userManager, _roleManager);
            var result = await userManagement.AddUser(model);

            return result;
        }

        [HttpPost]
        [Route("AddRole")]
        [Authorize]
        public async Task<ResultObject<UserRole>> AddRole([FromBody] ReqUserRole model)
        {
            UserManagement userManagement = new UserManagement(_configuration, _serviceProvider, _userManager, _roleManager);
            var result = await userManagement.AddRole(model);
            return result;
        }

        [HttpPost]
        [Route("GetUser")]
        [Authorize]
        public ResultObjectList<User> GetUser([FromBody] ReqUser model)
        {
            UserManagement userManagement = new UserManagement(_configuration, _serviceProvider, _userManager, _roleManager);
            var result = userManagement.GetUser(model);
            return result;
        }

        [HttpPost]
        [Route("GetRole")]
        [Authorize]
        public ResultObjectList<UserRole> GetRole([FromBody] ReqUserRole model)
        {
            UserManagement userManagement = new UserManagement(_configuration, _serviceProvider, _userManager, _roleManager);
            var result = userManagement.GetRole(model);
            return result;
        }
    }
}
