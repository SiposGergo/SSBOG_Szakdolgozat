import React from "react";
import { NavLink } from "react-router-dom";
import AddHikeHelperModal from "./AddHikeHelperModal";
import { connect } from "react-redux";

import {
    openModal as openModalAction,
    closeModal as closeModalAction,
    postAddHikeHelper
} from "../../actions/UserPageActions";

class UserOrganizedHikes extends React.Component {

    openModal = (hikeId) => {
        this.props.dispatch(openModalAction());
    }

    closeModal = () => {
        this.props.dispatch(closeModalAction());
    }

    handleSubmit = (event) => {
        event.preventDefault();
        const userName = event.target.userName.value;
        const hikeId = event.target.hikeId.value
        this.props.dispatch(postAddHikeHelper(userName, hikeId))
    }

    render() {
        return (
            <div>
                Saját rendezésű túrák:
            {
                    this.props.organizedHikes.map((hike) =>
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


                            <AddHikeHelperModal
                                modalIsOpen={this.props.isModalOpen}
                                closeModal={this.closeModal}
                                hikeId = {hike.id}
                                onSubmit = {this.handleSubmit}
                            />
                            <button onClick={this.openModal}>Segítő hozzáadása</button>


                            {hike.courses && hike.courses.map((course) => <div key={course.id}>
                                <NavLink exact={true} to={"/course/edit/" + hike.id + '/' + course.id} activeClassName="is-active">
                                    {course.name}
                                </NavLink>
                            </div>)}
                        </div>
                    )}
            </div>
        )
    }
}

const mapStateToProps = (state) => {
    {
        return {
            isModalOpen: state.userPageReducer.isModalOpen
        }
    }
}

export default connect(mapStateToProps)(UserOrganizedHikes);