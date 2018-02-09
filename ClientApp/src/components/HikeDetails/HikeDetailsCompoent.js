import React from "react";
import moment from "moment";

class HikeDetailsCompoent extends React.Component {
    render() {
        const hike = this.props.hike;
        return (
            <div>
                <p>{hike.name}</p>
                <p>{hike.description}</p>
                <p>{moment(hike.date).format('YYYY. MM. DD.')}</p>
                <a href={hike.website} target="_blank">A Túra oldala</a>
            </div>
        );
    }
}

export default HikeDetailsCompoent;