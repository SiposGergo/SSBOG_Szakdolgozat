import React from "react";
import moment from "moment";
import {config} from "../../helpers/config.js";


class HikeDetailsCompoent extends React.Component {
    render() {
        const hike = this.props.hike;
        return (
            <div>
                <br />
                <p>{hike.name}</p>
                <p>{hike.description}</p>
                <p>{moment(hike.date).format(config.dateFormat)}</p>
                <a href={hike.website} target="_blank">A Túra oldala</a>
            </div>
        );
    }
}

export default HikeDetailsCompoent;