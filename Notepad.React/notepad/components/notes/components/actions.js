import { initialize } from "redux-form";

export function initNote(data) {
  return function (dispatch) {
    dispatch(initialize("note", data));
  };
}
