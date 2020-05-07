using System;
using Taskly.Comments.Model;

namespace Taskly.Comments.Application.Entities
{
    public class CommentEntity
    {
        public CommentEntity(Comment model, string parentId)
        {
            if (!string.IsNullOrEmpty(model.Id))
            {
                Id = int.Parse(model.Id);
            }

            if (!string.IsNullOrEmpty(parentId))
            {
                ParentId = int.Parse(parentId);
            }

            AuthorId = int.Parse(model.AuthorId);
            Text = model.Text;
            Timestamp = model.Timestamp;
            IsDeleted = model.IsDeleted;
            LocatorSection = model.Locator.Section;
            LocatorElement = model.Locator.Element;
            LocatorAdditional = model.Locator.Additional;
        }

        protected CommentEntity()
        {
        }

        public int Id { get; set; }

        public int ParentId { get; set; }

        public int AuthorId { get; set; }

        public string Text { get; set; }

        public DateTime Timestamp { get; set; }

        public bool IsDeleted { get; set; }

        public string LocatorSection { get; set; }

        public string LocatorElement { get; set; }

        public string LocatorAdditional { get; set; }

        public Comment ToModel()
        {
            return new Comment
            {
                Id = Id == 0 ? string.Empty : Id.ToString(),
                AuthorId = AuthorId.ToString(),
                Text = Text,
                Timestamp = Timestamp,
                IsDeleted = IsDeleted,
                Locator = new Locator
                {
                    Section = LocatorSection,
                    Element = LocatorElement,
                    Additional = LocatorAdditional
                }
            };
        }
    }
}
