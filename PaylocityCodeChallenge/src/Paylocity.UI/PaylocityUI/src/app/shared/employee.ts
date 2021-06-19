import { Dependent } from '../shared/dependent.model';

// export interface Employee {
//   employeeId?: number;
//   firstName: string;
//   lastName: string;
//   paycheck: number;
//   dependents: Dependent[];
// }

export class Employee {
  public deduction!:number;
  public firstName: string;
  public lastName: string;
  public dependents: Dependent[];

  constructor(firstName: string, lastName: string, dependents: Dependent[]){
    this.firstName = firstName;
    this.lastName = lastName;
    this.dependents = dependents;
  }
}

