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
        private static CommentService _commentService;

        static async Task Main(string[] args)
        {
            _userService = new UserService();
            _postService = new PostService();
            _commentService = new CommentService();

            var post = _postService.GetByIdAsync(4);
            Console.WriteLine(post);

            //var users = await _userService.GetAllAsync();
            //var posts = await _postService.GetAllAsync();
            //var comments = await _commentService.GetAllAsync();

            //var newPost = new PostDto
            //{
            //    Title = "Title 123",
            //    Content = "New Post Content"
            //};

            //var newComment = new CommentDto
            //{
            //    Content = "New comment 1",
            //    UserId = 4,
            //    PostId = 1,
            //};

            //var response = _postService.CreateAsync(newPost);
            //Console.WriteLine(response.Result);

            //var response = _commentService.CreateAsync(newComment);
            //Console.WriteLine(response.Result);

            //foreach (var user in users)
            //{
            //    Console.WriteLine($"ID: {user.Id}");
            //    Console.WriteLine($"Username: {user.Username}");
            //    Console.WriteLine($"Email: {user.Email}");
            //}

            //foreach (var post in posts)
            //{
            //    Console.WriteLine($"ID: {post.Id}");
            //    Console.WriteLine($"Title: {post.Title}");
            //}

            //foreach (var comment in comments)
            //{
            //    Console.WriteLine($"ID: {comment.Id}");
            //    Console.WriteLine($"Title: {comment.Content}");
            //    Console.WriteLine($"User ID: {comment.UserId}");
            //    Console.WriteLine($"Post ID: {comment.PostId}");
            //}
        }
    }
}
