import {getCourseResultService} from "../services/ResultService";

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