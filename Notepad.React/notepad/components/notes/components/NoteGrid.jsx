import React, { useState } from "react";
import styles from "../../../styles/Home.module.css";
import EditNoteModal from "./EditNoteModal";
import NoteItem from "./NoteItem";

import NoteService from "services/NoteService";
import useService from "services/useService";

import { Spinner } from "react-bootstrap";

const emptyArray = [];

export default function NoteGrid() {
  const [show, setShow] = useState(false);
  const [currentNote, setInitialNote] = useState(null);
  const { inProgress, results } = useService(
    NoteService.getAll,
    emptyArray,
    show
  );

  const handleClose = () => {
    setShow(false);
    setInitialNote(null);
  };
  const handleShow = (data) => {
    setShow(true);
    setInitialNote(data);
  };
  const isNew = !currentNote || !currentNote.id;

  const handleSave = async (data) => {
    const save = isNew ? NoteService.create : NoteService.update;

    const mappedData = {
      ...data,
      tagIds: (data.tags || emptyArray).map((t) => t.id),
    };

    await save(mappedData);
    handleClose();
  };

  return (
    <>
      <EditNoteModal
        show={show}
        onHide={handleClose}
        onSave={handleSave}
        initialValues={currentNote}
      />
      <div className={styles.grid}>
        {inProgress ? (
          <Spinner animation="border" />
        ) : (
          results.map((item) => (
            <NoteItem
              key={item.id}
              note={item}
              onClick={() => handleShow(item)}
            />
          ))
        )}
      </div>
    </>
  );
}
