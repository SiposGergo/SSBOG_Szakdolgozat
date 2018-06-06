const defaultState = {
    user: {},
    isLoading: true,
    hasErrored: false,
    isModalOpen: false,
    modalHikeId : 0
}

export const userPageReducer = (state = defaultState, action) => {
    switch (action.type) {
        case 'USER_PAGE_ITEMS_HAS_ERRORED':
            return { ...state, hasErrored: true, error:action.error }
        case 'USER_PAGE_ITEMS_IS_LOADING':
            return { ...state, isLoading: action.isLoading };
        case 'USER_PAGE_ITEMS_FETCH_DATA_SUCCESS':
            return { user: action.user }
        case "USER_PAGE_OPEN_MODAL":
            return {...state, isModalOpen:true}
        case "USER_PAGE_CLOSE_MODAL":
            return {...state, isModalOpen:false}
        default:
            return state
    }
};