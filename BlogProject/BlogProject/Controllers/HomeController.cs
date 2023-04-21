using BlogProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BlogProject.Controllers
{
    public class HomeController : Controller
    {
      
        private readonly ApplicationDbContext _post;

        public HomeController(ApplicationDbContext post)
        {
            _post = post;
        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<Post> objList = await _post.Posts!.Take(4).ToListAsync();
           
            return View(objList);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}