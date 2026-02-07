using http_practice.Dto;
using http_practice.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

            while (true)
            {
                ShowMainMenu();

                switch (Console.ReadLine().ToLower()) {
                    case "show users":
                        await ShowUsers();
                        break;
                    case "show posts":
                        await ShowPosts();
                        break;
                    case "show comments":
                        await ShowComments();
                        break;
                    case "show post by id":
                        await ShowPostById();
                        break;
                    case "create post":
                        await CreatePost();
                        break;
                    case "edit post":
                        await EditPost();
                        break;
                    case "delete post":
                        await DeletePost();
                        break;
                    case "create comment":
                        await CreateComment();
                        break;

                    case "help":
                        Console.WriteLine("=== Help ===");
                        Console.WriteLine("Commands:");

                        Console.WriteLine("Show actions:");
                        Console.WriteLine("|> Show Users - gets users from server and show them in console");
                        Console.WriteLine("|> Show Posts - gets posts from server and show them in console");
                        Console.WriteLine("|  |> Show Post By Id - gets post from server by id, and shows it in console");
                        Console.WriteLine("|> Show Comments - gets comments from server and show them in console");

                        Console.WriteLine("Create actions:");
                        Console.WriteLine("|> Create Post - creating a new post on server");
                        Console.WriteLine("|  |> Edit Post - edits an existing post on server");
                        Console.WriteLine("|  |> Delete Post - delete an existing post on server");
                        Console.WriteLine("|> Create Comment - creating a new comment for post on server");

                    case "exit":
                        Console.WriteLine("До свидания!");
                        return;

                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void ShowMainMenu()
        {
            Console.WriteLine("=== HTTP Practice API ===");
            Console.WriteLine("Enter \"Help\" for commands list; Enter \"Exit\" to close the program");
        }

        static async Task ShowUsers()
        {
            Console.WriteLine("\n=== ПОЛЬЗОВАТЕЛИ ===");
            try
            {
                var users = await _userService.GetAllAsync();

                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.Id}");
                    Console.WriteLine($"Username: {user.Username}");
                    Console.WriteLine($"Email: {user.Email}");
                    Console.WriteLine("-------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки пользователей: {ex.Message}");
            }
        }

        static async Task ShowPosts()
        {
            Console.WriteLine("\n=== ПОСТЫ ===");
            try
            {
                var posts = await _postService.GetAllAsync();

                foreach (var post in posts)
                {
                    Console.WriteLine($"ID: {post.Id}");
                    Console.WriteLine($"Заголовок: {post.Title}");
                    Console.WriteLine($"Содержание: {post.Content}");
                    Console.WriteLine("-------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки постов: {ex.Message}");
            }
        }

        static async Task ShowComments()
        {
            Console.WriteLine("\n=== КОММЕНТАРИИ ===");
            try
            {
                var comments = await _commentService.GetAllAsync();

                foreach (var comment in comments)
                {
                    Console.WriteLine($"ID: {comment.Id}");
                    Console.WriteLine($"Содержание: {comment.Content}");
                    Console.WriteLine($"User ID: {comment.UserId}");
                    Console.WriteLine($"Post ID: {comment.PostId}");
                    Console.WriteLine("-------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки комментариев: {ex.Message}");
            }
        }

        static async Task ShowPostById()
        {
            Console.Write("Введите ID поста: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    var post = await _postService.GetByIdAsync(id);
                    Console.WriteLine("\n=== ПОСТ ===");
                    Console.WriteLine($"ID: {post.Id}");
                    Console.WriteLine($"Заголовок: {post.Title}");
                    Console.WriteLine($"Содержание: {post.Content}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: Пост с ID {id} не найден - {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Неверный ID поста");
            }
        }

        static async Task CreatePost()
        {
            Console.WriteLine("\n=== СОЗДАНИЕ ПОСТА ===");
            Console.Write("Заголовок: ");
            string title = Console.ReadLine();

            Console.Write("Содержание: ");
            string content = Console.ReadLine();

            var newPost = new PostDto
            {
                Title = title,
                Content = content
            };

            try
            {
                var response = await _postService.CreateAsync(newPost);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("✅ Пост успешно создан!");
                }
                else
                {
                    Console.WriteLine($"❌ Ошибка создания поста: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static async Task EditPost()
        {
            Console.Write("Введите ID поста для редактирования: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID поста");
                return;
            }

            try
            {
                // Получаем текущий пост для просмотра
                var currentPost = await _postService.GetByIdAsync(id);
                Console.WriteLine("\nТекущие данные поста:");
                Console.WriteLine($"Заголовок: {currentPost.Title}");
                Console.WriteLine($"Содержание: {currentPost.Content}");
            }
            catch (Exception)
            {
                Console.WriteLine($"Пост с ID {id} не найден");
                return;
            }

            Console.WriteLine("\n=== РЕДАКТИРОВАНИЕ ПОСТА ===");
            Console.Write("Новый заголовок (Enter для пропуска): ");
            string title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title)) title = null;

            Console.Write("Новое содержание (Enter для пропуска): ");
            string content = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(content)) content = null;

            var updatedPost = new PostDto
            {
                Title = title,
                Content = content
            };

            try
            {
                var response = await _postService.EditAsync(updatedPost, id);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("✅ Пост успешно обновлен!");
                }
                else
                {
                    Console.WriteLine($"❌ Ошибка обновления: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static async Task DeletePost()
        {
            Console.Write("Введите ID поста для удаления: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID поста");
                return;
            }

            try
            {
                var post = await _postService.GetByIdAsync(id);
                Console.WriteLine("\nПост для удаления:");
                Console.WriteLine($"ID: {post.Id}");
                Console.WriteLine($"Заголовок: {post.Title}");
                Console.WriteLine($"Содержание: {post.Content}");

                Console.Write("Вы уверены? (y/n): ");
                if (Console.ReadLine()?.ToLower() == "y")
                {
                    var response = await _postService.DeleteAsync(id);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("✅ Пост успешно удален!");
                    }
                    else
                    {
                        Console.WriteLine($"❌ Ошибка удаления: {response.StatusCode}");
                    }
                }
                else
                {
                    Console.WriteLine("Удаление отменено");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: Пост с ID {id} не найден - {ex.Message}");
            }
        }

        static async Task CreateComment()
        {
            Console.WriteLine("\n=== СОЗДАНИЕ КОММЕНТАРИЯ ===");
            Console.Write("Содержание: ");
            string content = Console.ReadLine();

            Console.Write("User ID: ");
            if (!int.TryParse(Console.ReadLine(), out int userId))
            {
                Console.WriteLine("Неверный User ID");
                return;
            }

            Console.Write("Post ID: ");
            if (!int.TryParse(Console.ReadLine(), out int postId))
            {
                Console.WriteLine("Неверный Post ID");
                return;
            }

            var newComment = new CommentDto
            {
                Content = content,
                UserId = userId,
                PostId = postId
            };

            try
            {
                var response = await _commentService.CreateAsync(newComment);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("✅ Комментарий успешно создан!");
                }
                else
                {
                    Console.WriteLine($"❌ Ошибка создания комментария: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
