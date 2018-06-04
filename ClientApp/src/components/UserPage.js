import React from "react";
import { connect } from "react-redux";
import { getUserById } from "../actions/UserPageActions";
import ReactLoading from "react-loading";
import { NavLink } from "react-router-dom";

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
            Előnevezések:
            {
                user.registrations.map((reg) =>
                    <div key={reg.id}>
                        <NavLink exact={true} to={"/hike/" + reg.hikeCourse.hikeId} activeClassName="is-active">
                            {reg.hikeCourse.name}
                        </NavLink>
                        Rajtszám: {reg.startNumber}
                    </div>
                )}

            Saját rendezésű túrák:
            {
                user.organizedHikes.map((hike) =>
                    <div key={hike.id}>

                        <NavLink exact={true} to={"/hike/" + hike.id} activeClassName="is-active">
                            {hike.name}
                        </NavLink>

                        <NavLink exact={true} to={"/hike/edit/" + hike.id} activeClassName="is-active">
                            Szerkesztés
                        </NavLink>

                        <NavLink exact={true} to={"/hike/add-course/" + hike.id} activeClassName="is-active">
                            Új táv
                        </NavLink>
                        {hike.courses && hike.courses.map((course) => <div key={course.id}>
                            <NavLink exact={true} to={"/course/edit/" + course.id} activeClassName="is-active">
                                {course.name}
                            </NavLink>
                        </div>)}
                    </div>
                )}
        </div>)

    }
}

const mapStateToProps = (state) => {
    {
        const { user, isLoading, hasErrored, error } = state.userPageReducer;
        return {
            user,
            isLoading,
            hasErrored,
            error
        }
    }
}

export default connect(mapStateToProps)(UserPage);