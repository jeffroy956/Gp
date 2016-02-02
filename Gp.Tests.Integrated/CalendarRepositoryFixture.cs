using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gp.Data.Entities;
using Gp.Data.Sql;

namespace Gp.Tests.Integrated
{
    [TestClass]
    public class CalendarRepositoryFixture
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
        public void SaveNewCalendar()
        {
            Calendar calendar = new Calendar()
            {
                Description = "2016 garden",
                Year = 2016
            };

            CalendarRepository calendarRepo = new CalendarRepository(_unitOfWork);

            calendarRepo.Insert(calendar);

            Assert.IsNotNull(calendar.CalendarId, "CalendarId");

            Calendar savedCalendar = calendarRepo.Get(calendar.CalendarId.Value);
            Assert.AreEqual("2016 garden", savedCalendar.Description);
            Assert.AreEqual(2016, savedCalendar.Year);
        }

    }
}
