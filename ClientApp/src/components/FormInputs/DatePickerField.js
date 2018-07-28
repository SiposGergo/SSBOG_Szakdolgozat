import React from "react";
import { Field } from "redux-form";
import { DatePicker } from "redux-form-material-ui";
import moment from "moment";
import { config } from "../../helpers/config";

export const DatePickerField = props => (
  <div>
    <label htmlFor={props.name}>{props.label}</label>
    <Field
      name={props.name}
      component={DatePicker}
      cancelLabel="Kilépés"
      okLabel="Oké"
      formatDate={date => moment(date).format(config.dateFormat)}
      hintText={props.label}
    />
  </div>
);