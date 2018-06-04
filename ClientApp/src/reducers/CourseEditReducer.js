const defaultState = {
    course: { },
    hasErrored: false
}

export const courseEditReducer = (state = defaultState, action) => {
    switch (action.type) {
        
        case 'COURSE_DETAILS_FOR_EDIT_FETCH_DATA_SUCCESS':
            return { course: action.details }

        case "COURSE_DETAILS_FOR_EDIT_HAS_ERRORED":
            return {course : {} , hasErrored: true}

        case "COURSE_DETAILS_FOR_EDIT_DELETE_DATA":
            return defaultState

        default:
            return state
    }
};