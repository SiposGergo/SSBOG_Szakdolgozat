import React from 'react';
import { Field, FieldArray, reduxForm } from 'redux-form';
import validate from './HikeFormValidate';
import {renderField} from "./RenderField";
import Datepicker from "../Datepicker.js"
import moment from "moment";
import { connect } from 'react-redux';


const HikeForm = props => {
  const { change, handleSubmit, pristine, reset, submitting } = props;
  return (
    <form onSubmit={handleSubmit}>
    <div className="col-md-6 col-md-offset-3">
    <h2>{props.title}</h2>
      <Field
        name="name"
        type="text"
        component={renderField}
        label="Túra neve"
      />
      <Field
        name="description"
        type="text"
        component={renderField}
        label="Túra leírása"
      />
      <Field
        name="website"
        type="text"
        component={renderField}
        label="A túra weboldala"
      />
      <Field name="date" label="Dátum" component={Datepicker} change={change}
                        initDate={props.initialValues.date ? moment(props.initialValues.date) : moment()} 
        />
      <div>
        <button type="submit" disabled={submitting}>Elküld</button>
      </div>
      </div>
    </form>
  );
};

let form =  reduxForm({
  form: 'HikeForm',
  validate,
  enableReinitialize : true
})(HikeForm)

export default form;