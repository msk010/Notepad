import React, { useRef } from "react";
import TagService from "services/TagService";
import useService from "services/useService";
import Select from "react-select";
import { Form } from "react-bootstrap";
import { findDOMNode } from "react-dom";

const emptyArray = [];

const getOptionLabel = (option) => option.name;
const getOptionValue = (option) => option.id;

const InputSelect = ({ className, ...others }) => (
  <div tabIndex="1" className={className}>
    <Select {...others} />
  </div>
);

export default function TagSelect({ input: { value, onChange }, ...others }) {
  const { inProgress, results } = useService(
    TagService.getAll,
    emptyArray,
    emptyArray
  );
  // const formControlRef = useRef(null);

  // const onClick = () => {
  //   debugger;
  //   console.log(formControlRef);
  //   formControlRef.current.focus();
  // };

  // formControlRef && formControlRef.current && formControlRef.current.focus();
  // formControlRef &&
  //   formControlRef.current &&
  //   console.log(findDOMNode(formControlRef.current)) &&
  //   findDOMNode(formControlRef.current).focus();

  return (
    <Form.Control
      as={InputSelect}
      {...others}
      autoFocus
      value={value}
      onChange={onChange}
      options={results}
      getOptionLabel={getOptionLabel}
      getOptionValue={getOptionValue}
      // ref={formControlRef}
     // onClick={onClick}
      isMulti
      isLoading={inProgress}
    />
  );
}
