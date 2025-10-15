using MediatR;
using CQRS_DDD.Application.Commands.Fruta;
using CQRS_DDD.Domain.Entities;
using CQRS_DDD.Domain.Interfaces;
using CQRS_DDD.Domain.Responses;

#pragma warning disable
namespace CQRS_DDD.Application.Handlers.Fruta
{
    public class CreateComunicadoLojaHandler : IRequestHandler<CreateComunicadoLojaCommand, APIResponse>
    {
        private readonly ICiLojaRepository _context;

        public CreateComunicadoLojaHandler(ICiLojaRepository context)
        #region MyRegion
        {
            _context = context;
        } 
        #endregion

        public async Task<APIResponse> Handle(CreateComunicadoLojaCommand request, CancellationToken cancellationToken)
        #region MyRegion
        {
            try
            {
                var comunicado = new ComunicadoLojaEntity
                {
                    LojaEntityId = request.LojaEntityId,
                    CiMsg = request.CiMsg,
                    Ativa = request.Ativa
                };

                _context.BeginTransaction();
                await _context.Add(comunicado, cancellationToken);
                _context.CommitTransaction();

                APIResponse response = new();
                response.setSuccessResponse("Comunicado criado com sucesso!", comunicado);

                return response;
            }
            catch (Exception ex)
            {
                _context.RollbackTransaction();
                throw new ApplicationException($"Erro ao criar comunicado '{request.CiLojaEntityId}': {ex.Message}", ex);
            }
        }
    } 
    #endregion
}
