import React from "react";
import moment from "moment";
import { Link } from "react-router-dom";

class HikeListItem extends React.Component {

    render() {
        const props = this.props;
        return (<div>
            <h1><Link to={"/hike/" + props.hike.id}>{props.hike.name}</Link></h1>
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