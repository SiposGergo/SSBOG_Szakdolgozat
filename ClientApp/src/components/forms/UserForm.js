import React from 'react'
import { Field, reduxForm } from 'redux-form'
import validator from "validator";
import Datepicker from "../Datepicker.js"
import { connect } from "react-redux";
import moment from 'moment';


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
    <div className={"form-group " + (touched && error && "has-error")}>
        <label>{label}</label>
        <div>
            <input className="form-control" {...input} placeholder={label} type={type} />
            {touched && (error && <div className="help-block">{error}</div>)}
        </div>
    </div>
)

class UserForm extends React.Component {

    render() {
        const { handleSubmit, pristine, reset, submitting, change } = this.props;
        return (
            <form onSubmit={handleSubmit}>
                <div className="col-md-6 col-md-offset-3">
                    <h2>{this.props.title}</h2>
                    <Field name="name" type="text" component={renderField} label="Név:" />
                    <Field name="userName" type="text" component={renderField} label="Felhasználónév" />
                    <Field name="email" type="text" component={renderField} label="E-mail cím" />
                    <Field name="dateOfBirth" label="Születési Dátum" component={Datepicker} change={change}
                        initDate={this.props.data ? moment(this.props.data.dateOfBirth) : moment()} />
                    <Field name="town" type="text" component={renderField} label="Település" />
                    <Field name="phoneNumber" type="text" component={renderField} label="Telefonszám" />

                    <div>
                        <label>Nem</label>
                        <div >
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
                        <button className="btn btn-info" type="submit" disabled={submitting}>{this.props.buttonText}</button>
                    </div>
                </div>
            </form>
        )
    }
}



let form = reduxForm({
    form: 'UserForm',
    validate
})(UserForm)


form = connect(state => {
    return {
        initialValues: state.authentication.user ? state.authentication.user : { gender: "Male" }
    }
})(form);

export default form;