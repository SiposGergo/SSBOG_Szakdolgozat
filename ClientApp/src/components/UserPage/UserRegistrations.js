import React from "react";
import { NavLink } from "react-router-dom";

const UserRegistrations = (props) => (
    <div>
    Előnevezések:
    {
        props.registrations.map((reg) =>
            <div key={reg.id}>
                <NavLink exact={true} to={"/hike/" + reg.hikeCourse.hikeId} activeClassName="is-active">
                    {reg.hikeCourse.name}
                </NavLink>
                Rajtszám: {reg.startNumber}
            </div>
        )}
    </div>
)

export default UserRegistrations;