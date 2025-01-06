using image_api_netcore.src.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using image_api_netcore.src.DTOs;
using image_api_netcore.src.Models;
using Microsoft.AspNetCore.Authorization;

namespace image_api_netcore.src.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts(){
            try {  
                var result = await _postService.GetPosts();
                return Ok(result);

            } catch (Exception e) {
                return Unauthorized(e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost([FromForm] CreatePost newPost){
            try {  
                var result = await _postService.CreatePost(newPost);
                return Ok(result);

            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }
    }
}