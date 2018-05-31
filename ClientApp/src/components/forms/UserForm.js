import React from 'react'
import { Field, reduxForm } from 'redux-form'
import Datepicker from "../Datepicker.js"
import { connect } from "react-redux";
import moment from 'moment';
import {renderField} from "./RenderField";
import {validate} from "./UserFormValidate";

export class UserForm extends React.Component {

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
        initialValues: state.authentication.user ? 
        state.authentication.user : 
        { 
            gender: "Male"
        }
    }
})(form);

export default form;