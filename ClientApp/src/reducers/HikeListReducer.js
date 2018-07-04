import moment from "moment";

// Filters reducer
const filtersReducerDefaultState = {
    hikes: [],
    text: "",
    sortBy: "name",
    startDate: moment().startOf('year'),
    endDate: moment().endOf('year'),
    isOldHikesVisible: false,
    slider: [0, 100],
    hasErrored: false,
    isLoading: false,
}

const filtersReducer = (state = filtersReducerDefaultState, action) => {
    switch (action.type) {
        case "SET_TEXT_FILTER":
            return { ...state, text: action.text }
        case "SORT_BY_DATE":
            return { ...state, sortBy: "date" }
        case "SORT_BY_NAME":
            return { ...state, sortBy: "name" }
        case "SET_START_DATE":
            return { ...state, startDate: action.value }
        case "SET_END_DATE":
            return { ...state, endDate: action.value }
        case "SET_OLD_HIKES_VISIBILITY":
            return { ...state, isOldHikesVisible: action.bool }
        case "SET_SLIDER_VALUES":
            return { ...state, slider: action.value }

        case 'ITEMS_HAS_ERRORED':
            return { ...state, hasErrored: action.hasErrored }
        case 'ITEMS_IS_LOADING':
            return { ...state, isLoading: action.isLoading };
        case 'ITEMS_FETCH_DATA_SUCCESS':
            return { ...state, hikes: action.items }

        case "HIKE_LIST_RESET":
            return {
                ...state,
                text: "",
                sortBy: "name",
                startDate: moment().startOf('year'),
                endDate: moment().endOf('year'),
                isOldHikesVisible: false,
                slider: [0, 100],
            }

        default:
            return state
    }
};

export default filtersReducer;