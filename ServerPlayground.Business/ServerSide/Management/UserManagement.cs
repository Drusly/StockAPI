using Grpc.Core;
using Karluna.DAL.UnitOfWork;
using Karluna.Entities.Entities;
using Karluna.Entities.Models.Requests;
using Karluna.Entities.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Karluna.Entities.Enums.GeneralEnum;

namespace Karluna.Business.ServerSide.Management
{
    public class UserManagement
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        public UserManagement(IConfiguration configuration, IServiceProvider serviceProvider, UserManager<User> userManager, RoleManager<UserRole> roleManager)
        {
            _serviceProvider = serviceProvider;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<ResultObject<User>> AddUser(ReqUser model)
        {
            ResultObject<User> Result = new ResultObject<User>();
            
            try
            {
                var userExists = await _userManager.FindByNameAsync(model.User.UserName);
                if (userExists != null)
                {
                    Result.StatusCode = ResultStatusEnum.UserAlreadyExist;
                    return Result;
                }

                User user = new()
                {
                    Email = model.User.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.User.UserName
                };

                if (model.strRole == null)
                {
                    Result.StatusCode = ResultStatusEnum.UserMustHaveRole;
                    return Result;
                }

                var result = await _userManager.CreateAsync(user, model.PasswordInput);
                
                if (!result.Succeeded)
                {
                    Result.StatusCode = ResultStatusEnum.Error;
                    Result.Message = string.Join(",", result.Errors.Select(c => c.Description));
                    return Result;
                }

                var test = await _userManager.AddToRoleAsync(_userManager.FindByNameAsync(model.User.UserName).Result, model.strRole);
                var tets = await _userManager.GetRolesAsync(user);
            }
            catch (Exception ex)
            {
                Result.StatusCode = ResultStatusEnum.Error;
                Result.Message = ex.Message;
            }

            return Result;
        }

        public async Task<ResultObject<UserRole>> AddRole(ReqUserRole model)
        {
            ResultObject<UserRole> Result = new ResultObject<UserRole>();

            try
            {
                var result = await _roleManager.CreateAsync(model.Role);
                if (!result.Succeeded)
                {
                    Result.StatusCode = ResultStatusEnum.Error;
                    Result.Message = string.Join(',', result.Errors.Select(c => c.Description));
                    return Result;
                }

                Result.StatusCode = ResultStatusEnum.Succeed;
            }
            catch (Exception ex)
            {
                Result.StatusCode = ResultStatusEnum.Error;
                Result.Message = ex.Message;
            }

            return Result;
        }

        public ResultObjectList<User> GetUser(ReqUser reqUser)
        {
            ResultObjectList<User> Result = new ResultObjectList<User>();

            try
            {
                using (Worker worker = new Worker(_serviceProvider))
                {
                    Result.Result = worker.User.GetUsers(reqUser).ToList();
                    if(Result.Result.Count > 0)
                    {
                        Result.Result.ForEach(user =>
                        {
                           user.RoleList = _userManager.GetRolesAsync(user).Result.ToList();
                        });
                    }
                }

                Result.StatusCode = ResultStatusEnum.Succeed;
            }
            catch (Exception ex)
            {
                Result.StatusCode = ResultStatusEnum.Error;
                Result.Message = ex.Message;
            }

            return Result;
        }

        public ResultObjectList<UserRole> GetRole(ReqUserRole model)
        {
            ResultObjectList<UserRole> Result = new ResultObjectList<UserRole>();

            try
            {
                using (Worker worker = new Worker(_serviceProvider))
                {
                    Result.Result = worker.UserRole.GetAll().Result.ToList();
                }

                Result.StatusCode = ResultStatusEnum.Succeed;
            }
            catch (Exception ex)
            {
                Result.StatusCode = ResultStatusEnum.Error;
                Result.Message = ex.Message;
            }

            return Result;
        }

        public JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            JwtSecurityToken token = new JwtSecurityToken();
            try
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

                token = new JwtSecurityToken(
                    //issuer: _configuration["JWT:ValidIssuer"],
                    //audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
            }
            catch (Exception ex) 
            { 
                
            }

            return token;
            
        }
    }
}
