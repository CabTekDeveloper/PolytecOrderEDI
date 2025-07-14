//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace PolytecOrderEDI
{
    static class ColorManager
    {

        public static Color GetJobTypeForeColor(JOBTYPE jobType)
        {
            return jobType switch
            {
                JOBTYPE.Vinyl => Color.White,
                JOBTYPE.Melamine => Color.White,
                _ => Color.Black,
            };
            
        }

        public static Color GetJobTypeBackColor(JOBTYPE jobType)
        {
            return jobType switch
            {
                JOBTYPE.Vinyl => Color.DeepSkyBlue,
                JOBTYPE.Melamine => Color.Crimson,
                _ => Color.LightGray,
            };
        }


    }
}
