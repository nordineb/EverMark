using System.Collections.Generic;

namespace EvernoteMvcExample.ViewModels.Notes
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            Notebooks = new List<Notebook>();
        }

        public IList<Notebook> Notebooks { get; set; }

        public class Notebook
        {
            public Notebook()
            {
                Notes = new List<Note>();
            }

            public string Name { get; set; }
            public string Id { get; set; }
            public IList<Note> Notes { get; set; }
        }

        public class Note
        {
            public string Name { get; set; }
            public string Id { get; set; }
        }
    }
}