import React from 'react'
import { Field, reduxForm } from 'redux-form'
import validator from "validator";
import Datepicker from "../Datepicker.js"

const validate = values => {
    const errors = {}

    if (!values.userName) {
        errors.userName = 'Felhasználónév megadása szükséges.'
    }

    if (!values.name) {
        errors.name = 'Név megadása szükséges.'
    }

    if (!values.email) {
        errors.email = 'E-mail cím megadása szükséges.'
    } else if (!validator.isEmail(values.email)) {
        errors.email = 'Érvénytelen e-mail cím.'
    }

    if (!values.town) {
        errors.town = 'Település megadása szükséges, a lakhelyed csak az eredménylistákban fog szerepelni.'
    }

    if (!values.phoneNumber) {
        errors.phoneNumber = 'Telefonszám megadása szükséges, hogy a túra szervezők veszélyhelyzetben elérhessenek.'
    } else if (!validator.isMobilePhone(values.phoneNumber, "hu-HU")) {
        errors.phoneNumber = "Érvénytelen telefonszám, helyes formátum: +36301234567"
    }

    if (!values.gender) {
        errors.gender = 'Nem megadása szükséges.'
    }

    if (!values.password) {
        errors.password = 'Jelszó megadása szükséges.'
    }

    return errors
}

const renderField = ({ input, label, type, meta: { touched, error, warning } }) => (
    <div>
        <label>{label}</label>
        <div>
            <input {...input} placeholder={label} type={type} />
            {touched && (error && <span>{error}</span>)}
        </div>
    </div>
)

const UserForm = (props) => {
    const { handleSubmit, pristine, reset, submitting, change } = props
    return (
        <form onSubmit={handleSubmit}>
            <Field name="name" type="text" component={renderField} label="Név:" />
            <Field name="userName" type="text" component={renderField} label="Felhasználónév" />
            <Field name="email" type="text" component={renderField} label="E-mail cím" />
            <Field name="dateOfBirth" label="Születési Dátum" component={Datepicker} change={change} />
            <Field name="town" type="text" component={renderField} label="Település" />
            <Field name="phoneNumber" type="text" component={renderField} label="Telefonszám" />

            <div>
                <label>Nem</label>
                <div>
                    <label>
                        <Field name="gender" component="input" type="radio" value="Male" />
                        {' '}
                        Férfi
              </label>
                    <label>
                        <Field name="gender" component="input" type="radio" value="Female" />
                        {' '}
                        Nő
              </label>
                </div>
            </div>
            <Field name="password" type="password" component={renderField} label="Jelszó" />
            <div>
                <button type="submit" disabled={submitting}>{props.buttonText}</button>
            </div>

        </form>
    )
}

export default reduxForm({
    form: 'UserForm',
    validate
})(UserForm)