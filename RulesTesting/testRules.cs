using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using RulesMicroservice;
using RulesMicroservice.Models;
using RulesMicroservice.Controllers;
using RulesMicroservice.Repository;
using System;
using System.Collections.Generic;

namespace RulesTesting
{
    public class Tests
    {
        AccountCheck d1 = new AccountCheck { AccountId = 1, Amount = 2000 };
        RuleStatus rul = new RuleStatus();
        ServiceCharge s1 = new ServiceCharge { ServiceChargeBalance = 200 };
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void getServiceCharges_BalanceIsLessthanMinimumBakance_ReturnsServiceCharges()
        {
            var mock = new Mock<IRulesRep>();
            mock.Setup(p => p.getServiceCharges()).Returns(s1);
            RulesController obj = new RulesController(mock.Object);
            var data = obj.getServiceCharges();
            var res = data as OkObjectResult;
            Assert.AreEqual(res.StatusCode, 200);
        }

        [Test]
        public void getServiceCharges_BalanceIsMorethanMinimumBakance_NoServiceChargeapplicable()
        {
            try { 
            var mock = new Mock<IRulesRep>();
            mock.Setup(p => p.getServiceCharges()).Returns(s1);
            RulesController obj = new RulesController(mock.Object);
            var data = obj.getServiceCharges();
            var res = data as BadRequestResult;
            Assert.AreEqual(400, res.StatusCode);
             }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void evaluateMinBalSavings_CorrectBalancePassed_ReturnsOkResult()
        {
            try
            {
                var mock = new Mock<IRulesRep>();
                mock.Setup(p => p.evaluateMinBal(d1)).Returns(rul);
                RulesController obj = new RulesController(mock.Object);
                var data = obj.evaluateMinBal(d1);
                var res = data as OkObjectResult;
                Assert.AreEqual(200, res.StatusCode);
            }
            catch(Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }
        [Test]
        public void evaluateMinBalSavings_IncorrectBalancePassed_ReturnsBadRequestResult()
        {
            try
            {
                var mock = new Mock<IRulesRep>();
                mock.Setup(p => p.evaluateMinBal(d1)).Returns(rul);
                RulesController obj = new RulesController(mock.Object);
                var data = obj.evaluateMinBal(d1);
                var res = data as BadRequestObjectResult;
                Assert.AreEqual(400, res.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }
    }
}