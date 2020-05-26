using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taskly.Comments.Application;
using Taskly.Comments.Model;
using Taskly.Comments.Model.Exceptions;
using Taskly.Comments.WebApi.Dto;

namespace Taskly.Comments.WebApi.Controllers
{
    [Route("api/v1/comments")]
    public class CommentsController : ControllerBase
    {
        public CommentsController(IMapper mapper, ICommentsService commentsService)
        {
            _mapper = mapper;
            _commentsService = commentsService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CommentsListDto>> GetCommentsList(LocatorDto locatorDto,
            [Required] int page, [Required] int pageSize)
        {
            ThrowIfNull(locatorDto);

            Locator locator = new Locator(locatorDto.Section, locatorDto.Subsection, locatorDto.Element);
            PaginatedList<Comment> comments = await _commentsService.GetCommentsByLocator(locator, page, pageSize);

            var commentsDto = new PaginatedList<CommentDto>(
                comments.Select(x => _mapper.Map<CommentDto>(x)).ToList(), comments.TotalItems, page, pageSize);
            return Ok(new CommentsListDto(commentsDto));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CommentDto>> GetComment(string id)
        {
            int commentId = ParseId(id);
            Comment comment = await _commentsService.GetCommentById(commentId);
            CommentDto commentDto = _mapper.Map<CommentDto>(comment);
            return Ok(commentDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CommentDto>> CreateComment([FromBody] CommentCreateDto dto)
        {
            ThrowIfNull(dto);

            var locator = new Locator(dto.Locator.Section, dto.Locator.Subsection, dto.Locator.Element);
            int commentParentId = ParseParentId(dto.ParentId);
            var comment = new Comment(commentParentId, dto.UserId, locator, dto.Text);
            comment = await _commentsService.AddComment(comment);

            CommentDto createdCommentDto = _mapper.Map<CommentDto>(comment);
            return CreatedAtAction(nameof(GetComment), new { id = createdCommentDto.Id }, createdCommentDto);
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteComment(string id, string removalUserId)
        {
            int commentId = ParseId(id);
            await _commentsService.MarkAsDeleted(commentId, removalUserId);
            return NoContent();
        }

        [HttpGet("deleted/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DeletedCommentDto>> GetDeletedComment(string id)
        {
            int commentId = ParseId(id);
            DeletedComment comment = await _commentsService.GetDeletedComment(commentId);
            DeletedCommentDto commentDto = _mapper.Map<DeletedCommentDto>(comment);
            return Ok(commentDto);
        }

        private int ParseId(string id)
        {
            if (!int.TryParse(id, out int result))
            {
                throw new InvalidArgumentDomainException("Invalid ID.");
            }

            return result;
        }

        private int ParseParentId(string parentId)
        {
            int result = 0;
            if (!string.IsNullOrEmpty(parentId) && !int.TryParse(parentId, out result))
            {
                throw new InvalidArgumentDomainException("Invalid parent ID.");
            }

            return result;
        }

        private void ThrowIfNull(object obj)
        {
            if (obj is null)
            {
                throw new InvalidArgumentDomainException("Invalid argument.");
            }
        }

        private readonly IMapper _mapper;
        private readonly ICommentsService _commentsService;
    }
}
