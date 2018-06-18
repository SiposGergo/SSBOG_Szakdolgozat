import { config } from "../helpers/config";
import { handleResponse, handleError } from "./Handlers"
import { authHeader } from "../helpers/auth-header";

export function postCheckpointPassService(recordDto) {
    const requestOptions = {
        method: 'POST',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(recordDto)
    };

    return fetch(config.apiUrl + '/Admin/record-checkpoint-pass/', requestOptions)
        .then(handleResponse, handleError, );
}