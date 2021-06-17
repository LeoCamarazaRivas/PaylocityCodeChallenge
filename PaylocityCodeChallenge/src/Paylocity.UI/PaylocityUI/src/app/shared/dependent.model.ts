
export class Dependent {
  public dependentId?: number;
  public firstName: string;
  public lastName: string;
  public relationship: string;

  constructor(firstName: string, lastName: string, relationship: string){
    this.firstName = firstName;
    this.lastName = lastName;
    this.relationship = relationship;
  }
}
