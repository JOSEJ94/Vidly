using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Vidly.DTO;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        //@Access:    GET  /api/Customers
        public IEnumerable<CustomerDTO> GetCustomers()
        {
            return _context.Customers.ToList().Select(Mapper.Map<Customer,CustomerDTO>);
        }

        //@Access:    GET  /api/Customers/:id
        public CustomerDTO GetCustomer(int id)
        {
            if (id == 0)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            Customer customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return Mapper.Map<Customer,CustomerDTO>(customer);
        }

        //@Access:    POST  /api/Customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDTO customer)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            Customer customerForDB = Mapper.Map<CustomerDTO, Customer>(customer);
            _context.Customers.Add(customerForDB);
            _context.SaveChanges();
            customer.Id = customerForDB.Id;
            return Created(new Uri(Request.RequestUri+"/"+customer.Id), customer);
        }

        //@Access:    PUT  /api/Customers
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDTO customer)
        {
            if (id == 0)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            Customer customerInDB = _context.Customers.SingleOrDefault(c => c.Id == id);
            if(customerInDB==null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            Mapper.Map(customer, customerInDB);
            _context.SaveChanges();
        }

        //@Access:    PUT  /api/Customers/:id
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            if (id == 0)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            Customer customerInDB = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Customers.Remove(customerInDB);
            _context.SaveChanges();
        }
    }
}
