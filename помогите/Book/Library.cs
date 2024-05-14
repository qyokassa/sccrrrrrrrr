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
    }
}
