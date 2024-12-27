using Karluna.Data.DbContext;
using Karluna.Entities.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karluna.DAL.Generic
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        //Logger eklemek lazım patlatan yerleri anlık log alalım
        private KtsDbContext _context;
        public BaseRepository(KtsDbContext dbContext)
        {
            _context = dbContext;
        }

        public KtsDbContext KtsDbContext { get { return this._context; } }

        public Task Add(T entity)
        {
            try
            {
                var changes = _context.Add(entity);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }

        public Task Delete(T entity)
        {
            try
            {
                _context.Remove(entity);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                return await _context.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<T> GetById(int id)
        {
            try
            {
                return await _context.Set<T>().FindAsync(id);
            }
            catch (Exception ex) 
            {
                throw;
            }
        }

        public Task Update(T entity)
        {
            try
            {
                _context.Update(entity);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }
    }
}
