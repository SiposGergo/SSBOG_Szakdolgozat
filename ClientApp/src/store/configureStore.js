import { createStore, applyMiddleware } from "redux";
import filtersReducer from "../reducers/HikeListReducer"
import thunk from "redux-thunk";


export default () => {
    const store =  createStore(filtersReducer,applyMiddleware(thunk));
    return store;
}
