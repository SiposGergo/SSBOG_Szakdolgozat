import { userService } from '../services/UserServices';
import { alertActions } from './AlertActions';

export const userActions = {
    login,
    logout,
    register,
    getAll,
    delete: _delete
};

// Bejelentkezés
function login(username, password, history) {
    const request = (user) => ({ type: 'USERS_LOGIN_REQUEST', user })
    const success = (user) => ({ type: 'USERS_LOGIN_SUCCESS', user })
    const failure = (error) => ({ type: 'USERS_LOGIN_FAILURE', error })
    return dispatch => {
        dispatch(request({ username }));

        userService.login(username, password)
            .then(
            user => {
                dispatch(success(user));
                history.push('/home');
            },
            error => {
                dispatch(failure(error));
                dispatch(alertActions.error(error));
            }
            );
    };
}

// Kijelentkezés
function logout(history) {
    userService.logout();
    history.push("/login");
    return { type: 'USERS_LOGOUT' };
}

// regisztráció
function register(user, history) {
    const request = (user) => ({ type: 'USERS_REGISTER_REQUEST', user })
    const success = (user) => ({ type: 'USERS_REGISTER_SUCCESS', user })
    const failure = (error) => ({ type: 'USERS_REGISTER_FAILURE', error })
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
}

// összes user lekérése
function getAll() {
    const request = () => ({ type: 'USERS_GETALL_REQUEST' })
    const success = (users) => ({ type: 'USERS_GETALL_SUCCESS', users })
    const failure = (error) => ({ type: 'USERS_GETALL_FAILURE', error })
    return dispatch => {
        dispatch(request());

        userService.getAll()
            .then(
            users => dispatch(success(users)),
            error => dispatch(failure(error))
            );
    };
}

// törlés
// _ mert fentartott kifejezés a delete
function _delete(id) {
    const request = (id) => ({ type: 'USERS_DELETE_REQUEST', id })
    const success = (id) => ({ type: 'USERS_DELETE_SUCCESS', id })
    const failure = (id, error) => ({ type: 'USERS_DELETE_FAILURE', id, error })
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
}