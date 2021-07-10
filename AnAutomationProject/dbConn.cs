using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TheTechnoMarketAutomation
{
    class dbConn
    {
        public SqlConnection conn()
        {
            SqlConnection con = new SqlConnection(@"Data Source=dbservername;Initial Catalog=dbTheTechnoMarket;Integrated Security=True");
            con.Open();
            return con;
        }
    }
}
