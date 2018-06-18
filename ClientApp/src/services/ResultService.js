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