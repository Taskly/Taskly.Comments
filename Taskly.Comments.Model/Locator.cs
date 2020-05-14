using System;

namespace Taskly.Comments.Model
{
    public class Locator
    {
        public Locator(string section, string subsection, string element)
        {
            Section = section;
            Subsection = subsection;
            Element = element;
        }

        public string Section { get; }

        public string Subsection { get; }

        public string Element { get; }
    }
}
