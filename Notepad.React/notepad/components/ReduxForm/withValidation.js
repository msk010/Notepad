import React from "react";
import { Tooltip, OverlayTrigger } from "react-bootstrap";

export default function withValidation() {
  return (WrappedComponent) => {
    return React.memo(function ValidationWrapper(props) {
      const {
        meta: { touched, valid, error },
        ...others
      } = props;

      const isValid = touched && valid;
      const isInvalid = touched && !valid;

      return (
        <OverlayTrigger
          placement="top"
          show={isInvalid}
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
              {...others}
              isValid={isValid}
              isInvalid={isInvalid}
            />
          </div>
        </OverlayTrigger>
      );
    });
  };
}
