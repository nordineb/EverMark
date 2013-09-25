using System;
using System.Linq;
using EverMark.ViewModels.Notes;
using Evernote.EDAM.NoteStore;
using System.Web.Mvc;

namespace EverMark.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        public ActionResult Index(string notebook)
        {
            var viewModel = new IndexViewModel();

            var noteStore = Evernote.GetNoteStore(User.Identity.Name);

            foreach (var n in noteStore.listNotebooks(User.Identity.Name))
            {
                var notesCount = noteStore.findNoteCounts(User.Identity.Name, new NoteFilter { NotebookGuid = n.Guid }, false).NotebookCounts.First().Value;
                var notebookViewModel = new IndexViewModel.Notebook
                {
                    Id = n.Guid,
                    Name = n.Name,
                    NotesCount = notesCount
                };

                if (n.Guid == notebook)
                    foreach (var note in noteStore.findNotes(User.Identity.Name, new NoteFilter { NotebookGuid = n.Guid }, 0, 1000).Notes)
                        notebookViewModel.Notes.Add(new IndexViewModel.Note
                        {
                            Id = note.Guid,
                            Name = note.Title,
                            UpdateDate = new DateTime(1970, 1, 1).AddMilliseconds(note.Updated)
                        });

                viewModel.Notebooks.Add(notebookViewModel);
            }

            return View(viewModel);
        }
    }
}
