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

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel()
            {
                MembershipTypes = membershipTypes
            };

            // 指定 View 為 CustomerForm.cshtml，否則會因為找不到 Edit.cshtml 而出錯
            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int Id)
        {
            // 找出客戶資料
            var customer = _context.Customers.SingleOrDefault(c => c.Id == Id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            // 指定 View 為 CustomerForm.cshtml，否則會因為找不到 Edit.cshtml 而出錯
            return View("CustomerForm", viewModel);
        }
    }
}