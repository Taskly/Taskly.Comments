using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taskly.Comments.WebApi.Dto;

namespace Taskly.Comments.WebApi.Controllers
{
    [Route("api/comments")]
    public class CommentsController : ControllerBase
    {
        static CommentsController()
        {
            var rand = new Random();
            Data = new List<CommentDto>();
            _counter = 50000;
            for (int i = 0; i < _counter; ++i)
            {
                Data.Add(new CommentDto
                {
                    Id = (i + 1).ToString(),
                    ParentId = 0.ToString(),
                    AuthorId = rand.Next(1, 100000).ToString(),
                    Text = $"Some comment text. ({i}).",
                    Timestamp = DateTime.UtcNow
                });
            }
        }

        [HttpGet("list/{locator}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CommentsListDto> GetCommentsList(string locator, int page = 0, int pageSize = 100,
            CommentsListSortType sort = CommentsListSortType.NewToOld)
        {
            IEnumerable<CommentDto> commentsDto = Data.Skip(page * pageSize).Take(pageSize);
            switch (sort)
            {
                case CommentsListSortType.NewToOld:
                    commentsDto = commentsDto.OrderByDescending(x => x.Id);
                    break;
                case CommentsListSortType.OldToNew:
                    commentsDto = commentsDto.OrderBy(x => x.Id);
                    break;
            }

            var dto = new CommentsListDto
            {
                Locator = locator,
                Comments = commentsDto.ToList()
            };
            return Ok(dto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CommentDto> GetComment(string id)
        {
            CommentDto dto = Data.FirstOrDefault(x => x.Id == id);
            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CommentDto> CreateComment([FromBody] CommentCreateDto dto)
        {
            var commentDto = new CommentDto
            {
                Id = (_counter++).ToString(),
                ParentId = dto.ParentId,
                AuthorId = dto.AuthorId,
                Text = dto.Text,
                Timestamp = DateTime.UtcNow
            };

            Data.Add(commentDto);
            return CreatedAtAction(nameof(GetComment), new { id = commentDto.Id }, commentDto);
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteComment(string id)
        {
            CommentDto dto = Data.FirstOrDefault(x => x.Id == id);
            if (dto == null)
            {
                return NotFound();
            }

            Data.Remove(dto);
            return NoContent();
        }

        private static readonly List<CommentDto> Data;
        private static int _counter;
    }
}
