const defaultState = {
    checkpoints: [],
    registrations: [],
    hasErrored: false,
    isLoading: true
}

export const resultReducer = (state = defaultState, action) => {
    switch (action.type) {

        case 'RESULT_ITEMS_HAS_ERRORED':
            return { ...state, hasErrored: true }

        case 'RESULT_ITEMS_IS_LOADING':
            return { ...state, isLoading: true };

        case 'RESULT_ITEMS_FETCH_DATA_SUCCESS':
            return { checkpoints: action.data.checkpoints, registrations: action.data.registrations }

        default:
            return state
    }
};