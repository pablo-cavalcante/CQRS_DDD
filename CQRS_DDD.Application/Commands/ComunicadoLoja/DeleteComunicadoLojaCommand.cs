using MediatR;
using CQRS_DDD.Domain.Responses;

#pragma warning disable
namespace CQRS_DDD.Application.Commands.Fruta
{
    public class DeleteComunicadoLojaCommand : IRequest<APIResponse>
    {
        public int CiLojaEntityId { get; set; }
    }
}
