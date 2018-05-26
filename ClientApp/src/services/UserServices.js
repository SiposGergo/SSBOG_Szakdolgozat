import { authHeader } from "../helpers/auth-header";
import {config} from "../helpers/config";

import {handleResponse, handleError} from "./Handlers"

export const userService = {
    login,
    logout,
    register,
    getAll,
    getById,
    update,
    delete: _delete
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

function getAll() {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };
    return fetch(config.apiUrl + '/users/all/', requestOptions).then(handleResponse, handleError);
}

function getById(id) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(config.apiUrl + '/users/user/' + _id, requestOptions).then(handleResponse, handleError);
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

// prefixum mert foglalt kifejezés a delete
function _delete(id) {
    const requestOptions = {
        method: 'DELETE',
        headers: authHeader()
    };

    return fetch(config.apiUrl + '/users/delete/' + id, requestOptions).then(handleResponse, handleError);
}