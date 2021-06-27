import styles from "../../../styles/Home.module.css";

import NoteTag from "./NoteTag";
import { convertDate } from "components/helpers";

export default function NoteItem({ note, onClick }) {
  return (
    <a className={styles.card} onClick={onClick}>
      <div className="mb-2">
        {note.tags.map((item) => (
          <NoteTag key={item.id} tag={item} />
        ))}
      </div>
      <h2>{note.title} &rarr;</h2>
      <p className="text-secondary">
        Created on: {convertDate(note.createdOn)}
      </p>
      {note.updatedOn && (
        <p className="text-secondary">
          Updated on: {convertDate(note.updatedOn)}
        </p>
      )}
      <p dangerouslySetInnerHTML={{ __html: note.content }} />
    </a>
  );
}
