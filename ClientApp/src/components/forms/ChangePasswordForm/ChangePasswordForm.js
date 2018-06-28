import React from 'react';
import { Field, reduxForm } from 'redux-form';
import validate from './ChangePasswordValidate';
import { renderField } from "../RenderField";

const ChangePasswordForm = props => {
    const { change, handleSubmit, pristine, reset, submitting } = props;
    return (
        <form onSubmit={handleSubmit}>
            <div className="col-md-6 col-md-offset-3">
                <h2>{props.title}</h2>
                <Field
                    name="currentPassword"
                    type="password"
                    component={renderField}
                    label="Jelenlegi jelszó"
                />
                <Field
                    name="newPassword"
                    type="password"
                    component={renderField}
                    label="Új jelszó"
                />
                <div>
                    <button type="submit" disabled={submitting} className="btn btn-green">Elküld</button>
                </div>
            </div>
        </form>
    );
};

let form = reduxForm({
    form: 'ChangePasswordForm',
    validate,
})(ChangePasswordForm)

export default form;