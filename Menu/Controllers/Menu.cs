﻿using Microsoft.AspNetCore.Mvc;
using Menu.Models;
using Menu.Data;
using Microsoft.EntityFrameworkCore;

namespace Menu.Controllers
{
    public class Menu : Controller
    {
        private readonly MenuContext _context;

        public Menu(MenuContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View( await _context.Dishes.ToListAsync());
        }

        public async Task<ActionResult> Details (int? id)
        {
            var dish = await _context.Dishes
                .Include(di  => di.DishIngredients)
                .ThenInclude(i => i.Ingredient)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (dish == null)
            {
                return NotFound();
            }
            return View(dish);
        }
    }
}