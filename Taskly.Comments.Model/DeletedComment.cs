using System;

namespace Taskly.Comments.Model
{
    public class DeletedComment : Comment
    {
        public DeletedComment()
        {
        }

        public DeletedComment(Comment comment, DateTime removalTimestamp)
        {
            Id = comment.Id;
            AuthorId = comment.Id;
            Locator = comment.Locator;
            Text = comment.Text;
            Timestamp = comment.Timestamp;
            Replies = comment.Replies;
            RemovalTimestamp = removalTimestamp;
        }

        public DateTime RemovalTimestamp { get; set; }
    }
}
