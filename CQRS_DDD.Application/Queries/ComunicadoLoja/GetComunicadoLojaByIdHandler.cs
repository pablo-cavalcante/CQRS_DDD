using CQRS_DDD.Application.Queries.Fruta;
using CQRS_DDD.Domain.Entities;
using CQRS_DDD.Domain.Interfaces;
using MediatR;

#pragma warning disable
namespace CQRS_DDD.Application.Queries.ComunicadoLoja
{
    public class GetComunicadoLojaByIdHandler : IRequestHandler<GetComunicadoLojaByIdQuery, ComunicadoLojaEntity?>
    {
        private readonly ICiLojaRepository repository;

        public GetComunicadoLojaByIdHandler(ICiLojaRepository repository)
        #region MyRegion
        {
            this.repository = repository;
        } 
        #endregion

        public async Task<ComunicadoLojaEntity?> Handle(GetComunicadoLojaByIdQuery request, CancellationToken cancellationToken)
        #region MyRegion
        {
            return await repository.FindAsync(request.CiLojaEntityId, cancellationToken);
        } 
        #endregion
    }
}
