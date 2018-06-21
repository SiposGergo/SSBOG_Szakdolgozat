import React from "react";

const Card = WrappedComponent => class extends React.Component {
    render() {
        return (
            <div className="card card-green">
                <div className="card-header card-header-green">
                    <h3>{this.props.title}</h3>
                </div>
                <div className="card-body">
                    <WrappedComponent {...this.props} />
                </div>
            </div>
        )
    }
};

export default Card;