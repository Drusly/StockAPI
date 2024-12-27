using Karluna.DAL.Repositories;
using Karluna.DAL.UnitOfWork;
using Karluna.Entities.Entities;
using Karluna.Entities.Models.Requests;
using Karluna.Entities.Models.Response;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.Business.ServerSide.Management
{
    public class StockProductManagement
    {
        private readonly IServiceProvider _serviceProvider;
        private IdentityUser _identityUser;
        public StockProductManagement(IServiceProvider serviceProvider, IdentityUser user)
        { 
            _serviceProvider = serviceProvider;
            _identityUser = user;
        }

        public ResultObject<StockProduct> SaveStockProduct(ReqStockProduct reqStockProduct)
        {
            ResultObject<StockProduct> result = new ResultObject<StockProduct>();
            
            try
            {
                using (Worker worker = new Worker(_serviceProvider))
                {
                    var product = string.IsNullOrEmpty(reqStockProduct.StockProduct.Code) ? null : worker.StockProduct.GetStockProducts(reqStockProduct).FirstOrDefault();
                    
                    var productCode = GetStockProductCode(worker, reqStockProduct.StockProduct);

                    if (product != null)
                    {
                        product.LastModifiedBy = _identityUser.UserName;
                        product.Name = reqStockProduct.StockProduct.Name;
                        product.BrandId = reqStockProduct.StockProduct.BrandId;
                        product.VersionId = reqStockProduct.StockProduct.VersionId;
                        product.SubCategoryId = reqStockProduct.StockProduct.SubCategoryId;
                        product.CategoryId = reqStockProduct.StockProduct.CategoryId;
                        product.Price = reqStockProduct.StockProduct.Price;
                        product.TotalCount = reqStockProduct.StockProduct.TotalCount;
                        product.Code = productCode;
                        product.Notes = reqStockProduct.StockProduct.Notes;

                        product.LastModifiedOnUtc = DateTime.UtcNow;

                        worker.StockProduct.Update(product);
                    }
                    else
                    {
                        var duplicateCheck = worker.StockProduct.GetStockProducts(new ReqStockProduct() { StockProduct = new StockProduct { Code = productCode } }).Any();
                        
                        if (duplicateCheck)
                        {
                            result.StatusCode = Entities.Enums.GeneralEnum.ResultStatusEnum.RecordCannotDuplicate;
                            return result;
                        }

                        reqStockProduct.StockProduct.SubCategoryId = reqStockProduct.StockProduct.SubCategoryId == 0 ? null : reqStockProduct.StockProduct.SubCategoryId;
                        reqStockProduct.StockProduct.Code = productCode;
                        reqStockProduct.StockProduct.CreatedDate = DateTime.UtcNow;
                        reqStockProduct.StockProduct.CreatedBy = _identityUser.UserName;
                        reqStockProduct.StockProduct.LastModifiedBy = _identityUser.UserName;
                        worker.StockProduct.Add(reqStockProduct.StockProduct);
                    }

                    worker.SaveChanges();
                }

                result.StatusCode = Entities.Enums.GeneralEnum.ResultStatusEnum.Succeed;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.StatusCode = Entities.Enums.GeneralEnum.ResultStatusEnum.Error;
            }

            return result;
        }

        public ResultObjectList<StockProduct> GetStockProduct(ReqStockProduct reqStockProduct)
        {
            ResultObjectList<StockProduct> result = new ResultObjectList<StockProduct>();
            StockProduct brand = new StockProduct();

            try
            {
                using (Worker worker = new Worker(_serviceProvider))
                {
                    result.Result = worker.StockProduct.GetStockProducts(reqStockProduct).ToList();
                }

                result.StatusCode = Entities.Enums.GeneralEnum.ResultStatusEnum.Succeed;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.StatusCode = Entities.Enums.GeneralEnum.ResultStatusEnum.Error;
            }

            return result;
        }

        public ResultObject<StockCategory> SaveStockCategory(ReqStockCategory reqStockCategory)
        {
            ResultObject<StockCategory> result = new ResultObject<StockCategory>();
            
            try
            {
                using (Worker worker = new Worker(_serviceProvider))
                {

                    var category =  reqStockCategory.StockCategory.Id != 0 ?  worker.StockCategory.GetStockCategory(reqStockCategory).FirstOrDefault() : null;
                    //If category included
                    if (category != null)
                    {
                        category.MaterialStatus = reqStockCategory.StockCategory.MaterialStatus;
                        category.Name = reqStockCategory.StockCategory.Name;
                        category.DomainName = reqStockCategory.StockCategory.DomainName;
                        category.Notes = reqStockCategory.StockCategory.Notes;
                        
                        category.LastModifiedOnUtc = DateTime.UtcNow;
                        category.LastModifiedBy = _identityUser.UserName;

                        worker.StockCategory.Update(category);
                    }
                    else
                    {
                        category = reqStockCategory.StockCategory;
                        category.CreatedDate = DateTime.UtcNow;
                        category.LastModifiedBy = _identityUser.UserName;
                        category.CreatedBy = _identityUser.UserName;
                        worker.StockCategory.Add(category);
                    }
                        

                    worker.SaveChanges();
                }

                result.StatusCode = Entities.Enums.GeneralEnum.ResultStatusEnum.Succeed;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.StatusCode = Entities.Enums.GeneralEnum.ResultStatusEnum.Error;
            }

            return result;
        }

        public ResultObjectList<StockCategory> GetStockCategory(ReqStockCategory reqStockBrand)
        {
            ResultObjectList<StockCategory> result = new ResultObjectList<StockCategory>();
            StockCategory brand = new StockCategory();

            try
            {
                using (Worker worker = new Worker(_serviceProvider))
                {
                    result.Result = worker.StockCategory.GetAll().Result.ToList();
                }

                result.StatusCode = Entities.Enums.GeneralEnum.ResultStatusEnum.Succeed;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.StatusCode = Entities.Enums.GeneralEnum.ResultStatusEnum.Error;
            }

            return result;
        }

        public ResultObject<StockSubCategory> SaveStockSubCategory(ReqStockSubCategory reqStockSubCategory)
        {
            ResultObject<StockSubCategory> result = new ResultObject<StockSubCategory>();
            
            try
            {
                using (Worker worker = new Worker(_serviceProvider))
                {
                    var subCategory = reqStockSubCategory.StockSubCategory.Id != 0 ? worker.StockSubCategory.GetStockSubCategory(reqStockSubCategory).FirstOrDefault(): null;
                    
                    if (subCategory != null)
                    {
                        subCategory.CategoryId = reqStockSubCategory.StockSubCategory.CategoryId;
                        subCategory.Name = reqStockSubCategory.StockSubCategory.Name;
                        
                        subCategory.LastModifiedOnUtc = DateTime.UtcNow;

                        worker.StockSubCategory.Update(subCategory);
                    }
                    else
                    {
                        subCategory = reqStockSubCategory.StockSubCategory;
                        subCategory.CreatedDate = DateTime.UtcNow;
                        subCategory.CreatedBy = _identityUser.UserName;
                        subCategory.LastModifiedBy = _identityUser.UserName;
                        subCategory.CategoryId = reqStockSubCategory.StockSubCategory.CategoryId;
                        worker.StockSubCategory.Add(subCategory);
                    }

                    worker.SaveChanges();
                }

                result.StatusCode = Entities.Enums.GeneralEnum.ResultStatusEnum.Succeed;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.StatusCode = Entities.Enums.GeneralEnum.ResultStatusEnum.Error;
            }

            return result;
        }

        public ResultObjectList<StockSubCategory> GetStockSubCategory(ReqStockSubCategory reqStockSubCategory)
        {
            ResultObjectList<StockSubCategory> result = new ResultObjectList<StockSubCategory>();
            StockSubCategory brand = new StockSubCategory();

            try
            {
                using (Worker worker = new Worker(_serviceProvider))
                {
                    result.Result = worker.StockSubCategory.GetStockSubCategory(reqStockSubCategory).ToList();
                }

                result.StatusCode = Entities.Enums.GeneralEnum.ResultStatusEnum.Succeed;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.StatusCode = Entities.Enums.GeneralEnum.ResultStatusEnum.Error;
            }

            return result;
        }

        public ResultObject<StockBrand> SaveStockBrand(ReqStockBrand reqStockBrand)
        {
            ResultObject<StockBrand> result = new ResultObject<StockBrand>();
            
            try
            {
                using (Worker worker = new Worker(_serviceProvider))
                {
                    var brand = worker.StockBrand.GetStockBrand(reqStockBrand).FirstOrDefault();

                    if (brand != null)
                    {
                        brand.Address = reqStockBrand.StockBrand.Address;
                        brand.Name = reqStockBrand.StockBrand.Name;
                        brand.PhoneNumber = reqStockBrand.StockBrand.PhoneNumber;
                        
                        brand.LastModifiedOnUtc = DateTime.UtcNow;
                        brand.LastModifiedBy = _identityUser.UserName;

                        worker.StockBrand.Update(brand);
                    }
                    else
                    {
                        brand = reqStockBrand.StockBrand;
                        brand.CreatedDate = DateTime.UtcNow;
                        brand.CreatedBy = _identityUser.UserName;
                        brand.LastModifiedBy = _identityUser.UserName;
                        worker.StockBrand.Add(brand);
                    }

                    worker.SaveChanges();
                }

                result.StatusCode = Entities.Enums.GeneralEnum.ResultStatusEnum.Succeed;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.StatusCode = Entities.Enums.GeneralEnum.ResultStatusEnum.Error;
            }

            return result;
        }

        public ResultObjectList<StockBrand> GetStockBrand(ReqStockBrand reqStockBrand)
        {
            ResultObjectList<StockBrand> result = new ResultObjectList<StockBrand>();
            StockBrand brand = new StockBrand();

            try
            {
                using (Worker worker = new Worker(_serviceProvider))
                {
                    result.Result = worker.StockBrand.GetAll().Result.ToList();
                }

                result.StatusCode = Entities.Enums.GeneralEnum.ResultStatusEnum.Succeed;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.StatusCode = Entities.Enums.GeneralEnum.ResultStatusEnum.Error;
            }

            return result;
        }

        public ResultObject<StockProductVersion> SaveStockVersion(ReqStockProductVersion reqStockProductVersion)
        {
            ResultObject<StockProductVersion> result = new ResultObject<StockProductVersion>();
            
            try
            {
                using (Worker worker = new Worker(_serviceProvider))
                {
                    var productVersion = worker.StockProductVersion.GetStockProductVersion(reqStockProductVersion).FirstOrDefault();
                    
                    if (productVersion != null)
                    {
                        productVersion = new StockProductVersion();
                        productVersion.SubCategoryId = reqStockProductVersion.StockProductVersion.SubCategoryId;
                        productVersion.Name = reqStockProductVersion.StockProductVersion.Name;

                        productVersion.LastModifiedOnUtc = DateTime.UtcNow;
                        productVersion.LastModifiedBy = _identityUser.UserName;

                        worker.StockProductVersion.Update(productVersion);
                    }
                    else
                    {
                        productVersion = new StockProductVersion();
                        
                        productVersion.Name = reqStockProductVersion.StockProductVersion.Name;
                        productVersion = reqStockProductVersion.StockProductVersion;
                        productVersion.Code = GetStockProductVersionCode(worker, reqStockProductVersion);
                        productVersion.SubCategoryId = reqStockProductVersion.StockProductVersion.SubCategoryId;

                        productVersion.LastModifiedOnUtc = DateTime.UtcNow;
                        productVersion.LastModifiedBy = _identityUser.UserName;
                        productVersion.CreatedDate = DateTime.UtcNow;
                        productVersion.CreatedBy = _identityUser.UserName;
                        worker.StockProductVersion.Add(productVersion);
                    }

                    worker.SaveChanges();
                }

                result.StatusCode = Entities.Enums.GeneralEnum.ResultStatusEnum.Succeed;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.StatusCode = Entities.Enums.GeneralEnum.ResultStatusEnum.Error;
            }

            return result;
        }

        public ResultObjectList<StockProductVersion> GetStockVersion(ReqStockProductVersion reqStockBrand)
        {
            ResultObjectList<StockProductVersion> result = new ResultObjectList<StockProductVersion>();
            StockProductVersion brand = new StockProductVersion();

            try
            {
                using (Worker worker = new Worker(_serviceProvider))
                {
                    result.Result = worker.StockProductVersion.GetAll().Result.ToList();
                }

                result.StatusCode = Entities.Enums.GeneralEnum.ResultStatusEnum.Succeed;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.StatusCode = Entities.Enums.GeneralEnum.ResultStatusEnum.Error;
            }

            return result;
        }

        public string GetStockProductVersionCode(Worker worker, ReqStockProductVersion req)
        {
            var lastVersion = worker.StockProductVersion.GetLast(req);
            var Code = "000";
            
            if (lastVersion != null)
              Code = (Convert.ToInt32(lastVersion.Code) + 1).ToString().PadLeft(3,'0');

            return Code;
        }

        public string GetStockProductCode(Worker worker, StockProduct req)
        {
            var Code = "";
            var Category = worker.StockCategory.GetStockCategory(new ReqStockCategory { 
                StockCategory = new StockCategory() { Id = (int) req.CategoryId }
            }).ToList().FirstOrDefault();
            var SubCategory = req.SubCategoryId != null ? worker.StockSubCategory.GetStockSubCategory( new ReqStockSubCategory { StockSubCategory = 
                new StockSubCategory
                {
                    Id = (int)req.SubCategoryId,
                }
            }).FirstOrDefault() : null;

            Code += ((int)Category.DomainName + 1).ToString();
            Code += ((int)Category.MaterialStatus + 1).ToString();
            Code += (worker.StockCategory.GetStockCategory(new ReqStockCategory
            {
                StockCategory = new StockCategory() { DomainName = Category.DomainName, MaterialStatus = Category.MaterialStatus },
                GetForCode = true,
            })
                .OrderBy(c => c.Id)
                .Select(c => c.Id)
                .ToList().IndexOf(Category.Id)
                 + 1)
                .ToString().PadLeft(2,'0');
            Code += req.SubCategoryId == 0 ? 0 : (worker.StockSubCategory.GetStockSubCategory(new ReqStockSubCategory 
            { 
                CategoryId = Category.Id
            }).OrderBy(c => c.Id)
                .ToList()
                .IndexOf(SubCategory) + 1)
                .ToString();
            if (string.IsNullOrEmpty(req.Code))
            {
                var LastProduct = worker.StockProduct.GetStockProducts(new ReqStockProduct {
                    
                    BrandId = req.BrandId,
                    SubCategoryId = req.SubCategoryId,
                    CategoryId = req.CategoryId,

                }).OrderBy(c => c.Code).LastOrDefault();
                Code += "-" + (Convert.ToInt32( LastProduct != null ? LastProduct.Code.Split('-')[1] : 0 ) + 1)
                .ToString().PadLeft(5, '0');
            }
            else
            {
                Code += "-" + req.Code.Split('-')[1];
            }
            
            if(req.VersionId != null)
                Code += "-" + worker.StockProductVersion.GetById((int)req.VersionId).Result.Code;

            return Code;
        }
    }
}
