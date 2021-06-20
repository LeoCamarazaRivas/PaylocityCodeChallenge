using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Paylocity.DAL.Data;
using Paylocity.DAL.Data.Model;
using Paylocity.DAL.Profiles;
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
        public void GetEmployees_Returns200OK_WhenDBIsNotEmpty()
        {
            // Arrange            
            mockRepo.Setup(repo => repo.GetEmployees()).Returns(GetEmployees(0));

            var realProfile = new DeductionProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            IMapper mapper = new Mapper(configuration);

            var controller = new DeductionController(mockRepo.Object, mapper);

        }

        [Fact]
        public void StoreEmployee_ReturnOneEmployee_WhenFireBaseDBIsEmpty()
        {
            // Arrange

            // Act


            // Assert

        }

        private List<Employee> GetEmployees(int num)
        {
            var employees = new List<Employee>();
            if (num > 0)
            {
                employees.Add(                   
                    new Employee
                    {
                        Id = 3,
                        name = "EmployeeTest3Name",
                        lastname = "EmployeeTest3Lastname",
                        deduction = 192,
                        Dependents = new List<Dependent>
                        {
                            new Dependent
                            {
                                Id = 3,
                                name = "DependentTest1Name",
                                lastname = "DependentTest1Lastname",
                                 EmployeeId = 2,
                                 relationshipWithEmployee = "child"
                            }
                        }
                    });
            }
            return employees;
        }

    }
}

/*
 *  new Employee
                    {
                        Id = 1,
                        name = "EmployeeTest1Name",
                        lastname = "EmployeeTest1Lastname",
                        deduction = 202,
                        Dependents = null
                    },
                    new Employee
                    {
                        Id = 2,
                        name = "EmployeeTest2Name",
                        lastname = "EmployeeTest2Lastname",
                        deduction = 302,
                        Dependents = null
                    },
 */