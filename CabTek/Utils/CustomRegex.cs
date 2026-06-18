//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.Text.RegularExpressions;

namespace PolytecOrderEDI
{
    public static partial class CustomRegex
    {
        [GeneratedRegex(@"^[a-zA-Z0-9]+$")]
        public static partial Regex LetterAndDigit();

        [GeneratedRegex(@"\s+")]
        public static partial Regex WhiteSpaces();

        //[GeneratedRegex(@"45Deg FPHA FPHBA", RegexOptions.IgnoreCase)]
        //public static partial Regex MelamineHandles();
    }
}
