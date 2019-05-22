using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        // 建構式，初始化 ApplicationDbContext 物件
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // 覆寫 Dispose()，讓 ApplicationDbContext 物件可以釋放資源
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies
        public ActionResult Index()
        {
            // 原本 hard code 客戶資料
            //var movies = GetMovies();
            var movies = _context.Movies.Include(m => m.Genre);

            return View(movies);
        }

        // GET: Movies/Details
        [Route("movies/details/{Id:regex(\\d+)}")]
        public ActionResult Details(int Id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == Id);

            return View(movie);
        }

        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek" };
            var customers = new List<Customer>()
            {
                new Customer { Name = "Customer 1" },
                new Customer { Name = "Customer 2" }
            };

            // 建立 ViewModel 實體
            var viewModel = new RandomMovieViewModel()
            {
                Movie = movie,
                Customers = customers
            };

            // 改為回傳 ViewModel
            return View(viewModel);
        }

        // 原本 hard code 客戶資料
        //private IEnumerable<Movie> GetMovies()
        //{
        //    return new List<Movie>()
        //    {
        //        new Movie { Name = "Shrek" },
        //        new Movie { Name = "Wall-e" }
        //    };
        //}
    }
}