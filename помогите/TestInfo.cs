using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace помогите_
{
    public class TestInfo 

    {
        

        ObservableCollection<Test> testInfos;

        


        public int ID { get; set; }
        public string? Name { get; set; }

        public int NumberOfQuestions { get; set; }
        public int IDAuthor { get; set; }
        public string Author { get; set; } 
    }
}
