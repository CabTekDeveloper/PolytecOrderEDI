//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace PolytecOrderEDI
{
    static class FileFilter
    {
        public static string ICB { get { return "ICB File|*.ICB"; }  }
        public static string CSV { get { return "csv File|*.csv"; } }
        public static string EXCEL { get { return "Excel Files|*.xls;*.xlsx;*.xlsm"; } }
    }
}
