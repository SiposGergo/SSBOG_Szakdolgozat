import React from "react";
import { PrivateRoute } from '../components/PrivateRouter';
import { Switch, Router, Route } from 'react-router-dom';

import HikeListPage from "../components/HikeList/HikeListPage.js";
import Header from "../components/Header.js";
import HikeDetailsPage from "../components/HikeDetails/HikeDetailsPage.js"
import { HomePage } from '../components/HomePage';
import { LoginPage } from '../components/LoginPage';
import { UserDetailsPage } from "../components/UserDetailsaPage";
import ErrorPage from "../components/404Page";
import RegisteringPage from "../components/RegisteringPage";
import UserPage from "../components/UserPage/UserPage";
import AddHikePage from "../components/AddHikePage";
import EditHikePage from "../components/EditHikePage";
import AddCoursePage from "../components/AddCoursePage";
import EditCoursePage from "../components/EditCoursePage";
import TodayHikes from "../components/TodayHikes";
import AdminPage from "../components/AdminPage";
import CourseResultPage from "../components/CourseResultPage/CourseResultPage";
import ForgottenPasswordPage from "../components/ForgottenPasswordPage";
import ChangePasswordPage from "../components/ChangePasswordPage";

import { history } from "../helpers/history"

const AppRouter = () => (
    <Router history={history}>
        <div>
            <Header />
            <Switch>
                <Route exact={true} path="/hikes" component={HikeListPage} />
                <PrivateRoute exact={true} path="/hike/add" component={AddHikePage} />
                <PrivateRoute exact={true} path="/hike/edit/:id" component={EditHikePage} />
                <PrivateRoute exact={true} path="/hike/add-course/:id" component={AddCoursePage} />
                <PrivateRoute exact={true} path="/hike/admin/:id" component={AdminPage} />
                <PrivateRoute exact={true} path="/course/edit/:hikeId/:courseId" component={EditCoursePage} />
                <Route exact={true} path="/course/live/:id" component={CourseResultPage} />
                <Route exact={true} path="/hike/:id" component={HikeDetailsPage} />
                <PrivateRoute exact={true} path="/home" component={HomePage} />
                <PrivateRoute exact={true} path="/me" component={UserDetailsPage} />
                <Route exact={true} path="/login" component={LoginPage} />
                <Route exact={true} path="/register" component={RegisteringPage} />
                <Route exact={true} path="/user/:id" component={UserPage} />
                <Route exact={true} path="/forgotten-password" component={ForgottenPasswordPage} />
                <Route exact={true} path="/change-password" component={ChangePasswordPage} />
                <Route path="/" exact={true} component={TodayHikes} />
                <Route path="/" component={ErrorPage} />
            </Switch>
        </div>
    </Router>
)

export default AppRouter;