using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Taskly.Comments.Application;

namespace Taskly.Comments.WebApi.Dto
{
    public class CommentsListDto
    {
        public CommentsListDto(PaginatedList<CommentDto> model)
        {
            Page = model.PageIndex;
            TotalPages = model.TotalPages;
            TotalItems = model.TotalItems;
            Items = model;
        }

        [Required]
        public int Page { get; set; }

        [Required]
        public int TotalPages { get; set; }

        [Required]
        public int TotalItems { get; set; }

        [Required]
        public List<CommentDto> Items { get; set; }
    }
}
