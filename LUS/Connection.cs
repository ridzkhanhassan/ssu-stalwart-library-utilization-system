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

            return new MySqlConnection(connString);
        }
    }
}
