using System.Collections.Generic;

//namespace EntityFrameworkCore.Models
namespace _20202101_EFCore21.Models
{
public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; } = new List<Post>();
    }
}