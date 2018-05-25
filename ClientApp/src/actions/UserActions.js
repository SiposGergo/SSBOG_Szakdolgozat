import { userService } from '../services/UserServices';

import { SendDanger, SendSuccess } from "../services/NotificationSender";

import { history } from "../helpers/history";

export const userActions = {
    login,
    logout,
    register,
    getAll,
    delete: _delete,
    update
};

// Bejelentkezés
function login(username, password) {
    const request = (user) => ({ type: 'USERS_LOGIN_REQUEST', user })
    const success = (user) => ({ type: 'USERS_LOGIN_SUCCESS', user })
    const failure = (error) => ({ type: 'USERS_LOGIN_FAILURE', error })
    return dispatch => {
        dispatch(request({ username }));

        userService.login(username, password)
            .then(
                user => {
                    dispatch(SendSuccess(`Üdv az oldalon, ${user.userName}`));
                    dispatch(success(user));
                    history.push('/home');
                },
                error => {
                    dispatch(failure(error));
                    dispatch(SendDanger("Sikertelen belépés!"));
                }
            );
    };
}

// Kijelentkezés
function logout() {
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
                    dispatch(SendSuccess("Sikere regisztáció!"));
                },
                error => {
                    dispatch(failure(error));
                    dispatch(SendDanger(error));
                }
            );
    };
}

function update(user) {
    const success = (user) => ({ type: 'USER_UPDATE_SUCCESS', user })
    return (dispatch) => {
        userService.update(user).
            then(
                () => {
                    console.log(user)
                    dispatch(success(user))
                    dispatch(SendSuccess("Sikeres adatmódosítás"));
                },
                (error) => {
                    dispatch(SendDanger(error));
                })
    }
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