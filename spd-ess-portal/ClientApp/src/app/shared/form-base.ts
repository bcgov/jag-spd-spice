import { AbstractControl, FormGroup, ValidatorFn } from '@angular/forms';
import { Moment } from 'moment';

export class FormBase {
    form: FormGroup;

    public dateRangeValidator(startDate: Moment, endDate: Moment): ValidatorFn {
      return (control: AbstractControl): { [key: string]: any } | null => {
        const date = control.value;

        if (date === '' || date === null) {
          return null;
        } else if (date.isBefore(startDate)) {
          return { 'beforeStart': { value: control.value } };
        } else if (date.isAfter(endDate)) {
          return { 'afterEnd': { value: control.value } };
        } else {
          return null;
        }
      };
    }

    public notEqualFieldValidator(otherFieldName: string): ValidatorFn {
      return (control: AbstractControl): { [key: string]: any } | null => {
        if (!control.root) {
          return null;
        }

        const otherField = control.root.get(otherFieldName);
        if (!otherField) {
          return null;
        }

        if (control.value.toUpperCase() === otherField.value.toUpperCase()) {
          return { 'equal': { value: control.value }};
        }

        return null;
      };
    }
}
