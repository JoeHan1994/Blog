using Blog.IRepository;
using Blog.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        //pass from child class
        protected IBaseRepository<TEntity> _baseRepository;

        public async Task<bool> CreateAsync(TEntity entity)
        {
            return await _baseRepository.CreateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            return await _baseRepository.DeleteAsync(Id);
        }

        public async Task<bool> EditAsync(TEntity entity)
        {
            return await _baseRepository.EditAsync(entity);
        } 

        public async Task<TEntity> FindAsync(int Id)
        {
            return await _baseRepository.FindAsync(Id);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func)
        {
            return await _baseRepository.FindAsync(func);
        }

        public async Task<List<TEntity>> QueryAsync()
        {
            return await _baseRepository.QueryAsync();
        }

        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func)
        {
            return await _baseRepository.QueryAsync(func);
        }

        public async Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await _baseRepository.QueryAsync(page,size,total);
        }

        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await _baseRepository.QueryAsync(func, page,size,total);
        }
    }
}
