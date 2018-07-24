import React from "react"
import { connect } from "react-redux";
import { getCourseResult, getCourseLiveResult } from "../../actions/ResultActions";
import moment from "moment";
import momentDurationFormatSetup from "moment-duration-format";
import { config } from "../../helpers/config";
import LoadSpinner from "../LoadSpinner";
import ResultFilters from "./ResultFilters";
import getVisibleRegistrations from "../../selectors/ResultSelector";
import { Link } from "react-router-dom";

const refreshRateMs = 10000;
const refreshRateSec = refreshRateMs / 1000;

class CourseResultPage extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            counter: refreshRateSec,
        }
    }

    componentDidMount() {
        momentDurationFormatSetup(moment);
        this.props.dispatch(getCourseResult(this.props.match.params.id));
        this.stopBackCount = setInterval(this.update, refreshRateMs);
        this.stopUpdate = setInterval(() => this.setState((prevState) => ({ counter: prevState.counter - 1 })), 1000)
    }

    update = () => {
        this.props.dispatch(getCourseLiveResult(this.props.match.params.id));
        this.setState({ counter: refreshRateSec });
    }

    componentWillUnmount() {
        clearInterval(this.stopBackCount);
        clearInterval(this.stopUpdate);
    }

    render() {
        if (this.props.hasErrored) return (<div>Hiba!</div>)
        if (this.props.isLoading) return (<LoadSpinner />)


        if (this.props.checkpoints && this.props.visibleRegistrations) {
            return (

                <div>
                    Az adatok {this.state.counter} másodperc múlva frissülnek.
                    <ResultFilters />
                    <div className="table-responsive">
                        <table className="minimalistBlack ">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Rajtszám</th>
                                    <th>Túrázó</th>
                                    <th>Státusz</th>
                                    <th>Eredmény</th>
                                    <th>Átlagsebesség</th>
                                    {this.props.checkpoints.map(cp => (<th key={cp.id}> {cp.name}({(cp.distanceFromStart / 1000)}km) </th>))}
                                </tr>
                            </thead>
                            <tbody>
                                {
                                    this.props.visibleRegistrations.map((reg) => {
                                        let fullTime = null;
                                        if (reg.passes[0] && reg.passes[reg.passes.length - 1].nettoTime) {
                                            fullTime = moment.duration(reg.passes[reg.passes.length - 1].nettoTime);
                                        }

                                        return (
                                            <tr key={reg.id} className={reg.hiker.gender.toLowerCase()}>
                                                <td>{this.props.visibleRegistrations.indexOf(reg) + 1}</td>
                                                <td>{reg.startNumber}</td>
                                                <td>
                                                    <Link className="link" to={"/user/" + reg.hiker.id}>
                                                        {reg.hiker.name}
                                                    </Link>
                                                </td>
                                                <td>
                                                    {!fullTime && "Nem ért célba"}
                                                    {fullTime && fullTime < this.props.limitTime && "Szintidőn belül beért"}
                                                    {fullTime && fullTime > this.props.limitTime && "Szintidőn kívül ért be"}
                                                </td>
                                                <td>
                                                    {fullTime ? fullTime.format(config.timeFormatLong, { trim: false }) : "-"}
                                                </td>
                                                <td>{reg.avgSpeed ? reg.avgSpeed + "km/h" : "-"}</td>
                                                {
                                                    this.props.checkpoints.map(cp => {
                                                        const pass = reg.passes.find((reg) => reg.checkPointId == cp.id);
                                                        return (<td key={cp.id}>
                                                            {this.props.time == "brutto" && pass && pass.timeStamp ? moment(pass.timeStamp).format(config.timeFormatLong) : ""}
                                                            {this.props.time == "netto" && pass && pass.nettoTime ? moment.duration(pass.nettoTime).format(config.timeFormatLong, { trim: false }) : ""}
                                                        </td>)
                                                    })
                                                }
                                            </tr>
                                        )
                                    })
                                }
                            </tbody>
                        </table></div>

                </div>)
        }
    }
}

const mapStateToProps = (state) => {
    return {
        checkpoints: state.resultReducer.checkpoints,
        visibleRegistrations: getVisibleRegistrations(state.resultReducer),
        hasErrored: state.resultReducer.hasErrored,
        isLoading: state.resultReducer.isLoading,
        time: state.resultReducer.time,
        limitTime: moment.duration(state.resultReducer.limitTime)
    }
}

export default connect(mapStateToProps)(CourseResultPage);