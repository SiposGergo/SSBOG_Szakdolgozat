import React from "react";
import moment from "moment";
import { config } from "../../helpers/config";

class CourseDetailsFirstCard extends React.Component {
    render() {
        const props = this.props;
        const registratedPercent = ((props.course.numOfRegisteredHikers / props.course.maxNumOfHikers) * 100).toString() + "%";

        return (
            <div>
                <h3><center>{props.course.name}</center></h3>
                <table className="course-details-table">
                    <tbody>
                        <tr>
                            <td>Nevezési díj</td>
                            <td>{props.course.price + " Ft"}</td>
                        </tr>
                        <tr>
                            <td>Táv</td>
                            <td>{props.course.distance / 1000}{" km"}</td>
                        </tr>
                        <tr>
                            <td>Szint emelkedés</td>
                            <td>{props.course.elevation + " m"}</td>
                        </tr>
                        <tr>
                            <td>Rajt</td>
                            <td>{props.course.placeOfStart}</td>
                        </tr>
                        <tr>
                            <td>Cél</td>
                            <td>{props.course.placeOfFinish}</td>
                        </tr>
                        <tr>
                            <td>Rajt ideje</td>
                            <td> {moment.utc(props.course.beginningOfStart).local().format(config.timeFormat)}
                                - {moment.utc(props.course.endOfStart).local().format(config.timeFormat)}</td>
                        </tr>
                        <tr>
                            <td>Szintidő</td>
                            <td>{props.course.limitTime + " óra"}</td>
                        </tr>
                        <tr>
                            <td>Létszámkorlát</td>
                            <td>{props.course.maxNumOfHikers + " fő"}</td>
                        </tr>
                        <tr>
                            <td>Eddig nevezettek száma</td>
                            <td>{props.course.numOfRegisteredHikers + " fő"}</td>
                        </tr>
                        <tr>
                            <td>Nevezési határidő</td>
                            <td>{moment.utc(props.course.registerDeadline).local().format(config.dateTimeFormat)}</td>
                        </tr>
                    </tbody>
                </table>
                <div className="progress">
                    <div className="progress-bar" style={{ width :registratedPercent, background :"#2c7a20" }}></div>
                </div>

            </div>
        )
    }
}

export default CourseDetailsFirstCard;