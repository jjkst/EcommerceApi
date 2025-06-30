using Microsoft.AspNetCore.Mvc;
using EcommerceApi.Context;
using EcommerceApi.Models;
using System.Net.Mail;
using System.Net;

namespace EcommerceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController(ApplicationDbContext context, ILogger<EmailController> emailController) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> SendEmail(Contact contact)
        {
            var emailSent = await SendEmailNotification(contact);
            if (!emailSent)
                return StatusCode(500, "Email notification failed.");
            return Ok("Email notification sent successfully.");
        }

        private static async Task<bool> SendEmailNotification(Contact contact)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("karthi.jayaraman@gmail.com", "potbelly"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("karthi.jayaraman@gmail.com"),
                    Subject = "Question from Ecommerce Application",
                    Body = $"A new question has been submitted:\n\n" +
                           $"Name: {contact.FirstName} {contact.LastName}\n" +
                           $"Email: {contact.Email}\n" +
                           $"Phone: {contact.PhoneNumber}\n" +
                           $"Question: {contact.Questions}",
                    IsBodyHtml = false,
                };

                mailMessage.To.Add("jjkst.dev@gmail.com");

                await Task.Run(() => smtpClient.Send(mailMessage));        
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }
    }
}