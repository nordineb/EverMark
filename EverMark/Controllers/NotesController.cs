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
                var notebookViewModel = new IndexViewModel.Notebook
                {
                    Id = n.Guid,
                    Name = n.Name
                };

                if (n.Guid == notebook)
                    foreach (var note in noteStore.findNotes(User.Identity.Name, new NoteFilter {NotebookGuid = n.Guid}, 0, 1000).Notes)
                        notebookViewModel.Notes.Add(new IndexViewModel.Note
                        {
                            Id = note.Guid,
                            Name = note.Title
                        });

                viewModel.Notebooks.Add(notebookViewModel);
            }

            return View(viewModel);
        }
    }
}
