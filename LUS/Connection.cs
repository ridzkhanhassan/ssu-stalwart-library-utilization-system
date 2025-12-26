using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LUS
{
    internal class Connection
    {
        public static MySqlConnection GetConnection()
        {
            string connString = "Server=192.168.10.26;Database=library_utilization_database;Uid=root;Pwd=sulu2022//;";
            //string connString = "Server=localhost;Database=library_utilization_database;Uid=root;Pwd=sulu2022//;";

            return new MySqlConnection(connString);
        }
    }
}
