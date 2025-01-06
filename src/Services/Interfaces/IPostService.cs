using image_api_netcore.src.Models;
using image_api_netcore.src.DTOs;

namespace image_api_netcore.src.Services.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPosts();

        Task<Post> CreatePost(CreatePost newPost);
    }
}