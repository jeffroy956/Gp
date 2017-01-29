using GpCore.Model.Sql;
using System;
using System.Linq;
using Xunit;

namespace Gp.Tests.Integrated
{
    public class PlanRepositoryFixture : IDisposable
    {

        SqlUnitOfWork _unitOfWork;

        public PlanRepositoryFixture()
        {
            _unitOfWork = new SqlUnitOfWork("GP");
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        [Fact]
        public void GetAllPlans()
        {
            SqlPlanRepository repo = new SqlPlanRepository(_unitOfWork);

            var plans = repo.GetAll();

            Assert.True(plans.Any(plan => plan.Variety != null && plan.EventDescription.Contains("basil")));
        }

    }
}
