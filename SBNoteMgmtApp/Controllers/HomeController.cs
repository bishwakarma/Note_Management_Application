using Microsoft.AspNetCore.Mvc;
using SBNoteMgmtApp.Models;
using SBNoteMgmtApp.Reposotory;
using System;
using System.Diagnostics;
using System.Linq;

namespace SBNoteMgmtApp.Controllers
{
    public class HomeController : Controller
    {
        //Using the created repository for the Application in this class
        //by passing the Interface to the constructor method of the class.

        /// <summary>
        /// Private variable FIELD, is the copy of MY REPOSITORY. 
        /// </summary>
        private readonly INoteRepository _noteRepository;

        /// <summary>
        /// HomeController accepts the noteRepository and assigns it to the field above.
        /// </summary>
        /// <param name="noteRepository"></param>
        public HomeController(INoteRepository noteRepository)
        {
            //Assigning the noteRepository to the field.
            // Anytime I need the Sevice the application will give the Copy of the Service.

            _noteRepository = noteRepository;
        }


        /// <summary>
        /// Index() method renders the Home Page view. 
        /// My Home Page lists all the notes from the repository.
        /// Condition of the list are the notes that are not deleted from the field repository.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            //Fetching list of the Notes and passing it to the View
            // Condition of the list that are the notes NOT deleted from the field repository.
            //Assigned the created notes to the Var notes.
            var notes = _noteRepository.GetAllNotes().Where( n => n.IsDeleted = false);
            //Passing the notes to the View Page a a model.
            return View(notes);
        }

        /// <summary>
        /// NoteDetail() action metod. It accepts the ID to find the notes as the identifier of the notes.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult NoteDetail(Guid Id)
        {
            //Get the notes by ID
            var notes = _noteRepository.FindNoteById(Id);
            //Pass the notes to the view as a model.
            return View(notes);
        }


        //NoteEditor() method when called will Display the View or the Form to the Client.
        //Note Form can be empty or can be there with the conent to edit for the client.
        //Calling the NoteEditor() method with the ID will provide existing content to
        //the Client to make an Edit.
        //If NoteEditor() method is not called with an ID, then the empty form will be
        //displayed to the Client to make a new Entry. 
        [HttpGet]
        public IActionResult NoteEditor( Guid Id = default)
        {
            //Render the content if the form is not empty..ie WHEN the ID is passed. 
            if (Id != Guid.Empty)
            {
                //Get the Content.
                var notes = _noteRepository.FindNoteById(Id);
                //Passing the content to the View as a Model.
                return View(notes);
            }
            return View();      //If the ID is not provided, provided an empty form to the Client for a new entry.
        }


        // Wheather it is a new form or an existing form after entering the
        // details if user clicks Submit to submit a form,
        // the NoteEditor() method below will accept the form.
        // Parameter of the NoteEditor method is the NoteModel.
        [HttpPost]
        public IActionResult NoteEditor(NoteModel noteModel)
        {
            if (ModelState.IsValid)
            {
                //Create a date in a Run Time and assign to the date variable.
                var date = DateTime.Now;

                //Validate FROM the EXISTING NOTE if the note is NOT null
                //and the Id is NOT empty. Basically validating if the note is NOT the existing note.
                if (noteModel != null && noteModel.Id == Guid.Empty)
                {
                    //If the condition is valid.
                    //Create a new ID.
                    noteModel.Id = Guid.NewGuid();
                    //Create a new date.
                    noteModel.CreatedDate = date;
                    //Create the date when modifed.
                    noteModel.LastModified = date;
                    //GRAB the repository for the note and save the model to the repository.
                    _noteRepository.SaveNotes(noteModel);
                }
                else        //Validating if the note is the exisitng note.
                {
                    //Find the note from the repository using teh ID from the Client.
                    var notes = _noteRepository.FindNoteById(noteModel.Id);
                    //Create a new date when modified the existing note.
                    notes.LastModified = date;
                    //Add the subject to the property of the note model.
                    notes.Subject = noteModel.Subject;
                    //Add the details for the notes in the model.
                    notes.Details = noteModel.Details;
                }
                return RedirectToAction("Index");       //Redirecting the Client to the Index Page when the above HTTP POST Request are completed by the Application
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// Method to delete the existing note from the list of the note by calling the Delete() method.
        /// The note will be deleted from the List of the notes. BUT, the note will remain in the repository. 
        /// </summary>
        /// <returns></returns>
        public IActionResult DeleteNote(Guid Id)
        {
            //Get the note client requested from the repository field.
            var note = _noteRepository.FindNoteById(Id);
            //Validate the action to delete is true from the list of notes in the Index Page.
            note.IsDeleted = true;
            //If delete action is true, rendering the Client to the Index Page of the Application.
            return RedirectToAction("Index");
        }

        public IActionResult DeleteNotes(Guid Id)
        {
            var note = _noteRepository.FindNoteById(Id);
            note.IsDeleted = true;
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
