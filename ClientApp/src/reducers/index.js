import { combineReducers } from 'redux';

import { authentication } from './AuthenticationReducer';
import { registration } from './RegistrationReducer';
import { users } from './UsersReducer';
import HikeListReducer from "./HikeListReducer";
import { reducer as notifReducer } from 'redux-notifications';
import { reducer as reduxFormReducer } from 'redux-form';
import {hikeDetailsReducer} from "./HikeDetailsReducer"
import {userPageReducer} from "./UserPageReducer";
import {hikeEditReducer} from "./HikeEditReducer";
import {courseEditReducer} from "./CourseEditReducer";

const rootReducer = combineReducers({
    HikeListReducer,
    authentication,
    registration,
    users,
    notifs: notifReducer,
    form: reduxFormReducer,
    hikeDetailsReducer,
    userPageReducer,
    hikeEditReducer,
    courseEditReducer
});

export default rootReducer;