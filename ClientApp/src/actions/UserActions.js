import { userService } from '../services/UserServices';

import { SendDanger, SendSuccess, SendWarning } from "../services/NotificationSender";

import { history } from "../helpers/history";

export const userActions = {
    login,
    logout,
    register,
    update,
    changePassword,
    forgottenPassword
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
                    if (user.mustChangePassword) {
                        dispatch(SendWarning("Biztonsági okokból a generált jelszavad meg kell változtatnod!"));
                        history.push('/change-password');
                    }
                    else { history.push('/user/' + user.id) }

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

const passwordChangeSuccess = () => ({ type: 'PASSWORD_CHANGE_SUCCESS' })

function changePassword(dto) {
    return dispatch => {
        userService.postChangePasswordService(dto)
            .then(
                () => {
                    dispatch(SendSuccess("Sikeres jelszó változtatás!"));
                    dispatch(passwordChangeSuccess());
                    history.push('/');
                },
                (error) => dispatch(SendDanger(error))
            );
    };
}

function forgottenPassword(dto) {
    return dispatch => {
        userService.postForgottenPasswordService(dto)
            .then(
                () => dispatch(SendSuccess("Új jelszavadat elküldtük emailben!")),
                (error) => dispatch(SendDanger(error))
            );
    };
}