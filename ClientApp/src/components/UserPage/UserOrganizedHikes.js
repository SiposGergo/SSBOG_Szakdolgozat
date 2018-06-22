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
                    {
                        this.props.organizedHikes.map((hike) =>
                            <div key={hike.id} className="own-hike-item" >
                                <h3>{hike.name}</h3>
                                <Link to={"/hike/" + hike.id} >
                                    <button className="btn btn-green">Túra adatlapja</button>
                                </Link>

                                <Link  to={"/hike/edit/" + hike.id} >
                                <button className="btn btn-green">Szerkesztés</button>
                                </Link>

                                <Link  to={"/hike/add-course/" + hike.id} >
                                <button className="btn btn-green">Új táv hozzáadása</button>
                                </Link>


                                <AddHikeHelperModal
                                    modalIsOpen={this.props.isModalOpen}
                                    closeModal={this.closeModal}
                                    hikeId={hike.id}
                                    onSubmit={this.handleSubmit}
                                />
                                <button className="btn btn-green" onClick={this.openModal}>Segítő hozzáadása</button>

                                    {hike.courses && hike.courses.map((course) =>
                                        <div key={course.id} >
                                            <h4>{course.name}</h4>
                                            <Link to={"/course/edit/" + hike.id + '/' + course.id} >
                                                <button className="btn btn-green">Szerkesztés</button>
                                            </Link>
                                            <button className="btn btn-green" onClick={() => { this.handleDownloadPdf(course.id) }}>
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