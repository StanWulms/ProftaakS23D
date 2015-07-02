using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

namespace Reservering_Reparatie
{
    public class StoredProcedure
    {
        //WEG?
        public StoredProcedure()
        {

        }
        
        /// <summary>
        /// Hiermee wordt de gemaakte PL/SQL Stored Procedure aangeroepen die de reservatie
        /// pleegt. Er wordt een heefboeker en een aantal accounts toegevoegd.
        /// </summary>
        /// <param name="hoofdboeker">De hoofdboeker van de reservatie</param>
        /// <param name="accounts">Alle personen die bij de reservatie horen</param>
        static string ReserveerAccounts(Hoofdboeker hoofdboeker, List<Account> accounts)
        {
            using (OracleConnection objConn = new OracleConnection("Data Source=ORCL; User ID=scott; Password=tiger"))
            {
                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "get_count_emp_by_dept";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("pin_deptno", OracleType.Number).Value = 20;
                objCmd.Parameters.Add("return_value", OracleType.Number).Direction = ParameterDirection.ReturnValue;

                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                    return "ok";
                    System.Console.WriteLine("Number of employees in department 20 is {0}", objCmd.Parameters["return_value"].Value);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Exception: {0}", ex.ToString());
                    return "ok";
                }

                objConn.Close();
            }
        }

    }
}