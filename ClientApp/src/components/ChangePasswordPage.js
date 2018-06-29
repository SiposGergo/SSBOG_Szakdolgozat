import React from "react";
import { connect } from 'react-redux';
import { userActions } from '../actions/UserActions';
import ChangePasswordForm from "./forms/ChangePasswordForm/ChangePasswordForm";

class ChangePasswordPage extends React.Component {

    handlePasswordChange = (values) => {
        console.log(values);
        this.props.dispatch(userActions.changePassword(values));
    }

    componentWillUnmount() {
        if (this.props.user.mustChangePassword) {
            this.props.dispatch(userActions.logout())
        }
    }

    render() {
        return (
            <div>
                <ChangePasswordForm
                    onSubmit={this.handlePasswordChange}
                    title="Jelszó megváltoztatása" />
            </div>)
    }
}

function mapStateToProps(state) {
    return {
        user: state.authentication.user
    };
}

const connetced = connect(mapStateToProps)(ChangePasswordPage);
export default connetced;