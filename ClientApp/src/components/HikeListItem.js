import React from "react";
import moment from "moment";

const HikeListItem = (props) => (
    <div>
        <h1>{props.hike.name}</h1>
        <a href={props.hike.website}>A Túra oldala</a>
        <table>
            <tbody>
                <tr>
                    <td>Időpont:</td>
                    <td>{new moment(props.hike.date).format('YYYY. M. D.')}</td>
                </tr>
                <tr>
                    <td>Távok: </td>
                    <td>{props.hike.courses.map(course =>
                        course.distance / 1000 + " km, ")}
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
);

export default HikeListItem;