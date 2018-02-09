import React from "react";
import moment from "moment";
import { NavLink } from "react-router-dom";

class HikeListItem extends React.Component {

    render() {
        const props = this.props;
        return (<div>
            <h1><NavLink to={"/hike/" + props.hike.id} exact={true}>{props.hike.name}</NavLink></h1>
            <a href={props.hike.website} target="_blank">A Túra oldala</a>
            <table>
                <tbody>
                    <tr>
                        <td>Időpont:</td>
                        <td>{new moment(props.hike.date).format('YYYY. MM. DD.')}</td>
                    </tr>
                    <tr>
                        <td>Távok: </td>
                        <td>{props.hike.courses.length == 0 ? "Nincsenek távok"
                            : props.hike.courses.map(course =>
                                course.distance / 1000 + " km, ")}
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>)
    }
}

export default HikeListItem;