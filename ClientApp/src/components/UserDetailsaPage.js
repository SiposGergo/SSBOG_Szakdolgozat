import React from 'react';
import { connect } from 'react-redux';
import { userActions } from '../actions/UserActions';
import UserForm from "./forms/UserForm/UserForm"


class UserDetailsPAge extends React.Component {

    handleSubmit = (values) => {
        const { dispatch } = this.props;
        dispatch(userActions.update(values));
    }

    render() {
        return (
            <div>
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