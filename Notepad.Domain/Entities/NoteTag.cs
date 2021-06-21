namespace Notepad.Domain.Entities
{
    public class NoteTag
    {
        private NoteTag() { }
        public NoteTag(int tagId, int noteId)
        {
            TagId = tagId;
            NoteId = noteId;
        }

        public int TagId { get; private set; }
        public Tag Tag { get; private set; }
        public int NoteId { get; private set; }
        public Note Note { get; private set; }
    }
}
