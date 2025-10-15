using MediatR;
using CQRS_DDD.Application.Commands.Fruta;
using CQRS_DDD.Domain.Interfaces;
using CQRS_DDD.Domain.Responses;

#pragma warning disable
namespace CQRS_DDD.Application.Handlers.Fruta
{
    public class UpdateFrutaHandler : IRequestHandler<UpdateFrutaCommand, APIResponse?>
    {
        private readonly IFrutaRepository repository;

        public UpdateFrutaHandler(IFrutaRepository repository)
        #region MyRegion
        {
            this.repository = repository;
        } 
        #endregion

        public async Task<APIResponse> Handle(UpdateFrutaCommand request, CancellationToken cancellationToken)
        #region MyRegion
        {
            try
            {
                var fruta = await repository.FindAsync(request.FrutasEntityId, cancellationToken);
                APIResponse response = new();

                if (fruta == null)
                {
                    response.setErrorReponsePlain("Fruta não encontrada!");
                    return response;
                }

                fruta.Nome = request.Nome;
                fruta.Qtde = request.Qtde;
                fruta.Ativa = request.Ativa;

                repository.BeginTransaction();
                await repository.Update(fruta, cancellationToken);
                repository.CommitTransaction();

                response.setSuccessResponse("Fruta atualizada com sucesso!", fruta);

                return response;
            }
            catch (Exception ex)
            {
                repository.RollbackTransaction();
                throw new ApplicationException($"Erro ao criar fruta '{request.FrutasEntityId}': {ex.Message}", ex);
            }
        } 
        #endregion
    }
}
