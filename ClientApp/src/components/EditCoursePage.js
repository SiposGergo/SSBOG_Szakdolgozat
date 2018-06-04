import React from 'react';
import { connect } from 'react-redux';
import { getCourseDetails, postEditCourse, deleteData } from '../actions/EditCourseActions';
import CourseForm from "./forms/CourseForm/CourseForm"
import moment from "moment";

class EditCoursePage extends React.Component {

    componentWillMount() {
        this.props.dispatch(getCourseDetails(this.props.match.params.id));
    }

    componentWillUnmount() {
        this.props.dispatch(deleteData());
    }

    handleSubmit = (values) => {
        this.props.dispatch(postEditCourse(values, this.props.match.params.id));
    }

    render() {

        const course = this.props.course;
        /* if (course.registerDeadline) {course.registerDeadline = moment(course.registerDeadline)}
        if (course.beginningOfStart) {course.beginningOfStart = moment(course.beginningOfStart)}
        if (course.endOfStart) course.endOfStart = moment(course.endOfStart);
        if(course.checkPoints) {
            course.checkPoints.forEach((checkPoint) => {
                checkPoint.open = moment(checkPoint.open)
                checkPoint.close = moment(checkPoint.close)
            });
        } */


        if (this.props.hasErrored) {
            return (<div>Nincs ilyen túra táv!</div>);
        }

        /* if(this.props.hike.organizer && this.props.hike.organizer.id != this.props.user.id){
            return (<div>Csak a saját szervezésű túráid adatait tudod szerkeszteni!</div>)
        }
        else{ */

        return (
            <div>
                <CourseForm
                    onSubmit={this.handleSubmit}
                    title="Túra adatai"
                    initialValues={course}
                />
            </div>
        )
        /* } */
        return (<div></div>)

    }
}

function mapStateToProps(state) {
    return {
        course: state.courseEditReducer.course,
        hasErrored: state.courseEditReducer.hasErrored
    };
}

export default connect(mapStateToProps)(EditCoursePage);