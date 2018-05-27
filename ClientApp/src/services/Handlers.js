export function handleResponse(response) {
    console.log(response);
    return new Promise((resolve, reject) => {
        if (response.ok) {
            // jsont adunk vissza ha volt a responseban
            var contentType = response.headers.get("content-type");
            if (contentType && contentType.includes("application/json")) {
                response.json().then(json => resolve(json));
            } else {
                resolve();
            }
        }
        else if (response.status == 401) {
            reject("Nincs jogod az oldal megtekintéséhez!");
        }
        else {
            // hibaüzenetet adunk vissza ha hiba történt
            response.text().then(text => reject(text));
        }
    });
}

export function handleError(error) {
    return Promise.reject(error && error.message);
}