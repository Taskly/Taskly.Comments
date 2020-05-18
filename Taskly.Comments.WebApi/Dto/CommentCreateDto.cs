﻿using System.ComponentModel.DataAnnotations;

namespace Taskly.Comments.WebApi.Dto
{
    public class CommentCreateDto
    {
        public string UserId { get; set; }

        [Required]
        public LocatorDto Locator { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
