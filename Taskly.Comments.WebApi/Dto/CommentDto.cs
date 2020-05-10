using System;
using System.Collections.Generic;

namespace Taskly.Comments.WebApi.Dto
{
    public class CommentDto
    {
        public string Id { get; set; }

        public string ParentId { get; set; }

        public string AuthorId { get; set; }

        public LocatorDto Locator { get; set; }

        public string Text { get; set; }

        public DateTime Timestamp { get; set; }

        public List<CommentDto> Replies { get; set; }
    }
}
