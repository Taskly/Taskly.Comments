namespace Taskly.Comments.WebApi.Dto
{
    public class CommentCreateDto
    {
        public string ParentId { get; set; }

        public LocatorDto Locator { get; set; }

        public string AuthorId { get; set; }

        public string Text { get; set; }
    }
}
