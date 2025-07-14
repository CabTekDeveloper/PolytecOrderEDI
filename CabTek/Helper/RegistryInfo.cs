
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using Microsoft.Win32;

namespace PolytecOrderEDI
{
    static class RegistryInfo
    {
        public static string GetUserName()
        {
            try
            {
                string keyName = "Name";
                var registryKey = Registry.CurrentUser.OpenSubKey(FileAndDirectory.KeyPath_PruchaseOrderForm_Information);
                var keyVal = (registryKey == null) ? "" : registryKey.GetValue(keyName, "");
                return (string) keyVal;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return string.Empty;
            }
        }
        
    }
}
