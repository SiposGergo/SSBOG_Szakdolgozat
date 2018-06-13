import React from "react";
import { NavLink } from "react-router-dom";

function isHelper(staff, hikerId) {
    if (!staff) {return false}
    const filtered = staff.filter((s)=> s.hikerId == hikerId);
    return filtered.length ? true : false;
}

const TodayHikesItem = (props) => (
    <div>
        <h4>{props.hike.name}</h4>
        {isHelper(props.hike.staff, props.user.id) && <NavLink exact={true} to={"/hike/admin/"+props.hike.id} activeClassName="is-active">
        Adminisztráció
    </NavLink>} 
    </div>
)

export default TodayHikesItem;