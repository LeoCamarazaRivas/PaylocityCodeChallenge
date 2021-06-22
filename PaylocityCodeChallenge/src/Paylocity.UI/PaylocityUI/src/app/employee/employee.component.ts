import { Component, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatAccordion } from '@angular/material/expansion';

import { BaseFormComponent } from '../shared/base-form.component';
import { Dependent } from '../shared/dependent.model';
import { EmployeeService } from './employee.service';
import { Employee } from '../shared/employee.model';
import { Router, ActivatedRoute, ɵEmptyOutletComponent } from '@angular/router';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent extends BaseFormComponent implements OnInit {

  // form model
  form: FormGroup;
  // view title
  title: string = 'Employee Information';
  // Employee model
  employee: Employee;
  // the employee object id, as fetched from the active route:
  // It's NULL when we are adding a new employee,
  // and not NULL when we are editing an existing one.
  id?: number;
  message: string = null;

  // expand/collapse employee data
  @ViewChild(MatAccordion) accordion!: MatAccordion;

  constructor(
    private employeeService: EmployeeService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {
    super();
  }

  ngOnInit() {
    this.form = new FormGroup({
      name: new FormControl('', Validators.required),
      lastname: new FormControl('', Validators.required),
      dependents: new FormArray([new FormGroup({
        name: new FormControl(''),
        lastname: new FormControl(''),
        relationshipWithEmployee: new FormControl('')
      })])
    });
    this.loadData();
  }

  loadData() {
    // retrieve the employee id from 'id' parameter
    this.id = +this.activatedRoute.snapshot.paramMap.get('id');
    if (this.id) {
      this.employeeService.getEmployeeById(this.id)
        .subscribe((employee) => {
          console.log(employee);
          this.employee = employee;
          this.title = `Edit Employee - ${this.employee.name}`;
          this.form.patchValue({ name: this.employee.name, lastname: this.employee.lastname});         
          this.employee.dependents.forEach(depe => {
            (<FormArray>this.form.get('dependents')).push(new FormGroup({
              name: new FormControl(depe.name),
              lastname: new FormControl(depe.lastname),
              relationshipWithEmployee: new FormControl(depe.relationshipWithEmployee),
            }))
          })
          
        },
          // handling errors
          (error) => {
            console.log(error);
          });
    } 
  }

  onSubmit() {
    let employee = this.id ? this.employee : <Employee>{};
    employee.name = this.form.get('name')?.value;
    employee.lastname = this.form.get('lastname')?.value;
    employee.dependents = this.form.get('dependents')?.value;
    // set dependets to null if has not values
    employee.dependents.map(dependent => {
      if (dependent.name == '' || dependent.lastname == '' || dependent.relationshipWithEmployee == '') {
        employee.dependents = null;
      } 
    })
    
    if (this.id) {
      // Edit an employee
      console.log(this.id);
      this.employeeService.put<Employee>(this.id, employee)
        .subscribe(
          (employee) => {
            confirm(`Employee ${employee.name} has been updated`);
            // navigate to Employee Details
            this.router.navigate(['/employeeDetails']);
          },
          // handling errors
          (error) => console.log(error));
    } else {
      // Add New Employee
      this.employeeService.post<Employee>(employee)
        .subscribe(
          (response) => {
            //this.employeeService.employeeSubject.next(response);
            this.message += `Employee ${response.name} ${response.lastname} has been successfully added.`;
            this.router.navigate(['/employeeDetails'])
            console.log(response);
          },
          //handling errors
          (error) => {
            console.log(error);
          });
    }

  }

  onAddDependents() {
    (<FormArray>this.form.get('dependents')).push(
      new FormGroup({
        name: new FormControl(''),
        lastname: new FormControl(''),
        relationshipWithEmployee: new FormControl(''),
      })
    );    
  }

  onCancel() {
    this.form.reset();
    (<FormArray>this.form.get('dependents')).clear();
    this.router.navigate(['/employee']);
  }

  onCloseAlert() {
    this.message = null;
  }

  get controls() {
    return (<FormArray>this.form.get('dependents')).controls;
  }

}
