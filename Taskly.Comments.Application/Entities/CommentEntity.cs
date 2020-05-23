using System;
using Taskly.Comments.Model;

namespace Taskly.Comments.Application.Entities
{
    public class CommentEntity
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public string UserId { get; set; }

        public CommentStatus Status { get; set; }

        public string LocatorSection { get; set; }

        public string LocatorSubsection { get; set; }

        public string LocatorElement { get; set; }

        public string Text { get; set; }

        public DateTime Timestamp { get; set; }

        public string RemovalUserId { get; set; }

        public DateTime? RemovalTimestamp { get; set; }
    }
}
