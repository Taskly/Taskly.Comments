using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taskly.Comments.WebApi.Dto;

namespace Taskly.Comments.WebApi.Controllers
{
    [Route("api/comments")]
    public class CommentsController : ControllerBase
    {
        [HttpGet("list/{locator}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CommentsListDto> GetCommentsList(string locator)
        {
            var dto = new CommentsListDto
            {
                Locator = locator,
                Comments = new List<CommentDto>()
            };

            for (int i = 0; i < 50; ++i)
            {
                dto.Comments.Add(new CommentDto
                {
                    Id = (i + 1).ToString(),
                    ParentId = 0.ToString(),
                    AuthorId = (i + 350).ToString(),
                    Text = $"Some comment text. ({i}).",
                    Timestamp = DateTime.UtcNow
                });
            }

            return Ok(dto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CommentDto> GetComment(string id)
        {
            var dto = new CommentDto
            {
                Id = id,
                ParentId = 0.ToString(),
                AuthorId = 350.ToString(),
                Text = $"Some comment text.",
                Timestamp = DateTime.UtcNow
            };

            return Ok(dto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CommentDto> CreateComment([FromBody] CommentCreateDto dto)
        {
            var commentDto = new CommentDto
            {
                Id = 367.ToString(),
                ParentId = dto.ParentId,
                AuthorId = dto.AuthorId,
                Text = dto.Text,
                Timestamp = DateTime.UtcNow
            };

            return CreatedAtAction(nameof(GetComment), new { id = commentDto.Id }, commentDto);
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteComment(string id)
        {
            return NoContent();
        }
    }
}
