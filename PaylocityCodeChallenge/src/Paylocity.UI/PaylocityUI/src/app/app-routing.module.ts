import { EmployeeDetailsComponent } from './employee/employee-details/employee-details/employee-details.component';
import { HomeComponent } from './home/home.component';
import { EmployeeComponent } from './employee/employee.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full'},
  { path:'home', component: HomeComponent },
  { path: 'employee', component: EmployeeComponent },
  { path: 'employee/:id', component: EmployeeComponent },
  { path:'employeeDetails', component: EmployeeDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
