using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMVC06_RemoteValidation.Models;
using MyMVC06_RemoteValidation.Models.ViewModel;

namespace MyMVC06_RemoteValidation.Controllers
{
    public class UserController : Controller
    {
        private readonly EFCoreDBContext _context;
        private readonly GenerateSuggestions _generateSuggestions;
        public UserController(EFCoreDBContext context, GenerateSuggestions generateSuggestions)
        {
            _context = context;
            _generateSuggestions = generateSuggestions;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    // Proceed with saving the new user or any other business logic
                    User user = new User
                    {
                        Email = model.Email,
                        UserName = model.UserName,
                        Password = model.Password
                    };

                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Successful");
                }
            }

            // Return the view with validation messages if any checks fail
            return View(model);

        }

        public string Successful()
        {
            return "User Added Successfully";
        }

    }
}
