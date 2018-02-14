import React from "react";
import { connect } from "react-redux";

class AlertBar extends React.Component {
    constructor(props){
        super(props);
        
    }
    render() {
        const alert = this.props.alert;
        return (
            <div>
                <div className="col-sm-8 col-sm-offset-2">
                    {
                        alert && alert.message &&
                        <div className={`alert ${alert.type}`}>{alert.message}</div>
                    }
                </div>
                <div>

                </div>
            </div>)
    }
}

function mapStateToProps (state) { console.log(state.alert); return { alert: state.alert }};

const ConnectedAlertBar = connect(mapStateToProps)(AlertBar);
export default ConnectedAlertBar;