import { Dependent } from '../shared/dependent.model';

export interface Employee {
  id?: number;
  name: string;
  lastname: string;
  deduction?: number;
  dependents: Dependent[];
}
