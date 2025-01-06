using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace image_api_netcore.src.Models
{
    public class User
    {
        public int id { get; set; }

        public string email { get; set; } = null!;

        public string password { get; set; } = null!;

    }
}