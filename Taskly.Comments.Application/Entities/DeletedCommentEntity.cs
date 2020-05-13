using System;
using Taskly.Comments.Model;

namespace Taskly.Comments.Application.Entities
{
    public class DeletedCommentEntity
    {
        public DeletedCommentEntity(CommentEntity commentEntity, DateTime removalTimestamp)
        {
            Id = commentEntity.Id;
            ParentId = commentEntity.ParentId;
            AuthorId = commentEntity.AuthorId;
            Text = commentEntity.Text;
            Timestamp = commentEntity.Timestamp;
            LocatorSection = commentEntity.LocatorSection;
            LocatorSubsection = commentEntity.LocatorSubsection;
            LocatorElement = commentEntity.LocatorElement;
            RemovalTimestamp = removalTimestamp;
        }

        protected DeletedCommentEntity()
        {
        }

        public int Id { get; set; }

        public int ParentId { get; set; }

        public int AuthorId { get; set; }

        public string Text { get; set; }

        public DateTime Timestamp { get; set; }

        public string LocatorSection { get; set; }

        public string LocatorSubsection { get; set; }

        public string LocatorElement { get; set; }

        public DateTime RemovalTimestamp { get; set; }

        public DeletedComment ToModel()
        {
            return new DeletedComment
            {
                Id = Id == 0 ? string.Empty : Id.ToString(),
                AuthorId = AuthorId.ToString(),
                Text = Text,
                Timestamp = Timestamp,
                Locator = new Locator
                {
                    Section = LocatorSection,
                    Subsection = LocatorSubsection,
                    Element = LocatorElement
                },
                RemovalTimestamp = RemovalTimestamp
            };
        }
    }
}
