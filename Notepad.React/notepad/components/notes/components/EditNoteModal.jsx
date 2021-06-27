import React from "react";
import { reduxForm } from "redux-form";

import { Button, Form, Modal } from "react-bootstrap";

import {
  FormControlField,
  TagSelectField,
  RichTextBoxField,
} from "../../ReduxForm";

function EditNoteModal(props) {
  const {
    handleSubmit,
    reset,
    submitting,
    onHide,
    onSave,
    onDelete,
    show,
    isNew,
  } = props;

  return (
    <Modal show={show} onHide={onHide}>
      <Modal.Header closeButton>
        <Modal.Title>{isNew ? "Add Note" : "Edit Note"}</Modal.Title>
      </Modal.Header>
      <Form noValidate onSubmit={handleSubmit(onSave)}>
        <Modal.Body>
          <Form.Group role="form" controlId="title">
            <Form.Label>Title</Form.Label>
            <FormControlField name="title" type="text" placeholder="Title" />
          </Form.Group>

          <Form.Group role="form" controlId="content">
            <Form.Label>Content</Form.Label>
            <RichTextBoxField name="content" />
          </Form.Group>
          <Form.Group role="form" controlId="tags">
            <Form.Label>Tags</Form.Label>
            <TagSelectField name="tags" />
          </Form.Group>
        </Modal.Body>
        <Modal.Footer>
          <>
            <Button variant="secondary" onClick={onHide} disabled={submitting}>
              Close
            </Button>
            <Button variant="secondary" onClick={reset} disabled={submitting}>
              {isNew ? "Clear Values" : "Restore values"}
            </Button>
            <Button variant="primary" type="submit" disabled={submitting}>
              Save Note
            </Button>
            {!isNew && (
              <Button
                variant="danger"
                onClick={handleSubmit(onDelete)}
                disabled={submitting}
              >
                Delete Note
              </Button>
            )}
          </>
        </Modal.Footer>
      </Form>
    </Modal>
  );
}

export default reduxForm({
  form: "note", // a unique identifier for this form
  enableReinitialize: true,
})(EditNoteModal);
