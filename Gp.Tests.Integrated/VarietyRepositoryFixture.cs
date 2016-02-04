using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gp.Data.Sql;
using Gp.Data.Entities;
using System.Collections.Generic;
using Moq;
using Gp.Data.Common;

namespace Gp.Tests.Integrated
{
    [TestClass]
    public class VarietyRepositoryFixture
    {

        SqlUnitOfWork _unitOfWork;
        Mock<Repository<Family>> _familyRepoMock;

        [TestInitialize]
        public void Setup()
        {
            _unitOfWork = new SqlUnitOfWork("GP");
            _familyRepoMock = new Mock<Repository<Family>>();
        }

        [TestCleanup]
        public void TearDown()
        {
            _unitOfWork.Dispose();
        }

        [TestMethod]
        public void InsertNewVariety()
        {
            VarietyRepository varietyRepo = new VarietyRepository(_unitOfWork, _familyRepoMock.Object);

            Family bushBeans = new Family()
            {
                FamilyId = 1,
                Name = "Beans, Bush"
            };
                
            Variety variety = new Variety()
            {
                Name = "Provider",
                Family = bushBeans
            };

            _familyRepoMock.Setup(fr => fr.Get(1)).Returns(bushBeans);

            varietyRepo.Insert(variety);

            Assert.IsNotNull(variety.VarietyId, "VarietyId");

            Variety savedVariety = varietyRepo.Get(variety.VarietyId.Value);

            Assert.AreEqual("Provider", savedVariety.Name);
            Assert.IsNotNull(savedVariety.Family, "Family");

            Assert.AreEqual("Beans, Bush", savedVariety.Family.Name);
        }

        [TestMethod]
        public void GetAllVarieties()
        {
            Family bushBeans = new Family()
            {
                FamilyId = 1,
                Name = "Beans, Bush"
            };

            Family turnips = new Family()
            {
                FamilyId = 2,
                Name = "Turnips"
            };

            VarietyRepository varietyRepo = new VarietyRepository(_unitOfWork, _familyRepoMock.Object);

            Variety provider = new Variety()
            {
                Name = "Provider",
                Family = bushBeans
            };

            _familyRepoMock.Setup(fr => fr.GetAll()).Returns(new List<Family>() { bushBeans, turnips });

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

        [TestMethod]
        public void UpdateVariety()
        {
            Family bushBeans = new Family()
            {
                FamilyId = 1,
                Name = "Beans, Bush"
            };

            VarietyRepository varietyRepo = new VarietyRepository(_unitOfWork, _familyRepoMock.Object);

            Variety variety = new Variety()
            {
                Name = "Provider",
                Family = bushBeans
            };

            varietyRepo.Insert(variety);

            Variety savedVariety = varietyRepo.Get(variety.VarietyId.Value);

            Family gmoBeans = new Family()
            {
                FamilyId = 3,
                Name = "gmo beans"
            };

            savedVariety.Name = "Provider especial";
            savedVariety.Family = gmoBeans;

            varietyRepo.Update(savedVariety);

            _familyRepoMock.Setup(fr => fr.Get(1)).Returns(bushBeans);
            _familyRepoMock.Setup(fr => fr.Get(3)).Returns(gmoBeans);

            Variety updatedVariety = varietyRepo.Get(variety.VarietyId.Value);

            Assert.AreEqual("Provider especial", updatedVariety.Name);
            Assert.AreEqual("gmo beans", updatedVariety.Family.Name);
        }
    }
}
