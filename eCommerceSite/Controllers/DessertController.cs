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

        public async Task<IActionResult> Index()
        {
            // Get all desserts from DB
            //List<Dessert> desserts = await _context.Desserts.ToListAsync();
            List<Dessert> desserts = await (from dessert in _context.Desserts
                                           select dessert).ToListAsync();
            // Show them on the page
            return View(desserts);
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
    }
}