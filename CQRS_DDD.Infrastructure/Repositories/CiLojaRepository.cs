using CQRS_DDD.Domain.Entities;
using CQRS_DDD.Domain.Interfaces;
using CQRS_DDD.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

#pragma warning disable
namespace CQRS_DDD.Infrastructure.Repositories
{
    public class CiLojaRepository: Repository, ICiLojaRepository
    {
        public CiLojaRepository(PgsqlContext context) : base(context) { }

        public async Task<ComunicadoLojaEntity?> FindAsync(int id, CancellationToken cancellationToken)
        #region MyRegion
        {
            return await this.context.ciLojas.FindAsync(new object[] { id }, cancellationToken);
        }
        #endregion

        public async Task<IEnumerable<ComunicadoLojaEntity>> ToListAsync(CancellationToken cancellationToken)
        #region MyRegion
        {
            return await this.context.ciLojas.ToListAsync(cancellationToken);
        }
        #endregion
    }
}
