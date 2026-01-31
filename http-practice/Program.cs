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

        static async Task Main(string[] args)
        {
            _userService = new UserService();

            var users = await _userService.GetAllAsync();

            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}");
                Console.WriteLine($"Username: {user.Username}");
                Console.WriteLine($"Email: {user.Email}");
            }
        }
    }
}
