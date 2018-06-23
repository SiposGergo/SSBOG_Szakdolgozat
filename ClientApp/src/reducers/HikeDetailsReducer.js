const defaultState = {
    hike: { comments: [], organizer: {}, courses: [] },
    isLoading: true,
    hasErrored: false
}

export const hikeDetailsReducer = (state = defaultState, action) => {
    switch (action.type) {

        case 'HIKE_DETAILS_ITEMS_HAS_ERRORED':
            return { ...state, hasErrored: action.hasErrored }

        case 'HIKE_DETAILS_ITEMS_IS_LOADING':
            return { ...state, isLoading: action.isLoading };

        case 'HIKE_DETAILS_ITEMS_FETCH_DATA_SUCCESS':
            return { hike: action.details }

        case "HIKE_DETAILS_LOAD_NEW_COMMENTS":
            return {
                hike: { ...state.hike, comments: [...state.hike.comments, action.comment] }
            }

        case "HIKE_DETAILS_LOAD_NEW_REGISTRATION_HIKE":
            let course = state.hike.courses.find(x => x.id == action.registration.hikeCourseId)
            const newCourses = state.hike.courses.filter(x => x.id != action.registration.hikeCourseId)
            course = { ...course, numOfRegisteredHikers: course.numOfRegisteredHikers + 1 }
            const newHike = { ...state.hike, courses: [...newCourses, course] }
            return { ...state, hike: newHike }

        case "HIKE_DETAILS_DELETE_OLD_REGISTRATION_HIKE":
            course = state.hike.courses.find(x => x.id == action.idToDelete)
            const newCourses2 = state.hike.courses.filter(x => x.id != action.idToDelete)
            course = { ...course, numOfRegisteredHikers: course.numOfRegisteredHikers - 1 }
            const newHike2 = { ...state.hike, courses: [...newCourses2, course] }
            return { ...state, hike: newHike2 }

        default:
            return state
    }
};