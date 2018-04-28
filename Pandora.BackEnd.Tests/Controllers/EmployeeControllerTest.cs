using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pandora.BackEnd.Api.Controllers;
using Pandora.BackEnd.Business;
using Pandora.BackEnd.Business.Contracts;
using Pandora.BackEnd.Business.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pandora.BackEnd.Tests.Controllers
{
    [TestClass]
    public class EmployeeControllerTest
    {
        [TestMethod]
        public void Get_AllEmployees()
        {
            // Arrange
            var mockService = new Mock<IEmployeeSVC>();
            mockService
                .Setup(svc => svc.GetAllAsync()).Returns(Task.FromResult(new BLResponse<List<EmployeeDTO>>()));

            EmployeeController controller = new EmployeeController(mockService.Object, null);

            // Act
            var resp = controller.Get();

            // Assert
            Assert.IsNotNull(resp.Result, "Result is null");
        }
    }
}
