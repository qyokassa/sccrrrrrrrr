using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace помогите_
{

    public class Textbook
    {
        


        public required string Title { get; set; }
        public int Year { get; set; }
        public string? Author { get; set; }
        public string? Content { get; set; }

        private ObservableCollection<Chapter> chapters = new ObservableCollection<Chapter>();

        
        }
    }


