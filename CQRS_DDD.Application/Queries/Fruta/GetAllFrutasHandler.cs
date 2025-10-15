using MediatR;
using CQRS_DDD.Domain.Entities;
using CQRS_DDD.Domain.Interfaces;

#pragma warning disable
namespace CQRS_DDD.Application.Queries.Fruta
{
    public class GetAllFrutaHandler: IRequestHandler<GetAllFrutaQuery, IEnumerable<FrutasEntity>>
    {
        private readonly IFrutaRepository repository;

        public GetAllFrutaHandler(IFrutaRepository repository)
        #region MyRegion
        {
            this.repository = repository;
        } 
        #endregion

        public async Task<IEnumerable<FrutasEntity>> Handle(GetAllFrutaQuery request, CancellationToken cancellationToken)
        #region MyRegion
        {
            return await repository.ToListAsync(cancellationToken);
        } 
        #endregion
    }
}
