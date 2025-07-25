//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace PolytecOrderEDI
{

    static class FileAndDirectory
    {
        //////Uncomment the paths below for inhouse EDI App release. For Cabtek Latop uncomment the paths below, otherwise comment them to avoid confilicts.
        
        //public static string GoogleApiFolder {   get { return AppDomain.CurrentDomain.BaseDirectory + "\\CabTek\\Api\\GoogleApis" ; } }
        //public static string GoogleApiCredentialsFile { get { return $"{GoogleApiFolder}\\Credentials\\GoogleOauthCredentials.json"; } }
        //public static string GoogleApiTokenFile { get { return $"{GoogleApiFolder}\\Credentials\\token.json"; } }

        //public static string MelamineOrdersFolder
        //{
        //    get { return "\\\\Cbtksbs\\stolen\\Door Orders\\Polytec_EDI_Orders_Melamine"; }
        //}

        //public static string VinylOrdersFolder
        //{
        //    get { return "\\\\Cbtksbs\\stolen\\Door Orders\\Polytec EDI Orders"; }
        //}

        //public static string PolytecEDIordersFolder
        //{
        //    get { return "\\\\Cbtksbs\\stolen\\Door Orders\\Polytec EDI Orders"; }
        //}

        //public static string PolytecEDIdatabase
        //{
        //    get { return "\\\\Manager1\\SharedDatabase\\Polytec EDI Database\\EdiAppDatabase.db"; }
        //}

        //public static string KeyPath_PruchaseOrderForm_Information
        //{
        //    get { return "Software\\VB and VBA Program Settings\\PurchaseOrderForm\\Information"; }
        //}

        //public static string KitFilesFolder
        //{
        //    get { return "\\\\cbtksbs\\Machines\\Eze Import Back End\\Cabtek\\Kit files"; }
        //}

        //public static string Desktop
        //{
        //    get { return @"C:\Users\User\Desktop"; }
        //}

        //public static string Downloads
        //{
        //    get { return @"C:\Users\User\Downloads"; }
        //}
        //// End of Inhouse EDI App File Direcoty Paths

        // Uncomment the File Directory Paths below For CabTek Laptop

        public static string CabtTekLogoURL { get { return "https://drive.google.com/file/d/1shDh3fHc945DqfcdM56HwnjMPxAyDAXE/view?usp=sharing"; } }

        public static string GoogleApiFolder { get { return AppDomain.CurrentDomain.BaseDirectory + "\\CabTek\\Api\\GoogleApis"; } }
        public static string GoogleApiCredentialsFile { get { return $"{GoogleApiFolder}\\Credentials\\GoogleOauthCredentials.json"; } }
        public static string GoogleApiTokenFile { get { return $"{GoogleApiFolder}\\Credentials\\token.json"; } }


        private static readonly string Ddrive = "D:\\LOCAL_EDI_APP";
        public static string MelamineOrdersFolder
        {
            get { return $"{Ddrive}\\Door Orders\\Polytec_EDI_Orders_Melamine"; }
        }

        public static string VinylOrdersFolder
        {
            get { return $"{Ddrive}\\Door Orders\\Polytec_EDI_Orders_Vinyl"; }
        }

        public static string PolytecEDIdatabase
        {
            get { return $"{Ddrive}\\Database\\EdiAppDatabase.db"; }
        }

        public static string KitFilesFolder
        {
            get { return "M:\\Machines\\Eze Import Back End\\Cabtek\\Kit files"; }
        }

        public static string KeyPath_PruchaseOrderForm_Information
        {
            get { return "Software\\VB and VBA Program Settings\\PurchaseOrderForm\\Information"; }
        }

        public static string Downloads
        {
            get { return @"C:\Users\User\Downloads"; }
        }

        public static string Desktop
        {
            get { return @"C:\Users\User\Desktop"; }
        }

        // End of CabTek Laptop File Direcotry Paths


    }
}
