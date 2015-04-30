using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace InschrijvingSysteem
{
    class Database
    {
        private OracleConnection conn = new OracleConnection();
        String dbconnect_user = "dbi324575";
        String dbconnect_pw = "YT4Yr6gF81";
        

        public void ConnectToDatabase()
        {
            conn.ConnectionString = "User Id=" + dbconnect_user + ";Password=" + dbconnect_pw + ";Data Source=" + " //192.168.15.50:1521/fhictora" + ";";
            conn.Open();
        }


        public void ExecuteInsertQuery(String query)
        {
            ConnectToDatabase();

            OracleCommand cmd = new OracleCommand("", conn);

            OracleTransaction txn = conn.BeginTransaction(IsolationLevel.ReadCommitted);

            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            txn.Commit();

            conn.Close();
        }
        

    }
}
