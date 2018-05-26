import {itemsHasErrored,itemsIsLoading, itemsFetchDataSuccess,loadNewComment } from "../../actions/HikeDetailsActions"

test("itemsHasErrored action generator",()=>{
    const result =  itemsHasErrored();
    expect(result).toEqual({
        type: 'HIKE_DETAILS_ITEMS_HAS_ERRORED',
        hasErrored: true,
    });
});

test("itemsIsLoading action generator",()=>{
    const result =  itemsIsLoading();
    expect(result).toEqual({
        type: 'HIKE_DETAILS_ITEMS_IS_LOADING',
        isLoading: true
    });
});

test("itemsFetchDataSuccess action generator",()=>{
    const result =  itemsFetchDataSuccess("dummyDetails");
    expect(result).toEqual({
        type: 'HIKE_DETAILS_ITEMS_FETCH_DATA_SUCCESS',
        details: "dummyDetails"
    });
});

test("loadNewComment action generator",()=>{
    const result =  loadNewComment("dummyComment");
    expect(result).toEqual({
        type: 'HIKE_DETAILS_LOAD_NEW_COMMENTS',
        comment: "dummyComment"
    });
});