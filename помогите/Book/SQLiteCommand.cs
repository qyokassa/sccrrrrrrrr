using System;
using Microsoft.Data.Sqlite;
using MySqlConnector;

namespace помогите_.Book
{
    internal class SQLiteCommand
    {
        internal object Parameters;
        private string v;
        private MySqlConnection connection;

        public SQLiteCommand(string v, MySqlConnection connection)
        {
            this.v = v;
            this.connection = connection;
        }



        internal void ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }

        internal SqliteDataReader ExecuteReader()
        {
            throw new NotImplementedException();
        }
    }

}