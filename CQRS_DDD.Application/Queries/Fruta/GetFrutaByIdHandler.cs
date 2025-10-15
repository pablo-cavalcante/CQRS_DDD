using MediatR;
using CQRS_DDD.Domain.Entities;
using CQRS_DDD.Domain.Interfaces;

#pragma warning disable
namespace CQRS_DDD.Application.Queries.Fruta
{
    public class GetFrutaByIdHandler : IRequestHandler<GetFrutaByIdQuery, FrutasEntity?>
    {
        private readonly IFrutaRepository repository;

        public GetFrutaByIdHandler(IFrutaRepository repository)
        #region MyRegion
        {
            this.repository = repository;
        } 
        #endregion

        public async Task<FrutasEntity?> Handle(GetFrutaByIdQuery request, CancellationToken cancellationToken)
        #region MyRegion
        {
            return await repository.FindAsync(request.FrutasEntityId, cancellationToken);
        } 
        #endregion
    }
}
