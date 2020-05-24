using Taskly.Comments.Model.Exceptions;

namespace Taskly.Comments.Model
{
    public class Locator
    {
        public Locator(string section, string subsection = null, string element = null)
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new InvalidArgumentDomainException("Locator section required.");
            }

            Section = section;
            Subsection = string.IsNullOrEmpty(subsection) ? null : subsection;
            Element = string.IsNullOrEmpty(element) ? null : element;
        }

        protected Locator()
        {
        }

        public string Section { get; private set; }

        public string Subsection { get; private set; }

        public string Element { get; private set; }
    }
}
