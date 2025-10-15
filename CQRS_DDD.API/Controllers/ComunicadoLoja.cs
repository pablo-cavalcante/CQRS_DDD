using CQRS_DDD.Application.Commands.Fruta;
using CQRS_DDD.Application.Queries.ComunicadoLoja;
using CQRS_DDD.Application.Queries.Fruta;
using CQRS_DDD.Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

#pragma warning disable
namespace CQRS_DDD.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComunicadoLoja : ControllerBase
    {
        #region MyRegion
        private readonly IMediator _mediator;

        public ComunicadoLoja(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> GetAll()
        #region MyRegion
        {
            var comunicado = await _mediator.Send(new GetAllComunicadoLojaQuery());
            APIResponse response = new();
            response.setSuccessResponse("OK", comunicado);
            return CreatedAtAction(nameof(GetAll), null, response);
        }
        #endregion

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        #region MyRegion
        {
            var comunicado = await _mediator.Send(new GetComunicadoLojaByIdQuery { CiLojaEntityId = id });
            APIResponse response = new();

            if (comunicado == null)
            {
                response.setErrorReponsePlain("Comunicado não encontrado");
            }
            else
            {
                response.setSuccessResponse("OK", comunicado);
            }

            return CreatedAtAction(nameof(GetById), id, response);
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> Create(CreateComunicadoLojaCommand command)
        #region MyRegion
        {
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(Create), command, response);
        }
        #endregion

        [HttpPut]
        public async Task<IActionResult> Update(UpdateComunicadoLojaCommand command)
        #region MyRegion
        {
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(Update), command, response);
        }
        #endregion

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteComunicadoLojaCommand command)
        #region MyRegion
        {
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(Delete), command, response);
        }
        #endregion
    }
}
