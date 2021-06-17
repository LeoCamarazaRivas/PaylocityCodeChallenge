import { Component, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatAccordion } from '@angular/material/expansion';

import { BaseFormComponent } from '../shared/base-form.component';
import { Dependent } from '../shared/dependent.model';
import { EmployeeService } from './employee.service';
import { Employee } from './../shared/employee';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent extends BaseFormComponent implements OnInit {

  // form model
  form!: FormGroup;
  // Employee model
  employee!: Employee;

  // expand/collapse employee data
  @ViewChild(MatAccordion) accordion!: MatAccordion;

  constructor(
    private employeeService: EmployeeService,
    private router: Router
  ) {
    super();
  }

  ngOnInit() {

    this.form = new FormGroup({
      firstName: new FormControl(null, Validators.required),
      lastName: new FormControl(null, Validators.required),
      paycheck: new FormControl(2000, Validators.pattern(/^-?(0|[1-9]\d*)?$/)),
      dependents: new FormArray([])
    });
  }

  onSubmit(){
    let employeeId = this.getRandomId(100);
    let employeeName = this.form.get('firstName')?.value;
    let employeeLastname = this.form.get('lastName')?.value;
    let dependentEmployee: Dependent[] = this.form.get('dependents')?.value;
    this.employee = new Employee(
      employeeName,
      employeeLastname,
      dependentEmployee
    );
    this.employeeService.post<Employee>(this.employee)
        .subscribe(
          (response) => {
            //this.employeeService.employeeSubject.next(response);
            this.router.navigate(['/employeeDetails'])
            console.log(response);
          },
          //handling errors
          (error) => {

          });
  }

  onAddDependents() {
    (<FormArray>this.form.get('dependents')).push(
      new FormGroup({
        firstName: new FormControl(null),
        lastName: new FormControl(null),
        relationship: new FormControl(null),
      })
    )
  }

  get controls() {
   return (<FormArray>this.form.get('dependents')).controls;
  }

  getRandomId(max: number){
    return Math.floor(Math.random() * max);
  }

}
