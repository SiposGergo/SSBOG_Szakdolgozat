import React from "react";
import {config} from "../../helpers/config";
import moment from "moment";

 const BasicUserData = props => (
    <div>
        <p>Név: {props.user.name}</p>
        <p>Felhasználónév: {props.user.userName}</p>
        <p>E-mail cím: {props.user.email}</p>
        <p>Születési dátum: {moment.utc(props.user.dateOfBirth).local().format(config.dateFormat)}</p>
    </div>
 )

 export default BasicUserData;