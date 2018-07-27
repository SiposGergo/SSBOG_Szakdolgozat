import {
  getCourseResultService,
  getCourseLiveResultService
} from "../services/ResultService";

export const sortDefault = () => ({
  type: "RESULT_SET_SORT_BY",
  value: "default"
});

export const sortByName = () => ({
  type: "RESULT_SET_SORT_BY",
  value: "name"
});

export const sortByNetto = () => ({
  type: "RESULT_SET_SORT_BY",
  value: "nettoTime"
});

export const setTimeNetto = () => ({
  type: "RESULT_SET_TIME",
  value: "netto"
});

export const setTimeBrutto = () => ({
  type: "RESULT_SET_TIME",
  value: "brutto"
});

export const setJustFinishers = value => ({
  type: "RESULT_SET_JUST_FINISHERS",
  value
});

export const setGenderMale = () => ({
  type: "RESULT_SET_GENDER",
  value: "male"
});

export const setGenderFemale = () => ({
  type: "RESULT_SET_GENDER",
  value: "female"
});

export const setGenderBoth = () => ({
  type: "RESULT_SET_GENDER",
  value: "both"
});

export const setStartNumberText = value => ({
  type: "RESULT_SET_START_NUMBER_TEXT",
  value
});

export const setNameText = value => ({
  type: "RESULT_SET_NAME_TEXT",
  value
});

export const reset = () => ({
  type: "RESULT_RESET"
});

export const deleteData = () => ({
	type: "RESULT_DELETE"
  });

export function itemsHasErrored(error) {
  return {
    type: "RESULT_ITEMS_HAS_ERRORED",
    error
  };
}

export function itemsIsLoading() {
  return {
    type: "RESULT_ITEMS_IS_LOADING"
  };
}

export function itemsFetchDataSuccess(data) {
  return {
    type: "RESULT_ITEMS_FETCH_DATA_SUCCESS",
    data
  };
}

export function itemsUpdated(data) {
  return {
    type: "RESULT_ITEMS_UPDATED",
    data
  };
}

export function getCourseResult(courseId) {
  return dispatch => {
    dispatch(itemsIsLoading());

    getCourseResultService(courseId).then(
      data => dispatch(itemsFetchDataSuccess(data)),
      error => dispatch(itemsHasErrored(error))
    );
  };
}

export function getCourseLiveResult(courseId) {
  return dispatch => {
    getCourseLiveResultService(courseId).then(
      data => dispatch(itemsUpdated(data)),
      error => dispatch(itemsHasErrored(error))
    );
  };
}
