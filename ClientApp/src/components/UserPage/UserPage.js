import React from "react";
import { connect } from "react-redux";
import ReactLoading from "react-loading";
import { NavLink } from "react-router-dom";
import UserRegistrations from "./UserRegistrations";
import UserOrganizedHikes from "./UserOrganizedHikes";
import {
    getUserById
} from "../../actions/UserPageActions";


export class UserPage extends React.Component {

    componentDidMount() {
        this.props.dispatch(getUserById(this.props.match.params.id));
    }

    render() {
        {
            if (this.props.hasErrored) {
                return <p>Hiba! {this.props.error}</p>;
            }
        }

        if (this.props.isLoading) {
            return <ReactLoading type="spin" color="#000000" height={40} width={40} />
        }

        const user = this.props.user;
        return (<div>
            <p>{user.name}</p>
            <p>{user.userName}</p>
            <p>{user.email}</p>
            <p>{user.dateOfBirth}</p>

            <UserOrganizedHikes organizedHikes={user.organizedHikes} />
            <UserRegistrations registrations={user.registrations} />
        </div>)

    }
}

const mapStateToProps = (state) => {
    {
        const { user, isLoading, hasErrored, error, isModalOpen } = state.userPageReducer;
        return {
            user,
            isLoading,
            hasErrored,
            error
        }
    }
}

export default connect(mapStateToProps, null, null, { pure: false })(UserPage);