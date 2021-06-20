import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';


import { EmployeeService } from './../../employee.service';
import { MatPaginator } from '@angular/material/paginator';
import { Employee } from '../../employee';

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
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  public deductionFormat = '';

  constructor(private employeeService: EmployeeService) { }

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
          item.deduction = item.deduction * 100;
          this.deductionFormat = formatter.format(item.deduction);
        }
        tempArray.push(item);
      }
      this.employees = new MatTableDataSource<Employee>(tempArray);
      this.employees.paginator = this.paginator;
    });
  }
}
