import React from 'react';
import moment from 'moment';
import RcTimePicker from 'rc-time-picker';
 
class TimePicker extends React.Component {

    handleChange = time => {
        console.log(time);
        this.props.change(this.props.input.name, time)
    }

    render() {
        return (
            
            <div className = {(this.props.meta.error && this.props.meta.touched) ?  "has-error" : ""}>
            <p><b>{this.props.label}</b></p>
            <RcTimePicker onChange={this.handleChange} defaultValue={this.props.default}/> 
            {this.props.meta.touched && <div className="help-block">{this.props.meta.error}</div>}
            </div>
        )
    }
}

export default TimePicker;