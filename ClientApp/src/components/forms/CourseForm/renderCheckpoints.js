import React from "react";
import { Field } from 'redux-form';
import { renderField } from "../RenderField";
import TimePicker from "../../TimePicker.js"
import moment from "moment";

export const renderCheckpoints = ({ baseDate, initialValues, change, fields, meta: { touched, error, submitFailed } }) => (
    <div className="">
        <button type="button" onClick={() => fields.push({})}>Új ellenőrzőpont</button>
        {(touched || submitFailed) && error && <span>{error}</span>}
        <div className="row">
            {fields.map((course, index) =>
                <div className="card" key={index} style={{ width: "400px" }}>

                    <div className="card-header">
                        <button
                            type="button"
                            title="Törlés"
                            onClick={() => fields.remove(index)} />

                        <h4 >ellenőrzőpont #{index + 1}</h4>
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
                            initTime={(initialValues &&initialValues.checkPoints[index]) ? moment(initialValues.checkPoints[index].close) : moment(baseDate)}
                        />

                    </div>

                </div>
            )}
        </div>
    </div>
)