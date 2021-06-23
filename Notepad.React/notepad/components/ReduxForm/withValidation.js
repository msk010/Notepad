import { Form } from "react-bootstrap";

export default function withValidation() {
  return (WrappedComponent) => {
    return function ValidationWrapper(props) {
      const {
        meta: { touched, valid, error },
        ...others
      } = props;

      const isValid = touched && valid;
      const isInvalid = touched && !valid;

      return (
        <>
          <WrappedComponent
            {...others}
            isValid={isValid}
            isInvalid={isInvalid}
          />
          <Form.Control.Feedback type="invalid">
            {error && error.map((e) => <span>{e}</span>)}
          </Form.Control.Feedback>
        </>
      );
    };
  };
}
