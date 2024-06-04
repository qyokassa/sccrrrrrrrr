using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace помогите_.Book
{
    public class BookData
    {
        public class ChapterData
        {
            public int Id { get; set; }
            public string Name { get; private set; }
            public string Content { get; private set; }

            public ChapterData(string name, string content)
            {
                Name = name;
                Content = content;
            }
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }
        public int AuthorID { get; set; }

        public int Year { get; set; }

        public string Annotation { get; set; }
        public ObservableCollection<ChapterData> Chapters { get; internal set; } = new();
    }
}
