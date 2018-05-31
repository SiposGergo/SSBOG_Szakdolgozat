import React from "react";

export const renderField = ({ input, label, type, meta: { touched, error, warning } }) => (
    <div className={"form-group " + (touched && error && "has-error")}>
        <label>{label}</label>
        <div>
            <input className="form-control" {...input} placeholder={label} type={type} />
            {touched && (error && <div className="help-block">{error}</div>)}
        </div>
    </div>
)