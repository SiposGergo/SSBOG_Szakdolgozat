import { config } from "../helpers/config";
import { handleResponse, handleError } from "./Handlers"

export function getCourseResultService(courseId) {
    const requestOptions = {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' }
    };

    return fetch(config.apiUrl + '/Result/result/'+courseId , requestOptions)
        .then(handleResponse, handleError );
}

export function getCourseLiveResultNettoTimeService(courseId) {
    const requestOptions = {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' }
    };

    return fetch(config.apiUrl + '/Result/live-result-netto/'+courseId , requestOptions)
        .then(handleResponse, handleError );
}

export function getCourseLiveResultService(courseId) {
    const requestOptions = {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' }
    };

    return fetch(config.apiUrl + '/Result/live-result/'+courseId , requestOptions)
        .then(handleResponse, handleError );
}