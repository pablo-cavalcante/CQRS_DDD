using CQRS_DDD.Domain.Entities;

#pragma warning disable
namespace CQRS_DDD.Domain.Interfaces
{
    public interface IFrutaRepository: IRepository
    {
        public Task<FrutasEntity?> FindAsync(int id, CancellationToken cancellationToken);

        public Task<IEnumerable<FrutasEntity>> ToListAsync(CancellationToken cancellationToken);
    } 
}
