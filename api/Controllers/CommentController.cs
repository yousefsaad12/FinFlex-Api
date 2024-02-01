using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Comment;
using api.Extenstions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepo _commentRepo;
        private readonly IStockRepo _stockRepo;
        private readonly UserManager<AppUser> _userManger;

        public CommentController(ICommentRepo commentRepo, IStockRepo stockRepo, UserManager<AppUser> userManger)
       {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManger = userManger;
        }

       [HttpGet]
       public async Task<IActionResult> GetAllComments()
       { 
          if(!ModelState.IsValid)
            return BadRequest(ModelState);

          var comments = await _commentRepo.GetComments();

          var commentsDto = comments.Select(c => c.ToCommentDto());

          return Ok(commentsDto);
       }

       [HttpGet("{commentId:int}")]
       public async Task<IActionResult> GetComment(int commentId)
       {
          if(!ModelState.IsValid)
            return BadRequest(ModelState);

          var comment = await _commentRepo.GetComment(commentId);

          if(comment == null)
            return NotFound();

          return Ok(comment.ToCommentDto());
       }

        [HttpPost("{stockId:int}")]
       public async Task<IActionResult> CreateComment(int stockId, [FromBody] CommentRequest commentRequest)
       {
          if(!ModelState.IsValid)
                return BadRequest(ModelState);

          if(! await _stockRepo.StockExist(stockId))
                return BadRequest("Stock does not exist");

          var userName = User.GetUserName();
          var user = await _userManger.FindByNameAsync(userName);
          
          var comment = commentRequest.ToComment(stockId);
          comment.AppUserId = user.Id;

          await _commentRepo.CreateComment(comment);

          return CreatedAtAction(nameof(CreateComment), new{ id = comment},comment.ToCommentDto());
       }

        [HttpPut("{commentId:int}")]

       public async Task<IActionResult> UpdateComment(int commentId, [FromBody] UpdateCommentReq updateReq)
       {    
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var comment = await _commentRepo.UpdateComment(commentId, updateReq);

            if(comment == null)
                return NotFound();
                
            return Ok(comment);
       }

       [HttpDelete("{commentId:int}")]

       public async Task<IActionResult> DeleteComment(int commentId)
       {    
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var comment = await _commentRepo.DeleteComment(commentId);

            if(comment == null)
                return NotFound();

            return Ok(comment);
       }
    }
}