import React from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import moment from "moment";
import { userActions } from '../actions/UserActions';
import UserDataForm from './forms/UserDataForm';


class RegisterPage extends React.Component {

    state = {
        user: {
            name: '',
            userName: '',
            password: '',
            email: '',
            gender: 'Male',
            phoneNumber: '',
            town: "",
            dateOfBirth: moment().startOf('year')
        },
        submitted: false,
        calendarFocused: false
    };

    handleChange = (event) => {
        const { name, value } = event.target;
        const { user } = this.state;
        console.log(value);
        this.setState({
            user: {
                ...user,
                [name]: value
            }
        });
    }

    onDateChange = (date) => {
        setState({ user: { ...user, dateOfBirth: date } })
    }

    onFocusChange = (focused) => {
        this.setState({ calendarFocused: focused })
    }

    checkProperties = (obj) => {
        for (var key in obj) {
            if (!obj[key])
                return false;
        }
        return true;
    }

    handleSubmit = (event) => {
        event.preventDefault();

        this.setState({ submitted: true });
        const { user } = this.state;
        const { dispatch } = this.props;
        if (this.checkProperties(user)) {
            dispatch(userActions.register(user, this.props.history));
        }
    }

    render() {
        return (
            <form name="form" onSubmit={this.handleSubmit}>
                <UserDataForm
                    handleChange = {this.handleChange}
                    registering={this.props.registering}
                    state={this.state}
                    onDateChange={this.onDateChange}
                    onFocusChange={this.onFocusChange}
                />
                <div className="col-md-6 col-md-offset-3">
                    <button className="btn btn-primary">Regisztráció</button>
                    {this.props.registering &&
                        <img src="data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA==" />
                    }
                    <Link to="/login" className="btn btn-link">Mégse</Link>
                </div>
            </form>)
    }
}

function mapStateToProps(state) {
    const { registering } = state.registration;
    return {
        registering
    };
}

const connectedRegisterPage = connect(mapStateToProps)(RegisterPage);
export { connectedRegisterPage as RegisterPage };