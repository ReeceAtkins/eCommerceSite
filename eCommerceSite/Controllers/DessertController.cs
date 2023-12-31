﻿using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Controllers
{
    public class DessertsController : Controller
    {
        private readonly DessertContext _context;

        public DessertsController(DessertContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            const int NumGamesToDisplayPerPage = 3;
            const int PageOffset = 1; // Need a page offset to use current page and figure out, num desserts per page

            int currPage = id ?? 1; // Set currPage to id if it has a value, otherwise use 1

            int totalNumOfProudcts = await _context.Desserts.CountAsync();
            double maxNumPage = Math.Ceiling((double)totalNumOfProudcts / NumGamesToDisplayPerPage);
            int lastPage = Convert.ToInt32(maxNumPage); // Rounding pages up, to next whole page

            // Get all desserts from DB
            //List<Dessert> desserts = await _context.Desserts.ToListAsync();
            List<Dessert> desserts = await (from dessert in _context.Desserts
                                           select dessert)
                                           .Skip(NumGamesToDisplayPerPage * (currPage - PageOffset))
                                           .Take(NumGamesToDisplayPerPage)
                                           .ToListAsync();

            DessertCatalogViewModel catalogModel = new(desserts, lastPage, currPage);
            return View(catalogModel);
        }

        [HttpGet]
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

        public async Task<IActionResult> Edit(int id)
        {
            Dessert? dessertToEdit = await _context.Desserts.FindAsync(id);

            if (dessertToEdit == null)
            {
                return NotFound(); // returns a 404 not found
            }

            return View(dessertToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Dessert dessertModel)
        {
            if (ModelState.IsValid)
            {
                _context.Desserts.Update(dessertModel);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"{dessertModel.Name} was updated successfully!";
                return RedirectToAction("Index");
            }

            return View(dessertModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Dessert? dessertToDelete = await _context.Desserts.FindAsync(id);

            if (dessertToDelete == null)
            {
                return NotFound();
            }

            return View(dessertToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Dessert dessertToDelete = await _context.Desserts.FindAsync(id);

            if (dessertToDelete != null)
            {
                _context.Remove(dessertToDelete);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"{dessertToDelete.Name} was deleted successfully!";
                return RedirectToAction("Index");
            }

            TempData["Message"] = "This game was already deleted";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            Dessert? dessertDetails = await _context.Desserts.FindAsync(id);

            if (dessertDetails == null)
            {
                return NotFound();
            }

            return View(dessertDetails);
        }
    }
}