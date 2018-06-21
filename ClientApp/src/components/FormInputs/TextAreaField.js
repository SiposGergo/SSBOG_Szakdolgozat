import React from "react";

class TextAreaField extends React.Component {

    constructor(props) {
        super(props)
        this.state = {
            text: this.props.initValue,
            sent: false,
            focused: false
        }
    }

    componentWillReceiveProps(nextProps){
        if(this.props.initValue != nextProps.initValue && !this.state.focused && !this.state.sent) {
          this.setState((prevState) => { return {...prevState, text:nextProps.initValue}})
        }
      } 

    handleChange = (event) => {
        this.setState({text:event.target.value});
        this.setState(() => { return {sent:true}})
        this.props.change(this.props.input.name, event.target.value)
    }


    render() {
        return (
            <div className={(this.props.meta.error && this.props.meta.touched) ? "has-error" : ""}>
                <p>{this.props.label}</p>
                <div>
                    <textarea
                        className="form-control"
                        rows={"10"}
                        cols={"40"}
                        placeholder={this.props.label}
                        onChange={this.handleChange}
                        onFocus = {() => {this.setState({focused:true})}}
                        value = {this.state.text}
                    />
                    {this.props.meta.touched && <div className="help-block">{this.props.meta.error}</div>}
                </div>
            </div>
        )
    }
}

export default TextAreaField;