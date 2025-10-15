using MediatR;
using CQRS_DDD.Domain.Responses;

#pragma warning disable
namespace CQRS_DDD.Application.Commands.Fruta
{
    public class CreateComunicadoLojaCommand : IRequest<APIResponse>
    {
        public int CiLojaEntityId { get; set; }
        public int LojaEntityId { get; set; }
        public string CiMsg { get; set; }
        public bool Ativa { get; set; }
    }
}
