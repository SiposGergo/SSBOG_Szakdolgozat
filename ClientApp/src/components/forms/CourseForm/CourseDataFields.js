import React from "react";
import { renderField } from "../RenderField";
import Datepicker from "../../FormInputs/Datepicker.js"
import TimePicker from "../../FormInputs/TimePicker.js"
import moment from "moment";
import { Field } from "redux-form";


const CourseDataFields = (props) => (
    <div className="row">
        <div className="col-md">
            <Field
                name="name"
                component={renderField}
                label="Táv neve"
                type="text"
            />

            <Field
                name="placeOfStart"
                type="text"
                component={renderField}
                label="A Start helye"
            />

            <Field
                name="placeOfFinish"
                type="text"
                component={renderField}
                label="A cél helye"
            />

            <Field
                name="distance"
                type="number"
                component={renderField}
                label="Távolság (méterben)"
            />
        </div>
        <div className="col-md">
            <Field
                name="elevation"
                type="number"
                component={renderField}
                label="Szintemelkedés (méterben)"
            />

            <Field
                name="price"
                type="number"
                component={renderField}
                label="Nevezési díj"
            />


            <Field
                name="maxNumOfHikers"
                type="number"
                component={renderField}
                label="Nevezési létszámkorlát (fő)"
            />

            <Field name="limitTime"
                label="Szintidő"
                component={renderField}
                type="number"
            />

        </div>
        <div className="col-md">
            <Field name="beginningOfStart"
                label="A rajt kezdete"
                component={TimePicker}
                change={props.change}
                initTime={props.initialValues ? moment(props.initialValues.beginningOfStart) : moment(props.baseDate)}
            />

            <Field name="endOfStart"
                label="A rajt vége"
                component={TimePicker}
                change={props.change}
                initTime={props.initialValues ? moment(props.initialValues.endOfStart) : moment(props.baseDate)}
            />
            <Field name="registerDeadline"
                label="Nevezési határidő"
                component={Datepicker}
                change={props.change}
                initDate={props.initialValues ? moment(props.initialValues.registerDeadline) : moment(props.baseDate)}
            />

        </div>
    </div>
)

export default CourseDataFields;