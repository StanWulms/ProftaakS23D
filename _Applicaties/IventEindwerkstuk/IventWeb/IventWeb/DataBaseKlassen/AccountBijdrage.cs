using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess;

namespace IventWeb
{
    public class AccountBijdrage
    {
        public int ID { get; set; }
        public int AccountID { get; set; }
        public int BijdrageID { get; set; }
        public int Like { get; set; }
        public int Report { get; set; }

        public AccountBijdrage()
        {

        }

        public AccountBijdrage(int id, int accountid, int bijdrageid, int like, int report)
        {
            this.ID = id;
            this.AccountID = accountid;
            this.BijdrageID = bijdrageid;
            this.Like = like;
            this.Report = report;
        }

        public String getLikeReports()
        {
            string bijdrageID = (String)System.Web.HttpContext.Current.Session["bijdrage"];
            int like = -1;
            int ongewenst = -1;

            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                if (con == null)
                {
                    //return "Error! No Connection";
                }
                con.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                if (com == null)
                {
                    //return "Error! No Command";
                }
                com.Connection = con;
                com.CommandText = @"SELECT ""like"", ""ongewenst"" FROM ACCOUNT_BIJDRAGE WHERE ""bijdrage_id"" =" + bijdrageID;
                DbDataReader reader = com.ExecuteReader();
                try
                {
                    reader.Read();
                    like = reader.GetInt32(0);
                    ongewenst = reader.GetInt32(1);
                }
                catch (NullReferenceException)
                {
                    like = 0;
                    ongewenst = 0;
                }
                catch { like = 0; ongewenst = 0; }
                return like + "." + ongewenst;
            }
        }

        public void GiveLike()
        {
            string bijdrageID = (String)System.Web.HttpContext.Current.Session["bijdrage"];
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                if (con == null)
                {
                    //return "Error! No Connection";
                }
                con.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                if (com == null)
                {
                    //return "Error! No Command";
                }
                com.Connection = con;
                OracleCommand cmd = (OracleCommand)con.CreateCommand();
                try
                {
                    OracleTransaction otn = (OracleTransaction)con.BeginTransaction(IsolationLevel.ReadCommitted);
                    cmd.CommandText = @"INSERT INTO ACCOUNT_BIJDRAGE(""account_id"", ""bijdrage_id"", ""like"", ""ongewenst"") VALUES(1," + bijdrageID + ",1,0)";
                    cmd.ExecuteNonQuery();
                    otn.Commit();
                }
                catch (NullReferenceException)
                {

                }
            }
        }

        public void GiveReport()
        {
            string bijdrageID = (String)System.Web.HttpContext.Current.Session["bijdrage"];
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                if (con == null)
                {
                    //return "Error! No Connection";
                }
                con.ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                if (com == null)
                {
                    //return "Error! No Command";
                }
                com.Connection = con;
                OracleCommand cmd = (OracleCommand)con.CreateCommand();
                try
                {
                    OracleTransaction otn = (OracleTransaction)con.BeginTransaction(IsolationLevel.ReadCommitted);
                    cmd.CommandText = @"INSERT INTO ACCOUNT_BIJDRAGE(""account_id"", ""bijdrage_id"", ""like"", ""ongewenst"") VALUES(1," + bijdrageID + ",0,1)";
                    cmd.ExecuteNonQuery();
                    otn.Commit();
                }
                catch (NullReferenceException)
                {

                }
                //
            }
        }
    }
}