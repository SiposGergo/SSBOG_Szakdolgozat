import React from "react";
import { NavLink } from "react-router-dom";

const UserRegistrations = (props) => (
    <div>
        Előnevezések:
        <ul className="list-group">
            {
                props.registrations.map((reg) =>
                    <div key={reg.id} className="list-group-item">
                        <NavLink exact={true} to={"/hike/" + reg.hikeCourse.hikeId} activeClassName="is-active">
                            {reg.hikeCourse.name}
                        </NavLink>
                        Rajtszám: {reg.startNumber}
                    </div>)
            }</ul>
    </div>
)

export default UserRegistrations;