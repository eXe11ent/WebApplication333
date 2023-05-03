using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication333
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private List<User> _users;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
            _users = new List<User>();
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            _users.Add(user);
            _logger.LogInformation("User {0} created.", user.Username);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _users.Find(u => u.Id == id);
            if (user != null)
            {
                _users.Remove(user);
                _logger.LogInformation("User {0} deleted.", user.Username);
                return Ok();
            }
            else
            {
                _logger.LogInformation("User with id {0} not found.", id);
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditUser(int id, User updatedUser)
        {
            var user = _users.Find(u => u.Id == id);
            if (user != null)
            {
                user.Username = updatedUser.Username;
                user.Email = updatedUser.Email;
                _logger.LogInformation("User {0} updated.", user.Username);
                return Ok();
            }
            else
            {
                _logger.LogInformation("User with id {0} not found.", id);
                return NotFound();
            }
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _users;
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
