using MediatR;
using CQRS_DDD.Domain.Responses;

#pragma warning disable
namespace CQRS_DDD.Application.Commands.Fruta
{
    public class DeleteFrutaCommand : IRequest<APIResponse>
    {
        public int FrutasEntityId { get; set; }
    }
}
