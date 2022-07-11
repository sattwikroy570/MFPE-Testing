using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RulesMicroservice.Models;
using RulesMicroservice.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RulesMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class RulesController : ControllerBase
    {
        IRulesRep db;
        public RulesController(IRulesRep _db)
        {
            db = _db;
        }
        // GET: api/<RulesController>

        [HttpGet]
        [Route("showServiceCharges")]
        public IActionResult getServiceCharges()
        {
            try
            {
                var ob = db.getServiceCharges();
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
        [Route("evaluateMinBal")]
        public IActionResult evaluateMinBal([FromBody] AccountCheck value)
        {
            TokenInfo.StringToken = Request.Headers["Authorization"];
            try
            {
                var obj = db.evaluateMinBal(value);
                if (obj == null)
                {
                    return NotFound();
                }
                return Ok(obj);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
