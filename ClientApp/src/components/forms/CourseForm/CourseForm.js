import React from 'react';
import { Field, FieldArray, reduxForm } from 'redux-form';
import validate from './CourseFormValidate';
import { renderField } from "../RenderField";
import Datepicker from "../../Datepicker.js"
import TimePicker from "../../TimePicker.js"
import TextAreaField from "../../TextAreaField";
import moment from "moment";
import { connect } from 'react-redux';
import { renderCheckpoints } from "./renderCheckpoints";

const CourseForm = props => {
  const { baseDate, change, handleSubmit, pristine, reset, submitting } = props;
  return (
    <form onSubmit={handleSubmit}>
      <div className="col-md-12">
        <h2>{props.title}</h2>

        <Field
          name="name"
          component={renderField}
          label="Táv neve"
          type="text"
        />

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

        <Field name="registerDeadline"
          label="Nevezési határidő"
          component={Datepicker}
          change={change}
          initDate={props.initialValues ? moment(props.initialValues.registerDeadline) : moment(baseDate)}
        />

        <Field name="beginningOfStart"
          label="A rajt kezdete"
          component={TimePicker}
          change={change}
          initTime={props.initialValues ? moment(props.initialValues.beginningOfStart) : moment(baseDate)}
        />

        <Field name="endOfStart"
          label="A rajt vége"
          component={TimePicker}
          change={change}
          initTime={props.initialValues ? moment(props.initialValues.endOfStart) : moment(baseDate)}
        />

        <Field name="limitTime"
          label="Szintidő"
          component={renderField}
          type="number"
        />

        <FieldArray change={change}
          name="checkPoints"
          baseDate={baseDate}
          component={renderCheckpoints}
          initialValues={props.initialValues} />

        <div>
          <button type="submit" disabled={submitting}>Elküld</button>
        </div>
      </div>
    </form>
  );
};

let form = reduxForm({
  form: 'CourseForm',
  validate,
  enableReinitialize: true
})(CourseForm)

export default form;