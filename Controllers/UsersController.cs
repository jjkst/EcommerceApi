using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceApi.Context;
using EcommerceApi.Models;

namespace EcommerceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(ApplicationDbContext context, ILogger<UsersController> logger) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger<UsersController> _logger = logger;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                _logger.LogInformation("Fetched {Count} users from the database.", users.Count);
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching users.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    _logger.LogWarning("User with ID {Id} not found.", id);
                    return NotFound($"User with ID {id} not found.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the user with ID {Id}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogWarning("Received a null user object in the request.");
                    return BadRequest("User object is null.");
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Created a new user with ID {Id}.", user.Id);
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new user.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id:int}/role")]
        public async Task<IActionResult> UpdateUserRole(int id, [FromBody] string newRole)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    _logger.LogWarning("User with ID {Id} not found for role update.", id);
                    return NotFound($"User with ID {id} not found.");
                }

                if (string.IsNullOrWhiteSpace(newRole))
                {
                    _logger.LogWarning("Received an invalid role value for user with ID {Id}.", id);
                    return BadRequest("Role value is invalid.");
                }

                // Validate newRole against the UserRole enum
                if (!Enum.TryParse(typeof(UserRole), newRole, true, out var roleObj) ||
                 roleObj is not UserRole validRole)
                {
                    _logger.LogWarning("Received a role value '{Role}' that is not valid for user with ID {Id}.", newRole, id);
                    return BadRequest($"Role value '{newRole}' is not valid.");
                }


                user.Role = validRole;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Updated role for user with ID {Id} to {Role}.", id, newRole);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the role for user with ID {Id}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    _logger.LogWarning("User with ID {Id} not found for deletion.", id);
                    return NotFound($"User with ID {id} not found.");
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Deleted user with ID {Id}.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the user with ID {Id}.", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}