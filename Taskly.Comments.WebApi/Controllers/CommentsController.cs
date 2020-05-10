using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taskly.Comments.Application;
using Taskly.Comments.WebApi.Dto;

namespace Taskly.Comments.WebApi.Controllers
{
    [Route("api/comments")]
    public class CommentsController : ControllerBase
    {
        /*public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }*/

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<CommentDto>> GetCommentsList(LocatorDto locator)
        {
            return Ok();
        }

        [HttpGet("top")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<CommentDto>> GetTopComments(LocatorDto locator)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CommentDto> GetComment(string id)
        {
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CommentDto> CreateComment([FromBody] CommentCreateDto dto)
        {
            // return CreatedAtAction(nameof(GetComment), new { id = commentDto.Id }, commentDto);
            return Ok();
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteComment(string id)
        {
            return NoContent();
        }

        // private readonly ICommentsService _commentsService;
    }
}
