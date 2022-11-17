using BukkyBooksApp.Data;
using BukkyBooksApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BukkyBooksApp.Controllers
{
    public class CategoryController1 : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController1(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.categories;
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Oreder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
           
            return View(obj);
           
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.ID == id);
           // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.ID == id);
           if(categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Oreder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Updated successfully";
                return RedirectToAction("Index");
            }

            return View(obj);

        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.ID == id);
            // var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.ID == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //POST
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
           
                _db.categories.Remove(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
