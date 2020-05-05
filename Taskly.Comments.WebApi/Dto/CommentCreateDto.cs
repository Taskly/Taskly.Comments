namespace Taskly.Comments.WebApi.Dto
{
    public class CommentCreateDto
    {
        public string ParentId { get; set; }

        public string Locator { get; set; }

        public string AuthorId { get; set; }

        public string Text { get; set; }
    }
}
