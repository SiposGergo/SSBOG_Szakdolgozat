import {postAddCourseToHikeService} from "../services/CourseServices"
import { SendDanger, SendSuccess } from "../services/NotificationSender";

export function AddCourseToHike(courseDto, hikeId) {
    return dispatch => {
        postAddCourseToHikeService(courseDto, hikeId) 
        .then(
            ok => {
                dispatch(SendSuccess("Új táv siekresen felvéve"))},
            error => dispatch(SendDanger(error))
        );
    }
}


