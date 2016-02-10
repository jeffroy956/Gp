using Gp.Data.Entities;
using Gp.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gp.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar
        public ActionResult Index()
        {
            IEnumerable<Calendar> allCalendars;

            using (SqlUnitOfWork unitOfWork = new SqlUnitOfWork("gp"))
            {
                CalendarRepository calendarRepo = new CalendarRepository(unitOfWork);
                allCalendars = calendarRepo.GetAll()
                    .OrderBy(cal => cal.Year)
                    .ThenBy(cal => cal.Description);

                unitOfWork.Commit();
            }

            return View(allCalendars);
        }

        // GET: Calendar/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Calendar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Calendar/Create
        [HttpPost]
        public ActionResult Create(Calendar calendar)
        {
            using (SqlUnitOfWork unitOfWork = new SqlUnitOfWork("gp"))
            {
                CalendarRepository calendarRepo = new CalendarRepository(unitOfWork);
                calendarRepo.Insert(calendar);

                unitOfWork.Commit();
            }

            return RedirectToAction("Index");
        }

        // GET: Calendar/Edit/5
        public ActionResult Edit(int id)
        {
            Calendar editCalendar = null;
            using (SqlUnitOfWork unitOfWork = new SqlUnitOfWork("gp"))
            {
                CalendarRepository calendarRepo = new CalendarRepository(unitOfWork);
                editCalendar = calendarRepo.Get(id);

                unitOfWork.Commit();
            }

            return View(editCalendar);
        }

        // POST: Calendar/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Calendar calendar)
        {
            calendar.CalendarId = id;
            using (SqlUnitOfWork unitOfWork = new SqlUnitOfWork("gp"))
            {
                CalendarRepository calendarRepo = new CalendarRepository(unitOfWork);
                calendarRepo.Update(calendar);

                unitOfWork.Commit();
            }

            return RedirectToAction("Index");
        }

        // GET: Calendar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Calendar/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
