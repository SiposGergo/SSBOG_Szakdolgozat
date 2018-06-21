import React from "react";
import moment from "moment";
import CheckpointList from "./CheckpointList";
import { connect } from "react-redux"
import { postRegister, postUnRegister } from "../../actions/HikeDetailsActions"
import Card from "../Card";
import CourseDetailsFirstCard from "./CourseDetailsFirstCard";

class CourseDetails extends React.Component {

    isPossibleToRegister = () => {
        let value = true;
        if (!this.props.user) {
            value = false;
        }
        else {
            this.props.user.registrations.forEach((reg) => {
                if (reg.hikeCourseId == this.props.course.id) {
                    value = false;
                }
            })
        }
        const deadline = moment(this.props.course.registerDeadline);
        const now = moment();
        if (now > deadline) {
            value = false;
        }
        return value;
    }

    isPossibleToUnregister = () => {
        let value1 = false;
        if (!this.props.user) {
            value1 = false;
        }
        else {
            this.props.user.registrations.forEach((reg) => {
                if (reg.hikeCourseId == this.props.course.id) {
                    value1 = true;
                }
            })
        }
        return value1
    }

    onRegisterClick = () => {
        this.props.dispatch(postRegister(this.props.course.id, this.props.user.id, this.props.hikeId));
    }

    onUnRegisterClick = () => {
        this.props.dispatch(postUnRegister(this.props.course.id, this.props.user.id, this.props.hikeId));
    }

    render() {
        const course = this.props.course;

        const isRegisterButtonEnabled = this.isPossibleToRegister();
        const isUnregisterButtonEnabled = this.isPossibleToUnregister();
        const Card1 = (Card)(CourseDetailsFirstCard);
        const Card2 = (Card)(CheckpointList)

        return (
            <div>
                <div className="row">
                    <Card1 title="Táv adatai" course={course} />
                    <Card2 title="Ellenőrzőpontok" checkpoints={course.checkPoints}/>
                </div>
                {
                    isRegisterButtonEnabled &&
                    <button className="btn btn-green" onClick={this.onRegisterClick}>
                        Előnevezés
                    </button>
                }

                {
                    isUnregisterButtonEnabled &&
                    <button className="btn btn-danger" onClick={this.onUnRegisterClick}>
                        Előnevezés visszavonása
                    </button>
                }
            </div>
        )
    }
}

const mapStateToProps = (state) => {
    return {
        user: state.authentication.user
    }

}
export default connect(mapStateToProps, null, null, { pure: false })(CourseDetails);