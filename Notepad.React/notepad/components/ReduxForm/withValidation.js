import { useRef, forwardRef } from "react";
import { Form, Tooltip, OverlayTrigger } from "react-bootstrap";

export default function withValidation() {
  return (WrappedComponent) => {
    return function ValidationWrapper(props) {
      const {
        meta: { touched, valid, error },
        ...others
      } = props;

      const isValid = touched && valid;
      const isInvalid = touched && !valid;

      if (!isInvalid) {
        return (
          <WrappedComponent
            {...others}
            isValid={isValid}
            isInvalid={isInvalid}
          />
        );
      }

      return (
        <>
          <OverlayTrigger
            placement="top"
            show={true}
            delay={{ show: 250, hide: 400 }}
            overlay={
              <Tooltip id={`tooltip-validation-error`}>
                {error && error.map((e) => <p key={e}>{e}</p>)}
              </Tooltip>
            }
            error={error}
          >
            <div>
              <WrappedComponent
                isValid={isValid}
                isInvalid={isInvalid}
                {...others}
              />
            </div>
          </OverlayTrigger>
          {/* <WrappedComponent
            {...others}
            isValid={isValid}
            isInvalid={isInvalid}
          />
          <Form.Control.Feedback type="invalid">
            {error && error.map((e) => <span key={e}>{e}</span>)}
          </Form.Control.Feedback> */}
        </>
      );
    };
  };
}
