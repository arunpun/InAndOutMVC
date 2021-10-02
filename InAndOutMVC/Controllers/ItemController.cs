using InAndOutMVC.Data;
using InAndOutMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOutMVC.Controllers
{
    public class ItemController : Controller
    {
        //Use dependency injection to get data from the database using ApplicationDbContext
        private readonly ApplicationDbContext _db;

        //Constructor to get database context
        public ItemController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            //Get all the items from the database
            IEnumerable<Item> objList = _db.Items;

            return View(objList);
        }
    }
}
