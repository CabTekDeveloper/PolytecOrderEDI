//using System;
//using System.Collections.Generic;
//using System.Data.Common;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Schema;

using Microsoft.Data.Sqlite;

namespace PolytecOrderEDI
{
    static class TableDoorStyles
    {
        private static readonly SqliteConnection dbConnection = EdiAppDatabase.dbConnection;

        private static readonly string tn_DoorStyles = "DoorStyles";
        private static readonly string fn_StyleName = "StyleName";
        private static readonly string fn_Style = "Style";
        private static readonly string fn_Edge = "Edge";
        private static readonly string fn_MinHeight = "MinHeight";
        private static readonly string fn_MinWidth = "MinWidth";

        //static TableDoorStyles()
        //{
        //    if (!EdiAppDatabase.CheckTableExists(tn_DoorStyles)) CreateTableIfNotExists();
        //}

        //private static void CreateTableIfNotExists()
        //{
        //    sql = $"CREATE TABLE IF NOT EXISTS {tn_DoorStyles} ( {fn_StyleName} TEXT, {fn_Style} INTEGER, {fn_Edge} TEXT )";
        //    dbConnection.Open();
        //    using var command = new SqliteCommand(sql, dbConnection);
        //    command.ExecuteNonQuery();
        //    dbConnection.Close();
        //}


        public static int GetDoorStyleNo(string styleName)
        {
            dbConnection.Open();
            string sql = $" SELECT {fn_Style} FROM {tn_DoorStyles} WHERE {fn_StyleName} LIKE @{fn_StyleName}";
            using var command = new SqliteCommand(sql, dbConnection);
            command.Parameters.AddWithValue($"@{fn_StyleName}", styleName);
            var result = command.ExecuteScalar();
            int style = (result == null) ? 0 : Convert.ToInt32(result);
            dbConnection.Close();

            return style;
        }


        public static bool CheckStyleNameExists(string styleName)
        {
            dbConnection.Open();
            string sql = $" SELECT {fn_StyleName} FROM {tn_DoorStyles} WHERE {fn_StyleName} LIKE @{fn_StyleName}";
            using var command = new SqliteCommand(sql, dbConnection);
            command.Parameters.AddWithValue($"@{fn_StyleName}", styleName);
            bool styleNameExists = command.ExecuteScalar() !=null;
            dbConnection.Close();

            return styleNameExists;
        }


        public static string GetEdgeByStyleName(string styleName)
        {
            dbConnection.Open();
            string sql = $" SELECT {fn_Edge} FROM {tn_DoorStyles} WHERE {fn_StyleName} LIKE @{fn_StyleName}";
            using var command = new SqliteCommand(sql, dbConnection);
            command.Parameters.AddWithValue($"@{fn_StyleName}", styleName);
            var result = command.ExecuteScalar();
            string edge = (result == null) ? "" : (string)result;
            dbConnection.Close();

            return edge;
        }

    }

}


