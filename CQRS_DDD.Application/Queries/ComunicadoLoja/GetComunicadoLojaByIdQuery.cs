using MediatR;
using CQRS_DDD.Domain.Entities;

#pragma warning disable
namespace CQRS_DDD.Application.Queries.Fruta
{
    public class GetComunicadoLojaByIdQuery : IRequest<ComunicadoLojaEntity?>
    {
        public int CiLojaEntityId { get; set; }
    }
}
