import React from "react";
import { Switch, BrowserRouter, Route } from 'react-router-dom';
import HikeListPage from "../components/HikeListPage.js";
import Header from "../components/Header.js";

const AppRouter = () => (
    <BrowserRouter>
        <div>
            <Header />
            <Switch>
                <Route exact={true} path="/hikes" component={HikeListPage} />
            </Switch>
        </div>
    </BrowserRouter>
)

export default AppRouter;