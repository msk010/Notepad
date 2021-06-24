import PropTypes from "prop-types";
import {
  FormControl,
  Button,
  ButtonToolbar,
  ButtonGroup,
  InputGroup,
} from "react-bootstrap";

const emptyObject = {};

function NoteMenu(props) {
  const { onShowCreateNoteModal, onShowCreateTagModal } = props;
  return (
    <ButtonToolbar className="mb-3" aria-label="Toolbar with Button groups">
      <ButtonGroup className="me-2" aria-label="First group">
        <Button
          variant="outline-secondary"
          onClick={() => onShowCreateNoteModal(emptyObject)}
        >
          New Note
        </Button>{" "}
        <Button
          variant="outline-secondary"
          onClick={() => onShowCreateTagModal(emptyObject)}
        >
          New Tag
        </Button>
      </ButtonGroup>
      <InputGroup>
        <FormControl
          type="text"
          placeholder="Search..."
          aria-label="Search"
          aria-describedby="btnGroupAddon"
        />
      </InputGroup>
    </ButtonToolbar>
  );
}

NoteMenu.propTypes = {
  onShowCreateNoteModal: PropTypes.func,
  onShowCreateTagModal: PropTypes.func,
};

export default NoteMenu;
