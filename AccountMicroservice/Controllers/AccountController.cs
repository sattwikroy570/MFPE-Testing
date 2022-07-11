using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AccountMicroservice.Models;
using AccountMicroservice.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IAccountRep db;

        readonly log4net.ILog _log4net;

        public AccountController(IAccountRep _db)
        {
            _log4net = log4net.LogManager.GetLogger(typeof(AccountController));
            db = _db;
        }
        // GET: api/<AccountController>
        // GET api/<AccountController>/5
        [HttpGet]
        [Route("getCustomerAccounts/{id}")]
        [Authorize(Roles = "Customer")]
        public IActionResult getCustomerAccounts(string id)
        {
            try
            {
                var ob = db.getCustomerAccounts(id);
                if (ob == null)
                {
                    return NotFound();
                }
                return Ok(ob);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST api/<AccountController>
        [HttpPost]
        [Route("createAccount")]
        [Authorize(Roles = "Employee")]
        public IActionResult createAccount([FromBody] CustomerId CId)
        {
            try
            {
                var ob = db.createAccount(CId.customerId);
                if (ob == null)
                {
                    return NotFound();
                }
                return Ok(ob);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("getAccount/{id}")]
        [Authorize(Roles = "Customer")]
        public IActionResult getAccount(int id)
        {
            try
            {
                var ob = db.getAccount(id);
                if (ob == null)
                {
                    return NotFound();
                }
                return Ok(ob);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        [HttpPost]
        [Route("deposit")]
        [Authorize(Roles = "Customer")]
        public IActionResult deposit([FromBody] Transaction t)
        {


            _log4net.Info("Deposit");
            var StringToken = Request.Headers["Authorization"];
            try
            {
                var ob = db.deposit(t);
                if (ob == null)
                {
                    return NotFound();
                }
                return Ok(ob);
            }
            catch (Exception e)
            {

                _log4net.Info(e);
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("withdraw")]
        [Authorize(Roles = "Customer")]
        public IActionResult withdraw([FromBody] Transaction t)
        {
            try
            {
                var ob = db.withdraw(t);
                if (ob == null)
                {
                    return NotFound();
                }
                return Ok(ob);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("getAccountStatement/{AccountId}/{from_date}/{to_date}")]
        [Authorize(Roles = "Customer")]
        public IActionResult getAccountStatement(int AccountId, string from_date, string to_date)
        {
            try
            {
                var ob = db.getAccountStatement(AccountId, from_date, to_date);
                if (ob == null)
                {
                    return NotFound();
                }
                return Ok(ob);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
