import React from "react";
import { connect } from "react-redux";
import { NavLink } from "react-router-dom";

import { userActions } from "../actions/UserActions";

class Header extends React.Component {

    onLogoutClick = () => {
        this.props.logout();
    }

    render() {
        const user = this.props.user;
        return (
            <header>
                <h1>HikeX Rendszer</h1>
                <h3>{user && "Üdv az oldalon " + user.userName}</h3>
                <NavLink exact={true} to="/hikes" activeClassName="is-active">Túrák</NavLink>
                {!user && <NavLink exact={true} to="/login" activeClassName="is-active">Bejelentkezés</NavLink>}
                {!user && <NavLink exact={true} to="/register" activeClassName="is-active">Regisztráció</NavLink>}
                {user && <button className="btn" onClick={this.onLogoutClick}>Kilépés</button>}
                {user && <NavLink exact={true} to="/me" activeClassName="is-active">Profilom</NavLink>}
            </header>)
    }
}

const mapStateToProps = (state) => ({ user: state.authentication.user })

const mapDispatchToProps = (dispatch) => { return { logout: (history) => dispatch(userActions.logout(history)) } }


export default connect(mapStateToProps, mapDispatchToProps)(Header);