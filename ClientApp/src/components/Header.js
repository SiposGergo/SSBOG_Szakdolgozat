import React from "react";
import { connect } from "react-redux";
import { NavLink } from "react-router-dom";

import { userActions } from "../actions/UserActions";

import logo from "../images/icon.png"

export class Header extends React.Component {

    onLogoutClick = () => {
        this.props.logout();
    }

    render() {
        const user = this.props.user;
        return (
            <header className="sideNav">

                <h1>HikeX</h1>
                <NavLink exact={true} to="/" activeClassName="is-active">
                    <img src={logo} className="logo" />
                </NavLink>
                <h3><center>{user && "Üdv az oldalon, " + user.userName + "!"}</center></h3>


                {<NavLink exact={true} to="/" activeClassName="is-active" className="menu-item menu-item-first">Főoldal</NavLink>}
                {!user && <NavLink exact={true} to="/login" activeClassName="is-active" className="menu-item">Bejelentkezés</NavLink>}
                {!user && <NavLink exact={true} to="/register" activeClassName="is-active" className="menu-item">Regisztráció</NavLink>}
                <NavLink exact={true} to="/hikes" activeClassName="is-active" className="menu-item">Túrák</NavLink>
                {user && <NavLink exact={true} to="/me" activeClassName="is-active" className="menu-item">Adataim</NavLink>}
                {user && <NavLink exact={true} to={"/user/" + user.id} activeClassName="is-active" className="menu-item">Oldalam</NavLink>}
                {user && <NavLink exact={true} to={"/hike/add"} activeClassName="is-active" className="menu-item">Új túra</NavLink>}
                {user && <NavLink onClick={this.onLogoutClick} to="#" activeClassName="is-active" className="menu-item" >Kilépés</NavLink>}





            </header>)
    }
}

const mapStateToProps = (state) => ({ user: state.authentication.user })

const mapDispatchToProps = (dispatch) => { return { logout: (history) => dispatch(userActions.logout(history)) } }


export default connect(mapStateToProps, mapDispatchToProps, null, { pure: false })(Header);