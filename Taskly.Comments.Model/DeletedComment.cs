﻿using System;
using System.Collections.Generic;

namespace Taskly.Comments.Model
{
    public class DeletedComment : Comment
    {
        public DeletedComment(Comment comment)
            : this(comment.Id, comment.UserId, comment.Locator, comment.Text, comment.Timestamp, comment.Replies,
                DateTime.UtcNow)
        {
        }

        public DeletedComment(string id, string userId, Locator locator, string text, DateTime timestamp,
            List<Comment> replies, DateTime removalTimestamp)
            : base(id, userId, locator, text, timestamp, replies)
        {
            RemovalTimestamp = removalTimestamp;
        }

        public DateTime RemovalTimestamp { get; }
    }
}
