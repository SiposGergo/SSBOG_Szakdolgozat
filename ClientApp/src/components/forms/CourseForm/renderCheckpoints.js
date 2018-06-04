import React from "react";
import { Field } from 'redux-form';
import { renderField } from "../RenderField";
import TimePicker from "../../TimePicker.js"
import moment from "moment";

export const renderCheckpoints = ({baseDate, initialValues,  change, fields, meta: { touched, error, submitFailed } }) => (
    <ul>
        <li>
            <button type="button" onClick={() => fields.push({})}>Új ellenőrzőpont</button>
            {(touched || submitFailed) && error && <span>{error}</span>}
        </li>
        {fields.map((course, index) =>
            <li key={index}>

                <button
                    type="button"
                    title="Törlés"
                    onClick={() => fields.remove(index)} />

                <h4>ellenőrzőpont #{index + 1}</h4>

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
                    initTime={initialValues ? moment(initialValues.checkPoints[index].open) : moment(baseDate)}
                />

                <Field
                    name={`${course}.close`}
                    label="Zárás"
                    component={TimePicker}
                    change={change}
                    initTime={initialValues ? moment(initialValues.checkPoints[index].close) : moment(baseDate)}
                />
            </li>
        )}
    </ul>
)