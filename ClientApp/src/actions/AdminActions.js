import { SendDanger, SendSuccess } from "../services/NotificationSender";
import {
    postCheckpointPassService
} from "../services/AdminService";


export function postCheckpointPass(recordDto) {
    return dispatch => {
        postCheckpointPassService(recordDto)
            .then(
                json => dispatch(SendSuccess(json.message)),
                error => dispatch(SendDanger(error))
            );
    };
}