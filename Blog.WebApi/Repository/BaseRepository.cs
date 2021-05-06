using Blog.IRepository;
using Blog.Model;
using SqlSugar;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository
{
    public class BaseRepository<TEntity> : SimpleClient<TEntity>, IBaseRepository<TEntity> where TEntity : class, new()
    {
        public BaseRepository(ISqlSugarClient context=null):base(context)
        {
            base.Context = DbScoped.Sugar;
            //Create DB
            //base.Context.DbMaintenance.CreateDatabase();
            //base.Context.CodeFirst.InitTables(
            //    typeof(BlogNews),
            //    typeof(TypeInfo),
            //    typeof(WriterInfo)
            //    );
        }
        public async Task<bool> CreateAsync(TEntity entity)
        {
            return await base.InsertAsync(entity);
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            return await base.DeleteByIdAsync(Id);
        }

        public async Task<bool> EditAsync(TEntity entity)
        {
            return await base.UpdateAsync(entity);
        }

        public virtual async Task<TEntity> FindAsync(int Id)
        {
            return await base.GetByIdAsync(Id);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func)
        {
            return await base.GetSingleAsync(func);
        }

        public virtual async Task<List<TEntity>> QueryAsync()
        {
            return await base.GetListAsync();
        }

        public virtual async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func)
        {
            return await base.GetListAsync(func);
        }

        public virtual async Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<TEntity>()
               .ToPageListAsync(page, size,total);
        }

        public virtual async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<TEntity>().Where(func).ToPageListAsync(page,size,total);
        }
    }
}
