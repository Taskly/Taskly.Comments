using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace Taskly.Comments.Model
{
    public class Comment
    {
        public Comment()
        {
            IsDeleted = false;
            Locator = new Locator();
            Replies = new List<Comment>();
        }

        public string Id { get; set; }

        public string AuthorId { get; set; }

        public Locator Locator { get; set; }

        public string Text { get; set; }

        public DateTime Timestamp { get; set; }

        public bool IsDeleted { get; set; }

        public List<Comment> Replies { get; set; }
    }
}
