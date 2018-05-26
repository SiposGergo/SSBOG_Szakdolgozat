import {hikeDetailsReducer} from "../../reducers/HikeDetailsReducer";


const defaultState = {
    hike: { comments: [], organizer: {}, courses: [] },
        isLoading: true,
        hasErrored: false
}

test("should set default state", () => {
    const state = hikeDetailsReducer(undefined, { type: "@@INIT" });
    expect(state).toEqual(defaultState);
})

test("should set proper state when HIKE_DETAILS_ITEMS_HAS_ERRORED", () => {
    const state = hikeDetailsReducer(undefined, { type: "HIKE_DETAILS_ITEMS_HAS_ERRORED", hasErrored:true });
    expect(state).toEqual({...defaultState, hasErrored:true});
})

test("should set proper state when HIKE_DETAILS_ITEMS_IS_LOADING", () => {
    const state = hikeDetailsReducer(undefined, { type: "HIKE_DETAILS_ITEMS_IS_LOADING", isLoading:true });
    expect(state).toEqual({...defaultState, isLoading:true});
})

test("should set proper state when HIKE_DETAILS_ITEMS_IS_LOADING", () => {
    const state = hikeDetailsReducer(undefined, { type: "HIKE_DETAILS_ITEMS_FETCH_DATA_SUCCESS", details:{comments:[]} });
    expect(state).toEqual({hike:{comments:[]}});
})

test("should set proper state when HIKE_DETAILS_LOAD_NEW_COMMENTS", () => {
    const state = hikeDetailsReducer(undefined, { type: "HIKE_DETAILS_LOAD_NEW_COMMENTS", comment:{}});
    expect(state).toEqual({hike:{comments:[{}], courses:[], organizer:{}}});
})