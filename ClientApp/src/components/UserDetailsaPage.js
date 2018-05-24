import React from 'react';
import { connect } from 'react-redux';
import { userActions } from '../actions/UserActions';
import UserForm from "./forms/UserForm"


class UserDetailsPAge extends React.Component {

    handleSubmit = (values) => {
        /* const { user } = this.state;
        const { dispatch } = this.props;
        dispatch(userActions.register(values, this.props.history)); */
    }

    render() {
        return (
            <div>
                <UserForm onSubmit={this.handleSubmit} buttonText="Elküld" data={this.props.user} />
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