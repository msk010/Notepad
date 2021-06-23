import { SubmissionError } from "redux-form";
import { mapValidationErrors } from "./helpers";

function doNothing(config) {
  return config;
}

export function configureValidationErrors(axios) {
  axios.interceptors.response.use(
    //on success
    doNothing,

    //on error
    function (error) {
      const response = error.response || error;

      if (response.status === 400 && response.data && response.data.errors)
        throw new SubmissionError(mapValidationErrors(response.data.errors));

      return Promise.reject(error);
    }
  );
}
