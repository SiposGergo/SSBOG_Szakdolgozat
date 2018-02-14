import { userService } from '../services/UserServices';
import { alertActions } from './AlertActions';
import { history } from '../helpers/history';

export const userActions = {
    login,
    logout,
    register,
    getAll,
    delete: _delete
};

function login(username, password) {
    return dispatch => {
        dispatch(request({ username }));

        userService.login(username, password)
            .then(
                user => { 
                    dispatch(success(user));
                    history.push('/');
                },
                error => {
                    dispatch(failure(error));
                    dispatch(alertActions.error(error));
                }
            );
    };

    function request(user) { return { type: 'USERS_LOGIN_REQUEST', user } }
    function success(user) { return { type: 'USERS_LOGIN_SUCCESS', user } }
    function failure(error) { return { type: 'USERS_LOGIN_FAILURE', error } }
}

function logout() {
    userService.logout();
    return { type: 'USERS_LOGOUT' };
}

function register(user) {
    return dispatch => {
        dispatch(request(user));

        userService.register(user)
            .then(
                () => { 
                    dispatch(success());
                    history.push('/login');
                    dispatch(alertActions.success('Registration successful'));
                },
                error => {
                    dispatch(failure(error));
                    dispatch(alertActions.error(error));
                }
            );
    };

    function request(user) { return { type: 'USERS_REGISTER_REQUEST', user } }
    function success(user) { return { type: 'USERS_REGISTER_SUCCESS', user } }
    function failure(error) { return { type: 'USERS_REGISTER_FAILURE', error } }
}

function getAll() {
    return dispatch => {
        dispatch(request());

        userService.getAll()
            .then(
                users => dispatch(success(users)),
                error => dispatch(failure(error))
            );
    };

    function request() { return { type: 'USERS_GETALL_REQUEST' } }
    function success(users) { return { type: 'USERS_GETALL_SUCCESS', users } }
    function failure(error) { return { type: 'USERS_GETALL_FAILURE', error } }
}

// prefixed function name with underscore because delete is a reserved word in javascript
function _delete(id) {
    return dispatch => {
        dispatch(request(id));

        userService.delete(id)
            .then(
                () => { 
                    dispatch(success(id));
                },
                error => {
                    dispatch(failure(id, error));
                }
            );
    };

    function request(id) { return { type: 'USERS_DELETE_REQUEST', id } }
    function success(id) { return { type: 'USERS_DELETE_SUCCESS', id } }
    function failure(id, error) { return { type: 'USERS_DELETE_FAILURE', id, error } }
}