import { SendDanger, SendSuccess } from "../services/NotificationSender";
import {
    getHikeById,
    postEditHikeService
} from "../services/HikeService";


export function itemsFetchDataSuccess(details) {
    return {
        type: 'HIKE_DETAILS_FOR_EDIT_FETCH_DATA_SUCCESS',
        details
    };
}

export function itemsHasErrored() {
    return {
        type: 'HIKE_DETAILS_FOR_EDIT_HAS_ERRORED'
    };
}

export function deleteData() {
    return {
        type: 'HIKE_DETAILS_FOR_EDIT_DELETE_DATA'
    };
}

export function getHikeDetails(id) {
    return dispatch => {
        getHikeById(id)
            .then(
                details => dispatch(itemsFetchDataSuccess(details)),
                error => dispatch(itemsHasErrored())
            );
    };
}

export function postEditHike(id) {
    return dispatch => {
        postEditHikeService(id)
            .then(
                details => dispatch(SendSuccess("Sikeres módosítás!")),
                error => dispatch(SendDanger(error))
            );
    };
}


