using AutoMapper;
using Moq;
using Paylocity.DAL.Data.Model;
using Paylocity.DAL.Profiles;
using Paylocity.DAL.Repository;
using Paylocity.DAL.DTOs;
using Paylocity.UI.Controllers;
using System.Collections.Generic;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Paylocity.Tests
{
    public class DeductionControllerTest : IDisposable
    {
        Mock<IDeductionRepo> mockRepo;
        DeductionProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;

        public DeductionControllerTest()
        {
            mockRepo = new Mock<IDeductionRepo>();
            realProfile = new DeductionProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
        }
        public void Dispose()
        {
            mockRepo = null;
            realProfile = null;
            configuration = null;
            mapper = null;
        }

        [Fact]
        public void GetEmployees_Returns200OK_WhenDBIsNotEmpty()
        {
            // Arrange            
            mockRepo.Setup(repo => repo.GetEmployees()).Returns(GetEmployees(1));
            var controller = new DeductionController(mockRepo.Object, mapper);

            // Act
            var result = controller.GetEmployees();

            // Assert       
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void Post_ReturnOneEmployee_WhenDBIsEmpty()
        {
            // Arrange
            EmployeeCreateDTO employeeCreateDTO =
                new EmployeeCreateDTO
                {
                    name = "Ragnar",
                    lastname = "Lothbrok",
                    Dependents = new List<DependentDTO> { new DependentDTO { name = "Bjorn", lastname = "Lothbrok", relationshipWithEmployee = "child" } },
                    deduction = 0
                };
            var employee = mapper.Map<Employee>(employeeCreateDTO);
            mockRepo.Setup(repo => repo.AddEmployee(employee));
            var controller = new DeductionController(mockRepo.Object, mapper);

            // Act
            var result = controller.Post(new EmployeeCreateDTO { });

            // Assert
            Assert.IsType<ActionResult<EmployeeReadDTO>>(result);
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