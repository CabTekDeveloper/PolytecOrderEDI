//using Microsoft.VisualBasic;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace PolytecOrderEDI
{
    static class GlobalVariable
    {
        public static string DateFormat { get { return "dd/MM/yyyy"; } }
        public static JOBTYPE JobType {get; set;} = JOBTYPE.None;
        public static string CurrentUserName { get; set;} = string.Empty;
        public static string PoNumber { get; set;} = string.Empty;
        public static string RequestedDate { get; set; } = string.Empty;
        public static string FileName { get; set; } = string.Empty;
        public static string OutputDirectory { get; set; } = string.Empty;
        public static bool IsTestMode { get; set; } = false;
        public static string Contact {  get; set; } = string.Empty;

        //Some of the global variables need to be reset when the application starts or when the job type changes.
        public static void Reset()
        {
            PoNumber = string.Empty;
            RequestedDate = string.Empty;
            FileName = string.Empty;
            OutputDirectory = string.Empty;
            Contact = string.Empty;
        }

    }
}
