import React from "react";
import { NavLink } from "react-router-dom";

const Header = (props) => (
    <header>
        <h1>HikeX Rendszer</h1>
        <NavLink exact={true} to="/hikes" activeClassName="is-active">Túrák</NavLink>
    </header>
)

export default Header;