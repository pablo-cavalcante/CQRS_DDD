using MediatR;
using CQRS_DDD.Application.Commands.Fruta;
using CQRS_DDD.Domain.Entities;
using CQRS_DDD.Domain.Interfaces;
using CQRS_DDD.Domain.Responses;

#pragma warning disable
namespace CQRS_DDD.Application.Handlers
{
    public class CreateFrutaHandler : IRequestHandler<CreateFrutaCommand, APIResponse>
    {
        private readonly IFrutaRepository _context;

        public CreateFrutaHandler(IFrutaRepository context)
        #region MyRegion
        {
            _context = context;
        } 
        #endregion

        public async Task<APIResponse> Handle(CreateFrutaCommand request, CancellationToken cancellationToken)
        #region MyRegion
        {
            try
            {
                var fruta = new FrutasEntity
                {
                    Nome = request.Nome,
                    Qtde = request.Qtde,
                    Ativa = request.Ativa
                };

                this._context.BeginTransaction();
                await this._context.Add(fruta, cancellationToken);
                this._context.CommitTransaction();

                APIResponse response = new();
                response.setSuccessResponse("Fruta criada com sucesso!", fruta);

                return response;
            }
            catch (Exception ex)
            {
                this._context.RollbackTransaction();
                throw new ApplicationException($"Erro ao criar fruta '{request.Nome}': {ex.Message}", ex);
            }
        }
    } 
    #endregion
}
