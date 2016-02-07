using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gp.Data.Sql;
using Moq;
using Gp.Data.Common;
using Gp.Data.Entities;

namespace Gp.Tests.Integrated
{
    [TestClass]
    public class PlanRepositoryFixture
    {

        SqlUnitOfWork _unitOfWork;
        Mock<Repository<Calendar>> _calendarRepoMock;
        Mock<Repository<Variety>> _varietyRepoMock;

        [TestInitialize]
        public void Setup()
        {
            _unitOfWork = new SqlUnitOfWork("GP");
            _calendarRepoMock = new Mock<Repository<Calendar>>();
            _varietyRepoMock = new Mock<Repository<Variety>>();
        }

        [TestCleanup]
        public void TearDown()
        {
            _unitOfWork.Dispose();
        }
        [TestMethod]
        public void SaveNewPlan()
        {
            PlanRepository planRepo = new PlanRepository(_unitOfWork, _calendarRepoMock.Object, _varietyRepoMock.Object);

            Variety beefsteak = new Variety()
            {
                VarietyId = 1,
                Name = "beefsteak",
            };

            Calendar calendar2016 = new Calendar()
            {
                CalendarId = 1,
                Description = "2016 garden",
                Year = 2016
            };

            Plan plan = new Plan()
            {
                PlanDate = new DateTime(2016, 3, 15),
                Description = "Start tomato seeds",
                Variety = beefsteak,
                Notes = "started to early last year",
                ActualDate = new DateTime(2016, 3, 16),
                Calendar = calendar2016
            };

            planRepo.Insert(plan);

            Assert.IsNotNull(plan.PlanId, "PlanId");

            _varietyRepoMock.Setup(vr => vr.Get(1))
                .Returns(new Variety()
                {
                    VarietyId = 1,
                    Name = "beefsteak",
                });

            _calendarRepoMock.Setup(cr => cr.Get(1))
                .Returns(new Calendar()
                {
                    CalendarId = 1,
                    Description = "2016 garden",
                    Year = 2016
                });


            Plan savedPlan = planRepo.Get(plan.PlanId.Value);

            Assert.AreEqual(new DateTime(2016, 3, 15), savedPlan.PlanDate);
            Assert.AreEqual("Start tomato seeds", savedPlan.Description);
            Assert.IsNotNull(savedPlan.Variety, "Variety");
            Assert.AreEqual("beefsteak", savedPlan.Variety.Name);
            Assert.AreEqual("started to early last year", savedPlan.Notes);
            Assert.AreEqual(new DateTime(2016, 3, 16), savedPlan.ActualDate);
            Assert.IsNotNull(savedPlan.Calendar, "Calendar");
            Assert.AreEqual("2016 garden", savedPlan.Calendar.Description);

        }
    }
}
