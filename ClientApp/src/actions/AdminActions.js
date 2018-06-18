import { SendDanger, SendSuccess } from "../services/NotificationSender";
import {
    postCheckpointPassService
} from "../services/AdminService";


export function postCheckpointPass(recordDto) {
    return dispatch => {
        postCheckpointPassService(recordDto)
            .then(
                msg => dispatch(SendSuccess("Áthaladás rögzítve!")),
                error => dispatch(SendDanger(error))
            );
    };
}