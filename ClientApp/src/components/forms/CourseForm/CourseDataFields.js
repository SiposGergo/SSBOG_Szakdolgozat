import React from "react";
import { renderField } from "../RenderField";
import { Field } from "redux-form";
import {TimePickerField} from "../../FormInputs/TimePickerField";
import {DatePickerField} from "../../FormInputs/DatePickerField";

const CourseDataFields = props => (
  <div className="row">
    <div className="col-md">
      <Field name="name" component={renderField} label="Táv neve" type="text" />

      <Field
        name="placeOfStart"
        type="text"
        component={renderField}
        label="A Start helye"
      />

      <Field
        name="placeOfFinish"
        type="text"
        component={renderField}
        label="A cél helye"
      />

      <Field
        name="distance"
        type="number"
        component={renderField}
        label="Távolság (méterben)"
      />
    </div>
    <div className="col-md">
      <Field
        name="elevation"
        type="number"
        component={renderField}
        label="Szintemelkedés (méterben)"
      />

      <Field
        name="price"
        type="number"
        component={renderField}
        label="Nevezési díj"
      />

      <Field
        name="maxNumOfHikers"
        type="number"
        component={renderField}
        label="Nevezési létszámkorlát (fő)"
      />

      <Field
        name="limitTime"
        label="Szintidő"
        component={renderField}
        type="number"
      />
    </div>
    <div className="col-md">
      <TimePickerField name="beginningOfStart" label="A rajt kezdete" />
      <TimePickerField name="endOfStart" label="A rajt vége" />
      <DatePickerField name="registerDeadline" label="Nevezési határidő" />
    </div>
  </div>
);

export default CourseDataFields;
