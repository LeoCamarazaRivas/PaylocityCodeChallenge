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
      name: new FormControl('', Validators.required),
      lastname: new FormControl('', Validators.required),      
      dependents: new FormArray([])
    });
  }

  onSubmit(){    
    let employeeName = this.form.get('name')?.value;
    let employeeLastname = this.form.get('lastname')?.value;
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
        name: new FormControl('', Validators.required),
        lastname: new FormControl('', Validators.required),
        relationshipWithEmployee: new FormControl('', Validators.required),
      })
    )
  }

  onCancel() {
    this.form.reset();
    (<FormArray>this.form.get('dependents')).clear();
  }

  get controls() {
   return (<FormArray>this.form.get('dependents')).controls;
  }

  getRandomId(max: number){
    return Math.floor(Math.random() * max);
  }

}
