import { combineReducers } from 'redux';

import { authentication } from './AuthenticationReducer';
import { registration } from './RegistrationReducer';
import { users } from './UsersReducer';
import HikeListReducer from "./HikeListReducer";
import { reducer as notifReducer } from 'redux-notifications';

const rootReducer = combineReducers({
    HikeListReducer,
    authentication,
    registration,
    users,
    notifs: notifReducer
});

export default rootReducer;