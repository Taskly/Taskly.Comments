using System;
using System.ComponentModel.DataAnnotations;
using Taskly.Comments.Model;

namespace Taskly.Comments.WebApi.Dto
{
    public class CommentDto
    {
        public CommentDto(Comment model)
        {
            Id = model.Id.ToString();
            ParentId = model.ParentId.ToString();
            UserId = model.UserId;
            Status = model.Status;
            Locator = new LocatorDto(model.Locator);
            Text = model.Text;
            Timestamp = model.Timestamp;
        }

        [Required]
        public string Id { get; set; }

        [Required]
        public string ParentId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public CommentStatus Status { get; set; }

        [Required]
        public LocatorDto Locator { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }
    }
}
