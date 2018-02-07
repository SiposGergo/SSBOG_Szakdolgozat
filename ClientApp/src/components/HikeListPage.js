import React from "react";
import { connect } from "react-redux";
import getVisibleHikes from "../selectors/hikes.js";
import HikeListItem from "./HikeListItem";
import HikeListFilter from "./HikeListFilter.js";

import { itemsFetchData } from "../actions/filters"

const API = "http://localhost:4242/Hike/all";

class HikeListPage extends React.Component {
    componentDidMount() {
        this.props.fetchData(API);
    }

    render() {
        if (this.props.hasErrored) {
            return <p>Sorry! There was an error loading the items</p>;
        }

        if (this.props.isLoading) {
            return <p>Loading…</p>;
        }

        return (
            <div>
                <HikeListFilter />
                {
                    this.props.hikes.length === 0 ? (
                        <p>No Hikes</p>
                    ) : (
                            this.props.hikes.map((h) => {
                                return (<HikeListItem key={h.id} hike={h} />)
                            })
                        )
                }
            </div>
        )
    }
}

const mapStateToProps = (state) => {
    return {
        hikes: getVisibleHikes(state),
        hasErrored: state.hasErrored,
        isLoading: state.isLoading

    }
};

const mapDispatchToProps = (dispatch) => {
    return {
        fetchData: (url) => dispatch(itemsFetchData(url))
    };
};

export default connect(mapStateToProps, mapDispatchToProps)(HikeListPage);