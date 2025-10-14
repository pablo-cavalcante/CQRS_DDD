using MediatR;
using CQRS_DDD.Domain.Entities;
using CQRS_DDD.Domain.Interfaces;

#pragma warning disable
namespace CQRS_DDD.Application.Queries
{
    public class GetAllFrutasHandler: IRequestHandler<GetAllFrutasQuery, IEnumerable<FrutasEntity>>
    {
        private readonly IFrutaRepository repository;

        public GetAllFrutasHandler(IFrutaRepository repository)
        #region MyRegion
        {
            this.repository = repository;
        } 
        #endregion

        public async Task<IEnumerable<FrutasEntity>> Handle(GetAllFrutasQuery request, CancellationToken cancellationToken)
        #region MyRegion
        {
            return await this.repository.ToListAsync(cancellationToken);
        } 
        #endregion
    }
}
