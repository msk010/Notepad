using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notepad.Application.Features.NoteFeatures.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notepad.Api.Controllers
{
    [Route("api/note")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private IMediator _mediator;
        public NoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("create")]
        public virtual async Task<IActionResult> Create(CreateNoteCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost]
        [Route("update")]
        public virtual async Task<IActionResult> Update(UpdateNoteCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public virtual async Task<IActionResult> DeleteById(int id)
        {
            return Ok(await _mediator.Send(new DeleteNoteByIdCommand { Id = id }));
        }
    }
}
