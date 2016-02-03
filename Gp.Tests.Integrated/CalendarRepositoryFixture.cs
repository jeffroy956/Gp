using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gp.Data.Entities;
using Gp.Data.Sql;
using System.Collections.Generic;
using System.Linq;

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

        [TestMethod]
        public void UpdateCalendar()
        {
            Calendar calendar = new Calendar()
            {
                Description = "2016 garden",
                Year = 2016
            };

            CalendarRepository calendarRepo = new CalendarRepository(_unitOfWork);

            calendarRepo.Insert(calendar);

            calendar.Description = "2017 big garden";
            calendar.Year = 2017;

            calendarRepo.Update(calendar);

            Calendar savedCalendar = calendarRepo.Get(calendar.CalendarId.Value);
            Assert.AreEqual("2017 big garden", savedCalendar.Description);
            Assert.AreEqual(2017, savedCalendar.Year);
        }

        [TestMethod]
        public void GetAllCalendars()
        {

            CalendarRepository calendarRepo = new CalendarRepository(_unitOfWork);

            Calendar calendar1 = new Calendar()
            {
                Description = "2015 garden bulk",
                Year = 2015
            };
            Calendar calendar2 = new Calendar()
            {
                Description = "2016 garden bulk",
                Year = 2016
            };
            calendarRepo.Insert(calendar1);
            calendarRepo.Insert(calendar2);

            List<Calendar> savedCalendars = calendarRepo.GetAll();

            Assert.IsTrue(savedCalendars.Any(sc => sc.Description == "2015 garden bulk" && sc.Year == 2015), "2015 garden bulk");
            Assert.IsTrue(savedCalendars.Any(sc => sc.Description == "2016 garden bulk" && sc.Year == 2016), "2016 garden bulk");
        }

    }
}
