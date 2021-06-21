using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notepad.Application.Features.NoteFeatures.Commands;
using Notepad.Application.Features.NoteFeatures.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notepad.Api.Controllers
{
    [Route("api/tag")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private IMediator _mediator;
        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("list")]
        public virtual async Task<IActionResult> GetList()
        {
            return Ok(await _mediator.Send(new GetAllTagsQuery()));
        }

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            return Ok(await _mediator.Send(new GetTagByIdQuery(id)));
        }

        [HttpPost]
        [Route("create")]
        public virtual async Task<IActionResult> Create(CreateTagCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost]
        [Route("update")]
        public virtual async Task<IActionResult> Update(UpdateTagCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public virtual async Task<IActionResult> DeleteById(int id)
        {
            return Ok(await _mediator.Send(new DeleteTagByIdCommand { Id = id }));
        }
    }
}
