﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taskly.Comments.Application;
using Taskly.Comments.Model;
using Taskly.Comments.WebApi.Dto;

namespace Taskly.Comments.WebApi.Controllers
{
    [Route("api/comments")]
    public class CommentsController : ControllerBase
    {
        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CommentDto>>> GetCommentsList(LocatorDto locatorDto)
        {
            Locator locator = locatorDto.ToModel();
            List<Comment> comments = await _commentsService.GetCommentsByLocator(locator);
            List<CommentDto> commentsDto = comments.Select(x => new CommentDto(x)).ToList();
            return Ok(commentsDto);
        }

        [HttpGet("top")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<CommentDto>> GetTopComments(LocatorDto locator)
        {
            return Ok();
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CommentDto>>> GetCommentsByUser(string authorId)
        {
            List<Comment> comments = await _commentsService.GetCommentsByUser(authorId);
            List<CommentDto> commentsDto = comments.Select(x => new CommentDto(x)).ToList();
            return Ok(commentsDto);
        }

        [HttpGet("user/{userId}/deleted")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<DeletedCommentDto>>> GetDeletedCommentsByAuthor(string authorId)
        {
            List<DeletedComment> comments = await _commentsService.GetDeletedCommentsByUser(authorId);
            List<DeletedCommentDto> commentsDto = comments.Select(x => new DeletedCommentDto(x)).ToList();
            return Ok(commentsDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CommentDto>> GetComment(string id)
        {
            Comment comment = await _commentsService.GetCommentById(id);
            var commentDto = new CommentDto(comment);
            return Ok(commentDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CommentDto>> CreateComment([FromBody] CommentCreateDto dto)
        {
            var comment = new Comment(dto.AuthorId, dto.Locator.ToModel(), dto.Text);

            if (!string.IsNullOrEmpty(dto.UserId))
            {
                comment = await _commentsService.AddReply(dto.UserId, comment);
            }
            else
            {
                comment = await _commentsService.AddComment(comment);
            }

            var commentDto = new CommentDto(comment);
            return CreatedAtAction(nameof(GetComment), new { id = commentDto.Id }, commentDto);
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteComment(string id)
        {
            await _commentsService.MarkAsDeleted(id);
            return NoContent();
        }

        private readonly ICommentsService _commentsService;
    }
}
