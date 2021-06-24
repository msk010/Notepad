import React from "react";
import PropTypes from "prop-types";
import { reduxForm } from "redux-form";
import {
  Button,
  ButtonToolbar,
  ButtonGroup,
  Form,
  InputGroup,
} from "react-bootstrap";

import { FormControlField } from "components/ReduxForm";

const emptyObject = {};

function NoteMenu(props) {
  const {
    onShowCreateNoteModal,
    onShowCreateTagModal,
    onSearch,

    //redux-form props
    handleSubmit,
    submitting,
    reset,
  } = props;

  return (
    <Form noValidate onSubmit={handleSubmit(onSearch)}>
      <ButtonToolbar className="mb-3" aria-label="Toolbar with Button groups">
        <ButtonGroup className="me-2" aria-label="Create group">
          <Button
            variant="outline-secondary"
            onClick={() => onShowCreateNoteModal(emptyObject)}
          >
            New Note
          </Button>
          <Button
            variant="outline-secondary"
            onClick={() => onShowCreateTagModal(emptyObject)}
          >
            New Tag
          </Button>
        </ButtonGroup>
        <ButtonGroup className="me-2" aria-label="Tags">
          <Button
            variant="outline-secondary"
            onClick={() => onShowCreateTagModal(emptyObject)}
          >
            Tags
          </Button>
        </ButtonGroup>
        <Form.Group role="form" controlId="searchString">
          <InputGroup>
            <FormControlField
              name="searchString"
              type="text"
              placeholder="Search..."
            />
            <Button
              variant="outline-danger"
              disabled={submitting}
              onClick={reset}
              id="btnGroupAddon"
            >
              X
            </Button>
            <Button
              variant="outline-secondary"
              type="submit"
              disabled={submitting}
            >
              Search
            </Button>
          </InputGroup>
        </Form.Group>
      </ButtonToolbar>
    </Form>
  );
}

NoteMenu.propTypes = {
  onShowCreateNoteModal: PropTypes.func,
  onShowCreateTagModal: PropTypes.func,
  onSearch: PropTypes.func,
};

export default reduxForm({
  form: "searchNotes", // a unique identifier for this form
  enableReinitialize: true,
})(NoteMenu);
