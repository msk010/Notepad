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
  <div id={others.name} tabIndex="1" className={`${className} form-field`}>
    <Select {...others} for={others.name} />
  </div>
);

export default function TagSelect({ input: { value, onChange }, ...others }) {
  const { inProgress, results } = useService(
    TagService.getAll,
    emptyArray,
    emptyArray
  );

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
