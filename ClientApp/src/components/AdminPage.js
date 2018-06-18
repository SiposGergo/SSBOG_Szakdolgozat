import React from "react";
import {postCheckpointPass} from "../actions/AdminActions";
import {connect} from "react-redux";
import moment from "moment"; 

class AdminPage extends React.Component {
    constructor(props) {
        super(props);
        const locState = this.props.location.state;
        if (locState && locState.courses.length) {
            this.state = {
                courses: locState.courses,
                course: locState.courses[0].id,
                checkpoint: locState.courses[0].checkPoints[0].id,
                startNumber: ""
            }
        }
    }

    handleSubmit = (e) => {
        e.preventDefault();
        const dto = {
            startNumber: this.state.startNumber,
            checkpointId : this.state.checkpoint,
            timestamp: moment()
        }
        this.props.dispatch(postCheckpointPass(dto));
        this.setState({ startNumber: "" });
    }

    handleSelectCourse = (e) => {
        const checkpointId = this.state.courses
            .filter(x => x.id == e.target.value)[0]
            .checkPoints[0].id;
        this.setState({ course: e.target.value, checkpoint: checkpointId })
    }

    handleSelectCheckpoint = (e) => {
        this.setState({ checkpoint: e.target.value })
    }

    render() {
        if (!this.state) {
            return (<div>Az oldal a főoldali linkről érhető el!</div>)
        }

        return (
            <div>
                <form onSubmit={this.handleSubmit}>
                    <select name="courses" value={this.state.course}
                        onChange={this.handleSelectCourse}>
                        {this.state.courses.map((course) =>
                            (<option key={course.id} value={course.id}>{course.name}</option>))}
                    </select>

                    <select name="checkpoints"
                        onChange={this.handleSelectCheckpoint}>
                        {this.state.courses.filter(c => c.id == this.state.course)[0].checkPoints
                            .map((checkpoint) =>
                                (<option key={checkpoint.id} value={checkpoint.id}>{checkpoint.name}</option>))}
                    </select>

                    <input type="number"
                        name="startNumber"
                        value={this.state.startNumber}
                        autoFocus
                        onChange={e => this.setState({ startNumber: e.target.value })}
                    />

                    <input type="submit" value="elküld" />
                </form>
            </div>)
    }
}

export default connect()(AdminPage);