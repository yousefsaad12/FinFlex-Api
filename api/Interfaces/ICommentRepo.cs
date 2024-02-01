using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Comment;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepo
    {
        Task<List<Comment>> GetComments();
        Task<Comment?> GetComment(int commentId);
        Task<Comment> CreateComment(Comment comment);
        Task<Comment?> UpdateComment(int commentId,UpdateCommentReq commentReq);
        Task<Comment?> DeleteComment(int commentId);
        
    }
}