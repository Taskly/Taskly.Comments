using System.Collections.Generic;

namespace Taskly.Comments.WebApi.Dto
{
    public class CommentsListDto
    {
        public LocatorDto Locator { get; set; }

        public List<CommentDto> Comments { get; set; }
    }
}
