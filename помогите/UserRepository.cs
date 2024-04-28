using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace помогите_
{
    internal class UserRepository
    {
        public int Register(Users users)
        {
            var connect = MySqlDB.Instance.GetConnection();
            int id = MySqlDB.Instance.GetAutoID("Users");
            string sql = "INSERT INTO Users VALUES (0, @firstname, @lastname, @password, @role, @login);";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("firstname", users.Name));
                mc.Parameters.Add(new MySqlParameter("lastname", users.Surname));
                mc.Parameters.Add(new MySqlParameter("password", users.Password));
                mc.Parameters.Add(new MySqlParameter("role", users.Teacher));
                mc.Parameters.Add(new MySqlParameter("login", users.Login));
                mc.ExecuteNonQuery();
            }
            return id;
        }

        public Users Login(Users users)
        {
            var connect = MySqlDB.Instance.GetConnection();
            string sql = "Select * from users where login=@login and password=@password";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("password", users.Password));
                mc.Parameters.Add(new MySqlParameter("login", users.Login));
                using (var dr = mc.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        users.ID = dr.GetInt32("id");
                        users.Surname = dr.GetString("lastname");
                        users.Name = dr.GetString("firstname");
                        users.Teacher = dr.GetInt32("role") == 1;
                    }
                    else
                        return users;
                }
            }
            return users;
        }

        static UserRepository instance;
        public static UserRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new UserRepository();
                return instance;
            }
        }
    }
}
