import React from "react";
import { connect } from "react-redux";
import {
    setTextFilter, sortByName, sortByDate,
    setEndDate, setStartDate, setOldHikesVisibility, setSliderValues
}
    from "../../actions/HikeListActions";
import "react-dates/initialize";
import { DateRangePicker } from "react-dates";
import Slider, { Range } from 'rc-slider';
import Tooltip from 'rc-tooltip';


const rangeMarks = {
    0: { label: "0 km" },
    10: { label: "10 km" },
    20: { label: "20 km" },
    30: { label: "30 km" },
    40: { label: "40 km" },
    50: { label: "50 km" },
    60: { label: "60 km" },
    70: { label: "70 km" },
    80: { label: "80 km" },
    90: { label: "90 km" },
    100: { label: "100 km" }
}

const createSliderWithTooltip = Slider.createSliderWithTooltip;
const RangeSlider = createSliderWithTooltip(Range);


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

    handleTextBoxClick = (event) => {
        const target = event.target;
        this.props.setOldHikesVisibility(target.checked);
    }

    onSliderChange = (value) => {
        this.props.setSliderValues(value);
    }

    render() {
        return (
            <div className="hike-list-filter">
                <div className="row">
                    <div className="col-md">
                        <label htmlFor="keres">Keresés: </label>
                        <input
                            name="keres"
                            className="form-control"
                            type="text"
                            placeholder="Keresés"
                            defaultValue={this.props.filters.text}
                            onChange={this.onTextChange} />
                    </div>
                    <div className="col-md">
                        <label htmlFor="rendez">Rendezés: </label>
                        <select className="form-control"
                            name="rendez"
                            defaultValue={this.props.filters.sortBy}
                            onChange={this.onSortChange}
                        >
                            <option value="date">Dátum</option>
                            <option value="name">Név</option>
                        </select>
                    </div>

                    <div className="col-md">
                        <label>Túra dátuma:</label>
                        <br />
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
                            isOutsideRange={() => false} 
                            orientation="vertical"/>
                    </div>
                </div>


                <label htmlFor="oldHikes">
                    Már megrendezett túrák mutatása:
                    </label>
                <input type="checkbox" id="oldHikes" name="oldHikes"
                    onChange={this.handleTextBoxClick} />
                <br />

                <div style={{ marginTop: 10 }}>
                    <RangeSlider
                        min={0}
                        max={100}
                        marks={rangeMarks}
                        defaultValue={[0, 120]}
                        onChange={this.onSliderChange}


                        railStyle={{ backgroundColor: '#6dd45f', height: 5 }}
                        trackStyle={[{ backgroundColor: '#2c7a20', height: 5 }]}
                        handleStyle={[
                            { borderColor: '#2c7a20' }
                        ]}
                        dotStyle={{ borderColor: 'black' }}


                    />
                </div>
            </div>
        )
    }
}
const mapStateToProps = (state) => ({
    filters: state.HikeListReducer
});

const mapDispatchToProps = (dispatch, props) => ({
    setTextFilter: (text) => dispatch(setTextFilter(text)),
    sortByDate: () => dispatch(sortByDate()),
    sortByName: () => dispatch(sortByName()),
    setStartDate: (date) => dispatch(setStartDate(date)),
    setEndDate: (date) => dispatch(setEndDate(date)),
    setOldHikesVisibility: (bool) => dispatch(setOldHikesVisibility(bool)),
    setSliderValues: (value) => dispatch(setSliderValues(value))
});

export default connect(mapStateToProps, mapDispatchToProps, null, { pure: false })(HikeListFilter);