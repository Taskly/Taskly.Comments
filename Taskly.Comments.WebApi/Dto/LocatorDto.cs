using Taskly.Comments.Model;

namespace Taskly.Comments.WebApi.Dto
{
    public class LocatorDto
    {
        public LocatorDto()
        {
        }

        public LocatorDto(Locator model)
        {
            Section = model.Section;
            Subsection = model.Subsection;
            Element = model.Element;
        }

        public string Section { get; set; }

        public string Subsection { get; set; }

        public string Element { get; set; }

        public Locator ToModel()
        {
            return new Locator(Section, Subsection, Element);
        }
    }
}
