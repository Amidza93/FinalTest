using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using FinalTest.Controllers;
using FinalTest.Interfaces;
using FinalTest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FinalTest.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetReturnsEmployeeWithSameId()
        {
            // Arrange
            var mockRepository = new Mock<IEmployeeRepository>();
            mockRepository.Setup(x => x.GetById(42)).Returns(new Employee { Id = 42 });

            var controller = new zaposleniController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetById(42);
            var contentResult = actionResult as OkNegotiatedContentResult<Employee>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(42, contentResult.Content.Id);
        }

        [TestMethod]
        public void PutReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<IEmployeeRepository>();
            var controller = new zaposleniController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(new Employee { Id = 9, Name = "bla blaic" }, 10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void GetReturnsMultipleObjects()
        {
            // Arrange
            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee { Id = 1, Name = "bla blaic" });
            employees.Add(new Employee { Id = 2, Name = "tosa tosic" });

            var mockRepository = new Mock<IEmployeeRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(employees.AsEnumerable());
            var controller = new zaposleniController(mockRepository.Object);

            // Act
            IEnumerable<Employee> result = controller.GetEmployees();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(employees.Count, result.ToList().Count);
            Assert.AreEqual(employees.ElementAt(0), result.ElementAt(0));
            Assert.AreEqual(employees.ElementAt(1), result.ElementAt(1));
        }

        [TestMethod]
        public void GetReturnsMultipleObjects2()
        {
            // Arrange
            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee { Id = 1, Name = "bla blaic", Sallary = 2000 });
            employees.Add(new Employee { Id = 2, Name = "tosa tosic", Sallary = 2450 });
            MinMax m = new MinMax();
            m.Najmanje = 1900;
            m.Najvise = 2500;

            var mockRepository = new Mock<IEmployeeRepository>();
            mockRepository.Setup(x => x.GetAllSallary(m)).Returns(employees.AsEnumerable());
            var controller = new zaposleniController(mockRepository.Object);

            // Act
            IEnumerable<Employee> result = controller.PostPretraga(m);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(employees.Count, result.ToList().Count);
            Assert.AreEqual(employees.ElementAt(0), result.ElementAt(0));
            Assert.AreEqual(employees.ElementAt(1), result.ElementAt(1));
        }
    }
}
