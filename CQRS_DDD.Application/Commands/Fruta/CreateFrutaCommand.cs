using MediatR;
using CQRS_DDD.Domain.Responses;

#pragma warning disable
namespace CQRS_DDD.Application.Commands.Fruta
{
    public class CreateFrutaCommand : IRequest<APIResponse>
    {
        public string Nome { get; set; }
        public int Qtde { get; set; }
        public bool Ativa { get; set; }
    }
}
