import { config } from "../helpers/config";
import { handleResponse, handleError } from "./Handlers"
import { authHeader } from "../helpers/auth-header";

export function postAddCourseToHikeService(courseDto, hikeId) {
    const requestOptions = {
        method: 'PUT',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(courseDto)
    };

    return fetch(config.apiUrl + '/Course/add/'+hikeId , requestOptions)
        .then(handleResponse, handleError, );
}

export function getCourseDetailsService(courseId) {
    const requestOptions = {
        method: 'GET'
    };

    return fetch(config.apiUrl + '/Course/details/'+courseId , requestOptions)
        .then(handleResponse, handleError, );
}

export function postEditCourseService(courseDto, courseId) {
    const requestOptions = {
        method: 'PUT',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(courseDto)
    };

    return fetch(config.apiUrl + '/Course/edit/'+courseId , requestOptions)
        .then(handleResponse, handleError, );
}