using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gp.Data.Sql;
using Moq;
using Gp.Data.Common;
using Gp.Data.Entities;
using System.Linq;

namespace Gp.Tests.Integrated
{
    [TestClass]
    public class PlanRepositoryFixture
    {

        SqlUnitOfWork _unitOfWork;

        [TestInitialize]
        public void Setup()
        {
            _unitOfWork = new SqlUnitOfWork("GP");
        }

        [TestCleanup]
        public void TearDown()
        {
            _unitOfWork.Dispose();
        }

        [TestMethod]
        public void GetAllPlans()
        {
            SqlPlanRepository repo = new SqlPlanRepository(_unitOfWork);

            var plans = repo.GetAll();

            Assert.IsTrue(plans.Any(plan => plan.Variety != null && plan.EventDescription.Contains("basil")));
        }

    }
}
