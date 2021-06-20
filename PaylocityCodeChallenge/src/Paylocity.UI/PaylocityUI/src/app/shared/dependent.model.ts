
export class Dependent {
  public id?: number;
  public name: string;
  public lastname: string;
  public relationshipWithEmployee: string;
  public EmployeeId?: number;

  constructor(name: string, lastname: string, relationship: string){
    this.name = name;
    this.lastname = lastname;
    this.relationshipWithEmployee = relationship;
  }
}
