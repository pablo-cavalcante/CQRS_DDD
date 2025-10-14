using MediatR;
using CQRS_DDD.Application.Commands.Fruta;
using CQRS_DDD.Domain.Interfaces;
using CQRS_DDD.Domain.Responses;

#pragma warning disable
namespace CQRS_DDD.Application.Handlers
{
    public class DeleteFrutaHandler : IRequestHandler<DeleteFrutaCommand, APIResponse>
    {
        private readonly IFrutaRepository repository;

        public DeleteFrutaHandler(IFrutaRepository repository)
        #region MyRegion
        {
            this.repository = repository;
        } 
        #endregion

        public async Task<APIResponse> Handle(DeleteFrutaCommand request, CancellationToken cancellationToken)
        #region MyRegion
        {
            try
            {
                var fruta = await this.repository.FindAsync(request.FrutasEntityId, cancellationToken);
                APIResponse response = new APIResponse();

                if (fruta == null)
                {
                    response.setErrorReponsePlain("Fruta não encontrada!");
                    return response;
                }

                this.repository.BeginTransaction();
                await this.repository.Delete(fruta, cancellationToken);
                this.repository.CommitTransaction();

                response.setSuccessResponse("Fruta removida com sucesso!", fruta);

                return response;
            }
            catch (Exception ex)
            {
                this.repository.RollbackTransaction();
                throw new ApplicationException($"Erro ao criar fruta '{request.FrutasEntityId}': {ex.Message}", ex);
            }
        } 
        #endregion
    }
}
