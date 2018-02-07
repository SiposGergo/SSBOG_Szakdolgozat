import React from "react";
import moment from "moment";

const HikeListItem = (props) => (
    <div>
        <h1><a href={`/hike/${props.hike.id}`}>{props.hike.name}</a></h1>
        <a href={props.hike.website} target="_blank">A Túra oldala</a>
        <table>
            <tbody>
                <tr>
                    <td>Időpont:</td>
                    <td>{new moment(props.hike.date).format('YYYY. M. D.')}</td>
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
    </div>
);

export default HikeListItem;