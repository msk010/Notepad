import { Badge, Button } from "react-bootstrap";

export default function NoteTag({ tag }) {
  return (
    <>
      <Button variant="secondary" size="sm" disabled>
        {tag.name}
      </Button>{" "}
    </>
  );
}
