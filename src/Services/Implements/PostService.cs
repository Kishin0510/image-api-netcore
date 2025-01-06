using image_api_netcore.src.Models;
using image_api_netcore.src.DTOs;
using image_api_netcore.src.Services.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using image_api_netcore.src.Repositories.Interfaces;

namespace image_api_netcore.src.Services.Implements
{
    public class PostService : IPostService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public PostService(Cloudinary cloudinary, IPostRepository postRepository, IUserRepository userRepository)
        {
            _cloudinary = cloudinary;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<Post> CreatePost(CreatePost newPost)
        {
            if (newPost.image.ContentType != "image/jpg" && newPost.image.ContentType != "image/png")
            {
                throw new Exception("El formato del archivo no es válido.");
            }
            if (newPost.image.Length > 5 * 1024 * 1024)
            {
                throw new Exception("El tamaño del archivo es mayor a 5MB.");
            }
            var uploadParams = new ImageUploadParams{
                File = new FileDescription(newPost.image.FileName, newPost.image.OpenReadStream()),
                Folder = "image_api_netcore"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                throw new Exception("Error al subir la imagen.");
            }
            var user = await _userRepository.GetUserByEmail(newPost.userEmail);
            if (user == null)
            {
                throw new Exception("El usuario utilizado para crear el post no existe.");
            }
            var post = new Post
            {
                title = newPost.title,
                creationDate = DateTime.Now,
                imageURL = uploadResult.SecureUrl.AbsoluteUri,
                userEmail = newPost.userEmail,
                userId = user.id
            };

            var createdPost = await _postRepository.CreatePost(post);
            return createdPost;
        }

        public Task<IEnumerable<Post>> GetPosts()
        {
            var posts = _postRepository.GetPosts();
            return posts;
        }
    }
}