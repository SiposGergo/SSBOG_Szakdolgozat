import React from "react";
import HikeForm from "./forms/HikeForm/HikeForm";
import { AddHike } from "../actions/AddHikeActions";
import { connect } from "react-redux";

export class AddHikePage extends React.Component {
  onSubmit = values => {
    const val = {...values};
    val.date = new Date(val.date.getTime() - val.date.getTimezoneOffset()*60000)
    this.props.dispatch(AddHike(val));
  };

  render() {
    return (
      <div>
        <HikeForm
          onSubmit={this.onSubmit}
          title="Új túra felvitele"
          initialValues={{ date: new Date() }}
        />
      </div>
    );
  }
}

export default connect()(AddHikePage);
