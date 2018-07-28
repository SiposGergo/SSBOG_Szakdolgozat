import React from "react";
import UserForm from "./forms/UserForm/UserForm";
import { userActions } from "../actions/UserActions";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";

class RegisterPage extends React.Component {
  handleSubmit = values => {
    const val = {...values};
    val.dateOfBirth = new Date(val.dateOfBirth.getTime() - val.dateOfBirth.getTimezoneOffset()*60000); console.log(val);
    this.props.register({ val, history: this.props.history });
  };

  render() {
    return (
      <div>
        <UserForm
          onSubmit={this.handleSubmit}
          buttonText="Regisztráció"
          title="Regisztáció"
          initialValues={{ gender: "Male", dateOfBirth: new Date() }}
        />
      </div>
    );
  }
}

const mapDispatchToProps = dispatch => {
  return {
    register: params => {
      dispatch(userActions.register(params.val, params.history));
    }
  };
};

export default connect(
  null,
  mapDispatchToProps
)(RegisterPage);
