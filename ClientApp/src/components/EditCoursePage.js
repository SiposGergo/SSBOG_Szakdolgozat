import React from "react";
import { connect } from "react-redux";
import { postEditCourse } from "../actions/EditCourseActions";
import {
  getHikeDetails,
  deleteData as deleteHikeData
} from "../actions/EditHikeActions";
import CourseForm from "./forms/CourseForm/CourseForm";
import moment from "moment";
import { convertTimeZone } from "../helpers/convertTimeZone";

class EditCoursePage extends React.Component {
  componentWillMount() {
    this.props.dispatch(getHikeDetails(this.props.match.params.hikeId));
  }

  componentWillUnmount() {
    this.props.dispatch(deleteHikeData());
  }

  handleSubmit = values => {
    const val = convertTimeZone(values);
    this.props.dispatch(postEditCourse(val, this.props.match.params.courseId));
  };

  render() {
    const course = this.props.course;

    if (this.props.hasErrored) {
      return <div>Nincs ilyen túra táv!</div>;
    }

    if (
      this.props.hike.organizer &&
      this.props.hike.organizer.id != this.props.user.id
    ) {
      return (
        <div>Csak a saját szervezésű túráid adatait tudod szerkeszteni!</div>
      );
    }

    if (this.props.hike.courses) {
      const filtered = this.props.hike.courses.filter(
        course => course.id == this.props.match.params.courseId
      );
      if (!filtered.length) {
        return <div>Nincs ilyen táv!</div>;
      } else {
        filtered[0].beginningOfStart = new Date(filtered[0].beginningOfStart);
        filtered[0].endOfStart = new Date(filtered[0].endOfStart);
        filtered[0].registerDeadline = new Date(filtered[0].registerDeadline);
        filtered[0].checkPoints.forEach(x => {
          x.close = new Date(x.close);
          x.open = new Date(x.open);
        });

        return (
          <div>
            <CourseForm
              onSubmit={this.handleSubmit}
              title="Túra adatai"
              initialValues={filtered[0]}
              baseDate={this.props.hike.date}
            />
          </div>
        );
      }
    }

    return <div />;
  }
}

function mapStateToProps(state) {
  return {
    hasErrored: state.hikeEditReducer.hasErrored,
    hike: state.hikeEditReducer.hike,
    user: state.authentication.user
  };
}

export default connect(mapStateToProps)(EditCoursePage);
