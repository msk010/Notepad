import NoteMenu from "components/notes/components/NoteMenu";
import NoteGrid from "components/notes/components/NoteGrid";
import EditNoteModal from "components/notes/components/EditNoteModal";
import EditTagModal from "components/tags/components/EditTagModal";
import useSaveModal from "components/useSaveModal";
import NoteService from "services/NoteService";
import TagService from "services/TagService";

const mapSaveData = (data) => ({
  ...data,
  tagIds: (data.tags || emptyArray).map((t) => t.id),
});

export default function NotePage() {
  const saveNote = useSaveModal(
    NoteService.create,
    NoteService.update,
    mapSaveData
  );

  const saveTag = useSaveModal(TagService.create, TagService.update);

  const onDeleteNote = async () => {
    await NoteService.delete(saveNote.currentData.id);
    saveNote.handleClose();
  };
  const onDeleteTag = async () => {
    await NoteService.delete(saveTag.currentData.id);
    saveTag.handleClose();
  };

  return (
    <>
      <NoteMenu
        onShowCreateNoteModal={saveNote.handleShow}
        onShowCreateTagModal={saveTag.handleShow}
      />
      <NoteGrid
        shouldRefresh={saveNote.show}
        onShowEditModal={saveNote.handleShow}
      />
      <EditNoteModal
        show={saveNote.show}
        onHide={saveNote.handleClose}
        onSave={saveNote.handleSave}
        initialValues={saveNote.currentData}
        isNew={saveNote.isNew}
        onDelete={onDeleteNote}
      />
      <EditTagModal
        show={saveTag.show}
        onHide={saveTag.handleClose}
        onSave={saveTag.handleSave}
        initialValues={saveTag.currentData}
        isNew={saveTag.isNew}
        onDelete={onDeleteTag}
      />
    </>
  );
}
