using Gp.Data.Entities;
using Gp.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gp.Controllers
{
    public class VarietiesController : Controller
    {
        // GET: Varieties
        public ActionResult Index()
        {
            IEnumerable<Variety> allVarieties;

            using (SqlUnitOfWork unitOfWork = new SqlUnitOfWork("gp"))
            {
                VarietyRepository varietyRepo = new VarietyRepository(unitOfWork, new FamilyRepository(unitOfWork));
                allVarieties = varietyRepo.GetAll().OrderBy(v => v.Name);

                unitOfWork.Commit();
            }

            return View(allVarieties);
        }

        // GET: Varieties/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Varieties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Varieties/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Varieties/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Varieties/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Varieties/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Varieties/Delete/5
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
