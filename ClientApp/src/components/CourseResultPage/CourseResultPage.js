import React from "react"
import { connect } from "react-redux";
import { getCourseResult } from "../../actions/ResultActions";

import moment from "moment";
import { config } from "../../helpers/config";

class CourseResultPage extends React.Component {

    componentDidMount() {
        this.props.dispatch(getCourseResult(this.props.match.params.id));
        setInterval(()=> this.props.dispatch(getCourseResult(this.props.match.params.id)), 5000);
    }

    render() {
        if (this.props.hasErrored) return (<div>Hiba!</div>)
        if (this.props.isLoading) return (<div>Loading...</div>)

        if (this.props.checkpoints && this.props.registrations) {
            return (<div>
                <table className="minimalistBlack">
                    <thead>
                        <tr>
                            <th>Túrázók</th>
                            {this.props.checkpoints.map(cp => (<th key={cp.id}> {cp.name}({(cp.distanceFromStart / 1000)}km) </th>))}
                        </tr>
                    </thead>
                    <tbody>
                        {
                            this.props.registrations.map(reg => (
                                <tr key={reg.id} className={reg.hiker.gender.toLowerCase()}>
                                    <td>{reg.hiker.name}</td>
                                    {
                                        this.props.checkpoints.map(cp => {
                                            const pass = reg.passes.filter((reg) => reg.checkPointId == cp.id);
                                            return (<td key={cp.id}>
                                                {pass[0] ? moment(pass[0].timeStamp).format(config.timeFormatLong) : ""}
                                            </td>)
                                        })
                                    }
                                </tr>
                            ))
                        }
                    </tbody>
                </table>
            </div>)
        }
    }
}

const mapStateToProps = (state) => {
    return {
        checkpoints: state.resultReducer.checkpoints,
        registrations: state.resultReducer.registrations,
        hasErrored: state.resultReducer.isLoading,
        isLoading: state.resultReducer.hasErrored
    }
}

export default connect(mapStateToProps)(CourseResultPage);
