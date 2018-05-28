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

    return fetch(config.apiUrl + '/Hike/register' , requestOptions)
        .then(handleResponse, handleError, );
}

export function postUnRegistrationService(hikeCourseId, userId) {
    const requestOptions = {
        method: 'PUT',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify({ hikerId:userId, hikeCourseId  })
    };

    return fetch(config.apiUrl + '/Hike/unregister' , requestOptions)
        .then(handleResponse, handleError, );
}