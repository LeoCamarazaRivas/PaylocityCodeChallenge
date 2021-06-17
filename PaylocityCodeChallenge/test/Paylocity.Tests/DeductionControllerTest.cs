using Microsoft.AspNetCore.Mvc;
using Moq;
using Paylocity.DAL.Data;
using Paylocity.DAL.Repository;
using Paylocity.UI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Paylocity.Tests
{
    public class DeductionControllerTest
    {
        Mock<IDeductionRepo> mockRepo;

        public DeductionControllerTest()
        {
            mockRepo = new Mock<IDeductionRepo>();
        }

        [Fact]
        public void StoreEmployee_ReturnOneEmployee_WhenFireBaseDBIsEmpty()
        {
            // Arrange
            mockRepo.Setup(repo => repo.AddEmployeeAsync(GetEmployee(0)));
            var controller = new DeductionController(mockRepo.Object);

            // Act
            var result = controller.Post(GetEmployee(0));

            // Assert
            Assert.IsType<Employee>(result.Result);
        }

        private Employee GetEmployee(int num)
        {
            var employee = new Employee();
            if (num > 0)
            {
                employee = new Employee
                {
                    employeeId = 0,
                    firstName = "Leonel",
                    lastName = "Rivas",
                    dependents = null
                };
            }
            return employee;
        }        
    }
}
