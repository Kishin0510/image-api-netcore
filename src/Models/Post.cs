using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace image_api_netcore.src.Models
{
    public class Post
    {
        public int id { get; set; }

        public string title { get; set; } = null!;

        public DateTime creationDate { get; set; }

        public string imageURL { get; set; } = null!;

        public int userId { get; set; }

        public string userEmail { get; set; } = null!;
    }
}