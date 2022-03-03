import { Inject, Injectable, Optional } from '@angular/core';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import * as moment from 'moment';
import { Moment } from 'moment';

@Injectable()
export class StrictMomentDateAdapter extends MomentDateAdapter {
  constructor( @Optional() @Inject(MAT_DATE_LOCALE) dateLocale: string) {
    super(dateLocale);
  }

  parse(value: any, parseFormat: string | string[]): Moment | null {
    if (value === null || value === '') {
      return null;
    }

    // use strict mode when parsing the date
    const date = moment(value, parseFormat, this.locale, true);
    return date.isValid() ? date : this.invalid();
  }
}
