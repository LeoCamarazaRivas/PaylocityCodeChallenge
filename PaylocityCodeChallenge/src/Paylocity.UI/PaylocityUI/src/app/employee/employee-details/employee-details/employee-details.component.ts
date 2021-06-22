import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';


import { EmployeeService } from './../../employee.service';
import { MatPaginator } from '@angular/material/paginator';
import { Employee } from '../../employee';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-employee-details',
  templateUrl: './employee-details.component.html',
  styleUrls: ['./employee-details.component.css'],
})
export class EmployeeDetailsComponent implements OnInit {
  // set column for Mat Table
  public displayedColumns: string[] = [
    'name',
    'lastname',
    'dependents',
    'deduction',
  ];
  // the source for Mat Table
  public employees: MatTableDataSource<Employee>;
  @ViewChild(MatPaginator) paginator: MatPaginator;  
 
  constructor(private employeeService: EmployeeService) { }

  ngOnInit() {
    this.employeeService.getEmployees()
      .pipe(
        map((employees) => {
          return employees.map((employee) => {
            employee.deduction = employee.deduction * 100;
            return employee;
          })
        })
      ).subscribe((employees) => {
        console.log(employees);
        if (employees.length === 0) {
          this.employees = null;
        } else {
          this.employees = new MatTableDataSource<Employee>(employees);
          this.employees.paginator = this.paginator;
        }        
      },
        // handling errors
        (error) => {
          console.log(error);
        });
  }  
}
