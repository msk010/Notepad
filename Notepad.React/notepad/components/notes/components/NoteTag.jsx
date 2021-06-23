import { Badge } from "react-bootstrap";

export default function NoteTag({ tag }) {
  return <Badge variant="primary">{tag.name}</Badge>;
}
