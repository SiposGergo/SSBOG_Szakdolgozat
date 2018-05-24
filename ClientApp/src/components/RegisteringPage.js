import React from "react";
import UserForm from "./forms//UserForm";
import { userActions } from "../actions/UserActions";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

class RegisterPage extends React.Component {

    handleSubmit = (values) => {
        this.props.register({ values, history: this.props.history });
    }

    render() {
        return (
            <div>
                <UserForm onSubmit={this.handleSubmit} buttonText = "Regisztráció" title="Regisztáció" />
            </div>
        )
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        register: (params) => { dispatch(userActions.register(params.values, params.history)) }
    }
}

export default connect(null, mapDispatchToProps)(RegisterPage);