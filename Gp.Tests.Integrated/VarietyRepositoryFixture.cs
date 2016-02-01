using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gp.Data.Sql;
using Gp.Data.Entities;

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
    }
}
