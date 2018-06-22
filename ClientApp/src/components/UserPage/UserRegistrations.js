import React from "react";
import { NavLink } from "react-router-dom";

const UserRegistrations = (props) => (
    <div>
            {
                props.registrations.map((reg) =>
                    <div key={reg.id} className="own-hike-item">
                        <NavLink className="link" exact={true} to={"/hike/" + reg.hikeCourse.hikeId} activeClassName="is-active">
                            {reg.hikeCourse.name}
                        </NavLink>
                        <p>Rajtszám: {reg.startNumber}</p>
                    </div>)
            }
    </div>
)

export default UserRegistrations;