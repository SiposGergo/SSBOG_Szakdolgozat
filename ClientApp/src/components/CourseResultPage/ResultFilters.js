import React from "react";
import { connect } from "react-redux";

import {
    sortDefault,
    sortByName,
    setTimeNetto,
    setTimeBrutto,
    setJustFinishers,
    setGenderMale,
    setGenderFemale,
    setGenderBoth,
    setStartNumberText,
    setNameText,
    sortByNetto,
    reset
} from "../../actions/ResultActions";


class ResultFilters extends React.Component {

    onGenderChange = (value) => {
        if (value == "male") { this.props.dispatch(setGenderMale()) }
        if (value == "female") { this.props.dispatch(setGenderFemale()) }
        if (value == "both") { this.props.dispatch(setGenderBoth()) }
    }

    onTimeChange = (value) => {
        if (value.target.value == "brutto") {
            this.props.dispatch(setTimeBrutto())
        } else {
            this.props.dispatch(setTimeNetto())
        }
    }

    onSortByChange = (e) => {
        if (e.target.value == "name") { this.props.dispatch(sortByName()) }
        if (e.target.value == "nettoTime") { this.props.dispatch(sortByNetto()) }
        if (e.target.value == "default") { this.props.dispatch(sortDefault()) }
    }

    onReset = () => {
        this.props.dispatch(reset())
    }

    render() {
        return (
            <div className="filter">
                <button className="btn btn-close" onClick={this.onReset}>X</button>
                <div className="row">
                    <div className="col-md-4">
                        <label htmlFor="nameText">Név:</label>
                        <input
                            type="text"
                            name="nameText"
                            placeholder="Túrázó neve"
                            value={this.props.nameText}
                            className="form-control"
                            onChange={x => this.props.dispatch(setNameText(x.target.value))}
                        />

                        <label htmlFor="startNumberText">Rajtszám:</label>
                        <input
                            type="number"
                            className="form-control"
                            name="startNumberText"
                            placeholder="Túrázó rajtszáma"
                            value={this.props.startNumberText}
                            onChange={x => this.props.dispatch(setStartNumberText(x.target.value))}
                        />
                    </div>
                    <div className="col-md-4">
                        <label htmlFor="gender">Nem:</label>
                        <select
                            className="form-control"
                            name="gender"
                            value={this.props.gender}
                            onChange={x => (this.onGenderChange(x.target.value))} >
                            <option value="male">Férfiak</option>
                            <option value="female">Nők</option>
                            <option value="both">Mindkettő</option>
                        </select>

                        <label htmlFor="time">Idő:</label>
                        <select
                            className="form-control"
                            name="time"
                            value={this.props.time}
                            onChange={this.onTimeChange}
                        >
                            <option value="brutto">abszolút idő</option>
                            <option value="netto">versenyidő</option>
                        </select>
                    </div>
                    <div className="col-md-4">
                        <label htmlFor="sortBy">Rendezés:</label>
                        <select
                            className="form-control"
                            name="sortBy"
                            value={this.props.sortBy}
                            onChange={this.onSortByChange}
                        >
                            <option value="default">pozíció a pályán</option>
                            <option value="nettoTime">nettó idő</option>
                            <option value="name">név</option>
                        </select>

                        <label htmlFor="justFinishers" className="check-box-label">
                            Csak célba érkezettek:
                        </label> {console.log(this.props.justFinishers)}
                        <input type="checkbox"
                            id="justFinishers"
                            name="justFinishers"
                            onChange={x => this.props.dispatch(setJustFinishers(x.target.checked))}
                            checked={this.props.justFinishers}
                        />
                    </div>
                </div>







            </div>
        )
    }
}

const mapStateToProps = (state) => ({
    nameText: state.resultReducer.nameText,
    startNumberText: state.resultReducer.startNumberText,
    gender: state.resultReducer.gender,
    justFinishers: state.resultReducer.justFinishers,
    time: state.resultReducer.time,
    sortBy: state.resultReducer.sortBy
})

export default connect(mapStateToProps)(ResultFilters);