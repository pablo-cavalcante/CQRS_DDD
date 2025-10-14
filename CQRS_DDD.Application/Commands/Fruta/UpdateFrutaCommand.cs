using MediatR;
using CQRS_DDD.Domain.Responses;

#pragma warning disable
namespace CQRS_DDD.Application.Commands.Fruta
{
    public class UpdateFrutaCommand : IRequest<APIResponse?>
    {
        public int FrutasEntityId { get; set; }
        public string Nome { get; set; }
        public int Qtde { get; set; }
        public bool Ativa { get; set; }
    }
}
