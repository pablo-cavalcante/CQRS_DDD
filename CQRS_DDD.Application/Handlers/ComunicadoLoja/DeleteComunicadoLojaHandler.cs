using MediatR;
using CQRS_DDD.Application.Commands.Fruta;
using CQRS_DDD.Domain.Interfaces;
using CQRS_DDD.Domain.Responses;

#pragma warning disable
namespace CQRS_DDD.Application.Handlers.Fruta
{
    public class DeleteComunicadoLojaHandler : IRequestHandler<DeleteComunicadoLojaCommand, APIResponse>
    {
        private readonly ICiLojaRepository repository;

        public DeleteComunicadoLojaHandler(ICiLojaRepository repository)
        #region MyRegion
        {
            this.repository = repository;
        } 
        #endregion

        public async Task<APIResponse> Handle(DeleteComunicadoLojaCommand request, CancellationToken cancellationToken)
        #region MyRegion
        {
            try
            {
                var comunicado = await repository.FindAsync(request.CiLojaEntityId, cancellationToken);
                APIResponse response = new APIResponse();

                if (comunicado == null)
                {
                    response.setErrorReponsePlain("Comunicado não encontrado!");
                    return response;
                }

                repository.BeginTransaction();
                await repository.Delete(comunicado, cancellationToken);
                repository.CommitTransaction();

                response.setSuccessResponse("Comunicado removido com sucesso!", comunicado);

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
