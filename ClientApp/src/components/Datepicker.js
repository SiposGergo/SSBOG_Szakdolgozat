import React from 'react';
import moment from 'moment';
import { SingleDatePicker } from 'react-dates';
import {config} from "../helpers/config.js";

export class Datepicker extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      date: this.props.initDate,
      focused: false,
      sent: false
    };
  }

   componentWillReceiveProps(nextProps){
    if(this.props.initDate != nextProps.initDate && !this.state.focused && !this.state.sent) {
      this.setState((prevState) => { return {...prevState, date:nextProps.initDate}})
    }
  } 

  handleDateChange = (date) => {
    this.setState({ date });
    this.setState(() => { return {sent:true}})
    this.props.change(this.props.input.name, date)
  }

  render() {
    return (
      <div className = {(this.props.meta.error && this.props.meta.touched) ?  "has-error" : ""}>
      <p><b>{this.props.label}</b></p>
        <SingleDatePicker
          date={this.state.date} 
          onDateChange={this.handleDateChange}
          focused={this.state.focused}
          onFocusChange={({ focused }) => this.setState({ focused })} 
          showClearDate={true}
          numberOfMonths={1}
          isOutsideRange={() => false}
          displayFormat={config.dateFormat}
        />
        {this.props.meta.touched && <div className="help-block">{this.props.meta.error}</div>}
        </div>
    );
  }
}
export default Datepicker;