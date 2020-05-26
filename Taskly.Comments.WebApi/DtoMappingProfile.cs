using AutoMapper;
using Taskly.Comments.Model;
using Taskly.Comments.WebApi.Dto;

namespace Taskly.Comments.WebApi
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<Locator, LocatorDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<DeletedComment, DeletedCommentDto>();
        }
    }
}
