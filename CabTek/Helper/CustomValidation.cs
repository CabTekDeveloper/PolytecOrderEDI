//using BorgEdi.Models;
//using PolytecOrderEDI;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace PolytecOrderEDI
{
    static class CustomValidation

    {
        public static readonly string[] glassFrameDoorStyleProfiles = { "A", "C", "E", "G", "I", "K" };
        public static readonly string[] drawerBankStyleProfiles = {"A","B","E","F","I","J","K","L" };
        public static readonly string[] compactLaminateEdgeProfiles = { "Shark Nose", "Aris", "2mm Double Fine Edge", "Square" };

        public static readonly string[] thermoHandles = { "Bronte" };
        public static readonly string[] cutAndRoutHandles = { "Bronte" ,"Clovelly", "Kingsford", "Portsea (right)",  "Waverley" };
        public static readonly string[] barPanelStyleProfiles = { "2P", "3P", "4P", "5P"};

        public static bool IsValidGlassFrameDoorStyleProfile(string styleProfile)
        {
            if (glassFrameDoorStyleProfiles.Contains(styleProfile, StringComparer.OrdinalIgnoreCase)) return true;
            return false;
        }

        public static bool IsValidCompactLaminateEdgeProfile(string edgeProfile)
        {
            if (compactLaminateEdgeProfiles.Contains(edgeProfile, StringComparer.OrdinalIgnoreCase)) return true;
            return false;
        }

        public static bool IsCutAndRoutHandle(string handleSystem)
        {
            return (cutAndRoutHandles.Contains(handleSystem, StringComparer.OrdinalIgnoreCase)) ;
        }

        public static bool IsThermoHandle ( string handleSystem)
        {
            return (thermoHandles.Contains(handleSystem, StringComparer.OrdinalIgnoreCase));
        }

        public static bool IsDrawerBankStyleProfile(string styleProfile)
        {
            if (drawerBankStyleProfiles.Contains(styleProfile, StringComparer.OrdinalIgnoreCase)) return true;
            return false;
        }

        public static bool IsBarPanelStyleProfile(string styleProfile)
        {
            if (barPanelStyleProfiles.Contains(styleProfile, StringComparer.OrdinalIgnoreCase)) return true;
            return false;
        }
    }
}

