import {postAddHike} from "../services/HikeService"
import { SendDanger, SendSuccess } from "../services/NotificationSender";
import {history} from "../helpers/history";

export function AddHike(hikeDto) {
    return dispatch => {
        postAddHike(hikeDto) 
        .then(
            registration => {
                dispatch(SendSuccess("Új túra siekresen felvéve"))
            },
            error => dispatch(SendDanger(error))
        );
    }
}
