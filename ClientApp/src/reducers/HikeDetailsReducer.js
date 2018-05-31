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
                hike: {...state.hike,  comments:[...state.hike.comments, action.comment]} 
            }

        default:
            return state
    }
};