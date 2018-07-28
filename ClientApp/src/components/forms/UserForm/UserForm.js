import React from "react";
import { Field, reduxForm } from "redux-form";
import { DatePicker } from "redux-form-material-ui";
import { connect } from "react-redux";
import { renderField } from "../RenderField";
import { validate } from "./UserFormValidate";
import { DatePickerField } from "../../FormInputs/DatePickerField";
import { MuiThemeProvider } from "material-ui/styles";
import { theme } from "../../../helpers/mui-theme";

export class UserForm extends React.Component {
  render() {
    const { handleSubmit, pristine, reset, submitting, change } = this.props;
    return (
      <form onSubmit={handleSubmit}>
        <div className="col-md-6">
          <h2>{this.props.title}</h2>
          <Field name="name" type="text" component={renderField} label="Név" />
          <Field
            name="userName"
            type="text"
            component={renderField}
            label="Felhasználónév"
          />
          <Field
            name="email"
            type="text"
            component={renderField}
            label="E-mail cím"
          />

          <MuiThemeProvider muiTheme={theme}>
            <DatePickerField name="dateOfBirth" label="Születési dátum" />
          </MuiThemeProvider>
          <Field
            name="town"
            type="text"
            component={renderField}
            label="Település"
          />
          <Field
            name="phoneNumber"
            type="text"
            component={renderField}
            label="Telefonszám"
          />

          <div>
            <label>Nem</label>
            <div>
              <label>
                <Field
                  name="gender"
                  component="input"
                  type="radio"
                  value="Male"
                />{" "}
                Férfi
              </label>
              <br />
              <label>
                <Field
                  name="gender"
                  component="input"
                  type="radio"
                  value="Female"
                />{" "}
                Nő
              </label>
            </div>
          </div>
          <Field
            name="password"
            type="password"
            component={renderField}
            label="Jelszó"
          />
          <div>
            <button
              className="btn btn-green"
              type="submit"
              disabled={submitting}
            >
              {this.props.buttonText}
            </button>
          </div>
        </div>
      </form>
    );
  }
}

let form = reduxForm({
  form: "UserForm",
  validate
})(UserForm);

export default form;
