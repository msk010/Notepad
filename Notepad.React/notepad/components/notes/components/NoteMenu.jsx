import {
  FormControl,
  Button,
  ButtonToolbar,
  ButtonGroup,
  InputGroup,
} from "react-bootstrap";

export default function NoteMenu() {
  return (
    <ButtonToolbar className="mb-3" aria-label="Toolbar with Button groups">
      <ButtonGroup className="me-2" aria-label="First group">
        <Button variant="outline-secondary">New note</Button>{' '}
        <Button variant="outline-secondary">New tag</Button>
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
