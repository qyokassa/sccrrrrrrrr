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
        public class TestInfo
        {
            public string? Name { get; set; }
            public int NumberOfQuestions { get; set; }

            public TestInfo(string name, string content)
            {
                Name = name;
               NumberOfQuestions = content.Length;
            }
        }


        public int ID { get; set; }
        public string QuestionText { get; set; }
        public ObservableCollection<Answer> Answers { get; set; } = new();
        public ObservableCollection<TestInfo> Tests { get; set; }
    }
}
