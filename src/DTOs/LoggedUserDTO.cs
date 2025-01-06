using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace image_api_netcore.src.DTOs
{
    public class LoggedUserDTO
    {
        public required string email { get; set; } = null!;

        public required string token { get; set; } = null!;
    }
}