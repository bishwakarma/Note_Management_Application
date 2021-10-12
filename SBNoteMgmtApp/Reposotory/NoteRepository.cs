using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBNoteMgmtApp.Models;

namespace SBNoteMgmtApp.Reposotory
{
    /// <summary>
    /// This class is a container to store the notes.
    ///Created a Interface to put the signature of the class. 
    /// </summary>
    public class NoteRepository : INoteRepository
    {
        /// <summary>
        /// Container for storing the notes.
        /// </summary>
        private readonly List<NoteModel> _notes;

        // Following methods will help to interact with the container. 

        public NoteRepository()
        {
            _notes = new List<NoteModel>();
        }
        public NoteModel FindNoteById(Guid id)
        {
            var note = _notes.Find(n => n.Id == id);
            return note;
        }
        public IEnumerable<NoteModel> GetAllNotes()
        {
            return _notes;
        }
        public void SaveNotes(NoteModel noteModel)
        {
            _notes.Add(noteModel);
        }
        public void DeleteNote(NoteModel noteModel)
        {
            _notes.Remove(noteModel);
        }
    }
}
