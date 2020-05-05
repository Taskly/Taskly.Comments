using System;

namespace Taskly.Comments.WebApi.Dto
{
    public class CommentDto
    {
        public string Id { get; set; }

        public string ParentId { get; set; }

        public string AuthorId { get; set; }

        public string Text { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
