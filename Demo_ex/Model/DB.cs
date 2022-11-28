using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Demo_ex.Model
{
    public class DB
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString);

        public void openConnection()
        {
            if(connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void closeConnection()
        {
            if( connection.State == ConnectionState.Open)
            {
                connection.Close(); 
            }
        }

        public SqlConnection getConnection()
        {
            return connection;
        }

    }
}
