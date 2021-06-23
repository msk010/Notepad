import { Form } from "react-bootstrap";
import { Field } from "redux-form";
import withValidation from "./withValidation";
import TagSelect from "components/tags/components/TagSelect";

const emptyArray = [];

function FormControl({ input: { value, onChange }, ...others }) {
  return <Form.Control {...others} value={value} onChange={onChange} />;
}

const FormControlWithValidation = withValidation()(FormControl);

export function FormControlField({ name, ...others }) {
  return (
    <Field name={name} {...others} component={FormControlWithValidation} />
  );
}

const TagSelectWithValidation = withValidation()(TagSelect);

export function TagSelectField({ name, ...others }) {
  return <Field name={name} {...others} component={TagSelectWithValidation} />;
}
