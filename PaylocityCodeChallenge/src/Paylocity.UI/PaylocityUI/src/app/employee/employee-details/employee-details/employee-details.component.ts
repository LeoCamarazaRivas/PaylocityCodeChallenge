import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

import { Employee } from './../../../shared/employee';
import { EmployeeService } from './../../employee.service';

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
  public employees!: MatTableDataSource<Employee>;
  public deductionFormat = '';

  constructor(private employeeService: EmployeeService) {}

  ngOnInit() {
    this.employeeService.getEmployees().subscribe((r) => {
      console.log(r);
      let tempArray: Employee[] = [];
      const formatter = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD',
        minimumFractionDigits: 2,
      });

      for (const item of Object.values(r)) {
        console.log(item);
        if (item.deduction !== undefined) {
          item.deduction = item.deduction / 1000;
          this.deductionFormat = formatter.format(item.deduction);
        }
        tempArray.push(item);
      }
      this.employees = new MatTableDataSource<Employee>(tempArray);
    });
  }
}
