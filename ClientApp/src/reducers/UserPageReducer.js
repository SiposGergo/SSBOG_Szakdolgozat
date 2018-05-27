const defaultState = {
    user: {},
    isLoading: true,
    hasErrored: false
}

export const userPageReducer = (state = defaultState, action) => {
    switch (action.type) {
        case 'USER_PAGE_ITEMS_HAS_ERRORED':
            return { ...state, hasErrored: true, error:action.error }
        case 'USER_PAGE_ITEMS_IS_LOADING':
            return { ...state, isLoading: action.isLoading };
        case 'USER_PAGE_ITEMS_FETCH_DATA_SUCCESS':
            return { user: action.user }
        default:
            return state
    }
};