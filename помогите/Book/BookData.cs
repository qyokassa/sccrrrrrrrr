using System;
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

        public int Year { get; set; }

        public string Annotation { get; set; }

        public int ChapterNumber => Chapters.Count;

        public ObservableCollection<ChapterData> Chapters { get; private set; }

        public BookData()
        {
            Title = string.Empty;
            Author = string.Empty;
            Year = default;
            Annotation = string.Empty;
            Chapters = new ObservableCollection<ChapterData>();
        }

        public BookData(string name, string author, int creationYear, string annotation)
        {
            if (name == null)
                throw new NullReferenceException($"{nameof(name)} is null.");

            if (author == null)
                throw new NullReferenceException($"{nameof(author)} is null.");

            if (annotation == null)
                throw new NullReferenceException($"{nameof(annotation)} is null.");

            Title = name;
            Author = author;
            Annotation = annotation;
            Year = creationYear;
            Chapters = new ObservableCollection<ChapterData>();
        }
    }
}
