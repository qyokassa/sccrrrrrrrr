using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace помогите_
{
    internal class ActiveUser
    {
        public Users? User { get; set; }

        static ActiveUser? instance;
        public static ActiveUser Instance
        {
            get
            {
                if (instance == null)
                    instance = new ActiveUser();
                return instance;
            }
        }
    }
}
