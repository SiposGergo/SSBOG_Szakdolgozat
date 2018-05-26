import React from 'react';
import moment from 'moment';
import { SingleDatePicker } from 'react-dates';

export class Datepicker extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      date: this.props.initDate,
      focused: false
    };
  }

  handleDateChange = (date) => {
    this.setState({ date });
    this.props.change(this.props.input.name, date)
  }

  render() {
    return (
      <div>
      <p><b>{this.props.label}</b></p>
        <SingleDatePicker
          date={this.state.date} 
          onDateChange={this.handleDateChange}
          focused={this.state.focused}
          onFocusChange={({ focused }) => this.setState({ focused })} 
          showClearDate={true}
          numberOfMonths={1}
          isOutsideRange={() => false}
          displayFormat="YYYY.MM.DD"
        />
        </div>
    );
  }
}
export default Datepicker;