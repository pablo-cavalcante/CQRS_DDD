using CQRS_DDD.Domain.Entities;

#pragma warning disable
namespace CQRS_DDD.Domain.Interfaces
{
    public interface ICiLojaRepository : IRepository
    {
        public Task<ComunicadoLojaEntity?> FindAsync(int id, CancellationToken cancellationToken);

        public Task<IEnumerable<ComunicadoLojaEntity>> ToListAsync(CancellationToken cancellationToken);
    }
}
