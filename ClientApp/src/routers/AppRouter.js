import React from "react";
import { Switch, BrowserRouter, Route } from 'react-router-dom';
import HikeListPage from "../components/HikeList/HikeListPage.js";
import Header from "../components/Header.js";
import HikeDetailsPage from "../components/HikeDetails/HikeDetailsPage.js"

import { PrivateRoute } from '../components/PrivateRouter';
import { HomePage } from '../components/HomePage';
import { LoginPage } from '../components/LoginPage';
import { RegisterPage } from '../components/RegisterPage';
import AlertBar from "../components/AlertBar";

const AppRouter = () => (
    <BrowserRouter>
        <div>
            <Header />
            <AlertBar/>
            <Switch>
                <Route exact={true} path="/hikes" component={HikeListPage} />
                <Route exact={true} path="/hike/:id" component={HikeDetailsPage} />
                <PrivateRoute exact path="/home" component={HomePage} />
                <Route path="/login" component={LoginPage} />
                <Route path="/register" component={RegisterPage} />
            </Switch>
        </div>
    </BrowserRouter>
)

export default AppRouter;