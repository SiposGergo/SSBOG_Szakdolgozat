const defaultState = {
    hike: { },
    hasErrored: false
}

export const hikeEditReducer = (state = defaultState, action) => {
    switch (action.type) {
        
        case 'HIKE_DETAILS_FOR_EDIT_FETCH_DATA_SUCCESS':
            return { hike: action.details }

        case "HIKE_DETAILS_FOR_EDIT_HAS_ERRORED":
            return {hike : {} , hasErrored: true}

        case "HIKE_DETAILS_FOR_EDIT_DELETE_DATA":
            return defaultState

        default:
            return state
    }
};