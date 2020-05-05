using System.Collections.Generic;

namespace Taskly.Comments.WebApi.Dto
{
    public class CommentsListDto
    {
        public string Locator { get; set; }

        public List<CommentDto> Comments { get; set; }
    }
}
