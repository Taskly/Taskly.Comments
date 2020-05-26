using AutoMapper;
using Taskly.Comments.Application.Entities;
using Taskly.Comments.Model;

namespace Taskly.Comments.Application
{
    public class EntitiesMappingProfile : Profile
    {
        public EntitiesMappingProfile()
        {
            /*CreateMap<PaginatedList<CommentEntity>, PaginatedList<Comment>>();
                .AfterMap((a, b, ctx) => ctx.Mapper.Map<List<CommentEntity>, List<Comment>>(a, b));*/

            CreateMap<Comment, CommentEntity>()
                .ForMember(x => x.RemovalTimestamp, options => options.Ignore())
                .ForMember(x => x.RemovalUserId, options => options.Ignore())
                .ReverseMap();

            CreateMap<DeletedComment, CommentEntity>()
                .ReverseMap();
        }
    }
}
