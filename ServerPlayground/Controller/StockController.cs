using Karluna.Business.ServerSide.Management;
using Karluna.Entities.Entities;
using Karluna.Entities.Models.Requests;
using Karluna.Entities.Models.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Karluna.API.Controller
{
    [Authorize(AuthenticationSchemes= JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly StockProductManagement _stockProductManagement;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private User _identityUser;
        public StockController(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager) 
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
            _userManager = userManager;
            var httpContext = _httpContextAccessor.HttpContext;
            _identityUser = userManager.FindByNameAsync(httpContext.User.FindFirst(ClaimTypes.Name)?.Value).Result;
            _stockProductManagement = new StockProductManagement(_serviceProvider, _identityUser);
        }
        
        [HttpPost]
        [Route("SaveStockProduct")]
        public ResultObject<StockProduct> SaveStockProduct([FromBody] ReqStockProduct req)
        {
            ResultObject<StockProduct> result = new ResultObject<StockProduct>();
            result = _stockProductManagement.SaveStockProduct(req);
            return result;
        }

        [HttpPost]
        [Route("GetStockProduct")]
        public ResultObjectList<StockProduct> GetStockProduct([FromBody] ReqStockProduct req)
        {
            req.IncludeSubCategory = true;
            req.IncludeCategory = true;
            ResultObjectList<StockProduct> result = new ResultObjectList<StockProduct>();
            result = _stockProductManagement.GetStockProduct(req);
            return result;
        }

        [HttpPost]
        [Route("SaveStockBrand")]
        public ResultObject<StockBrand> SaveStockBrand([FromBody] ReqStockBrand req)
        {
            ResultObject<StockBrand> result = new ResultObject<StockBrand>();
            result = _stockProductManagement.SaveStockBrand(req);
            return result;
        }

        [HttpPost]
        [Route("GetStockBrand")]
        public ResultObjectList<StockBrand> GetStockBrand([FromBody] ReqStockBrand? req)
        {
            ResultObjectList<StockBrand> result = new ResultObjectList<StockBrand>();
            result = _stockProductManagement.GetStockBrand(req);
            return result;
        }

        [HttpPost]
        [Route("SaveStockCategory")]
        public ResultObject<StockCategory> SaveStockCategory([FromBody] ReqStockCategory req)
        {
            ResultObject<StockCategory> result = new ResultObject<StockCategory>();
            result = _stockProductManagement.SaveStockCategory(req);
            return result;
        }

        [HttpPost]
        [Route("GetStockCategories")]
        public ResultObjectList<StockCategory> GetStockCategories([FromBody] ReqStockCategory? req)
        {
            ResultObjectList<StockCategory> result = new ResultObjectList<StockCategory>();
            result = _stockProductManagement.GetStockCategory(req);
            return result;
        }

        [HttpPost]
        [Route("SaveStockSubCategory")]
        public ResultObject<StockSubCategory> SaveStockSubCategory([FromBody] ReqStockSubCategory req)
        {
            ResultObject<StockSubCategory> result = new ResultObject<StockSubCategory>();
            result = _stockProductManagement.SaveStockSubCategory(req);
            return result;
        }

        [HttpPost]
        [Route("GetStockSubCategories")]
        public ResultObjectList<StockSubCategory> GetStockSubCategory([FromBody] ReqStockSubCategory req)
        {
            ResultObjectList<StockSubCategory> result = new ResultObjectList<StockSubCategory>();
            req.IncludeCategory = true;
            result = _stockProductManagement.GetStockSubCategory(req);
            return result;
        }

        [HttpPost]
        [Route("SaveStockVersion")]
        public ResultObject<StockProductVersion> SaveStockVersion([FromBody] ReqStockProductVersion req)
        {
            ResultObject<StockProductVersion> result = new ResultObject<StockProductVersion>();
            result = _stockProductManagement.SaveStockVersion(req);
            return result;
        }

        [HttpPost]
        [Route("GetStockVersion")]
        public ResultObjectList<StockProductVersion> GetStockVersion([FromBody] ReqStockProductVersion req)
        {
            ResultObjectList<StockProductVersion> result = new ResultObjectList<StockProductVersion>();
            result = _stockProductManagement.GetStockVersion(req);
            return result;
        }
    }
}
