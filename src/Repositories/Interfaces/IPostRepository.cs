using image_api_netcore.src.Models;

namespace image_api_netcore.src.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPosts();

        Task<Post> CreatePost(Post post);
    }
}