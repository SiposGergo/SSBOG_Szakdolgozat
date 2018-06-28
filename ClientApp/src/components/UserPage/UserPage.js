import React from "react";
import { connect } from "react-redux";
import ReactLoading from "react-loading";
import UserRegistrations from "./UserRegistrations";
import UserOrganizedHikes from "./UserOrganizedHikes";
import {
    getUserById
} from "../../actions/UserPageActions";
import BasicUserData from "./BasicUserData";
import Card from "../Card";

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
        const BasicUserDataCard = (Card)(BasicUserData);
        const UserOrganizedHikesCard = (Card)(UserOrganizedHikes);
        const UserRegistrationsCard = (Card)(UserRegistrations);
        return (<div>
            <div className="row">
                <BasicUserDataCard user={user} title="Adatok" />
            </div>
            <div className="row">
                <UserOrganizedHikesCard organizedHikes={user.organizedHikes} title="Saját szervezésű túrák" />
            </div>
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
            error
        }
    }
}

export default connect(mapStateToProps)(UserPage);