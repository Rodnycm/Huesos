using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections.Generic;

namespace DAL
{
    public class DBAccess
    {

        public static string connString;
        public static string Route;


        #region Utilities

        public DBAccess()
        {
            connString = "";
        }

        //Returns the ConnectionString from the Web.Config file
        public static string GetConnectionString()
        {
            try
            {
                var appSR = new AppSettingsReader();
                connString = ConfigurationManager.ConnectionStrings["Anatomia"].ConnectionString;
                return connString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Returns the SQLConnection
        public static SqlConnection GetConnection()
        {
            try
            {
                if (connString == "" || connString == null)
                {
                    connString = GetConnectionString();
                }
                return new SqlConnection(connString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Returns an initialized SqlCommand
        public static IDbCommand GetCommand()
        {
            return new SqlCommand();
        }

        #endregion


        #region SQL Methods

        //Executes SQL code that doesn't return anything.
        public static void ExecuteNonQuery(string pSQL)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(pSQL, GetConnection());
                cmd.Connection.Open();
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Executes SQL code that returns an IDataReader implemented class
        public static IDataReader ExecuteQuery(string pSQL, bool dif)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(pSQL, GetConnection());
                cmd.Connection.Open();
                cmd.CommandTimeout = 0;

                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //Executes SQL code that returns a DataSet
        public static DataSet ExecuteQuery(SqlCommand pCmd)
        {
            try
            {
                SqlDataAdapter adp;
                DataSet tempDataset = new DataSet();

                pCmd.Connection = GetConnection();
                pCmd.CommandTimeout = 0;

                adp = new SqlDataAdapter(pCmd);
                adp.Fill(tempDataset);

                pCmd.Connection.Close();

                return tempDataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Executes the SP but does'nt return anything
        public static void ExecuteSP(SqlCommand pCmd, string pName)
        {
            try
            {
                pCmd.Connection = GetConnection();
                pCmd.CommandText = pName;
                pCmd.CommandType = CommandType.StoredProcedure;
                pCmd.Connection.Open();
                pCmd.ExecuteReader();
                pCmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Executes the SP but does'nt return anything
        public static void ExecuteSPNonQuery(SqlCommand pCmd, string pName)
        {
            try
            {
                pCmd.Connection = GetConnection();
                pCmd.CommandText = pName;
                pCmd.CommandType = CommandType.StoredProcedure;
                pCmd.Connection.Open();
                pCmd.ExecuteNonQuery();
                pCmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Executes the SP and returns a filled dataset
        public static DataSet ExecuteSPWithDS(ref SqlCommand pCmd, string pName)
        {
            try
            {
                SqlDataAdapter adp;
                DataSet tempDataset = new DataSet();

                pCmd.Connection = GetConnection();
                pCmd.CommandText = pName;
                pCmd.CommandType = CommandType.StoredProcedure;
                pCmd.CommandTimeout = 0;

                adp = new SqlDataAdapter(pCmd);
                adp.Fill(tempDataset);

                pCmd.Connection.Close();

                return tempDataset;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        #endregion
    }
}