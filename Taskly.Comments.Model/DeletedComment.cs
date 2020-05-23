using System;
using Taskly.Comments.Model.Exceptions;

namespace Taskly.Comments.Model
{
    public class DeletedComment : Comment
    {
        public DeletedComment(Comment comment, string removalUserId)
        {
            Validate(comment, removalUserId);

            Id = comment.Id;
            ParentId = comment.ParentId;
            UserId = comment.UserId;
            Locator = comment.Locator;
            Text = comment.Text;
            Timestamp = comment.Timestamp;

            RemovalUserId = removalUserId;
            RemovalTimestamp = DateTime.UtcNow;
            Status = CommentStatus.Deleted;
        }

        protected DeletedComment()
        {
        }

        public string RemovalUserId { get; private set; }

        public DateTime RemovalTimestamp { get; private set; }

        private void Validate(Comment comment, string removalUserId)
        {
            if (comment is null)
            {
                throw new NullReferenceException("Comment is null.");
            }

            if (string.IsNullOrEmpty(removalUserId))
            {
                throw new InvalidArgumentDomainException("Removal User ID required.");
            }
        }
    }
}
