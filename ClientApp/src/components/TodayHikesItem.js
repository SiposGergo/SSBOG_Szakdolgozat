import React from "react";
import { Link } from "react-router-dom";

function isHelper(staff, hiker) {
    if (!staff || !hiker) {return false}
    const filtered = staff.filter((s)=> s.hikerId == hiker.id);
    return filtered.length ? true : false;
}

const TodayHikesItem = (props) => (
    <div>
        <h4>{props.hike.name}</h4>
        {isHelper(props.hike.staff, props.user) && 
            <Link to={
                { pathname: "/hike/admin/"+props.hike.id, state: {courses: props.hike.courses}}}>
                Adminisztráció
            </Link>} 
        {
            props.hike.courses.map((course) => (<div key={course.id}>
                <Link to={"/course/live/"+course.id}>
                    {course.name}
                </Link>
                </div>))
        }
    </div>
)

export default TodayHikesItem;