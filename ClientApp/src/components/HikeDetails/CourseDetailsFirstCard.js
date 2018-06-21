import React from "react";
import moment from "moment";
import {config} from "../../helpers/config";

class CourseDetailsFirstCard extends React.Component {
    render() {
        const props = this.props;
        const registratedPercent = ((props.course.numOfRegisteredHikers / props.course.maxNumOfHikers) * 100).toString() + "%";
        
        return (
            <div>
                <p>{props.course.name}</p>
                <p>Nevezési díj:{props.course.price}</p>
                <p>Táv:{props.course.distance}</p>
                <p>Szint emelkedés:{props.course.elevation}</p>
                <p>Rajt:{props.course.placeOfStart}</p>
                <p>Cél:{props.course.placeOfFinish}</p>
                <p>Rajt ideje:{moment(props.course.beginningOfStart).format(config.dateTimeFormat)}
                    - {moment(props.course.endOfStart).format(config.dateTimeFormat)}</p>
                <p>Szintidő:{props.course.limitTime}</p>
                <p>Létszámkorlát:{props.course.maxNumOfHikers}</p>
                <p>{props.course.numOfRegisteredHikers}/{props.course.maxNumOfHikers}</p>
                <div className="progress">
                    <div className="progress-bar" style={{ width: registratedPercent }}></div>

                </div>
                <p>Nevezési határidő: :{moment(props.course.registerDeadline).format(config.dateTimeFormat)}</p>

            </div>
        )
    }
}

export default CourseDetailsFirstCard;