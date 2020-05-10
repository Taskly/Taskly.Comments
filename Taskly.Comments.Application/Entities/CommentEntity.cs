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
            LocatorSection = model.Locator.Section;
            LocatorSubsection = model.Locator.Subsection;
            LocatorElement = model.Locator.Element;
        }

        protected CommentEntity()
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

        public Comment ToModel()
        {
            return new Comment
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
                }
            };
        }
    }
}
