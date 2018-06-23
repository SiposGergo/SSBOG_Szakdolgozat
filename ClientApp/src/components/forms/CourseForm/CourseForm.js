import React from 'react';
import { Field, FieldArray, reduxForm } from 'redux-form';
import validate from './CourseFormValidate';
import CourseDataFields from "./CourseDataFields";
import { renderCheckpoints } from "./renderCheckpoints";
import Card from "../../Card";

const CourseForm = props => {
  const { baseDate, change, handleSubmit, pristine, reset, submitting } = props;
  const CourseDataFieldsCard = (Card)(CourseDataFields);
  return (
    <form onSubmit={handleSubmit}>

      <CourseDataFieldsCard
        change={change}
        baseDate={baseDate}
        title={props.title} />

      <FieldArray change={change}
        name="checkPoints"
        baseDate={baseDate}
        component={renderCheckpoints}
        initialValues={props.initialValues}
        submitting = {submitting} />

      <div>
        
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