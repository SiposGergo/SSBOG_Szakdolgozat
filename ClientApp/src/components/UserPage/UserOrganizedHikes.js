import React from "react";
import { Link } from "react-router-dom";
import AddHikeHelperModal from "./AddHikeHelperModal";
import { connect } from "react-redux";
import {
    openModal as openModalAction,
    closeModal as closeModalAction,
    postAddHikeHelper,
    getCoursePdfInfo
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

    handleDownloadPdf = (id) => {
        this.props.dispatch(getCoursePdfInfo(id));
    }

    render() {
        return (
            <div>
                Saját rendezésű túrák:
                    {
                        this.props.organizedHikes.map((hike) =>
                            <div key={hike.id} >
                                <Link to={"/hike/" + hike.id} >
                                    {hike.name}
                                </Link>

                                <Link  to={"/hike/edit/" + hike.id} >
                                    Szerkesztés
                                </Link>

                                <Link  to={"/hike/add-course/" + hike.id} >
                                    Új táv
                                </Link>


                                <AddHikeHelperModal
                                    modalIsOpen={this.props.isModalOpen}
                                    closeModal={this.closeModal}
                                    hikeId={hike.id}
                                    onSubmit={this.handleSubmit}
                                />
                                <button onClick={this.openModal}>Segítő hozzáadása</button>

                                    {hike.courses && hike.courses.map((course) =>
                                        <div key={course.id} >
                                            <Link to={"/course/edit/" + hike.id + '/' + course.id} >
                                                {course.name}
                                            </Link>
                                            <button onClick={() => { this.handleDownloadPdf(course.id) }}>
                                                Nevezők listája (PDF)
                                            </button>
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

export default connect(mapStateToProps, null, null, {pure:false})(UserOrganizedHikes);