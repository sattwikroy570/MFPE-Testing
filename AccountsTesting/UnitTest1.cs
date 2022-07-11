using NUnit.Framework;
using AccountMicroservice.Controllers;
using AccountMicroservice.Models;
using AccountMicroservice.Repository;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using AccountMicroservice;

namespace AccountsTesting
{
    public class Tests
    {
        TransactionMsg tm = new TransactionMsg();
        AccountMsg am = new AccountMsg();
        Statement s1 = new Statement { Date = DateTime.Parse("30/10/2019"), Narration = "opening", Withdrawal = 10000, Deposit = 1200, ClosingBalance = 8400 };
        AccountStatement as1 = new AccountStatement();
        Transaction t1 = new Transaction { AccountId = 1, Amount = 200 };
        
        [SetUp]
        public void Setup()
        {
            tm = new TransactionMsg { AccountId = 1, Balance = 1000, Message = "Savings" };
            am = new AccountMsg { AccBal = 1000, AccId = 1 , AccType = "Savings"};
           
           
        }

        [Test]
        public void getcustomerAccounts_ValidOutput_ReturnsOkResult()
        {
            string id = "Harry";
            var mock = new Mock<AccountRep>();
            AccountController a1 = new AccountController(mock.Object);
            var data = a1.getCustomerAccounts(id);
            var res = data as ObjectResult;
            Assert.AreEqual(res.StatusCode, 200);
        }

        [Test]
        public void getcustomerAccounts_InvalidorNullOutput_ReturnBadResult()
        {
            try
            {

                string id = "Harry";
                var mock = new Mock<AccountRep>();
                AccountController a1 = new AccountController(mock.Object);
                var data = a1.getCustomerAccounts(id);
                var res = data as BadRequestResult;
                Assert.AreEqual(res.StatusCode, 400);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void createAccount_ValidInput_OkResult()
        {

            CustomerId id = new CustomerId { customerId = "Harry" };
            var mock = new Mock<AccountRep>();
            AccountController a1 = new AccountController(mock.Object);
            var data = a1.createAccount(id);
            var res = data as ObjectResult;
            Assert.AreEqual(res.StatusCode, 200);
        }

        [Test]
        public void createAccount_InvalidorNullInput_BadRequest()
        {
            try
            {
                CustomerId id = new CustomerId{ customerId = "Harry" };
                var mock = new Mock<AccountRep>();
                AccountController a1 = new AccountController(mock.Object);
                var data = a1.createAccount(id);
                var res = data as BadRequestObjectResult;
                Assert.AreEqual(res.StatusCode, 400);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

        }


        [Test]
        public void getAccounts_ValidOutput_ReturnsOkResult()
        {
            try { 
            int id = 1;
            var mock = new Mock<IAccountRep>();
                mock.Setup(m => m.getAccount(id)).Returns(am);
            AccountController a1 = new AccountController(mock.Object);
            var data = a1.getAccount(id);
            var res = data as ObjectResult;
            Assert.AreEqual(res.StatusCode, 200);
        }
            catch(Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void getCorrectAccount_ValidOutput_OkResult()
        {

            int id = 1;
                var mock = new Mock<IAccountRep>();
                mock.Setup(m => m.getAccount(id)).Returns(am);
                 AccountController a1 = new AccountController(mock.Object);
                var data = a1.getAccount(id);
                var res = data as ObjectResult;
                Assert.AreEqual(res.StatusCode, 200);
            
            
        }

        [Test]
        public void getCorrectAccountStatment_ValidOutput_OkResult()
        {
            try
            {
                int id = 1;
                var mock = new Mock<AccountRep>();

                AccountController a1 = new AccountController(mock.Object);
                var data = a1.getAccountStatement(id, "2019-10-30", "2020-10-30");
                var res = data as ObjectResult;
                Assert.AreEqual(res.StatusCode, 200);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

        }
        [Test]
        public void deposit_CorrectAmount_ReturnsOKResult()
        {
            var mock = new Mock<IAccountRep>();
            mock.Setup(m => m.deposit(t1)).Returns(tm);
            AccountController a1 = new AccountController(mock.Object);
            var data = a1.deposit(t1);
            var res = data as ObjectResult;
            Assert.AreEqual(res.StatusCode, 200);
        }

        [Test]
        public void deposit_InCorrectAmount_ReturnsBadResult()
        {
            try
            {
                var mock = new Mock<IAccountRep>();
                mock.Setup(m => m.deposit(t1)).Returns(tm);
                AccountController a1 = new AccountController(mock.Object);
                var data = a1.deposit(t1);
                var res = data as BadRequestResult;
                Assert.AreEqual(res.StatusCode, 400);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void withdraw_CorrectAmount_ReturnsOKResult()
        {
            var mock = new Mock<IAccountRep>();
            mock.Setup(m => m.withdraw(t1)).Returns(tm);
            AccountController a1 = new AccountController(mock.Object);
            var data = a1.withdraw(t1);
            var res = data as ObjectResult;
            Assert.AreEqual(res.StatusCode, 200);
        }

        [Test]
        public void withdraw_InCorrectAmount_ReturnsBadResult()
        {
            try
            {
                var mock = new Mock<IAccountRep>();
                mock.Setup(m => m.withdraw(t1)).Returns(tm);
                AccountController a1 = new AccountController(mock.Object);
                var data = a1.withdraw(t1);
                var res = data as ObjectResult;
                Assert.AreEqual(res.StatusCode, 200);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }


        }
    }
}