import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

import { Employee } from './../../../shared/employee';
import { EmployeeService } from './../../employee.service';

@Component({
  selector: 'app-employee-details',
  templateUrl: './employee-details.component.html',
  styleUrls: ['./employee-details.component.css']
})
export class EmployeeDetailsComponent implements OnInit {

  // set column for Mat Table
  public displayedColumns: string[] =
  [
    'name',
    'lastname',
    'dependents',
    'deduction'
  ];
  // the source for Mat Table
  public employees!: MatTableDataSource<Employee>;

  constructor(
    private employeeService: EmployeeService
  ) { }

  ngOnInit() {
    this.employeeService.getEmployees().subscribe(
      (r) => {
        console.log(r);
        let tempArray: Employee[] = [];
        for (const item of Object.values(r)) {
          console.log(item);
          tempArray.push(item);
        }
        this.employees = new MatTableDataSource<Employee>(tempArray);

        // map((employees) => {
        //   return Array.from(employees).map((employee, index, array) => {
        //     if (typeof array[index] !== "string") {
        //       this.employees.push(employee);
        //     }
        //     return this.employees;
        //   })

        // tempArray.map((employee) => {
        //   if (typeof employee !== "string") {
        //     tempArray.push(employee);
        //     this.employees = new MatTableDataSource<Employee>(tempArray);
        //   }
        // })


      }
    );


  }

}
