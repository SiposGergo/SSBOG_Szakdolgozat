import React from "react";
import { connect } from "react-redux";
import getVisibleHikes from "../../selectors/HikeListSelector";
import HikeListItem from "./HikeListItem";
import HikeListFilter from "./HikeListFilter.js";

import { itemsFetchData, reset } from "../../actions/HikeListActions"

const API = "http://localhost:4242/Hike/all";

export class HikeListPage extends React.Component {
    componentDidMount() {
        this.props.fetchData(API);
    }

    componentWillUnmount() {
        this.props.reset();
    }

    render() {
        if (this.props.hasErrored) {
            return <h3>Hiba történt az elemek betöltése közben!</h3>;
        }

        if (this.props.isLoading) {
            return <h3>Betöltés...</h3>;
        }

        return ( <div>
                <HikeListFilter />
                {
                    this.props.hikes.length === 0 ? (
                        <h3>Nincsen megjeleníthető túra!</h3>
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
        hikes: getVisibleHikes(state.HikeListReducer),
        hasErrored: state.HikeListReducer.hasErrored,
        isLoading: state.HikeListReducer.isLoading
    }
};

const mapDispatchToProps = (dispatch) => {
    return {
        fetchData: (url) => dispatch(itemsFetchData(url)),
        reset: () => dispatch(reset())
    };
};

export default connect(mapStateToProps, mapDispatchToProps, null, {pure:false})(HikeListPage);