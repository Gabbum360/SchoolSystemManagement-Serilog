using EFCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Repository<T>
    {
        private SchoolSystemDbContext _dbInstance;
        public Repository(SchoolSystemDbContext db)
        {
            _dbInstance = db;
        }

        /*public async Task Add(T model)
        {
            await _dbInstance.Set<T>().AddAsync(model);
        }

        public async Task AddRange(IEnumerable<T> models)
        {
            await _dbInstance.Set<T>().AddRangeAsync(models);
        }

        public void Update(T model)
        {
            _dbInstance.Set<T>().Update(model);
        }

        public void UpdateRange(IEnumerable<T> models)
        {
            _dbInstance.Set<T>().UpdateRange(models);
        }

        public void Delete(T model)
        {
            _dbInstance.Set<T>().Remove(model);
        }

        public void DeleteRange(IEnumerable<T> models)
        {
            _dbInstance.Set<T>().RemoveRange(models);
        }

        public async Task<T> Find(Expression<Func<T, bool>> predicate)
            => (await _dbInstance.Set<T>().FirstOrDefaultAsync(predicate))!;

        public async Task<T> Get(T id)
            => (await _dbInstance.Set<T>().FindAsync(id))!;

        public async Task<IEnumerable<T>> GetAll()
            => (await _dbInstance.Set<T>().ToListAsync())!;

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
            => (await _dbInstance.Set<T>().Where(predicate).ToListAsync())!;

        public async Task<int> Save()
        {
            var result = await _dbInstance.SaveChangesAsync(new CancellationToken());
            return result;
        }*/

    }
}
