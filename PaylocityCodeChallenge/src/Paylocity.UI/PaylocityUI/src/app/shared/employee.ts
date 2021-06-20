import { Dependent } from '../shared/dependent.model';

// export interface Employee {
//   employeeId?: number;
//   firstName: string;
//   lastName: string;
//   paycheck: number;
//   dependents: Dependent[];
// }

export class Employee {
  id?: number;
  name: string;
  lastname: string;
  deduction?: number;
  dependents: Dependent[];

  constructor(name: string, lastname: string, dependents: Dependent[]){
    this.name = name;
    this.lastname = lastname;
    this.dependents = dependents;
  }
}

