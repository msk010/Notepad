import styles from "../../../styles/Home.module.css";

import NoteTag from "./NoteTag";
import { Badge } from "react-bootstrap";

export default function NoteItem({ note, onClick }) {
  return (
    <a className={styles.card} onClick={onClick}>
      {note.tags.map((item) => (
        <NoteTag key={item.id} tag={item} />
      ))}
      <Badge variant="secondary">Secondary</Badge>{" "}
      <Badge variant="success">Success</Badge>{" "}
      <Badge variant="danger">Danger</Badge>{" "}
      <Badge variant="warning">Warning</Badge>{" "}
      <Badge variant="info">Info</Badge> <Badge variant="light">Light</Badge>{" "}
      <Badge variant="dark">Dark</Badge>
      <h2>{note.title} &rarr;</h2>
      <p>{note.content}</p>
    </a>
  );
}
