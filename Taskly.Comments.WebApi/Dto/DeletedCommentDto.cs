using System;
using System.ComponentModel.DataAnnotations;

namespace Taskly.Comments.WebApi.Dto
{
    public class DeletedCommentDto : CommentDto
    {
        [Required]
        public string RemovalUserId { get; set; }

        [Required]
        public DateTime RemovalTimestamp { get; set; }
    }
}
