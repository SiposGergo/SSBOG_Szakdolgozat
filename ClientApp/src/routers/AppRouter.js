import React from "react";
import { PrivateRoute } from '../components/PrivateRouter';
import { Switch, Router, Route } from 'react-router-dom';

import HikeListPage from "../components/HikeList/HikeListPage.js";
import Header from "../components/Header.js";
import HikeDetailsPage from "../components/HikeDetails/HikeDetailsPage.js"
import { HomePage } from '../components/HomePage';
import { LoginPage } from '../components/LoginPage';
import { RegisterPage } from '../components/RegisterPage';
import {UserDetailsPage} from "../components/UserDetailsaPage";
import ErrorPage from "../components/404Page";

import RegisteringPage from "../components/RegisteringPage";

import {history} from "../helpers/history"

const AppRouter = () => (
    <Router history={history}>
        <div>
            <Header />
            <Switch>
                <Route exact={true} path="/hikes" component={HikeListPage} />
                <Route exact={true} path="/hike/:id" component={HikeDetailsPage} />
                <PrivateRoute exact={true} path="/home" component={HomePage} />
                <PrivateRoute exact={true} path="/me" component={UserDetailsPage} />
                <Route exact={true} path="/login" component={LoginPage} />
                <Route exact={true} path="/register" component={RegisteringPage} />
                <Route path="/" component={ErrorPage} />
            </Switch>
        </div>
    </Router>
)

export default AppRouter;