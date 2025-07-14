
//using System;
//using System.Collections.Generic;
//using System.Data.Common;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace PolytecOrderEDI
{
    static class TableEdiAppVersionInfo
    {
        private static string sql = string.Empty;
        private static readonly SqliteConnection dbConnection = EdiAppDatabase.dbConnection;

        private static readonly string tn_EdiAppVersionInfo = "EdiAppVersionInfo";
        private static readonly string fn_VersionNo = "VersionNo";
        private static readonly string fn_ReleaseDate = "ReleaseFullDate";

        //static TableEdiAppVersionInfo()
        //{
        //    if (!EdiAppDatabase.CheckTableExists(tn_EdiAppVersionInfo)) CreateTableIfNotExists();
        //}

        //private static void CreateTableIfNotExists()
        //{
        //    sql = $"CREATE TABLE IF NOT EXISTS {tn_EdiAppVersionInfo} ( {fn_VersionNo} REAL, {fn_ReleaseDate} TEXT ) ";
        //    dbConnection.Open();
        //    using var command = new SqliteCommand(sql, dbConnection);
        //    command.ExecuteNonQuery();
        //    dbConnection.Close();
        //}
    }
}
