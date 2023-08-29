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

                LogUserIn(newMember.Email);

                // Redirect to home page
                return RedirectToAction("Index", "Home");
            }

            return View(regModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                // Check DB for credentials
                Member? m = (from member in _context.Members
                           where member.Email == loginModel.Email &&
                                 member.Password == loginModel.Password
                           select member).SingleOrDefault();

                // If exists, send to hompage
                if (m != null)
                {
                    LogUserIn(loginModel.Email);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Credentials not found!");
            }
            // If no record matches, display error or model state is invalid
            return View(loginModel);
        }

        private void LogUserIn(string email)
        {
            HttpContext.Session.SetString("Email", email);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
