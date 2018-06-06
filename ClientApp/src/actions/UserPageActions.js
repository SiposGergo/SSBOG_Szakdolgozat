import { userService } from "../services/UserServices";
import {postAddHikeHelperService} from "../services//HikeService";
import { SendDanger, SendSuccess } from "../services/NotificationSender";

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