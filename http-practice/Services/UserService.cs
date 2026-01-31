using http_practice.Dto;
using http_practice.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace http_practice.Services
{
    internal class UserService
    {
        private readonly HttpClient _http;

        public UserService()
        {
            _http = ApiClient.Instance;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            try
            {
                return await _http.GetFromJsonAsync<List<UserDto>>("/api/users");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error HTTP request: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error get all users: {ex.Message}");
                throw;
            }
        }
    }
}
