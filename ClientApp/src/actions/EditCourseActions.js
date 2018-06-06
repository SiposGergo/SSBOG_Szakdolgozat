import { SendDanger, SendSuccess } from "../services/NotificationSender";
import {
    postEditCourseService
} from "../services/CourseServices";


export function postEditCourse(courseDto, courseId) {
    return dispatch => {
        postEditCourseService(courseDto, courseId)
            .then(
                details => dispatch(SendSuccess("Sikeres módosítás!")),
                error => dispatch(SendDanger(error))
            );
    };
}


