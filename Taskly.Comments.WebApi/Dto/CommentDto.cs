using System;
using System.Collections.Generic;
using Taskly.Comments.Model;

namespace Taskly.Comments.WebApi.Dto
{
    public class CommentDto
    {
        public CommentDto(Comment model)
        {
            Id = model.Id;
            AuthorId = model.AuthorId;
            Locator = new LocatorDto(model.Locator);
            Text = model.Text;
            Timestamp = model.Timestamp;
            Replies = new List<CommentDto>();
        }

        public string Id { get; set; }

        public string AuthorId { get; set; }

        public LocatorDto Locator { get; set; }

        public string Text { get; set; }

        public DateTime Timestamp { get; set; }

        public List<CommentDto> Replies { get; set; }
    }
}
