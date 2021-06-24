import styles from "../../../styles/Home.module.css";

import NoteTag from "./NoteTag";

export default function NoteItem({ note, onClick }) {
  return (
    <a className={styles.card} onClick={onClick}>
      <div className="mb-2">
        {note.tags.map((item) => (
          <NoteTag key={item.id} tag={item} />
        ))}
      </div>
      <h2>{note.title} &rarr;</h2>
      <p>{note.content}</p>
    </a>
  );
}
