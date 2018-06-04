import { SendDanger, SendSuccess } from "../services/NotificationSender";
import {
    getCourseDetailsService,
    postEditCourseService
} from "../services/CourseServices";


export function itemsFetchDataSuccess(details) {
    return {
        type: 'COURSE_DETAILS_FOR_EDIT_FETCH_DATA_SUCCESS',
        details
    };
}

export function itemsHasErrored() {
    return {
        type: 'COURSEE_DETAILS_FOR_EDIT_HAS_ERRORED'
    };
}

export function deleteData() {
    return {
        type: 'COURSE_DETAILS_FOR_EDIT_DELETE_DATA'
    };
}

export function getCourseDetails(id) {
    return dispatch => {
        getCourseDetailsService(id)
            .then(
                details => dispatch(itemsFetchDataSuccess(details)),
                error => dispatch(itemsHasErrored())
            );
    };
}

export function postEditCourse(courseDto, courseId) {
    return dispatch => {
        postEditCourseService(courseDto, courseId)
            .then(
                details => dispatch(SendSuccess("Sikeres módosítás!")),
                error => dispatch(SendDanger(error))
            );
    };
}


