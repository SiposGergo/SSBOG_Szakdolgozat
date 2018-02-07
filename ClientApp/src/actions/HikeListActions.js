// szűréshez

export const setTextFilter = (text = "") => ({
    type: "SET_TEXT_FILTER",
    text
})

export const sortByDate = () => ({
    type: "SORT_BY_DATE"
})

export const sortByName = () => ({
    type: "SORT_BY_NAME"
})

export const setStartDate = (value = undefined) => ({
    type: "SET_START_DATE",
    value
})

export const setEndDate = (value = undefined) => ({
    type: "SET_END_DATE",
    value
})

export const setOldHikesVisibility = (bool = false) => ({
    type: "SET_OLD_HIKES_VISIBILITY",
    bool
})

export const setSliderValues = (value ) => ({
    type: "SET_SLIDER_VALUES",
    value
})

// fetch-hez

export function itemsHasErrored(bool) {
    return {
        type: 'ITEMS_HAS_ERRORED',
        hasErrored: bool
    };
}
export function itemsIsLoading(bool) {
    return {
        type: 'ITEMS_IS_LOADING',
        isLoading: bool
    };
}
export function itemsFetchDataSuccess(items) {
    return {
        type: 'ITEMS_FETCH_DATA_SUCCESS',
        items
    };
}

export function itemsFetchData(url) {
    return (dispatch) => {
        dispatch(itemsIsLoading(true));
        fetch(url)
            .then((response) => {
                if (!response.ok) {
                    throw Error(response.statusText);
                }
                dispatch(itemsIsLoading(false));
                return response;
            })
            .then((response) => response.json())
            .then((items) => dispatch(itemsFetchDataSuccess(items)))
            .catch(() => dispatch(itemsHasErrored(true)));
    };
}