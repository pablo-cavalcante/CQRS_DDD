using MediatR;
using Microsoft.AspNetCore.Mvc;
using CQRS_DDD.Application.Commands.Fruta;
using CQRS_DDD.Application.Queries;
using CQRS_DDD.Domain.Responses;

#pragma warning disable
namespace CQRS_DDD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FrutasController : ControllerBase
    {
        #region MyRegion
        private readonly IMediator _mediator;

        public FrutasController(IMediator mediator)
        {
            _mediator = mediator;
        } 
        #endregion

        [HttpGet]
        public async Task<IActionResult> GetAll()
        #region MyRegion
        {
            var frutas = await _mediator.Send(new GetAllFrutasQuery());
            APIResponse response = new();
            response.setSuccessResponse("OK", frutas);
            return CreatedAtAction(nameof(GetAll), null, response);
        } 
        #endregion

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        #region MyRegion
        {
            var fruta = await _mediator.Send(new GetFrutaByIdQuery { FrutasEntityId = id });
            APIResponse response = new();

            if (fruta == null) {
                response.setErrorReponsePlain("Fruta não encontrada");
            } 
            else
            {
                response.setSuccessResponse("OK", fruta);
            }

            return CreatedAtAction(nameof(GetById), id, response);
        } 
        #endregion

        [HttpPost]
        public async Task<IActionResult> Create(CreateFrutaCommand command)
        #region MyRegion
        {
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(Create), command, response);
        }
        #endregion

        [HttpPut]
        public async Task<IActionResult> Update(UpdateFrutaCommand command)
        #region MyRegion
        {
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(Update), command, response);
        }
        #endregion

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteFrutaCommand command)
        #region MyRegion
        {
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(Delete), command, response);
        }
        #endregion
    }
}
