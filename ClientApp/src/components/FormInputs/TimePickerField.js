import React from "react";
import { Field } from "redux-form";
import { TimePicker } from "redux-form-material-ui";

export const TimePickerField = props => (
  <div>
    <label htmlFor={props.name}>{props.label}</label>
    <Field
      name={props.name}
      props={{ format: "24hr" }}
      component={TimePicker}
      cancelLabel="Kilépés"
      okLabel="Oké"
      minutesStep={5}
      hintText={props.label}
    />
  </div>
);
