import React from "react";
import { reduxForm } from "redux-form";

import { Button, Form, Modal } from "react-bootstrap";

import { FormControlField } from "../../ReduxForm";

function EditTagModal(props) {
  const { handleSubmit, reset, submitting, onHide, onSave, show, isNew, onDelete, canDelete } =
    props;

  return (
    <Modal show={show} onHide={onHide}>
      <Modal.Header closeButton>
        <Modal.Title>{isNew ? "Add Tag" : "Edit Tag"}</Modal.Title>
      </Modal.Header>
      <Form noValidate onSubmit={handleSubmit(onSave)}>
        <Modal.Body>
          <Form.Group role="form" controlId="name">
            <Form.Label>Name</Form.Label>
            <FormControlField name="name" type="text" placeholder="Name" />
          </Form.Group>
          <Form.Group role="form" controlId="createdOn">
            <Form.Label>Created on</Form.Label>
            <FormControlField name="createdOn" type="date" placeholder="Created on" readOnly/>
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
            <Button
              variant="primary"
              type="submit"
              //onClick={onHide}
              disabled={submitting}
            >
              Save Tag
            </Button>
            {!isNew && canDelete && (
              <Button variant="danger" onClick={onDelete}>
                Delete Tag
              </Button>
            )}
          </>
        </Modal.Footer>
      </Form>
    </Modal>
  );
}

export default reduxForm({
  form: "tag", // a unique identifier for this form
  enableReinitialize: true,
})(EditTagModal);
