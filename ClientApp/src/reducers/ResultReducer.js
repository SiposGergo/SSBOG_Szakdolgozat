const defaultState = {
    checkpoints: [],
    registrations: [],
    hasErrored: false,
    isLoading: true,

    nameText: "",
    startNumberText: "",
    gender: "both",
    justFinishers: false,
    time: "brutto",
    sortBy: "default"
}

export const resultReducer = (state = defaultState, action) => {
    switch (action.type) {

        case 'RESULT_ITEMS_HAS_ERRORED':
            return { ...state, hasErrored: true }

        case 'RESULT_ITEMS_IS_LOADING':
            return { ...state, isLoading: true };

        case 'RESULT_ITEMS_FETCH_DATA_SUCCESS':
            return {
                ...state,
                checkpoints: action.data.checkpoints,
                registrations: action.data.registrations,
                isLoading: false
            }

        case "RESULT_ITEMS_UPDATED":
            return { ...state, registrations: action.data };

        case "RESULT_SET_NAME_TEXT":
            return { ...state, nameText: action.value }

        case "RESULT_SET_START_NUMBER_TEXT":
            return { ...state, startNumberText: action.value }

        case "RESULT_SET_GENDER":
            return { ...state, gender: action.value }

        case "RESULT_SET_JUST_FINISHERS":
            return { ...state, justFinishers: action.value }

        case "RESULT_SET_TIME":
            return { ...state, time: action.value }

        case "RESULT_SET_SORT_BY":
            return { ...state, sortBy: action.value }

        case "RESULT_RESET":
            return {
                ...state,
                nameText: "",
                startNumberText: "",
                gender: "both",
                justFinishers: false,
                time: "brutto",
                sortBy: "default"
            }

        default:
            return state
    }
};