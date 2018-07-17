import React from "react";
import { Field } from 'redux-form';
import { renderField } from "../RenderField";
import TimePicker from "../../FormInputs/TimePicker"
import moment from "moment";

export const renderCheckpoints = ({ baseDate, submitting, initialValues, change, fields, meta: { touched, error, submitFailed } }) => (
    <div style={{ margin: "15px" }}>
        <button type="button" className="btn btn-green" onClick={() => fields.push({})}>Új ellenőrzőpont</button>
        <button type="submit" className="btn btn-green" disabled={submitting}>Elküld</button>
        {(touched || submitFailed) && error && <span className="help-block">{error}</span>}
        <div className="row">
            {fields.map((course, index) =>
                <div className="card card-green checkpoint-card" key={index} style={{ minWidth: "370px" }}>

                    <div className="card-header card-header-green">
                        <button
                            type="button"
                            title="Törlés"
                            onClick={() => fields.remove(index)}
                            className="btn btn-close" >
                            X
                        </button>

                        <h4 >
                            {index == 0 ? "Rajt" : ""} 
                            {(index + 1) == fields.length ? "Cél" : ""}
                            {(index!=0 && index+1 < fields.length)? index + ". Ellenőrzőpont" : ""}
                        </h4>
                    </div>

                    <div className="card-body">

                        <Field
                            name={`${course}.name`}
                            type="text"
                            component={renderField}
                            label="Név" />

                        <Field
                            name={`${course}.description`}
                            type="text"
                            component={renderField}
                            label="Leírás" />

                        <Field
                            name={`${course}.distanceFromStart`}
                            type="number"
                            component={renderField}
                            label="Távolság a rajttól" />

                        <Field
                            name={`${course}.open`}
                            label="Nyitás"
                            component={TimePicker}
                            change={change}
                            initTime={(initialValues && initialValues.checkPoints[index]) ? moment(initialValues.checkPoints[index].open) : moment(baseDate)}
                        />

                        <Field
                            name={`${course}.close`}
                            label="Zárás"
                            component={TimePicker}
                            change={change}
                            initTime={(initialValues && initialValues.checkPoints[index]) ? moment(initialValues.checkPoints[index].close) : moment(baseDate)}
                        />

                    </div>

                </div>
            )}
        </div>
    </div>
)