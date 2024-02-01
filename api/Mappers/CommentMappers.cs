using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDTO ToCommentDto(this Comment comment)
        {
            return new CommentDTO()
            {
                Id = comment.Id,
                Titel = comment.Titel,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId,
                CreatedBy = comment.AppUser.UserName

            };
        }

        public static Comment ToComment(this CommentRequest comment, int stockId)
        {
            return new Comment()
            {
                Titel = comment.Titel,
                Content = comment.Content,
                StockId = stockId
            };
        }
    }
}