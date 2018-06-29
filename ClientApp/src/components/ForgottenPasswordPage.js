import React from 'react';
import { connect } from 'react-redux';
import { userActions } from '../actions/UserActions';
import ForgottenPasswordForm from "./forms/ForgottenPassword/ForgottenPasswordForm";

class ForgottenPasswordPage extends React.Component {

    handleSubmit = (values) => {
        this.props.dispatch(userActions.forgottenPassword(values));
    }

    render() {
        if (this.props.user) {return (<div><h3>Üdv, {this.props.user.name}!</h3></div>)}
        else {
            return (
                <div>
                    <ForgottenPasswordForm
                        onSubmit={this.handleSubmit}
                        title="Elfelejtett jelszó" />
                </div>
            )
        }
    }
}

function mapStateToProps(state) {
    return {
        user: state.authentication.user
    };
}

const ConnectedForgottenPasswordPage = connect(mapStateToProps)(ForgottenPasswordPage);
export default ConnectedForgottenPasswordPage;