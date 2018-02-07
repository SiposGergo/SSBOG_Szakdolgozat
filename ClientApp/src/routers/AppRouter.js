import React from "react";
import { Switch, BrowserRouter, Route } from 'react-router-dom';
import HikeListPage from "../components/HikeList/HikeListPage.js";
import Header from "../components/Header.js";
import HikeDetailsPage from "../components/HikeDetails/HikeDetailsPage.js"

const AppRouter = () => (
    <BrowserRouter>
        <div>
            <Header />
            <Switch>
                <Route exact={true} path="/hikes" component={HikeListPage} />
                <Route exact={true} path="/hike/:id" component={HikeDetailsPage} />
            </Switch>
        </div>
    </BrowserRouter>
)

export default AppRouter;