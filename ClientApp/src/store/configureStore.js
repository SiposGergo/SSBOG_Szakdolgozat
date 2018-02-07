import { createStore, applyMiddleware } from "redux";
import filtersReducer from "../reducers/filters.js"
import thunk from "redux-thunk";


export default () => {
    const store =  createStore(filtersReducer,applyMiddleware(thunk));
    return store;
}
