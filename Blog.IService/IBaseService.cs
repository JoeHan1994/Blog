using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.IService
{
   public  interface IBaseService<TEntity> where TEntity:class,new()
    {
        Task<bool> CreateAsync(TEntity entity);
        Task<bool> DeleteAsync(int Id);
        Task<bool> EditAsync(TEntity entity);
        Task<TEntity> FindAsync(int Id);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func);
        /// <summary>
        /// Get all data
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync();
        /// <summary>
        /// custome condition query
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func);
        /// <summary>
        /// split page query
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total);
        /// <summary>
        /// custome split page query
        /// </summary>
        /// <param name="func"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func, int page, int size, RefAsync<int> total);
    }
}
