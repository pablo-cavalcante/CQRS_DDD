using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using CQRS_DDD.Domain.Interfaces;
using CQRS_DDD.Infrastructure.Persistence;


#pragma warning disable
namespace CQRS_DDD.Infrastructure.Repositories
{
    public class Repository : IRepository
    {
        #region Constructor
        protected PgsqlContext context { get; set; }

        public Repository(PgsqlContext context)
        {
            this.context = context;
        }
        #endregion

        public async Task<object> Add(object objeto, CancellationToken cancellationToken)
        #region
        {
            context.Add(objeto);
            await context.SaveChangesAsync(cancellationToken);
            context.Entry(objeto).State = EntityState.Detached;

            return objeto;
        }
        #endregion

        public async Task<bool> Delete(object objeto, CancellationToken cancellationToken)
        #region
        {
            context.Remove(objeto);
            int flag = await context.SaveChangesAsync(cancellationToken);
            return flag > 0;
        }
        #endregion

        public async Task<object> Update(object objeto, CancellationToken cancellationToken)
        #region
        {
            EntityEntry _objeto = context.Attach(objeto);
            _objeto.State = EntityState.Modified;
            await context.SaveChangesAsync(cancellationToken);
            _objeto.State = EntityState.Detached;
            return objeto;
        }
        #endregion

        public RelationalTransaction BeginTransaction()
        #region
        {
            return (RelationalTransaction)context.Database.BeginTransaction();
        }
        #endregion

        public void CommitTransaction()
        #region
        {
            context.Database.CommitTransaction();
        }
        #endregion

        public void RollbackTransaction()
        #region
        {
            context.Database.RollbackTransaction();
        }
        #endregion
    }
}
