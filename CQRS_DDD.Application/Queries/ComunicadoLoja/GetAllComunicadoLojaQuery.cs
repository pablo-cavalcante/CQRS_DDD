using CQRS_DDD.Domain.Entities;
using MediatR;

#pragma warning disable
namespace CQRS_DDD.Application.Queries.ComunicadoLoja
{
    public class GetAllComunicadoLojaQuery : IRequest<IEnumerable<ComunicadoLojaEntity>>
    {
        
    }
}
