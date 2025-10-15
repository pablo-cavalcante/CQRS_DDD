using MediatR;
using CQRS_DDD.Domain.Entities;
using CQRS_DDD.Domain.Interfaces;

#pragma warning disable
namespace CQRS_DDD.Application.Queries.ComunicadoLoja
{
    public class GetAllComunicadoLojaHandler : IRequestHandler<GetAllComunicadoLojaQuery, IEnumerable<ComunicadoLojaEntity>>
    {
        private readonly ICiLojaRepository repository;

        public GetAllComunicadoLojaHandler(ICiLojaRepository repository)
        #region MyRegion
        {
            this.repository = repository;
        } 
        #endregion

        public async Task<IEnumerable<ComunicadoLojaEntity>> Handle(GetAllComunicadoLojaQuery request, CancellationToken cancellationToken)
        #region MyRegion
        {
            return await repository.ToListAsync(cancellationToken);
        } 
        #endregion
    }
}
