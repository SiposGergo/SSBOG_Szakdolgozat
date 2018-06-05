import React from 'react';
import moment from 'moment';
import RcTimePicker from 'rc-time-picker';

    class TimePicker extends React.Component {

        constructor(props) {
            super(props);
            this.state = {
                time: this.props.initTime,
                focused: false,
                sent: false
            };
        }

        componentWillReceiveProps(nextProps) {
            if (this.props.initTime != nextProps.initTime && !this.state.focused && !this.state.sent) {
                this.setState((prevState) => { return { ...prevState, time: nextProps.initTime } })
                this.props.change(this.props.input.name, nextProps.initTime)
            }
        }

        handleChange = time => {
            this.setState(() => { return { sent: true, time } })
            this.props.change(this.props.input.name, time)
        }

        render() {
            return (
                <div className={(this.props.meta.error && this.props.meta.touched) ? "has-error" : ""}>
                    <p><b>{this.props.label}</b></p>
                    <RcTimePicker
                        onChange={this.handleChange}
                        value={this.state.time}
                        onOpen={() => { this.setState({ focused: true }) }}
                        showSecond={false}
                        minuteStep={10} 
                    />
                    {this.props.meta.touched && <div className="help-block">{this.props.meta.error}</div>}
                </div>
            )
        }
    }

export default TimePicker;