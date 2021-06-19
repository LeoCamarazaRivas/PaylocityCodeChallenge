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
           
            // Act
           

            // Assert
            
        }

           
    }
}
