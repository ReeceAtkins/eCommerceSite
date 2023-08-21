using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceSite.Controllers
{
    public class MemberController : Controller
    {
        private readonly DessertContext _context;
        public MemberController(DessertContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel regModel)
        {
            if (ModelState.IsValid)
            {
                // Map RegisterViewModel data to Member object
                Member newMember = new()
                {
                    Email = regModel.Email,
                    Password = regModel.Password,
                };

                _context.Members.Add(newMember);
                await _context.SaveChangesAsync();

                // Redirect to home page
                return RedirectToAction("Index", "Home");
            }

            return View(regModel);
        }
    }
}
