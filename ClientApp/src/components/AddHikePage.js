import React from "react";
import HikeForm from "./forms/HikeForm/HikeForm"
import {AddHike} from "../actions/AddHikeActions"
import {connect} from "react-redux"

export class AddHikePage extends React.Component {

    onSubmit = (values) => {
        console.log(values)
        this.props.dispatch(AddHike(values));
        }

    render()
    {
        return(<div><HikeForm onSubmit={this.onSubmit} title="Új túra felvitele"/></div>)
    }
}

export default connect()(AddHikePage)
