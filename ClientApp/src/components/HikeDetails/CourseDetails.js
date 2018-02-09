import React from "react";
import moment from "moment";
import CheckpointList from "./CheckpointList";

class CourseDetails extends React.Component {
    render() {
        const course = this.props.course;
        return (
            <div>
                <p>{course.name}</p>
                <p>Nevezési díj:{course.price}</p>
                <p>Táv:{course.distance}</p>
                <p>Szint emelkedés:{course.elevation}</p>
                <p>Rajt:{course.placeOfStart}</p>
                <p>Cél:{course.placeOfFinish}</p>
                <p>Rajt ideje:{moment(course.beginningOfStart).format('YYYY MM DD HH:mm')}
                    - {moment(course.endOfStart).format('YYYY MM DD HH:mm')}</p>
                <p>Szintidő:{course.limitTime}</p>
                <CheckpointList checkpoints={course.checkPoints} />
            </div>
        )
    }
}

export default CourseDetails;