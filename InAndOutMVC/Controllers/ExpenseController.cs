using InAndOutMVC.Data;
using InAndOutMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOutMVC.Controllers
{
    public class ExpenseController : Controller
    {
        //Use dependency injection to get data from the database using ApplicationDbContext
        private readonly ApplicationDbContext _db;

        //Constructor to get database context
        public ExpenseController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            //Get all the items from the database
            IEnumerable<Expense> objList = _db.Expenses;

            return View(objList);
        }

        public IActionResult Create()
        {
            return View();
        }

        //Create POST action
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Expense obj)
        {
            //Server side validation
            if (ModelState.IsValid)
            {
                //Make entry to the database and save changes
                _db.Expenses.Add(obj);
                _db.SaveChanges();

                //Redirect user to index page
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }
        }

        //Update GET action
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.Expenses.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //Create POST action
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(Expense obj)
        {
            //Server side validation
            if (ModelState.IsValid)
            {
                //Make entry to the database and save changes
                _db.Expenses.Update(obj);
                _db.SaveChanges();

                //Redirect user to index page
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }
        }

        //Delete GET action
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            
            var obj = _db.Expenses.Find(id);
            
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //Delete POST action
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Expenses.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            //Delete entry and save changes
            _db.Expenses.Remove(obj);
            _db.SaveChanges();

            //Redirect user to index page
            return RedirectToAction("Index");
        }
    }
}
