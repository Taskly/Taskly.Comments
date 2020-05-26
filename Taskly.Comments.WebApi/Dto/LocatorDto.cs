using System.ComponentModel.DataAnnotations;

namespace Taskly.Comments.WebApi.Dto
{
    public class LocatorDto
    {
        [Required]
        public string Section { get; set; }

        public string Subsection { get; set; }

        public string Element { get; set; }
    }
}
