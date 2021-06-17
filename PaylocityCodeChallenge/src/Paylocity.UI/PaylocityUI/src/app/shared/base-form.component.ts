import { Component } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-base-form',
  template: '',
  styles: [
  ]
})
export abstract class BaseFormComponent {

  // shared form model
  form!: FormGroup;

  constructor(){}

  // retrieve a FormControl
  getControl(name: string){
    return this.form.get(name);
  }

  // return TRUE if the FormControl is valid
  isValid(name: string) {
    let fc = this.getControl(name);
    return fc && fc.valid;
  }

  // returns TRUE if the FormControl has been changed
  isChanged(name: string) {
    let fc = this.getControl(name);
    return fc && (fc.dirty || fc.touched);
  }

  // returns TRUE if the FormControl is raising an error
  hasError(name: string) {
    let fc = this.getControl(name);
    return fc && fc.invalid && (fc.dirty || fc.touched);
  }

}
