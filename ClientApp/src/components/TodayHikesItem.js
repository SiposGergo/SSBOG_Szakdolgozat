import React from "react";
import { NavLink } from "react-router-dom";

function isHelper(staff, hiker) {
    if (!staff || !hiker) {return false}
    const filtered = staff.filter((s)=> s.hikerId == hiker.id);
    return filtered.length ? true : false;
}

const TodayHikesItem = (props) => (
    <div>
        <h4>{props.hike.name}</h4>
        {isHelper(props.hike.staff, props.user) && 
            <NavLink exact={true} to={
                { pathname: "/hike/admin/"+props.hike.id, state: {courses: props.hike.courses}}}
                activeClassName="is-active">
                Adminisztráció
            </NavLink>} 
        {
            props.hike.courses.map((course) => (<div key={course.id}>
                <NavLink exact={true} to={"/course/live/"+course.id} activeClassName="is-active">
                    {course.name}
                </NavLink>
                </div>))
        }
    </div>
)

export default TodayHikesItem;