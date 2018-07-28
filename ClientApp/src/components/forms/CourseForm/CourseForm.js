import React from "react";
import { Field, FieldArray, reduxForm } from "redux-form";
import validate from "./CourseFormValidate";
import CourseDataFields from "./CourseDataFields";
import { renderCheckpoints } from "./renderCheckpoints";
import Card from "../../Card";
import { MuiThemeProvider } from "material-ui/styles";
import { theme } from "../../../helpers/mui-theme";

const CourseForm = props => {
  const { baseDate, change, handleSubmit, pristine, reset, submitting } = props;
  const CourseDataFieldsCard = Card(CourseDataFields);

  return (
    <MuiThemeProvider muiTheme={theme}>
      <form onSubmit={handleSubmit}>
        <CourseDataFieldsCard
          change={change}
          baseDate={baseDate}
          title={props.title}
          initialValues={props.initialValues}
        />

        <FieldArray
          change={change}
          name="checkPoints"
          baseDate={baseDate}
          component={renderCheckpoints}
          initialValues={props.initialValues}
          submitting={submitting}
        />

        <div />
      </form>
    </MuiThemeProvider>
  );
};

let form = reduxForm({
  form: "CourseForm",
  validate,
  enableReinitialize: true
})(CourseForm);

export default form;
