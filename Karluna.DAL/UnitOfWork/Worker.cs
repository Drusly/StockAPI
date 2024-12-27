using Karluna.DAL.Generic;
using Karluna.DAL.Interface;
using Karluna.DAL.Repositories;
using Karluna.Data.DbContext;
using Karluna.Entities.Entities;
using Karluna.Entities.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.DAL.UnitOfWork
{
    public class Worker : IWorker, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        KtsDbContext _context;
                
        public Worker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            var scope = _serviceProvider.CreateScope();
            _context = scope.ServiceProvider.GetRequiredService<KtsDbContext>();
        }

        public IRepStockProduct StockProduct {
            get {
                if (this._stockProduct != null)
                    return this._stockProduct;

                this._stockProduct = new RepStockProduct(_context);
                return this._stockProduct;
            }
            private set
            {
                this._stockProduct = value;
            }
        }
        public IRepStockBrand StockBrand { 
            get 
            {
                if (this._stockBrand != null)
                    return this._stockBrand;

                this._stockBrand = new RepStockBrand(_context);
                return this._stockBrand;
            }
            private set
            {
                this._stockBrand = value;
            }
        }
        public IRepStockSubCategory StockSubCategory { 
            get 
            {
                if (this._stockSubCategory != null)
                    return this._stockSubCategory;

                this._stockSubCategory = new RepStockSubCategory(_context);
                return this._stockSubCategory;
            }
            private set
            {
                this._stockSubCategory = value;
            }
        }
        public IRepStockCategory StockCategory { 
            get {
                if (this._stockCategory != null)
                    return this._stockCategory;

                this._stockCategory = new RepStockCategory(_context);
                _context = _stockCategory.KtsDbContext;
                return this._stockCategory;
            }
            private set
            {
                this._stockCategory = value;
            }
        }
        public IRepStockProductVersion StockProductVersion
        {
            get
            {
                if (this._stockProductVersion != null)
                    return this._stockProductVersion;

                this._stockProductVersion = new RepStockProductVersion(_context);
                _context = _stockProductVersion.KtsDbContext;
                return this._stockProductVersion;
            }
            private set
            {
                this._stockProductVersion = value;
            }
        }

        public IRepUser User { 
            get {
                if (this._user != null)
                    return this._user;

                this._user = new RepUser(_context);
                _context = _user.KtsDbContext;
                return this._user;
            }
            private set
            {
                this._user = value;
            }
        }

        public IRepUserRole UserRole
        {
            get
            {
                if (this._userRole != null)
                    return this._userRole;

                this._userRole = new RepUserRole(_context);
                _context = _userRole.KtsDbContext;
                return this._userRole;
            }
            private set
            {
                this._userRole = value;
            }
        }

        public void Dispose()
        {
            try
            {
                _context.Dispose();
                GC.SuppressFinalize(this);
            }
            catch (Exception ex) 
            { 
                
            }
        }

        public Task SaveChanges()
        {
            try
            {
                var changes = this._context.SaveChanges();
                Dispose();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Dispose();
                throw new Exception(ex.Message);
            }
        }

        private IRepStockProduct _stockProduct { get; set; }
        private IRepStockCategory _stockCategory { get; set; }
        private IRepStockSubCategory _stockSubCategory { get; set; }
        private IRepStockProductVersion _stockProductVersion { get; set; }
        private IRepStockBrand _stockBrand { get; set; }
        private IRepUser _user { get; set; }
        private IRepUserRole _userRole { get; set; }

    }
}
