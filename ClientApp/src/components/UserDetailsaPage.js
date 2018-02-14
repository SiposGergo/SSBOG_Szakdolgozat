import React from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import moment from "moment";
import { userActions } from '../actions/UserActions';
import UserDataForm from './forms/UserDataForm';


class UserDetailsPAge extends React.Component {

    state = {
        user: { ...this.props.user, dateOfBirth: moment(this.props.user.dateOfBirth) },
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
            <div>
                <form name="form" onSubmit={this.handleSubmit}>
                    <UserDataForm
                        handleChange={this.handleChange}
                        registering={this.props.registering}
                        state={this.state}
                        onDateChange={this.onDateChange}
                        onFocusChange={this.onFocusChange}
                    />
                    <div className="col-md-6 col-md-offset-3">
                        <button className="btn btn-primary">Mentés</button>
                        <div>
                            Add meg a jelszavad az adatok mentséhez.
                        </div>
                    </div>
                </form>
            </div>
        )
    }
}

function mapStateToProps(state) {
    return {
        user: state.authentication.user
    };
}

const connectedUserDetailsPAge = connect(mapStateToProps)(UserDetailsPAge);
export { connectedUserDetailsPAge as UserDetailsPage };