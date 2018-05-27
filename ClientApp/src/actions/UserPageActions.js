import { userService } from "../services/UserServices";
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
export function getUserById(id) {
    return dispatch => {
        dispatch(itemsIsLoading());
        console.log(id);
        userService.getById(id)
            .then(
                user =>  dispatch(itemsFetchDataSuccess(user)) ,
                error => dispatch(itemsHasErrored(error))
            );
    };
}