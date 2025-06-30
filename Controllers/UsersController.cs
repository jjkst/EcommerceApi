using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceApi.Context;
using EcommerceApi.Models;

namespace EcommerceApi.Controllers
{
    public class UsersController(ApplicationDbContext context, ILogger<UsersController> logger) : BaseController<User, ApplicationDbContext>(context, logger)
    {
        protected override object GetEntityId(User entity) => entity.Id;

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
    }
}