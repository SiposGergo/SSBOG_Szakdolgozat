import {
    setTextFilter,
    sortByName,
    sortByDate,
    setEndDate,
    setStartDate,
    setOldHikesVisibility,
    setSliderValues
    , itemsFetchDataSuccess,
    itemsHasErrored,
    itemsIsLoading,
} from "../../actions/HikeListActions";

test("setTextFilter action generator with no text", () => {
    const result = setTextFilter();
    expect(result).toEqual({ text: "", type: "SET_TEXT_FILTER" });
});

test("setTextFilter action generator with text", () => {
    const result = setTextFilter("asd");
    expect(result).toEqual({ text: "asd", type: "SET_TEXT_FILTER" });
});

test("sortByDate action generator", () => {
    const result = sortByDate();
    expect(result).toEqual({ type: "SORT_BY_DATE" });
});

test("sortByName action generator", () => {
    const result = sortByName();
    expect(result).toEqual({ type: "SORT_BY_NAME" });
});

test("setStartDate action generator without value", () => {
    const result = setStartDate();
    expect(result).toEqual({ value: undefined, type: "SET_START_DATE" });
});

test("setStartDate action generator with value", () => {
    const result = setStartDate("dumyDate");
    expect(result).toEqual({ value: "dumyDate", type: "SET_START_DATE" });
});

test("setEndDate action generator without value", () => {
    const result = setEndDate();
    expect(result).toEqual({ value: undefined, type: "SET_END_DATE" });
});

test("setEndDate action generator with value", () => {
    const result = setEndDate("dumyDate");
    expect(result).toEqual({ value: "dumyDate", type: "SET_END_DATE" });
});

test("setOldHikeVisibility action generator without value", () => {
    const result = setOldHikesVisibility();
    expect(result).toEqual({ bool: false, type: "SET_OLD_HIKES_VISIBILITY" });
});

test("setOldHikeVisibility action generator with value", () => {
    const result = setOldHikesVisibility(true);
    expect(result).toEqual({ bool: true, type: "SET_OLD_HIKES_VISIBILITY" });
});

test("setSliderValues action generator with value", () => {
    const result = setSliderValues("dummy");
    expect(result).toEqual({ value: "dummy", type: "SET_SLIDER_VALUES" });
});

test("itemsFetchDataSuccess action generator with value", () => {
    const result = itemsFetchDataSuccess(["dolor"]);
    expect(result).toEqual({ items: ["dolor"], type: "ITEMS_FETCH_DATA_SUCCESS" });
});

test("itemsFetchDataSuccess action generator with value", () => {
    const result = itemsFetchDataSuccess(["dolor"]);
    expect(result).toEqual({ items: ["dolor"], type: "ITEMS_FETCH_DATA_SUCCESS" });
});

test("itemsHasErrored action generator with value", () => {
    const result = itemsHasErrored(false);
    expect(result).toEqual({ hasErrored: false, type: "ITEMS_HAS_ERRORED" });
});

test("itemsIsLoading action generator with value", () => {
    const result = itemsIsLoading(false);
    expect(result).toEqual({ isLoading: false, type: "ITEMS_IS_LOADING" });
});