import { config } from "../helpers/config";
import { handleResponse, handleError } from "./Handlers"
import { authHeader } from "../helpers/auth-header";

export function getHikeById(id) {
    const requestOptions = {
        method: 'GET'
    };

    return fetch(config.apiUrl + '/Hike/details/' + id, requestOptions)
        .then(handleResponse, handleError);
}

export function postCommentService(hikeId, userId, message) {
    const requestOptions = {
        method: 'PUT',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify({ hikeId, authorId:userId, commentText : message  })
    };

    return fetch(config.apiUrl + '/Hike/comment' , requestOptions)
        .then(handleResponse, handleError, );
}

export function postRegistrationService(hikeCourseId, userId) {
    const requestOptions = {
        method: 'PUT',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify({ hikerId:userId, hikeCourseId  })
    };

    return fetch(config.apiUrl + '/Register/register' , requestOptions)
        .then(handleResponse, handleError, );
}

export function postUnRegistrationService(hikeCourseId, userId) {
    const requestOptions = {
        method: 'PUT',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify({ hikerId:userId, hikeCourseId  })
    };

    return fetch(config.apiUrl + '/Register/unregister' , requestOptions)
        .then(handleResponse, handleError, );
}

export function postAddHike(hikeDto) {
    const requestOptions = {
        method: 'POST',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(hikeDto)
    };

    return fetch(config.apiUrl + '/Hike/add' , requestOptions)
        .then(handleResponse, handleError, );
}

export function postEditHikeService(hikeDto) {
    const requestOptions = {
        method: 'PUT',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(hikeDto)
    };

    return fetch(config.apiUrl + '/Hike/edit' , requestOptions)
        .then(handleResponse, handleError, );
}

export function postAddHikeHelperService(userName, hikeId) {
    const requestOptions = {
        method: 'PUT',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify({userName})
    };

    return fetch(config.apiUrl + '/Hike/add-helper/'+hikeId , requestOptions)
        .then(handleResponse, handleError, );
}

export function getTodayHikesService() {
    const requestOptions = {
        method: 'GET'
    };

    return fetch(config.apiUrl + '/Hike/today/', requestOptions)
        .then(handleResponse, handleError, );
}

export function getHikeListService() {
    const requestOptions = {
        method: 'GET'
    };

    return fetch(config.apiUrl + '/Hike/all/', requestOptions)
        .then(handleResponse, handleError, );
}