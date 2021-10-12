using SBNoteMgmtApp.Models;
using System;
using System.Collections.Generic;

namespace SBNoteMgmtApp.Reposotory
{
    public interface INoteRepository
    {
        void DeleteNote(NoteModel noteModel);
        NoteModel FindNoteById(Guid id);
        IEnumerable<NoteModel> GetAllNotes();
        void SaveNotes(NoteModel noteModel);
    }
}