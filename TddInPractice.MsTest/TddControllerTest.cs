using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TddInPractice.Controllers;
using TddInPractice.Models;

namespace TddInPractice.MsTest
{
    [TestClass]
    public class TddControllerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange   

            var configSection = new Mock<IConfigurationSection>();
            configSection.SetupGet(m => m[It.Is<string>(s => s == "demo")]).Returns("Server=.;Database=TddDbDemo; Trusted_Connection=True;MultipleActiveResultSets=True");

            var configuration = new Mock<IConfiguration>();
            configuration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(configSection.Object);

                        
            var controller = new TddController();
            var customer = new Customer
            {
                City = "Lima",
                Country = "Peru",
                FirstName = "César",
                LastName = "Velarde",
                Phone = "555-555-555"
            };            

            //Act
            var result = controller.AddNewElement(customer, configuration.Object) as OkObjectResult;

            //Assert

            Assert.IsTrue(result.StatusCode == 200);

        }
    }
}
