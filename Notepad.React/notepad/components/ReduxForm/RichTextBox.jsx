import React, { useRef } from "react";
import { Form } from "react-bootstrap";

const JoditEditor = React.lazy(() => {
  return import("jodit-react");
});

const Editor = ({ className, ...others }) => (
  <div id={others.name} tabIndex="1" className={`${className} form-field`}>
    <React.Suspense fallback={<div>Loading...</div>}>
      <JoditEditor {...others} for={others.name} />
    </React.Suspense>
  </div>
);

function RichTextBox({ name, input: { value, onChange }, ...others }) {
  const editor = useRef(null);

  const config = {
    readonly: false, // all options from https://xdsoft.net/jodit/doc/,
    fullsize: false,
    useImageEditor: false,
  };

  return (
    <Form.Control
      as={Editor}
      {...others}
      name={name}
      ref={editor}
      value={value}
      config={config}
      tabIndex={9} // tabIndex of textarea
      onBlur={onChange} // preferred to use only this option to update the content for performance reasons
      onChange={(newContent) => {}}
    />
  );
}

export default React.memo(RichTextBox);
