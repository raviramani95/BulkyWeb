using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Bulky.DataAccess.Repository.IRepository;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unityOfWork;
        public CategoryController(IUnitOfWork db)
        {
            _unityOfWork = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategories = _unityOfWork.Category.GetAll();
            return View(objCategories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Category name cannot exactly match Display order.");
            }
            if (ModelState.IsValid)
            {
                _unityOfWork.Category.Add(category);
                _unityOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryDB = _unityOfWork.Category.Get(u => u.Id == id);
            if (categoryDB == null)
            {
                return NotFound();
            }
            return View(categoryDB);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _unityOfWork.Category.Update(category);
                _unityOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryDB = _unityOfWork.Category.Get(u => u.Id == id);
            if (categoryDB == null)
            {
                return NotFound();
            }
            return View(categoryDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? categoryDB = _unityOfWork.Category.Get(u => u.Id == id);
            if (categoryDB == null)
            {
                return NotFound();
            }
            _unityOfWork.Category.Remove(categoryDB);
            _unityOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
