using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context; 

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _context.Dispose();
        }

        // POST: Customers/New
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel { MembershipTypes = _context.MembershipTypes.ToList(), Customer = customer };
                return View("CustomerForm", viewModel);
            }
            if(customer.Id==0)// Create         
                _context.Customers.Add(customer);        
            else //update
            {
                var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == customer.Id);
                if (customerInDb == null)
                    return HttpNotFound();
                customerInDb.Name = customer.Name;
                customerInDb.Birthday = customer.Birthday;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSuscribedToNewsletter = customer.IsSuscribedToNewsletter;

            }
            _context.SaveChanges();
            return RedirectToAction("Index","Customers");
        }

        // GET: Customers/New
        public ActionResult New()
        {
            CustomerFormViewModel viewModel = new CustomerFormViewModel { MembershipTypes = _context.MembershipTypes.ToList(), Customer = new Customer() };
            return View("CustomerForm",viewModel);
        }

        // GET: Customers/New
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
            var viewModel = new CustomerFormViewModel { MembershipTypes = _context.MembershipTypes.ToList(), Customer = customer };
            return View("CustomerForm", viewModel);
        }

        // GET: Customers
        public ActionResult Index()
        {
            CustomersViewModel CustomersViewModel = new CustomersViewModel { Customers = _context.Customers.Include(customer => customer.MembershipType).ToList() };
            return View(CustomersViewModel);
        }

        // GET: Customers/Detail/:id
        public ActionResult Detail(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
            return View(customer);
        }
    }
}