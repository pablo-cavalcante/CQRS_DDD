using MediatR;
using CQRS_DDD.Application.Commands.Fruta;
using CQRS_DDD.Domain.Interfaces;
using CQRS_DDD.Domain.Responses;

#pragma warning disable
namespace CQRS_DDD.Application.Handlers.Fruta
{
    public class UpdateComunicadoLojaHandler : IRequestHandler<UpdateComunicadoLojaCommand, APIResponse?>
    {
        private readonly ICiLojaRepository repository;

        public UpdateComunicadoLojaHandler(ICiLojaRepository repository)
        #region MyRegion
        {
            this.repository = repository;
        } 
        #endregion

        public async Task<APIResponse> Handle(UpdateComunicadoLojaCommand request, CancellationToken cancellationToken)
        #region MyRegion
        {
            try
            {
                var comunicado = await repository.FindAsync(request.CiLojaEntityId, cancellationToken);
                APIResponse response = new();

                if (comunicado == null)
                {
                    response.setErrorReponsePlain("Comunicado não encontrado!");
                    return response;
                }

                comunicado.LojaEntityId = request.LojaEntityId;
                comunicado.CiMsg = request.CiMsg;
                comunicado.Ativa = request.Ativa;

                repository.BeginTransaction();
                await repository.Update(comunicado, cancellationToken);
                repository.CommitTransaction();

                response.setSuccessResponse("Comunicado atualizada com sucesso!", comunicado);

                return response;
            }
            catch (Exception ex)
            {
                repository.RollbackTransaction();
                throw new ApplicationException($"Erro ao criar comunicado '{request.CiLojaEntityId}': {ex.Message}", ex);
            }
        } 
        #endregion
    }
}
