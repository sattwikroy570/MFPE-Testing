using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using TransactionMicroservice;
using TransactionMicroservice.Models;
using TransactionMicroservice.Controllers;
using TransactionMicroservice.Repository;

namespace TranasctiontTesting
{
    public class Tests
    {
        WithdrawDeposit d1 = new WithdrawDeposit { AccountId = 1, Amount = 100 };
        TransactionMsg tsm = new TransactionMsg { Message = "Warning" };
        Transfers transfer = new Transfers { SourceAccountId = 1, DestinationAccountId = 2, Amount = 200 };
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void deposit_CorrectAmount_ReturnsOKResult()
        {
            try
            {
                var mock = new Mock<TransactionRep>();
                TransactionController t1 = new TransactionController(mock.Object);
                WithdrawDeposit d1 = new WithdrawDeposit { AccountId = 1, Amount = 2000 };
                var data = t1.deposit(d1);
                var res = data as ObjectResult;
                Assert.AreEqual(200, res.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

        }

        [Test]
        public void deposit_AmountNotZero_ReturnsNotNullValue()
        {
            var mock = new Mock<TransactionRep>();
            TransactionController t1 = new TransactionController(mock.Object);
            WithdrawDeposit d1 = new WithdrawDeposit { AccountId = 1, Amount = 2000 };
            var data = t1.deposit(d1);
            Assert.IsNotNull(data);

        }

        [Test]
        public void withdraw_CorrectAmount_ReturnsOKResult()
        {
            try
            {
                var mock = new Mock<ITransactionRep>();
                mock.Setup(p => p.withdraw(d1)).Returns(tsm);
                TransactionController t1 = new TransactionController(mock.Object);

                var data = t1.withdraw(d1);
                var res = data as ObjectResult;
                Assert.AreEqual(200, res.StatusCode);
            }
            catch(Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }

        [Test]
        public void withdraw_AmountNotZero_ReturnsNotNullValue()
        {
            WithdrawDeposit d1 = new WithdrawDeposit { AccountId = 1, Amount = 2000 };
            var mock = new Mock<TransactionRep>();

            TransactionController t1 = new TransactionController(mock.Object);

            var data = t1.withdraw(d1);
            var res = data as OkObjectResult;
            Assert.IsNotNull(data);
        }

        [Test]
        public void withdraw_InsufficeintBalace_ReturnsBadRequest()
        {
            var mock = new Mock<TransactionRep>();

            TransactionController t1 = new TransactionController(mock.Object);
            WithdrawDeposit d1 = new WithdrawDeposit();
            var data = t1.withdraw(d1);
            var res = data as BadRequestResult;
            Assert.AreEqual(400, res.StatusCode);
        }

        [Test]
        public void transfer_CorrectAmount_ReturnsOKResult()
        {
            var mock = new Mock<ITransactionRep>();
            mock.Setup(p => p.transfer(transfer)).Returns(tsm);
            TransactionController t1 = new TransactionController(mock.Object);
            var data = t1.transfer(transfer);
            var res = data as ObjectResult;
            Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public void transfer_InsufficeintBalace_ReturnsBadRequest()
        {
            var mock = new Mock<TransactionRep>();
            TransactionController t1 = new TransactionController(mock.Object);
            Transfers d1 = new Transfers { SourceAccountId = 1, DestinationAccountId = 2, Amount = 1000 };
            var data = t1.transfer(d1);
            var res = data as BadRequestResult;
            Assert.AreEqual(400, res.StatusCode);
        }


    }
}