using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        // 建構式，初始化 ApplicationDbContext 物件
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // 覆寫 Dispose()，讓 ApplicationDbContext 物件可以釋放資源
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {
            // 原本 hard code 客戶資料
            //var customers = GetCustomers();

            // 改成從 ApplicationDbContext 取得客戶資料
            var customers = _context.Customers.Include(c => c.MembershipType);

            return View(customers);
        }

        // GET: Customers/Details
        [Route("customers/details/{Id:regex(\\d+)}")]
        public ActionResult Details(int Id)
        {
            // 原本 hard code 客戶資料
            //var customer = GetCustomers().SingleOrDefault(c => c.Id == Id);

            // 改成從 ApplicationDbContext 取得客戶資料
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == Id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        // 原本 hard code 客戶資料
        //private IEnumerable<Customer> GetCustomers()
        //{
        //    return new List<Customer>()
        //    {
        //        new Customer { Id = 1, Name = "John Smith" },
        //        new Customer { Id = 2, Name = "Mary Williams" }
        //    };
        //}
    }
}