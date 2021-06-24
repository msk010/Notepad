import NoteMenu from "components/notes/components/NoteMenu";
import NoteGrid from "components/notes/components/NoteGrid";
import EditNoteModal from "components/notes/components/EditNoteModal";
import EditTagModal from "components/tags/components/EditTagModal";
import useSaveModal from "components/useSaveModal";
import useSearch from "components/useSearch";
import NoteService from "services/NoteService";
import TagService from "services/TagService";
import useService from "services/useService";

const emptyArray = [];

const mapSaveData = (data) => ({
  ...data,
  tagIds: (data.tags || emptyArray).map((t) => t.id),
});

export default function NotePage() {
  const {
    show: showNoteModal,
    handleClose: onCloseNoteModal,
    handleShow: onShowNoteModal,
    handleSave: onSaveNote,
    currentData: currentNote,
    isNew: isNewNote,
  } = useSaveModal(NoteService.create, NoteService.update, mapSaveData);

  const {
    show: showTagModal,
    handleClose: onCloseTagModal,
    handleShow: onShowTagModal,
    handleSave: onSaveTag,
    currentData: currentTag,
    isNew: isNewTag,
  } = useSaveModal(TagService.create, TagService.update);

  const {
    inProgress,
    results,
    onSearch: onSearchNote,
  } = useSearch("searchNotes", NoteService.search, showNoteModal);

  const onDeleteNote = async () => {
    await NoteService.delete(currentNote.id);
    onCloseNoteModal();
  };
  const onDeleteTag = async () => {
    await TagService.delete(currentTag.id);
    onCloseTagModal();
  };

  return (
    <>
      <NoteMenu
        onShowCreateNoteModal={onShowNoteModal}
        onShowCreateTagModal={onShowTagModal}
        onSearch={onSearchNote}
      />
      <NoteGrid
        onShowEditModal={onShowNoteModal}
        results={results}
        isLoading={inProgress}
      />
      <EditNoteModal
        show={showNoteModal}
        onHide={onCloseNoteModal}
        onSave={onSaveNote}
        initialValues={currentNote}
        isNew={isNewNote}
        onDelete={onDeleteNote}
      />
      <EditTagModal
        show={showTagModal}
        onHide={onCloseTagModal}
        onSave={onSaveTag}
        initialValues={currentTag}
        isNew={isNewTag}
        onDelete={onDeleteTag}
      />
    </>
  );
}
