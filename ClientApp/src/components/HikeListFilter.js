import React from "react";
import { connect } from "react-redux";
import { setTextFilter, sortByName, sortByDate, setEndDate, setStartDate } from "../actions/filters";
import "react-dates/initialize";
import { DateRangePicker } from "react-dates";

export class HikeListFilter extends React.Component {
    state = {
        calendarFocused: null
    };
    onDateChange = ({ startDate, endDate }) => {
        this.props.setStartDate(startDate);
        this.props.setEndDate(endDate);
    }
    onFocusChanged = (calendarFocused) => {
        this.setState(() => ({ calendarFocused }));
    }
    onTextChange = (e) => {
        this.props.setTextFilter(e.target.value);
    }
    onSortChange = (e) => {
        if (e.target.value === "date") {
            this.props.sortByDate();
        }
        else if (e.target.value === "name") {
            this.props.sortByName();
        }
    };

    render() {
        return (
            <div>
                <input type="text" defaultValue={this.props.filters.text} onChange={this.onTextChange} />
                <select
                    defaultValue={this.props.filters.sortBy}
                    onChange={this.onSortChange}
                >
                    <option value="date">Dátum</option>
                    <option value="name">Név</option>
                </select>
                <DateRangePicker
                    startDate={this.props.filters.startDate}
                    startDateId="asd"
                    endDate={this.props.filters.endDate}
                    endDateId="lol"
                    onDatesChange={this.onDateChange}
                    focusedInput={this.state.calendarFocused}
                    onFocusChange={this.onFocusChanged}
                    numberOfMonths={1}
                    showClearDates={true}
                    isOutsideRange={() => false} />
            </div>
        )
    }
}
const mapStateToProps = (state) => ({
    filters: state
});

const mapDispatchToProps = (dispatch, props) => ({
    setTextFilter: (text) => dispatch(setTextFilter(text)),
    sortByDate: () => dispatch(sortByDate()),
    sortByName: () => dispatch(sortByName()),
    setStartDate: (date) => dispatch(setStartDate(date)),
    setEndDate: (date) => dispatch(setEndDate(date))
});

export default connect(mapStateToProps, mapDispatchToProps)(HikeListFilter);