using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace ConfirmTopup.Models
{
    public class ConnectionString
    {
        public static string OpenDB(OracleConnection conn)
        {
            string strCon = "Data Source=(DESCRIPTION="
             + "(ADDRESS=(PROTOCOL=TCP)(HOST=172.28.12.95)(PORT=1521))"
             + "(CONNECT_DATA=(SERVICE_NAME=CCDB001)));"
             + "User Id=mtopup;Password=mtopupdb;";
            string result = "";
            try
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.ConnectionString = strCon;
                conn.Open();
                result = "Done";
            }
            catch (Exception ex)
            {
                result = "ERR: " + ex.Message.ToString();
            }
            return result;
        }
        public static string OpenDBNew(OracleConnection conn)
        {
            string strCon = "Data Source=(DESCRIPTION="
             + "(ADDRESS=(PROTOCOL=TCP)(HOST=172.28.14.86)(PORT=1521))"
             + "(CONNECT_DATA=(SERVICE_NAME=CCDB003)));"
             + "User Id=webcall;Password=webcall123;";
            string result = "";
            try
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.ConnectionString = strCon;
                conn.Open();
                result = "Done";
            }
            catch (Exception ex)
            {
                result = "ERR: " + ex.Message.ToString();
            }
            return result;
        }

        public static string OpenDBBanking(OracleConnection conn)
        {
            string strCon = "Data Source=(DESCRIPTION="
             + "(ADDRESS=(PROTOCOL=TCP)(HOST=172.28.12.95)(PORT=1521))"
             + "(CONNECT_DATA=(SERVICE_NAME=CCDB001)));"
             + "User Id=banking;Password=bankingdb;";
            string result = "";
            try
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.ConnectionString = strCon;
                conn.Open();
                result = "Done";
            }
            catch (Exception ex)
            {
                result = "ERR: " + ex.Message.ToString();
            }
            return result;
        }
    }
}