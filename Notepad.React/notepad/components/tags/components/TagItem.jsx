import styles from "../../../styles/Home.module.css";
import { convertDate } from "components/helpers";

export default function TagItem({ note, onClick }) {
  return (
    <a className={styles.card} onClick={onClick}>
      <h2>{note.name} &rarr;</h2>
      <p className="text-secondary">Created on: {convertDate(note.createdOn)}</p>
      {note.updatedOn && <p className="text-secondary">Updated on: {convertDate(note.updatedOn)}</p>}
    </a>
  );
}
