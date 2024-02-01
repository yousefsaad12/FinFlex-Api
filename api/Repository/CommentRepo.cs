using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Comment;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepo : ICommentRepo
    {
        private readonly AppDbContext _appDbContext;

        public CommentRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Comment> CreateComment(Comment comment)
        {
           await _appDbContext.Comments.AddAsync(comment);
           await _appDbContext.SaveChangesAsync();

           return comment;
        }

        public async Task<Comment?> DeleteComment(int commentId)
        {
            var comment = await _appDbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);

            if(comment == null)
                return null;

            _appDbContext.Comments.Remove(comment);
            await _appDbContext.SaveChangesAsync();
            
            return comment;
        }

        public async Task<Comment?> GetComment(int commentId)
        {
            var comment = await _appDbContext.Comments
                                             .Include(c => c.AppUser)
                                             .FirstOrDefaultAsync(c => c.Id == commentId);
            if(comment == null)
                return null;

            return comment;                                 
        }

        public async Task<Comment?> UpdateComment(int commentId,UpdateCommentReq commentReq)
        {
            var comment = await _appDbContext.Comments.FindAsync(commentId);

            if(comment == null)
                return null;

            comment.Titel = commentReq.Titel;
            comment.Content = commentReq.Content;

            await _appDbContext.SaveChangesAsync();
            
            return comment;
        }

        public async Task<List<Comment>> GetComments()
        {
            return await _appDbContext.Comments.Include(a => a.AppUser).ToListAsync();
        }
    }
}