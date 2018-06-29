import React from "react";
import { connect } from "react-redux";
import LoadSpinner from "../LoadSpinner";
import UserRegistrations from "./UserRegistrations";
import UserOrganizedHikes from "./UserOrganizedHikes";
import {
    getUserById
} from "../../actions/UserPageActions";
import BasicUserData from "./BasicUserData";
import Card from "../Card";
import { authentication } from "../../reducers/AuthenticationReducer";

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
            return <LoadSpinner />
        }
        const user = this.props.user;
        const BasicUserDataCard = (Card)(BasicUserData);
        const UserOrganizedHikesCard = (Card)(UserOrganizedHikes);
        const UserRegistrationsCard = (Card)(UserRegistrations);

        return (<div>
            <div className="row">
                <BasicUserDataCard user={user} title="Adatok" />
            </div>

            { this.props.loggedInUser.id == this.props.match.params.id && <div className="row">
                <UserOrganizedHikesCard organizedHikes={user.organizedHikes} title="Saját szervezésű túrák" />
            </div> }

            <div className="row">
                <UserRegistrationsCard registrations={user.registrations} title="Nevezések" />
            </div>
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
            error,
            loggedInUser : state.authentication.user
        }
    }
}

export default connect(mapStateToProps)(UserPage);