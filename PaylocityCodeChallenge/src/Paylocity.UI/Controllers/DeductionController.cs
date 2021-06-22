using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Paylocity.DAL.Data.Model;
using Paylocity.DAL.DTOs;
using Paylocity.DAL.Repository;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Paylocity.DAL.Data;

namespace Paylocity.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeductionController : ControllerBase
    {
        private readonly IDeductionRepo _repo;        
        private readonly IMapper _mapper;
        public DeductionController(IDeductionRepo repo, IMapper mapper)
        {
            _repo = repo;          
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EmployeeReadDTO>> GetEmployees()
        {            
            var employees = _repo.GetEmployees();
            return Ok(_mapper.Map<IEnumerable<EmployeeReadDTO>>(employees));
        }

        [HttpGet("{id}", Name = "GetEmployeeById")]        
        public ActionResult<EmployeeReadDTO> GetEmployeeById(int id)
        {
            var employeeItem = _repo.GetEmployeeById(id);
            if (employeeItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<EmployeeReadDTO>(employeeItem));
        }

        [HttpPut("{id}")]
        public ActionResult<EmployeeReadDTO> PutEmployee(int id, EmployeeUpdateDTOs putEmployee)
        {
            var employee = _mapper.Map<Employee>(putEmployee);
            if (id != employee.Id)
            {
                return BadRequest();
            }
            _repo.UpdateEmployee(id, employee);
            var employeeReadDto = _mapper.Map<EmployeeReadDTO>(employee);

            return CreatedAtRoute(nameof(GetEmployeeById), new { Id = employeeReadDto.Id }, employeeReadDto);
        }

        [HttpPost]
        public ActionResult<EmployeeReadDTO> Post(EmployeeCreateDTO employeeCreateDto)
        {
            var employee = _mapper.Map<Employee>(employeeCreateDto);
            // calculate employee deduction
            employee.deduction = _repo.CalcDeduction(employee);
            _repo.AddEmployee(employee);
            _repo.SaveChanges();

            var employeeReadDto = _mapper.Map<EmployeeReadDTO>(employee);

            return CreatedAtRoute(nameof(GetEmployeeById), new { Id = employeeReadDto.Id }, employeeReadDto);

        }        
    }
}
