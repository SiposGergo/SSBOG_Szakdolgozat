import filtersReducer from "../../reducers/HikeListReducer";
import moment from "moment";

const filtersReducerDefaultState = {
    hikes: [],
    text: "",
    sortBy: "name",
    startDate: moment().startOf('year'),
    endDate: moment().endOf('year'),
    isOldHikesVisible: false,
    slider: [0,100],
    hasErrored: false,
    isLoading: false,
}

test("should set default state", () => {
    const state = filtersReducer(undefined, { type: "@@INIT" });
    expect(state).toEqual(filtersReducerDefaultState);
})

test("should set text filter", () => {
    const state = filtersReducer(undefined, { type: "SET_TEXT_FILTER", text: "dummyText" });
    expect(state).toEqual({...filtersReducerDefaultState, text:"dummyText"});
})

test("should set sort by date", () => {
    const state = filtersReducer(undefined, { type: "SORT_BY_DATE" });
    expect(state).toEqual({...filtersReducerDefaultState, sortBy:"date"});
})

test("should set sort by name", () => {
    const state = filtersReducer(undefined, { type: "SORT_BY_NAME" });
    expect(state).toEqual({...filtersReducerDefaultState, sortBy:"name"});
})

test("should set start date", () => {
    const state = filtersReducer(undefined, { type: "SET_START_DATE", value:"dummyDate" });
    expect(state).toEqual({...filtersReducerDefaultState, startDate:"dummyDate"});
})

test("should set end date", () => {
    const state = filtersReducer(undefined, { type: "SET_END_DATE", value:"dummyDate" });
    expect(state).toEqual({...filtersReducerDefaultState, endDate:"dummyDate"});
})

test("should set old hikes visibility", () => {
    const state = filtersReducer(undefined, { type: "SET_OLD_HIKES_VISIBILITY", bool:true });
    expect(state).toEqual({...filtersReducerDefaultState, isOldHikesVisible:true});
})

test("should set slider values", () => {
    const state = filtersReducer(undefined, { type: "SET_SLIDER_VALUES", value:[] });
    expect(state).toEqual({...filtersReducerDefaultState, slider:[]});
})

test("should set error when load", () => {
    const state = filtersReducer(undefined, { type: "ITEMS_HAS_ERRORED", hasErrored:true });
    expect(state).toEqual({...filtersReducerDefaultState, hasErrored:true});
})

test("should set isloading when start to load", () => {
    const state = filtersReducer(undefined, { type: "ITEMS_IS_LOADING", isLoading:true });
    expect(state).toEqual({...filtersReducerDefaultState, isLoading:true});
})

test("should set items when they loaded", () => {
    const state = filtersReducer(undefined, { type: "ITEMS_FETCH_DATA_SUCCESS", items:[1,2] });
    expect(state).toEqual({...filtersReducerDefaultState, hikes:[1,2]});
})