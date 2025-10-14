using Microsoft.EntityFrameworkCore;
using CQRS_DDD.Domain.Entities;
using CQRS_DDD.Domain.Interfaces;
using CQRS_DDD.Infrastructure.Persistence;

#pragma warning disable
namespace CQRS_DDD.Infrastructure.Repositories
{
    public class FrutaRepository: Repository, IFrutaRepository
    {
        public FrutaRepository(PgsqlContext context) : base(context) {  }

        public async Task<FrutasEntity?> FindAsync(int id, CancellationToken cancellationToken)
        #region MyRegion
        {
            return await this.context.frutas.FindAsync(new object[] { id }, cancellationToken);
        }
        #endregion

        public async Task<IEnumerable<FrutasEntity>> ToListAsync(CancellationToken cancellationToken)
        #region MyRegion
        {
            return await this.context.frutas.ToListAsync(cancellationToken);
        } 
        #endregion
    }
}
