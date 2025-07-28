//using System;
//using System.Collections.Generic;
//using System.Data.Common;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Linq;
//using System.Xml.Schema;

using System.Data;
using Microsoft.Data.Sqlite;

namespace PolytecOrderEDI
{
    static class TableEdiAppConnectionLog
    {
        private static readonly SqliteConnection dbConnection = EdiAppDatabase.dbConnection;

        private static readonly string tn_EdiAppConnectionInfo = "EdiAppConnectionInfo";
        private static readonly string fn_Connected = "Connected";
        private static readonly string fn_UserName = "UserName";
        private static readonly string fn_ConnectDateTime = "ConnectDateTime";
        private static readonly string fn_DisConnectDateTime = "DisConnectDateTime";

        //static TableEdiAppConnectionLog()
        //{
        //    if(!EdiAppDatabase.CheckTableExists(tn_EdiAppConnectionInfo)) CreateTableIfNotExists();
        //}

        //private static void CreateTableIfNotExists()
        //{
        //    sql = $"CREATE TABLE IF NOT EXISTS {tn_EdiAppConnectionInfo} ( {fn_Connected} TEXT, {fn_UserName} TEXT, {fn_ConnectDateTime} TEXT, {fn_DisConnectDateTime} TEXT )";
        //    dbConnection.Open();
        //    using var command = new SqliteCommand(sql, dbConnection);
        //    command.ExecuteNonQuery();
        //    dbConnection.Close();
        //}

        public static void AddCurrentUserConnectDateTime()
        {
            string sql = string.Empty;
            string connected = "True";
            string name = GlobalVariable.CurrentUserName;
            string currentDateTime = DateTime.Now.ToString();

            if (CheckUserExistsInTable(name))
            {
                sql = $" UPDATE {tn_EdiAppConnectionInfo} " +
                      $" SET {fn_Connected} = @{fn_Connected}, {fn_ConnectDateTime} = @{fn_ConnectDateTime}, {fn_DisConnectDateTime} = @{fn_DisConnectDateTime} " +
                      $" WHERE {fn_UserName} LIKE @{fn_UserName} ";
            }
            else
            {
                sql = $" INSERT INTO {tn_EdiAppConnectionInfo} ({fn_Connected}, {fn_UserName}, {fn_ConnectDateTime}, {fn_DisConnectDateTime} ) " +
                      $" VALUES (@{fn_Connected}, @{fn_UserName}, @{fn_ConnectDateTime}, @{fn_DisConnectDateTime}) ";
            }

            dbConnection.Open();
            using var command = new SqliteCommand(sql, dbConnection);
            command.Parameters.AddWithValue($"@{fn_Connected}", connected);
            command.Parameters.AddWithValue($"@{fn_UserName}", name);
            command.Parameters.AddWithValue($"@{fn_ConnectDateTime}", currentDateTime);
            command.Parameters.AddWithValue($"@{fn_DisConnectDateTime}", "");
            command.ExecuteNonQuery();
            dbConnection.Close();

            //DateTime xxx = DateTime.Parse(currentDateTime);

        }

        public static void UpdateCurrentUserDisconnectDateTime()
        {
            string connected = "False";
            string name = GlobalVariable.CurrentUserName;
            string currentDateTime = DateTime.Now.ToString();


            string sql =   $" UPDATE {tn_EdiAppConnectionInfo} " +
                    $" SET {fn_Connected} = @{fn_Connected} , {fn_DisConnectDateTime} = @{fn_DisConnectDateTime}  " +
                    $" WHERE {fn_Connected} LIKE @True AND {fn_UserName} LIKE @{fn_UserName} ";

            dbConnection.Open();
            using var command = new SqliteCommand(sql, dbConnection);
            command.Parameters.AddWithValue($"@{fn_Connected}", connected);
            command.Parameters.AddWithValue($"@{fn_DisConnectDateTime}", currentDateTime);
            command.Parameters.AddWithValue($"@True", "True");
            command.Parameters.AddWithValue($"@{fn_UserName}", name);
            command.ExecuteNonQuery();
            dbConnection.Close();
        }

        private static bool CheckUserExistsInTable(string userName)
        {
            dbConnection.Open();
            string sql = $"SELECT {fn_UserName} FROM {tn_EdiAppConnectionInfo} WHERE {fn_UserName} = @{fn_UserName} ";
            using var command = new SqliteCommand(sql, dbConnection);
            command.Parameters.AddWithValue($"@{fn_UserName}", userName);
            using var reader = command.ExecuteReader();
            bool userExists = reader.HasRows;
            dbConnection.Close();

            return userExists;
        }

        public static string GetActiveUsers()
        {
            var activeUser = string.Empty;

            string sql = $" SELECT {fn_UserName} , {fn_ConnectDateTime} FROM {tn_EdiAppConnectionInfo} " +
                  $" WHERE {fn_Connected} = @True";
            dbConnection.Open();
            using var command = new SqliteCommand(sql, dbConnection);
            command.Parameters.AddWithValue($"@True", "True");
            using var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    activeUser += $"-{reader.GetValue($"{fn_UserName}")}\t[Connection time: {reader.GetValue($"{fn_ConnectDateTime}")}]\n";
                    
                }
            }
            reader.Close();
            dbConnection.Close();

            return activeUser;
        }

    }
}















