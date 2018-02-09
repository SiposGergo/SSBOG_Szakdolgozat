import React from "react";
import ReactDOM from "react-dom";
import AppRouter from "./routers/AppRouter.js";
import { Provider } from "react-redux";
import configureStore from "./store/configureStore.js";

import 'semantic-ui-css/semantic.min.css';
import 'react-dates/lib/css/_datepicker.css';
import 'rc-slider/assets/index.css';

const store = configureStore();

const jsx = (
    <Provider store={store}>
        <AppRouter />
    </Provider>
);

ReactDOM.render(jsx, document.getElementById("app"));