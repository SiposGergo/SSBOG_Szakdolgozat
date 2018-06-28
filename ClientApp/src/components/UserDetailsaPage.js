import React from 'react';
import { connect } from 'react-redux';
import { userActions } from '../actions/UserActions';
import UserForm from "./forms/UserForm/UserForm";
import ChangePasswordForm from "./forms/ChangePasswordForm/ChangePasswordForm";

class UserDetailsPAge extends React.Component {

    handleSubmit = (values) => {
        const { dispatch } = this.props;
        dispatch(userActions.update(values));
    }

    handlePasswordChange = (values) => {
        console.log(values);
        this.props.dispatch(userActions.changePassword(values));
    }

    render() {
        return (
            <div>
                <ChangePasswordForm
                    onSubmit={this.handlePasswordChange}
                    title="Jelszó megváltoztatása" />
                <UserForm
                    onSubmit={this.handleSubmit}
                    buttonText="Elküld"
                    data={this.props.user}
                    title="Adataim" />
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