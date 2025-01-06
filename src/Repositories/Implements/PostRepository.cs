using image_api_netcore.src.Data;
using image_api_netcore.src.Models;
using image_api_netcore.src.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace image_api_netcore.src.Repositories.Implements
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDBContext _context;

        public PostRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Post> CreatePost(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await _context.Posts.ToListAsync();
            return posts;
        }
    }
}