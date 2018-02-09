import React from "react";
import moment from "moment";

const CheckpointListRow = (props) => (
    <tr>
        <td>{props.checkpoint.name}</td>
        <td>{moment(props.checkpoint.open).format('HH:mm')}</td>
        <td>{moment(props.checkpoint.close).format('HH:mm')}</td>
        <td>{props.checkpoint.distanceFromStart/1000} km</td>
        <td>{props.checkpoint.description}</td>
    </tr>
)

export default CheckpointListRow;