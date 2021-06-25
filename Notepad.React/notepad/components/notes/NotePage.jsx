import { useState } from "react";
import NoteMenu, { ListTypes } from "components/notes/components/NoteMenu";
import NoteGrid from "components/notes/components/NoteGrid";
import TagGrid from "components/tags/components/TagGrid";
import EditNoteModal from "components/notes/components/EditNoteModal";
import EditTagModal from "components/tags/components/EditTagModal";
import useSaveModal from "components/useSaveModal";
import useSearch from "components/useSearch";
import NoteService from "services/NoteService";
import TagService from "services/TagService";

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

  const [currentListType, setListType] = useState(ListTypes.Notes);

  const {
    inProgress: notesAreLoading,
    results: noteResults,
    onSearch: onSearchNote,
  } = useSearch(
    "search",
    NoteService.search,
    showNoteModal && currentListType == ListTypes.Notes
  );

  const {
    inProgress: tagsAreLoading,
    results: tagResults,
    onSearch: onSearchTag,
  } = useSearch(
    "search",
    TagService.search,
    showTagModal && currentListType == ListTypes.Tags
  );

  const onDeleteNote = async () => {
    await NoteService.delete(currentNote.id);
    onCloseNoteModal();
  };
  const onDeleteTag = async () => {
    await TagService.delete(currentTag.id);
    onCloseTagModal();
  };

  const onSearch =
    currentListType == ListTypes.Notes ? onSearchNote : onSearchTag;
console.log(currentTag)
  return (
    <>
      <NoteMenu
        onShowCreateNoteModal={onShowNoteModal}
        onShowCreateTagModal={onShowTagModal}
        onSearch={onSearch}
        onSwitchList={setListType}
        listType={currentListType}
      />
      {currentListType == ListTypes.Notes && (
        <NoteGrid
          onShowEditModal={onShowNoteModal}
          results={noteResults}
          isLoading={notesAreLoading}
        />
      )}
      {currentListType == ListTypes.Tags && (
        <TagGrid
          onShowEditModal={onShowTagModal}
          results={tagResults}
          isLoading={tagsAreLoading}
        />
      )}
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
        canDelete={currentTag.canDelete}
        isNew={isNewTag}
        onDelete={onDeleteTag}
      />
    </>
  );
}
