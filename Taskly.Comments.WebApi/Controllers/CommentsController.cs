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
        [HttpGet("{locator}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CommentsListDto> GetComments(string locator)
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CreateComment([FromBody] CommentCreateDto dto)
        {
            return Ok();
        }
    }
}
