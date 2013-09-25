using EverMark.ViewModels.Editor;
using System.Web.Mvc;
using System.Xml.Linq;

namespace EverMark.Controllers
{
    [Authorize]
    public class EditorController : Controller
    {

        public ActionResult Index(string note)
        {
            return View(BuildViewModel(note));
        }

        public ActionResult Preview(string note)
        {
            return View(BuildViewModel(note));
        }

        [HttpPost]
        public ActionResult Index(string note, string content)
        {
            var noteStore = Evernote.GetNoteStore(User.Identity.Name);

            var n = noteStore.getNote(User.Identity.Name, note, true, false, false, false);

            content = content.Replace("\r", "").Replace("\n", "<br />");

            n.Content = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><!DOCTYPE en-note SYSTEM \"http://xml.evernote.com/pub/enml2.dtd\"><en-note><pre>" + content.Trim() + "</pre></en-note>";
            noteStore.updateNote(User.Identity.Name, n);

            return RedirectToAction("Index", new { note });
        }


        private IndexViewModel BuildViewModel(string noteId)
        {
            var viewModel = new IndexViewModel();
            var noteStore = Evernote.GetNoteStore(User.Identity.Name);

            var note = noteStore.getNote(User.Identity.Name, noteId, true, false, false, false);
            var notebook = noteStore.getNotebook(User.Identity.Name, note.NotebookGuid);
            viewModel.Title = note.Title;
            viewModel.NotebookAndTitle = notebook.Name + "\\" + note.Title;
            viewModel.NoteId = noteId;

            var xmlContent = XDocument.Parse(note.Content);
            if (xmlContent.Root != null)
            {
                var content = xmlContent.Root.ToString();

                //replace the <en-note> tag
                content = content.Substring(9);
                content = content.Substring(0, content.Length - 10);
                content = content.Replace("<br />", "\r\n").Trim();

                if (content.StartsWith("<pre>"))
                    content = content.Substring(5);
                if (content.EndsWith("</pre>"))
                    content = content.Substring(0, content.Length - 6);

                viewModel.Content = content.Trim();
            }

            return viewModel;
        }

    }
}
