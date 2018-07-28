import React from "react";
import { Field, reduxForm } from "redux-form";
import validate from "./HikeFormValidate";
import { renderField } from "../RenderField";
import TextAreaField from "../../FormInputs/TextAreaField";
import { DatePickerField } from "../../FormInputs/DatePickerField";
import { MuiThemeProvider } from "material-ui/styles";
import { theme } from "../../../helpers/mui-theme";

const HikeForm = props => {
  const { change, handleSubmit, pristine, reset, submitting } = props;
  return (
    <MuiThemeProvider muiTheme={theme}>
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
            component={TextAreaField}
            label="Túra leírása"
            initValue={
              props.initialValues ? props.initialValues.description : ""
            }
            change={change}
          />
          <Field
            name="website"
            type="text"
            component={renderField}
            label="A túra weboldala"
          />

          <DatePickerField name="date" label="Dátum" />

          <div>
            <button
              type="submit"
              disabled={submitting}
              className="btn btn-green"
            >
              Elküld
            </button>
          </div>
        </div>
      </form>
    </MuiThemeProvider>
  );
};

let form = reduxForm({
  form: "HikeForm",
  validate,
  enableReinitialize: true
})(HikeForm);

export default form;
