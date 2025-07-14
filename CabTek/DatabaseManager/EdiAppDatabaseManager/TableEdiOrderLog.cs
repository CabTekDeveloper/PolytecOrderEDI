
//using Microsoft.VisualBasic;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Linq;

using Microsoft.Data.Sqlite;
using System.Data;

namespace PolytecOrderEDI
{
    static class TableEdiOrderLog
    {
        private static string sql = string.Empty;
        private static readonly SqliteConnection dbConnection = EdiAppDatabase.dbConnection;

        private static readonly string tn_EdiOrderLog = "EdiOrderLog";
        private static readonly string fn_POnumber = "POnumber";
        private static readonly string fn_OderedBy = "OrderedBy";
        private static readonly string fn_SentTo = "SentTo";
        private static readonly string fn_DateTime = "DateTime";

        //static TableEdiOrderLog()
        //{
        //    if (!EdiAppDatabase.CheckTableExists(tn_EdiOrderLog)) CreateTableIfNotExists();
        //}

        //private static void CreateTableIfNotExists()
        //{
        //    sql = $"CREATE TABLE IF NOT EXISTS {tn_EdiOrderLog} ( {fn_POnumber} TEXT, {fn_OderedBy} TEXT, {fn_SentTo} TEXT, {fn_DateTime} TEXT  )";
        //    dbConnection.Open();
        //    using var command = new SqliteCommand(sql, dbConnection);
        //    command.ExecuteNonQuery();
        //    dbConnection.Close();
        //}

        public static void InsertNewOrderLog(string poNumber="", string orderedBy = "", string sentTo = "")
        {
            string currentDateTime = DateTime.Now.ToString();

            sql = $" INSERT INTO {tn_EdiOrderLog} ({fn_POnumber}, {fn_OderedBy}, {fn_SentTo}, {fn_DateTime} ) " +
                  $" VALUES (@{fn_POnumber}, @{fn_OderedBy}, @{fn_SentTo}, @{fn_DateTime} ) ";

            dbConnection.Open();
            using var command = new SqliteCommand(sql, dbConnection);
            command.Parameters.AddWithValue($"@{fn_POnumber}", poNumber.ToUpper());
            command.Parameters.AddWithValue($"@{fn_OderedBy}", orderedBy);
            command.Parameters.AddWithValue($"@{fn_SentTo}", sentTo.ToUpper());
            command.Parameters.AddWithValue($"@{fn_DateTime}", currentDateTime);

            command.ExecuteNonQuery();
            dbConnection.Close();
        }

        public static EdiOrderLog GetOrderLog(string poNumber)
        {
            EdiOrderLog orderLog = new();

            sql = $" SELECT * FROM {tn_EdiOrderLog} " +
                  $" WHERE {fn_POnumber}=@{fn_POnumber} COLLATE NOCASE";
            dbConnection.Open();
            using var command = new SqliteCommand(sql, dbConnection);
            command.Parameters.AddWithValue($"@{fn_POnumber}", poNumber);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {

                orderLog = new EdiOrderLog((string)reader.GetValue($"{fn_POnumber}"),
                                            (string)reader.GetValue($"{fn_OderedBy}"),
                                            (string)reader.GetValue($"{fn_SentTo}"),
                                            (string)reader.GetValue($"{fn_DateTime}"),
                                            ""
                                            );

                
            }
            reader.Close();
            dbConnection.Close();

            return orderLog;
        }

        public static string GetRecentOrderLogs(int totalRowsToGet = 5)
        {
            var strLogs = "";
            List<string> logs = [];
            sql = $" SELECT * FROM {tn_EdiOrderLog}";
            dbConnection.Open();
            using var command = new SqliteCommand(sql, dbConnection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var log = $"{reader.GetValue($"{fn_DateTime}")}\t" +
                          $"{reader.GetValue($"{fn_POnumber}")}   " +
                          $"{reader.GetValue($"{fn_OderedBy}")}";      
                logs.Add(log);
            }
            
            reader.Close();
            dbConnection.Close();

            if (logs.Count > 0)
            {  
                logs.Reverse();
                for (int i = 0; i < totalRowsToGet; i++) { strLogs += $"{logs[i]}\n"; }
            }
            return strLogs;
        }

        public static bool IsOrderSentToPolytec(string poNumber)
        {
            dbConnection.Open();
            sql = $" SELECT * FROM {tn_EdiOrderLog} " +
                  $" WHERE {fn_POnumber} = @{fn_POnumber} COLLATE NOCASE";
            using var command = new SqliteCommand(sql, dbConnection);
            command.Parameters.AddWithValue($"@{fn_POnumber}", poNumber);
            using var reader = command.ExecuteReader();
            bool isOrderSent = reader.HasRows;
            reader.Close();
            dbConnection.Close();

            return isOrderSent;
        }
    }

    
}
