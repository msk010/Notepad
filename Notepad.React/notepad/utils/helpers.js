import transform from "lodash/transform";
import camelCase from "lodash/camelCase"

export function mapValidationErrors(errors) {
  return transform(errors, (result, val, key) => {
    result[camelCase(key)] = val;
  });
}
