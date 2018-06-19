import React from "react";
import moment from "moment";
import CheckpointList from "./CheckpointList";
import {connect} from "react-redux"
import {config} from "../../helpers/config.js";
import {postRegister,postUnRegister} from "../../actions/HikeDetailsActions"

class CourseDetails extends React.Component {

  isPossibleToRegister = () => {
        let value = true;
        if (!this.props.user  ) {
            value = false;
        }
        else 
        {
            this.props.user.registrations.forEach((reg) => {
                if (reg.hikeCourseId ==this.props.course.id) {
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
        if (!this.props.user  ) {
            value1 = false;
        }
        else 
        {
            this.props.user.registrations.forEach((reg) => {
                if (reg.hikeCourseId ==this.props.course.id) {
                    value1 = true;
                }
            })
        }
        return value1
    }

    onRegisterClick = () => {
        this.props.dispatch(postRegister(this.props.course.id, this.props.user.id,this.props.hikeId));
    }

    onUnRegisterClick = () => {
        this.props.dispatch(postUnRegister(this.props.course.id, this.props.user.id,this.props.hikeId));
    }

    render() {
        const course = this.props.course;

        const isRegisterButtonEnabled = this.isPossibleToRegister();
        const isUnregisterButtonEnabled = this.isPossibleToUnregister();
        const registratedPercent = ((course.numOfRegisteredHikers / course.maxNumOfHikers)*100).toString()+"%";
        
        return (
            <div>
                { 
                    isRegisterButtonEnabled &&
                    <button className="btn btn-info" onClick={this.onRegisterClick}>
                        Előnevezés
                    </button>
                }

                { 
                    isUnregisterButtonEnabled &&
                    <button className="btn btn-danger" onClick={this.onUnRegisterClick}> 
                        Előnevezés visszavonása
                    </button>
                }

                <p>{course.name}</p>
                <p>Nevezési díj:{course.price}</p>
                <p>Táv:{course.distance}</p>
                <p>Szint emelkedés:{course.elevation}</p>
                <p>Rajt:{course.placeOfStart}</p>
                <p>Cél:{course.placeOfFinish}</p>
                <p>Rajt ideje:{moment(course.beginningOfStart).format(config.dateTimeFormat)}
                    - {moment(course.endOfStart).format(config.dateTimeFormat)}</p>
                <p>Szintidő:{course.limitTime}</p>
                <p>Létszámkorlát:{course.maxNumOfHikers}</p>
                <p>{course.numOfRegisteredHikers}/{course.maxNumOfHikers}</p>
                <div className="progress">
                     <div className="progress-bar" style={{width:registratedPercent}}></div>
                </div>
                <p>Nevezési határidő: :{moment(course.registerDeadline).format(config.dateTimeFormat)}</p>
                <CheckpointList checkpoints={course.checkPoints} />
            </div>
        )
    }
}

const mapStateToProps = (state) => {
    return {
        user: state.authentication.user
    }
    
}
export default connect(mapStateToProps, null, null, {pure:false})(CourseDetails);