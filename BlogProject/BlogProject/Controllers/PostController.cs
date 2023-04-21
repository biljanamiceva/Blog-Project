using BlogProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _post;

        public PostController(ApplicationDbContext post)
        {
            _post = post;
        }


        public async Task<IActionResult> Index(string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentFilter"] = searchString;
            if (_post.Posts == null)
            {
                return Problem("No products!");
            }

            var posts = from p in _post.Posts
                         select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(s => s.PostTitle!.Contains(searchString));
            }

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            int pageSize = 6;
            return View(await PaginatedList<Post>.CreateAsync(posts.OrderByDescending(p => p.PostDate).Include(e => e.Category).AsNoTracking(), pageNumber ?? 1, pageSize));
        

           
        
        }
      

        //GET
        public async Task<IActionResult> Create()
        {
            ViewData["Category"] = await _post.Categories!.ToListAsync();

            return View();
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post obj, int categoryId)
        {
            if (ModelState.IsValid)
            {
                Category? category = await _post.Categories!.FindAsync(categoryId);

                if (category == null)
                {
                    return NotFound();
                }
                obj.Category = category;
                _post.Add(obj);
                await _post.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = await _post.Posts!.Include(e => e.Category).FirstOrDefaultAsync(f => f.PostId == id);

            if (post == null)
            {
                return NotFound();
            }

            ViewData["Category"] = await _post.Categories!.ToListAsync();
           
            return View(post);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("PostId", "PostTitle", "PostDescription", "PostAuthor", "PostDate", "PostImg", "CurrentCategoryId")] Post obj)
        {
            if (ModelState.IsValid)
            {
             
                _post.Update(obj);
                await _post.SaveChangesAsync();             
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            var obj = await _post.Posts!.FirstOrDefaultAsync(x => x.PostId == id);
            if (obj != null)
            {
                _post.Posts!.Remove(obj);
                await _post.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _post.Posts == null)
            {
                return NotFound();
            }
            var postFromDb = await _post.Posts!.Include(e => e.Category).FirstOrDefaultAsync(f => f.PostId == id);


            //var postFromDb = await _post.Posts.FindAsync(id);
            if (postFromDb == null)
            {
                return NotFound();
            }

            return View(postFromDb);
        }
    }
}
