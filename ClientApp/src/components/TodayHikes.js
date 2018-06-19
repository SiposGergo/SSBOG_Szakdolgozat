import React from "react";
import { connect } from "react-redux";
import { getTodayHikes, reset } from "../actions/HikeListActions";
import TodayHikesItem from "./TodayHikesItem";

export class TodayHikes extends React.Component {
    componentDidMount() {
        this.props.getTodayHikes();
    }

    componentWillUnmount() {
        this.props.reset();
    }

    render() {
        if (this.props.hasErrored) {
            return <p>Sorry! There was an error loading the items</p>;
        }

        if (this.props.isLoading) {
            return <p>Loading…</p>;
        }

        return (<div>
            {
                this.props.hikes.length === 0 ? (
                    <p>No Hikes</p>
                ) : (
                        <div>
                            <h3>A mai nap túrái </h3>
                            {this.props.hikes.map((h) => {
                                return (
                                    <TodayHikesItem key={h.id} hike={h} user={this.props.user}/>
                                )
                            })}
                        </div>
                    )
            }
        </div>
        )
    }
}

const mapStateToProps = (state) => {
    return {
        hikes: state.HikeListReducer.hikes,
        hasErrored: state.HikeListReducer.hasErrored,
        isLoading: state.HikeListReducer.isLoading,
        user: state.authentication.user
    }
};

const mapDispatchToProps = (dispatch) => {
    return {
        getTodayHikes: () => dispatch(getTodayHikes()),
        reset: () => dispatch(reset())
    };
};

export default connect(mapStateToProps, mapDispatchToProps,null,{pure:false})(TodayHikes);