using System;
using System.Collections.Generic;
using Taskly.Comments.Model;

namespace Taskly.Comments.Application.Entities
{
    public class DeletedCommentEntity
    {
        public DeletedCommentEntity(DeletedComment model, string parentId)
        {
            if (!string.IsNullOrEmpty(model.Id))
            {
                Id = int.Parse(model.Id);
            }

            if (!string.IsNullOrEmpty(parentId))
            {
                ParentId = int.Parse(parentId);
            }

            AuthorId = model.AuthorId;
            Text = model.Text;
            Timestamp = model.Timestamp;
            LocatorSection = model.Locator.Section;
            LocatorSubsection = model.Locator.Subsection;
            LocatorElement = model.Locator.Element;
            RemovalTimestamp = model.RemovalTimestamp;
        }

        protected DeletedCommentEntity()
        {
        }

        public int Id { get; private set; }

        public int ParentId { get; private set; }

        public string AuthorId { get; private set; }

        public string Text { get; private set; }

        public DateTime Timestamp { get; private set; }

        public string LocatorSection { get; private set; }

        public string LocatorSubsection { get; private set; }

        public string LocatorElement { get; private set; }

        public DateTime RemovalTimestamp { get; private set; }

        public DeletedComment ToModel()
        {
            Locator locator = new Locator(LocatorSection, LocatorSubsection, LocatorElement);
            string id = Id == 0 ? string.Empty : Id.ToString();
            return new DeletedComment(id, AuthorId, locator, Text, Timestamp, new List<Comment>(), RemovalTimestamp);
        }
    }
}
