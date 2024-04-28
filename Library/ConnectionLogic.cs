using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class ConnectionLogic
    {
        //define connection string here and using everywhere
        public SqlConnection con = new SqlConnection("Data Source=OPTIMUM98\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=SSPI");

    }
}
