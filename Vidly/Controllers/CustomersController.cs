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
            return View();
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
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            // 指定 View 為 CustomerForm.cshtml，否則會因為找不到 Edit.cshtml 而出錯
            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            // Validate the form first
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                // 更新前要先撈出 DB 中的狀態
                var customerInDB = _context.Customers.Single(c => c.Id == customer.Id);

                // 依欄位更新
                customerInDB.Name = customer.Name;
                customerInDB.Birthday = customer.Birthday;
                customerInDB.MembershipTypeId = customer.MembershipTypeId;
                customerInDB.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

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