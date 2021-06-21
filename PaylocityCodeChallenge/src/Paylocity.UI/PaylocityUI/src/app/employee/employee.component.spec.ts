import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularMaterialModule } from '../angular-material.module';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { of } from 'rxjs';

import { EmployeeDetailsComponent } from './employee-details/employee-details/employee-details.component';
import { EmployeeService } from './employee.service';
import { Employee } from '../shared/employee.model';
import { Dependent } from '../shared/dependent.model';
import { getBaseUrl } from '../../main';


describe('EmployeeDetailsComponent', () => {
  let component: EmployeeDetailsComponent;
  let fixture: ComponentFixture<EmployeeDetailsComponent>;

  beforeEach(async () => {
    // initialize the required providers
    await TestBed.configureTestingModule({
      declarations: [EmployeeDetailsComponent],
      imports: [
        BrowserAnimationsModule,
        AngularMaterialModule,
        RouterTestingModule,
        HttpClientTestingModule 
      ],
      providers: [
        {
          provide: EmployeeService,
          useValue: employeeService
        }
      ],
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should display Employee(s) Information title', () => {
    let employeeTitle: HTMLElement = fixture.nativeElement;
    const h1 = employeeTitle.querySelector('h1');
    expect(h1?.textContent).toEqual('Employee(s) Information');    
  });

  it('should contain a table with a list of one or more employees', () => {
    let employeeTable: HTMLTableElement = fixture.nativeElement;
    const table = employeeTable.querySelector('table.mat-table');
    const tableRows = table?.querySelectorAll('tr.mat-row');

    expect(tableRows).toBeGreaterThan(0);
  });

  // Create a mock employeeService with a mock 'getEmployees' method
  const employeeService = jasmine.createSpyObj<EmployeeService>('EmployeeService', ["getEmployees"]);
  // Configure 'getEmployees' spy method
  employeeService.getEmployees.and.returnValue(
    of<Employee[]>(<Employee[]>
      [
        <Employee>{
          id: 1,
          name: 'EmployeeOne',
          lastname: 'EmployeeOneLastname',
          dependents: [],
          deduction: 202
        },
        <Employee>{
          id: 2,
          name: 'EmployeeTwo',
          lastname: 'EmployeeTwoLastname',
          dependents: [
            <Dependent>{
              id: 1,
              name: 'DependentOne',
              lastname: 'DependentOneLastname',
              EmployeeId: 2,
              relationshipWithEmployee: 'child'
            }
          ],
          deduction: 102
        }
      ])
  )
});
