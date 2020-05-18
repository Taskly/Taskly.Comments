﻿using System;
using System.Collections.Generic;

namespace Taskly.Comments.Model
{
    public class Comment
    {
        public Comment(string authorId, Locator locator, string text)
            : this(null, authorId, locator, text, DateTime.UtcNow, new List<Comment>())
        {
        }

        public Comment(string id, string userId, Locator locator, string text, DateTime timestamp,
            List<Comment> replies)
        {
            Id = id;
            UserId = userId;
            Locator = locator;
            Text = text;
            Timestamp = timestamp;
            Replies = replies;
        }

        public string Id { get; }

        public string UserId { get; }

        public Locator Locator { get; }

        public string Text { get; }

        public DateTime Timestamp { get; }

        public List<Comment> Replies { get; }
    }
}
