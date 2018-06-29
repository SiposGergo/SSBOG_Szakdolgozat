import React from 'react';
import { Field, reduxForm } from 'redux-form';
import validate from './ForgottenPasswordValidate';
import { renderField } from "../RenderField";

const ForgottenPasswordForm = props => {
    const { handleSubmit, pristine, reset, submitting } = props;
    return (
        <form onSubmit={handleSubmit}>
            <div className="col-md-6 col-md-offset-3">
                <h2>{props.title}</h2>
                <p>
                    Ha elfelejtetted a jelszavad, az alábbi mezők kitöltése után emailben kapsz egy újat,
                    az új jelszavad biztonsági okokból az első belépéskor meg kell változtatnod!
                    </p>
                <Field
                    name="email"
                    type="text"
                    component={renderField}
                    label="E-mail"
                />
                <Field
                    name="userName"
                    type="text"
                    component={renderField}
                    label="Felhasználónév"
                />
                <div>
                    <button type="submit" disabled={submitting} className="btn btn-green">Elküld</button>
                </div>
            </div>
        </form>
    );
};

let form = reduxForm({
    form: 'ForgottenPasswordForm',
    validate,
})(ForgottenPasswordForm)

export default form;