import { userService } from "../services/UserServices";
import {postAddHikeHelperService} from "../services//HikeService";
import { SendDanger, SendSuccess } from "../services/NotificationSender";
import {getCourseInfoPdfService} from "../services/CourseServices";
import download from "downloadjs";

export function itemsHasErrored(error) {
    return {
        type: 'USER_PAGE_ITEMS_HAS_ERRORED',
        error
    };
}
export function itemsIsLoading() {
    return {
        type: 'USER_PAGE_ITEMS_IS_LOADING',
        isLoading: true
    };
}
export function itemsFetchDataSuccess(user) {
    return {
        type: 'USER_PAGE_ITEMS_FETCH_DATA_SUCCESS',
        user
    };
}

export function openModal() {
    return {
        type: 'USER_PAGE_OPEN_MODAL'
    };
}

export function closeModal() {
    return {
        type: 'USER_PAGE_CLOSE_MODAL'
    };
}

export function postAddHikeHelper(userName, hikeId) {
    return dispatch => {
        postAddHikeHelperService(userName, hikeId)
            .then(
                () =>  {
                    dispatch(SendSuccess("Segítő felvéve!"))
                    dispatch(closeModal())
                } ,
                error => dispatch(SendDanger(error))
            );
    };
}

export function getUserById(id) {
    return dispatch => {
        dispatch(itemsIsLoading());
        userService.getById(id)
            .then(
                user =>  dispatch(itemsFetchDataSuccess(user)) ,
                error => dispatch(itemsHasErrored(error))
            );
    };
}

export function getCoursePdfInfo(id) {
    return dispatch => {
        getCourseInfoPdfService(id)
            .then(
                result =>  download(result, "nevezettek.pdf", "application/pdf") ,
                error => dispatch(SendDanger(error))
            );
    };
}
