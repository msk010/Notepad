import transform from "lodash/transform";

export function mapValidationErrors(errors) {
  return transform(errors, (result, val, key) => {
    result[key.toLowerCase()] = val;
  });
}
