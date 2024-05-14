using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace помогите_
{
    public class Questions
    {
        public int ID { get; set; }
        public string QuestionText { get; set; }
        public ObservableCollection<Answer> Answers { get; set; } = new();
    }
}
