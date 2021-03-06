import { authHeader } from "../helpers/auth-header";
import {config} from "../helpers/config";
import {handleResponse, handleError} from "./Handlers"

export const userService = {
    login,
    logout,
    register,
    getById,
    update,
    postChangePasswordService,
    postForgottenPasswordService,
    getAuthTestService
};

function login(username, password) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password })
    };

    return fetch(config.apiUrl + '/users/authenticate', requestOptions)
        .then(handleResponse, handleError)
        .then(user => {
            // sikeres a login ha van jwt
            if (user && user.token) {
                // loalstoreban tároljuk el a usert
                localStorage.setItem('user', JSON.stringify(user));
            }
            return user;
        });
}

function logout() {
    // ha kitöröljük kivan jelentkezve
    localStorage.removeItem('user');
}

function getById(id) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(config.apiUrl + '/users/user/' + id, requestOptions).then(handleResponse, handleError);
}

function register(user) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(user)
    };

    return fetch(config.apiUrl + '/users/register', requestOptions).then(handleResponse, handleError);
}

function update(user) {
    const requestOptions = {
        method: 'PUT',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(user)
    };

    return fetch(config.apiUrl + '/users/edit/' + user.id, requestOptions).then(handleResponse, handleError);
}

function postChangePasswordService(dto) {
    const requestOptions = {
        method: 'POST',
        headers: {...authHeader(), 'Content-Type': 'application/json'},
        body : JSON.stringify(dto)
    };
    return fetch(config.apiUrl + '/users/change-password/', requestOptions).then(handleResponse, handleError);
}

function postForgottenPasswordService(dto) {
    const requestOptions = {
        method: 'POST',
        headers: {...authHeader(), 'Content-Type': 'application/json'},
        body : JSON.stringify(dto)
    };
    return fetch(config.apiUrl + '/users/forgotten-password/', requestOptions).then(handleResponse, handleError);
}

function getAuthTestService() {
    const requestOptions = {
        method: 'GET',
        headers: {...authHeader()},
    };
    return fetch(config.apiUrl + '/users/test/', requestOptions).then(handleResponse, handleError);
}