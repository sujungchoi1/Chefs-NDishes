using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChefAndDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefAndDishes.Controllers
{
    public class HomeController : Controller
    {

        private ChefDishContext dbContext;
        public HomeController(ChefDishContext context)
        {
            dbContext = context;
        }

        // View Pages

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Chef> AllChefs = dbContext.Chefs
                .Include(c => c.CreatedDishes)
                .OrderByDescending(c => c.CreatedAt)
                .ToList();
            // ViewBag.AllChefs = AllChefs;
            return View("Index", AllChefs);
        }


        [HttpGet("dishes")]
        public IActionResult DishIndex()
        {
            List<Dish> AllDishes = dbContext.Dishes
                .Include(chef => chef.Creator)
                .OrderBy(d => d.Creator.FirstName)
                .ToList();
            // ViewBag.AllDishes = AllDishes;
            return View("DishIndex", AllDishes);
        }

        // Form

        [HttpGet("new")]
        public IActionResult NewChef()
        {
            return View("NewChef");
        }
        [HttpPost("create-chef")]
        public IActionResult CreateChef(Chef newChef)
        {
            if (ModelState.IsValid)
            {
                dbContext.Chefs.Add(newChef);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("NewChef");
        }

        [HttpGet("dishes/new")]
        public IActionResult NewDish()
        {
            ViewBag.ChefInfo = dbContext.Chefs.ToList();
            return View("NewDish");
        }
        [HttpPost("create-dish")]
        public IActionResult CreateDish(Dish newDish)
        {
            if (ModelState.IsValid)
            {
                dbContext.Dishes.Add(newDish);
                dbContext.SaveChanges();
                return RedirectToAction("DishIndex");
            }
            // *** can't use RedirectToAction for Model State validation (only use View)
            ViewBag.ChefInfo = dbContext.Chefs.ToList();
            return View("NewDish");
        }

        [HttpGet("delete/{chefId}")]
        public IActionResult Delete(int chefId)
        {
            Chef HasToRemove = dbContext.Chefs.FirstOrDefault(c => c.ChefId == chefId);
            dbContext.Chefs.Remove(HasToRemove);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}
