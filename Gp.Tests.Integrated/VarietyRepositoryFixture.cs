using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gp.Data.Sql;
using Gp.Data.Entities;
using System.Collections.Generic;

namespace Gp.Tests.Integrated
{
    [TestClass]
    public class VarietyRepositoryFixture
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
        public void InsertNewVariety()
        {
            FamilyRepository familyRepo = new FamilyRepository(_unitOfWork);

            Family bushBeans = new Family()
            {
                Name = "Beans, Bush"
            };

            familyRepo.Insert(bushBeans);

            VarietyRepository varietyRepo = new VarietyRepository(_unitOfWork);

            Variety variety = new Variety()
            {
                Name = "Provider",
                Family = bushBeans
            };

            varietyRepo.Insert(variety);

            Assert.IsNotNull(variety.VarietyId, "VarietyId");

            Variety savedVariety = varietyRepo.Get(variety.VarietyId.Value);

            Assert.AreEqual("Provider", savedVariety.Name);
            Assert.IsNotNull(savedVariety.Family, "Family");

            Assert.AreEqual("Beans, Bush", savedVariety.Family.Name);
        }

        //TODO: getAllTest
        [TestMethod]
        public void GetAll()
        {
            FamilyRepository familyRepo = new FamilyRepository(_unitOfWork);

            Family bushBeans = new Family()
            {
                Name = "Beans, Bush"
            };

            familyRepo.Insert(bushBeans);

            Family turnips = new Family()
            {
                Name = "Turnips"
            };

            familyRepo.Insert(turnips);

            VarietyRepository varietyRepo = new VarietyRepository(_unitOfWork);

            Variety provider = new Variety()
            {
                Name = "Provider",
                Family = bushBeans
            };

            varietyRepo.Insert(provider);

            Variety purpleGlobe = new Variety()
            {
                Name = "Purple Globe",
                Family = turnips
            };

            varietyRepo.Insert(purpleGlobe);

            Variety broccoli = new Variety()
            {
                Name = "Broccoli"
            };

            varietyRepo.Insert(broccoli);

            List<Variety> savedVarieties = varietyRepo.GetAll();

            Assert.IsTrue(savedVarieties.Any(sv => sv.Name == "Provider" && sv.Family != null && sv.Family.Name == "Beans, Bush"), "contains Provider");
            Assert.IsTrue(savedVarieties.Any(sv => sv.Name == "Purple Globe" && sv.Family != null && sv.Family.Name == "Turnips"), "contains Purple Globe");
            Assert.IsTrue(savedVarieties.Any(sv => sv.Name == "Broccoli" && sv.Family == null), "contains Broccoli");
        }
    }
}
