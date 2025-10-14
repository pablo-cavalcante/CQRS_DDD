using Microsoft.EntityFrameworkCore.Storage;

#pragma warning disable
namespace CQRS_DDD.Domain.Interfaces
{
    public interface IRepository
    {
        public Task<object> Add(object objeto, CancellationToken cancellationToken);

        public Task<object> Update(object objEntity, CancellationToken cancellationToken);

        public Task<bool> Delete(object objEntity, CancellationToken cancellationToken);

        public RelationalTransaction BeginTransaction();

        public void CommitTransaction();

        public void RollbackTransaction();
    }
}
