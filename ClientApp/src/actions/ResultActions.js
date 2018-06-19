import {getCourseResultService, getCourseLiveResultService, getCourseLiveResultNettoTimeService} from "../services/ResultService";

export function itemsHasErrored() {
    return {
        type: 'RESULT_ITEMS_HAS_ERRORED'
    };
}

export function itemsIsLoading() {
    return {
        type: 'RESULT_ITEMS_IS_LOADING'
    };
}

export function itemsFetchDataSuccess(data) {
    return {
        type: 'RESULT_ITEMS_FETCH_DATA_SUCCESS',
        data
    };
}

export function itemsUpdated(data) {
    return {
        type: 'RESULT_ITEMS_UPDATED',
        data
    };
}

export function getCourseResult(courseId) {
    return dispatch => {
        dispatch(itemsIsLoading());

        getCourseResultService(courseId)
            .then(
                data => dispatch(itemsFetchDataSuccess(data)),
                () => dispatch(itemsHasErrored())
            );
    };
}


export function getCourseLiveResult(courseId) {
    return dispatch => {

        getCourseLiveResultService(courseId)
            .then(
                data => dispatch(itemsUpdated(data)),
                () => dispatch(itemsHasErrored())
            );
    };
}

export function getCourseLiveResultNetto(courseId) {
    return dispatch => {

        getCourseLiveResultNettoTimeService(courseId)
            .then(
                data => dispatch(itemsUpdated(data)),
                () => dispatch(itemsHasErrored())
            );
    };
}