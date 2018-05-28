import React from "react";
import moment from "moment";
import {config} from "../../helpers/config.js";

const CheckpointListRow = (props) => (
    <tr>
        <td>{props.checkpoint.name}</td>
        <td>{moment(props.checkpoint.open).format(config.timeFormat)}</td>
        <td>{moment(props.checkpoint.close).format(config.timeFormat)}</td>
        <td>{props.checkpoint.distanceFromStart/1000} km</td>
        <td>{props.checkpoint.description}</td>
    </tr>
)

export default CheckpointListRow;