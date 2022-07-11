using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerMicroservice.Models;
using CustomerMicroservice.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CustomerMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Employee")]
    public class CustomerController : ControllerBase
    {
        ICustomerRepository db;
        public CustomerController(ICustomerRepository _db)
        {
            db = _db;
        }
        [HttpGet]
        [Route("getCustomerDetails/{id}")]
        public IActionResult getCustomerDetails(string id)
        {
            try
            {
                var obj = db.getCustomerDetails(id);
                if(obj == null)
                {
                    return NotFound();
                }
                return Ok(obj);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("createCustomer")]
        public IActionResult createCustomer([FromBody]CustomerDetails customer)
        {
            TokenInfo.StringToken = Request.Headers["Authorization"];
            try
            {
                var obj = db.createCustomer(customer);
                if(obj==null)
                {
                    return NotFound();
                }
                return Ok(obj);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }
}
