using http_practice.Dto;
using http_practice.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace http_practice
{
    internal class Program
    {
        private static UserService _userService;
        private static PostService _postService;

        static async Task Main(string[] args)
        {
            _userService = new UserService();
            _postService = new PostService();

            var users = await _userService.GetAllAsync();
            var posts = await _postService.GetAllAsync();

            var newPost = new PostDto
            {
                Title = "New Post Title",
                Content = "New Post Content"
            };

            var response = _postService.CreateAsync(newPost);
            Console.WriteLine(response.Result);

            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}");
                Console.WriteLine($"Username: {user.Username}");
                Console.WriteLine($"Email: {user.Email}");
            }

            foreach (var post in posts)
            {
                Console.WriteLine($"ID: {post.Id}");
                Console.WriteLine($"Title: {post.Title}");
            }
        }
    }
}
