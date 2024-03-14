using MarkelAPI.Controllers;
using MarkelAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MarkelApiTest
{
    public class CompanyControllerTest
    {
        private Mock<ClaimsContext> _claimsContext;

        [SetUp]
        public void Setup()
        {
            _claimsContext = new Mock<ClaimsContext>();
            List<Company> companies = new()
            {
                new()
                {
                    Active = true,
                    Address1 = "1 King Street",
                    Address2 = "Leeds",
                    Address3 = "West Yorkshire",
                    Country = "UK",
                    Id = 1,
                    InsuranceEndDate = DateTime.Now,
                    Name = "CompanyName",
                    Postcode = "LS1 1AA"
                },
                new()
                {
                    Active = true,
                    Address1 = "1 King Street",
                    Address2 = "Leeds",
                    Address3 = "West Yorkshire",
                    Country = "UK",
                    Id = 2,
                    InsuranceEndDate = DateTime.Now,
                    Name = "CompanyName",
                    Postcode = "LS1 1AA"
                },
            };
            var mockDbSetCompany = new Mock<DbSet<Company>>();
            mockDbSetCompany.Setup(a => a.FindAsync(It.IsAny<int>())).Returns(async () =>
            {
                await Task.Yield();
                return companies.First() ?? null;
            });
            DbSet<Company> dbSet = GetQueryableMockDbSet(companies);
            _claimsContext.Object.Companies = dbSet;
        }

        [Test]
        public async Task TestGetCompanyAsync()
        {
            var controller = new CompanyController(_claimsContext.Object);

            ActionResult<CompanyDetails> result = await controller.GetCompany(1);

            Assert.AreEqual(result.Value.Company.Id, 1);
        }

        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(a => a.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(a => a.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(a => a.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(a => a.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(a => a.FindAsync(It.IsAny<int>())).Returns(async () =>
            {
                await Task.Yield();
                return sourceList.First() ?? null;
            });
            return dbSet.Object;
        }

    }
}