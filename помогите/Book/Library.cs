using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace помогите_.Book
{
    public class Library
    {
        public readonly ObservableCollection<BookData> Books = new ObservableCollection<BookData>();

        public Library()
        {
            if (ActiveUser.Instance.User.Teacher)
                Books = new ObservableCollection<BookData>(BookRepository.Instance.GetAllBooks(ActiveUser.Instance.User.ID));
            else
                Books = new ObservableCollection<BookData>(BookRepository.Instance.GetAllBooks(0));
            Books.CollectionChanged += Books_CollectionChanged;
        }

        private void Books_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
           if( e.NewItems != null && e.NewItems.Count>0 )
            {
                BookRepository.Instance.AddBook(e.NewItems[0] as BookData);
            }
        }
    }
}
