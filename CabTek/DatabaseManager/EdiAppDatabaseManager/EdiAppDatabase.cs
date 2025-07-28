//using System;
//using System.Collections.Generic;
//using System.Data.Common;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Text;
//using System.Threading.Tasks;

using Microsoft.Data.Sqlite;

namespace PolytecOrderEDI
{
    static class EdiAppDatabase
    {
        private static string sql = string.Empty;
        public static SqliteConnection dbConnection { get; } = new SqliteConnection($"Data Source = {FileAndDirectory.PolytecEDIdatabase}");
        
        public static bool CheckTableIsNotEmpty(string tableName)
        {
            sql = $"SELECT * FROM {tableName}";
            dbConnection.Open();
            using var command = new SqliteCommand(sql, dbConnection);
            using var reader = command.ExecuteReader();
            bool tableIsNotEmpty = reader.HasRows;
            dbConnection.Close();

            return tableIsNotEmpty;
        }

        public static bool CheckTableExists(string tableName)
        {   
            dbConnection.Open();
            sql = $"SELECT name FROM sqlite_master WHERE type='table' AND name = @{tableName} ";
            using var command = new SqliteCommand(sql, dbConnection);
            command.Parameters.AddWithValue($"@{tableName}", tableName);
            using var reader = command.ExecuteReader();
            bool tableExists = reader.HasRows;
            dbConnection.Close();
              
            return tableExists;
        }

        public static void DropTable(string tableName)
        {
            dbConnection.Open();
            sql = $"DROP Table {tableName}";
            using var command = new SqliteCommand( sql, dbConnection);
            command.ExecuteNonQuery();
            dbConnection.Close();
        }

        public static void RenameTable(string tableName, string newName)
        {
            dbConnection.Open();
            sql = $"ALTER TABLE {tableName} RENAME TO {newName}";
            using var command = new SqliteCommand (sql, dbConnection);
            command.ExecuteNonQuery ();
            dbConnection.Close();
        }

    }
}

