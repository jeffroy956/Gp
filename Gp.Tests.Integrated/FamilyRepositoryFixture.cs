using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gp.Data.sql;
using Gp.Data.entities;
using System.Data.SqlClient;
using System.Configuration;

namespace Gp.Tests.Integrated
{
    [TestClass]
    public class FamilyRepositoryFixture
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
        public void AddNewFamily()
        {
            FamilyRepository familyRepo = new FamilyRepository(_unitOfWork);

            Family newFamily = new Family()
            {
                Name = "yo!"
            };

            familyRepo.Insert(newFamily);

            Assert.IsNotNull(newFamily.FamilyId);
        }

        [TestMethod]
        public void GetNewFamily()
        {
            FamilyRepository familyRepo = new FamilyRepository(_unitOfWork);

            Family newFamily = new Family()
            {
                Name = "yo!"
            };

            familyRepo.Insert(newFamily);

            Family getFamily = familyRepo.Get(newFamily.FamilyId.Value);

            Assert.AreEqual("yo!", getFamily.Name);
        }

        [TestMethod]
        public void AddNewFamilyWithCompanions()
        {
            FamilyRepository familyRepo = new FamilyRepository(_unitOfWork);

            Family corn = new Family()
            {
                Name = "corn"
            };

            familyRepo.Insert(corn);

            Family beets = new Family()
            {
                Name = "beets"
            };

            beets.Companions.Add(corn);

            familyRepo.Insert(beets);

            Family savedBeets = familyRepo.Get(beets.FamilyId.Value);

            Assert.AreEqual(1, savedBeets.Companions.Count);
            Assert.AreEqual("corn", savedBeets.Companions[0].Name);
        }

        [TestMethod]
        public void UpdateFamilyName()
        {
            FamilyRepository familyRepo = new FamilyRepository(_unitOfWork);

            Family corn = new Family()
            {
                Name = "corn"
            };

            familyRepo.Insert(corn);

            corn.Name = "white corn";

            familyRepo.Update(corn);

            Family savedCorn = familyRepo.Get(corn.FamilyId.Value);

            Assert.AreEqual("white corn", savedCorn.Name);
        }

        [TestMethod]
        public void UpdateFamilyCompanions()
        {
            FamilyRepository familyRepo = new FamilyRepository(_unitOfWork);

            Family corn = new Family()
            {
                Name = "corn"
            };

            familyRepo.Insert(corn);

            Family beans = new Family()
            {
                Name = "beans"
            };

            familyRepo.Insert(beans);

            Family spinach = new Family()
            {
                Name = "spinach"
            };

            familyRepo.Insert(spinach);

            Family okra = new Family()
            {
                Name = "okra"
            };

            okra.Companions.Add(beans);
            okra.Companions.Add(corn);

            familyRepo.Insert(okra);

            okra.Companions.Remove(corn);
            okra.Companions.Add(spinach);

            familyRepo.Update(okra);

            Family savedOkra = familyRepo.Get(okra.FamilyId.Value);

            Assert.AreEqual(2, savedOkra.Companions.Count, "Companions.Count");
            Assert.IsTrue(savedOkra.Companions.Any(comp => comp.Name == "beans"), "contains beans");
            Assert.IsTrue(savedOkra.Companions.Any(comp => comp.Name == "spinach"), "contains spinach");
        }


        [TestMethod]
        public void AddNewFamilyWithEnemies()
        {
            FamilyRepository familyRepo = new FamilyRepository(_unitOfWork);

            Family corn = new Family()
            {
                Name = "corn"
            };

            familyRepo.Insert(corn);

            Family beets = new Family()
            {
                Name = "beets"
            };

            beets.Enemies.Add(corn);

            familyRepo.Insert(beets);

            Family savedBeets = familyRepo.Get(beets.FamilyId.Value);

            Assert.AreEqual(1, savedBeets.Enemies.Count);
            Assert.AreEqual("corn", savedBeets.Enemies[0].Name);
        }
    }
}
