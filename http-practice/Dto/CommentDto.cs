using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace http_practice.Dto
{
    internal class CommentDto
    {
        public int Id { get; set; } 
        public int UserId { get; set; }
        public int PublicId { get; set; }
        public string Content { get; set; }

    }
}
