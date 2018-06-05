import React from "react";
import ReactDOM from "react-dom";
import AppRouter from "./routers/AppRouter.js";
import { Provider } from "react-redux";
import configureStore from "./store/configureStore.js";
import { Notifs } from 'redux-notifications';

import "./styles/main.scss";

const store = configureStore();

const jsx = (
    <Provider store={store}>
        <div className="container-fluid">
            <AppRouter />
            <Notifs/>
        </div>
    </Provider>
);

ReactDOM.render(jsx, document.getElementById("app"));