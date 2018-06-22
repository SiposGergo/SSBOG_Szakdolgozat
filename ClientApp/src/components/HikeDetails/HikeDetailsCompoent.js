import React from "react";
import moment from "moment";
import {config} from "../../helpers/config.js";


class HikeDetailsCompoent extends React.Component {
    render() {
        const hike = this.props.hike;
        return (
            <div className="col-md details-box">
                <h3 className="flo">{moment(hike.date).format(config.dateFormat)}</h3>
                <h3>{hike.name}</h3>
                <div>{hike.description}</div>
                <a href={hike.website} target="_blank"><button className="btn btn-green">A Túra oldala</button></a>
            </div>
        );
    }
}

export default HikeDetailsCompoent;