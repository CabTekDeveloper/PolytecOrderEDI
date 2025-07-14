
//using Microsoft.VisualBasic;
//using System;
//using System.Collections.Generic;
//using System.Data.Common;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Linq;

using Microsoft.Data.Sqlite;
using System.Data;

namespace PolytecOrderEDI
{
    static class TablePolytecBoardColors
    {
        private static string SQL { get; set; } = string.Empty;
        private static SqliteConnection DbConnection { get; } = EdiAppDatabase.dbConnection;
        private static string TableName { get; } = "PolytecBoardColors";
        private static string FN_MaterialCode { get; } = "MaterialCode";
        private static string FN_Color { get; } = "Color";
        private static string FN_Finish { get; } = "Finish";
        private static string FN_Side { get; } = "Side";
        private static string FN_MaterialDescription { get; } = "MaterialDescription";
        private static string FN_Grain { get; } = "Grain";


        public static PolyColor? GetColorInfo(string materialCode)
        {
            materialCode = materialCode.Trim();
            var color = string.Empty;
            var finish = string.Empty;
            var side = string.Empty;
            var grain = string.Empty;
            var materialDescription = string.Empty;

            SQL = $" SELECT * FROM {TableName} WHERE {FN_MaterialCode} = @{FN_MaterialCode} COLLATE NOCASE";
            DbConnection.Open();
            using var command = new SqliteCommand(SQL, DbConnection);
            command.Parameters.AddWithValue($"@{FN_MaterialCode}", materialCode.Trim());
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                materialCode = $"{reader.GetValue($"{FN_MaterialCode}")}";
                color = $"{reader.GetValue($"{FN_Color}")}";
                finish = $"{reader.GetValue($"{FN_Finish}")}";
                side = $"{reader.GetValue($"{FN_Side}")}";
                grain = $"{reader.GetValue($"{FN_Grain}")}";
                materialDescription = $"{reader.GetValue($"{FN_MaterialDescription}")}";
            }
            reader.Close();
            DbConnection.Close();

            var colorInfo = (materialDescription != "") ? new PolyColor(materialCode, color, finish, side, grain, materialDescription) : null;

            return colorInfo;
        }


        public static List<PolyColor> GetAllRecords()
        {
            List<PolyColor> polytecColors = [];

            DbConnection.Open();
            SQL = $" SELECT * FROM {TableName} ORDER BY {FN_MaterialCode} ASC";
            using var command = new SqliteCommand(SQL, DbConnection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var materialCode = $"{reader.GetValue($"{FN_MaterialCode}")}";
                var color = $"{reader.GetValue($"{FN_Color}")}";
                var finish = $"{reader.GetValue($"{FN_Finish}")}";
                var side = $"{reader.GetValue($"{FN_Side}")}";
                //var grain = (int) reader.GetValue($"{FN_Grain}");
                var grain = $"{reader.GetValue($"{FN_Grain}")}";

                var materialDescription = $"{reader.GetValue($"{FN_MaterialDescription}")}";
                polytecColors.Add(new PolyColor(materialCode, color, finish, side, grain, materialDescription));
            }
            reader.Close();
            DbConnection.Close();

            return polytecColors;
        }


        public static bool CheckRecordExists(string materialCode)
        {
            materialCode = materialCode.Trim();

            DbConnection.Open();
            SQL = $" SELECT * FROM {TableName} WHERE {FN_MaterialCode} = @{FN_MaterialCode} COLLATE NOCASE";
            using var command = new SqliteCommand(SQL, DbConnection);
            command.Parameters.AddWithValue($"@{FN_MaterialCode}", materialCode.Trim());
            using var reader = command.ExecuteReader();
            bool recordExists = reader.HasRows;
            reader.Close();
            DbConnection.Close();

            return recordExists;
        }

        public static void InsertRecord(PolyColor colorInfo)
        {
            DbConnection.Open();
            SQL = $" INSERT INTO {TableName} ({FN_MaterialCode}, {FN_Color}, {FN_Finish}, {FN_Side}, {FN_Grain}, {FN_MaterialDescription} ) " +
                  $" VALUES (@{FN_MaterialCode}, @{FN_Color}, @{FN_Finish}, @{FN_Side}, @{FN_Grain}, @{FN_MaterialDescription}) ";

            using var command = new SqliteCommand(SQL, DbConnection);
            command.Parameters.AddWithValue($"@{FN_MaterialCode}", colorInfo.MaterialCode);
            command.Parameters.AddWithValue($"@{FN_Color}", colorInfo.Color);
            command.Parameters.AddWithValue($"@{FN_Finish}", colorInfo.Finish);
            command.Parameters.AddWithValue($"@{FN_Side}", colorInfo.Side);
            command.Parameters.AddWithValue($"@{FN_Grain}", colorInfo.Grain);
            command.Parameters.AddWithValue($"@{FN_MaterialDescription}", colorInfo.MaterialDescription);
            
            command.ExecuteNonQuery();
            DbConnection.Close();
        }

       public static void DeleteRecord(string materialCode)
       {
            materialCode = materialCode.Trim();

            DbConnection.Open();
            SQL = $" DELETE FROM {TableName} WHERE {FN_MaterialCode} = @{FN_MaterialCode} COLLATE NOCASE";
            using var command = new SqliteCommand(SQL, DbConnection);
            command.Parameters.AddWithValue($"@{FN_MaterialCode}", materialCode);
            command.ExecuteNonQuery();
            DbConnection.Close();
        }

        public static void UpdateRecord(string materialCode,PolyColor colorInfo)
        {
            materialCode = materialCode.Trim();
            DbConnection.Open();
            SQL =   $" UPDATE {TableName} " +
                    $" SET  {FN_MaterialCode} = @{FN_MaterialCode}, " +
                    $"      {FN_Color} = @{FN_Color}, " +
                    $"      {FN_Finish} = @{FN_Finish}, " +
                    $"      {FN_Side} = @{FN_Side}, " +
                    $"      {FN_Grain} = @{FN_Grain}, " +
                    $"      {FN_MaterialDescription} = @{FN_MaterialDescription} " +
                    $" WHERE {FN_MaterialCode} = @OriginalMaterialCode COLLATE NOCASE";

            using var command = new SqliteCommand(SQL, DbConnection);
            command.Parameters.AddWithValue($"@{FN_MaterialCode}", colorInfo.MaterialCode);
            command.Parameters.AddWithValue($"@{FN_Color}", colorInfo.Color);
            command.Parameters.AddWithValue($"@{FN_Finish}", colorInfo.Finish);
            command.Parameters.AddWithValue($"@{FN_Side}", colorInfo.Side);
            command.Parameters.AddWithValue($"@{FN_Grain}", colorInfo.Grain);
            command.Parameters.AddWithValue($"@{FN_MaterialDescription}", colorInfo.MaterialDescription);
            command.Parameters.AddWithValue($"@OriginalMaterialCode", materialCode);
            command.ExecuteNonQuery();
            DbConnection.Close();
        }

    }
}


