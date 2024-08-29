using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMVC06_RemoteValidation.Models;

namespace MyMVC06_RemoteValidation.Controllers
{
    public class RemoteValidationController : Controller
    {
        private readonly EFCoreDBContext _context;
        private readonly GenerateSuggestions _generateSuggestions;

        public RemoteValidationController(EFCoreDBContext context, GenerateSuggestions generateSuggestions)
        {
            _context = context;
            _generateSuggestions = generateSuggestions;
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsEmailAvailable(string email)
        {
            // Use AnyAsync for asynchronous existence check
            if (!await _context.Users.AnyAsync(u => u.Email == email))
                return Json(true);

            //The second parameter will decide the number of unique suggestions to be generated
            var suggestedEmails = await _generateSuggestions.GenerateUniqueEmailsAsync(email, 3);
            return Json($"Thông báo từ Controller : Email is already in use. Try anyone of these: {suggestedEmails}");
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsUsernameAvailable(string username)
        {
            // Use AnyAsync for asynchronous existence check
            if (!await _context.Users.AnyAsync(u => u.UserName == username))
                return Json(true);

            //The second parameter will decide the number of unique suggestions to be generated
            var suggestedUsernames = await _generateSuggestions.GenerateUniqueUsernamesAsync(username, 3);
            return Json($"Thông báo từ Controller : Username is already taken. Try anyone of these:  {suggestedUsernames}");
        }
    } 
}
