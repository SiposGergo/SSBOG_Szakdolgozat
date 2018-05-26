import {getHikeById, postCommentService} from "../services/HikeService";
import { SendDanger, SendSuccess } from "../services/NotificationSender";

export function itemsHasErrored() {
    return {
        type: 'HIKE_DETAILS_ITEMS_HAS_ERRORED',
        hasErrored: true,
    };
}
export function itemsIsLoading() {
    return {
        type: 'HIKE_DETAILS_ITEMS_IS_LOADING',
        isLoading: true
    };
}
export function itemsFetchDataSuccess(details) {
    return {
        type: 'HIKE_DETAILS_ITEMS_FETCH_DATA_SUCCESS',
        details
    };
}

export function loadNewComment(comment) {
    return {
    type: 'HIKE_DETAILS_LOAD_NEW_COMMENTS',
    comment}
}

export function postComent(hikeId, userId, message) {
    return dispatch => {
        postCommentService(hikeId, userId, message)
            .then(
                comment => dispatch(loadNewComment(comment)),
                error => dispatch(SendDanger("Sikertelen komment küldés."))
            );
    };
}

export function getHikeDetails(id) {
    return dispatch => {
        dispatch(itemsIsLoading());

        getHikeById(id)
            .then(
                details => dispatch(itemsFetchDataSuccess(details)),
                error => dispatch(itemsHasErrored())
            );
    };
}