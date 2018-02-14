import { combineReducers } from 'redux';

import { authentication } from './AuthenticationReducer';
import { registration } from './RegistrationReducer';
import { users } from './UsersReducer';
import { alert } from './AlertReducer';
import HikeListReducer from "./HikeListReducer";

const rootReducer = combineReducers({
    HikeListReducer,
    authentication,
    registration,
    users,
    alert
});

export default rootReducer;