using MediatR;
using CQRS_DDD.Domain.Entities;

#pragma warning disable
namespace CQRS_DDD.Application.Queries
{
    public class GetFrutaByIdQuery : IRequest<FrutasEntity?>
    {
        public int FrutasEntityId { get; set; }
    }
}
