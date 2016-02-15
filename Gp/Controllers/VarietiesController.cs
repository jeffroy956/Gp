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
            VarietyEditViewModel vm = new VarietyEditViewModel();
            using (SqlUnitOfWork unitOfWork = new SqlUnitOfWork("gp"))
            {
                FamilyRepository familyRepo = new FamilyRepository(unitOfWork);
                VarietyRepository varietyRepo = new VarietyRepository(unitOfWork, familyRepo);
                vm.Variety = new Variety();
                vm.AvailableFamilies = familyRepo.GetAll().OrderBy(family => family.Name);

                unitOfWork.Commit();
            }

            return View("Edit", vm);
        }

        // POST: Varieties/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            using (SqlUnitOfWork unitOfWork = new SqlUnitOfWork("gp"))
            {
                FamilyRepository familyRepo = new FamilyRepository(unitOfWork);
                VarietyRepository varietyRepo = new VarietyRepository(unitOfWork, familyRepo);

                Variety newVariety = new Variety();

                MapVarietyFormData(collection, newVariety);
                varietyRepo.Insert(newVariety);

                unitOfWork.Commit();
            }

            return RedirectToAction("Index");
        }

        // GET: Varieties/Edit/5
        public ActionResult Edit(int id)
        {
            VarietyEditViewModel vm = new VarietyEditViewModel();
            using (SqlUnitOfWork unitOfWork = new SqlUnitOfWork("gp"))
            {
                FamilyRepository familyRepo = new FamilyRepository(unitOfWork);
                VarietyRepository varietyRepo = new VarietyRepository(unitOfWork, familyRepo);
                vm.Variety = varietyRepo.Get(id);
                vm.AvailableFamilies = familyRepo.GetAll().OrderBy(family => family.Name);

                unitOfWork.Commit();
            }

            return View(vm);
        }

        // POST: Varieties/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            using (SqlUnitOfWork unitOfWork = new SqlUnitOfWork("gp"))
            {
                FamilyRepository familyRepo = new FamilyRepository(unitOfWork);
                VarietyRepository varietyRepo = new VarietyRepository(unitOfWork, familyRepo);

                Variety currentVariety = varietyRepo.Get(id);

                MapVarietyFormData(collection, currentVariety);

                varietyRepo.Update(currentVariety);
                unitOfWork.Commit();
            }

            return RedirectToAction("Index");
        }

        private static void MapVarietyFormData(FormCollection collection, Variety variety)
        {
            variety.Name = collection["name"];

            int familyId;

            if (int.TryParse(collection["familyId"], out familyId))
            {
                variety.Family = new Family()
                {
                    FamilyId = familyId
                };
            }
            else
            {
                variety.Family = null;
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
