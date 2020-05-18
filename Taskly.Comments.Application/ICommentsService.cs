﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Taskly.Comments.Model;

namespace Taskly.Comments.Application
{
    public interface ICommentsService
    {
        Task<List<Comment>> GetCommentsByLocator(Locator locator);

        Task<List<Comment>> GetCommentsByUser(string userId);

        Task<List<DeletedComment>> GetDeletedCommentsByUser(string userId);

        Task<Comment> GetCommentById(string id);

        Task<Comment> AddComment(Comment comment);

        Task<Comment> AddReply(string parentId, Comment comment);

        Task<DeletedComment> MarkAsDeleted(string id);
    }
}
