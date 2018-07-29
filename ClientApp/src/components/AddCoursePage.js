import React from "react";
import { connect } from "react-redux";
import CourseForm from "./forms/CourseForm/CourseForm";
import { AddCourseToHike } from "../actions/AddHikeCourseActions";
import { getHikeDetails, deleteData } from "../actions/EditHikeActions";
import moment from "moment";

class AddCoursePage extends React.Component {
  componentWillMount() {
    this.props.dispatch(getHikeDetails(this.props.match.params.id));
  }

  componentWillUnmount() {
    this.props.dispatch(deleteData());
  }

  onSubmit = values => {
    this.props.dispatch(AddCourseToHike(values, this.props.match.params.id));
  };

  render() {
    if (this.props.hasErrored) {
      return <div>Nincs ilyen túra!</div>;
    }

    if (
      this.props.hike.organizer &&
      this.props.hike.organizer.id != this.props.user.id
    ) {
      return <div>Csak a saját szervezésű túrádhoz tudsz távot hozzáadni!</div>;
    } else {
      return (
        <div>
          <CourseForm
            onSubmit={this.onSubmit}
            title="Új táv hozzáadása!"
            baseDate={new Date(moment.utc(this.props.hike.date).local().toString())}
            initialValues={{
              registerDeadline: new Date(moment.utc(this.props.hike.date).local().toString()),
              beginningOfStart: new Date(moment.utc(this.props.hike.date).local().toString()),
              endOfStart: new Date(moment.utc(this.props.hike.date).local().toString())
            }}
          />
        </div>
      );
    }
  }
}

const mapStateToProps = state => {
  return {
    hike: state.hikeEditReducer.hike,
    hasErrored: state.hikeEditReducer.hasErrored,
    user: state.authentication.user
  };
};

export default connect(mapStateToProps)(AddCoursePage);
