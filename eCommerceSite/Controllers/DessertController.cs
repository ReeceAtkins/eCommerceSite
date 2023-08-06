using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceSite.Controllers
{
    public class DessertsController : Controller
    {
        private readonly DessertContext _context;

        public DessertsController(DessertContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Dessert dessert)
        {
            if (ModelState.IsValid)
            {
                _context.Desserts.Add(dessert);     // prepares
                await _context.SaveChangesAsync(); // executes pending inserts

                ViewData["Message"] = $"{dessert.Name} was added successfully";
                
                return View();
            }

            return View(dessert);
        }
    }
}