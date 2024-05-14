using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace помогите_
{
    public class Users
    {
        public string? Surname {  get; set; }
        public string? Name { get; set; }

        public string? Password { get; set; }

        public string? Login { get; set; }
        public  bool Teacher  { get; set; }
        public int ID { get; internal set; }
    }
}
