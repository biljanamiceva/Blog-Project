using BlogProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _category;

        public CategoryController(ApplicationDbContext category)
        {
            _category = category;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> objCategoryList = await  _category.Categories!.OrderBy(e => e.CategoryId).ToListAsync();

            return View(objCategoryList);
        }

        public async Task<IActionResult> GetCategoryById(int? id)
        {
            var obj = await _category.Categories!.Include(e => e.Posts).FirstOrDefaultAsync(x => x.CategoryId == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _category.Categories!.Add(obj);
                await _category!.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        //GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = await _category.Categories!.FindAsync(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category obj)
        {          
            if (ModelState.IsValid) 
            {
                _category.Categories!.Update(obj);
                await _category.SaveChangesAsync();             
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            var obj = await _category.Categories!.FindAsync(id);
            if (obj == null)
            {
                return NotFound();
            }

            _category.Categories.Remove(obj);
            await _category.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
