//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace PolytecOrderEDI
{

    public class Attachment(string fileName, string filePath)
    {
        //private string _fileName = fileName;
        //private string _filePath = filePath;

        public string FileName { get; } = fileName;
        public string FilePath { get; } = filePath;
    }
    
}
