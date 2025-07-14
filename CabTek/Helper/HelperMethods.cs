
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.Globalization;
using BorgEdi.Enums;

namespace PolytecOrderEDI
{
    static class HelperMethods
    {
        public static Edge GetReturnPanelEdge(string strEdge)
        {
            Edge returnEdge = new();
            if (strEdge == "left") returnEdge = Edge.Left;
            else if (strEdge == "right") returnEdge = Edge.Right;
            else if (strEdge == "top") returnEdge = Edge.Top;
            else if (strEdge == "bottom") returnEdge = Edge.Bottom;

            return returnEdge;
        }


        public static string ReplaceEdgeLocationTBLRby1s(string edgeLocation)
        {
            edgeLocation = edgeLocation.ToUpper();
            edgeLocation = edgeLocation.Replace("T", "1").Replace("B","1").Replace("L", "1").Replace("R", "1");
            return edgeLocation;
        }


        public static string ReplaceHbyTBLR(string edgeLocation)
        {
            edgeLocation = edgeLocation.ToUpper();

            if (edgeLocation[0] == 'H') edgeLocation = edgeLocation.Remove(0, 1).Insert(0, "T");
            if (edgeLocation[1] == 'H') edgeLocation = edgeLocation.Remove(1, 1).Insert(1, "B");
            if (edgeLocation[2] == 'H') edgeLocation = edgeLocation.Remove(2, 1).Insert(2, "L");
            if (edgeLocation[3] == 'H') edgeLocation = edgeLocation.Remove(3, 1).Insert(3, "R");

            return edgeLocation;
        }


        public static Dictionary<string,string> WorkoutCompactLaminateEdgeProfile(string edgeLocation, string handleSystem, string edgeMould)
        {
            edgeLocation = edgeLocation.ToUpper();

            Dictionary<char, string> edgingRule = new()
            {
                { 'H', handleSystem },
                { 'X' , "Square" },
                { 'T', edgeMould },
                { 'B', edgeMould },
                { 'L', edgeMould },
                { 'R', edgeMould },
            };
            Dictionary<string, string> dictEdgeProfile = [];

            dictEdgeProfile.Add("topEdge", edgingRule[edgeLocation[0]]);
            dictEdgeProfile.Add("bottomEdge", edgingRule[edgeLocation[1]]);
            dictEdgeProfile.Add("leftEdge", edgingRule[edgeLocation[2]]);
            dictEdgeProfile.Add("rightEdge", edgingRule[edgeLocation[3]]);

            return dictEdgeProfile;

        }


        public static string TitleCaseString(string str)
        {
            return string.Join(" ", str.Split([' '], StringSplitOptions.RemoveEmptyEntries).Select(c => c.Substring(0, 1).ToUpper() + c.Substring(1).ToLower()));

        }


        public static Dictionary<string, double> SplitParameter(string param)
        {
            Dictionary<string, double> parameter = [];
            try
            {
                if (param.Length > 0)
                {
                    var arrParam = param.Split('_');
                    foreach (var item in arrParam)
                    {
                        var tempVal = item.Split('=');
                        var key = tempVal[0].Trim();
                        var val = double.Parse(tempVal[1].Trim());
                        parameter.TryAdd(key, val);
                    }
                }
                return parameter;
            }
            catch
            {
                parameter.Clear();
                return parameter;
            }
        }


        public static bool CheckStringDateMatchesFormat(string date, string dateFormat)
        {
            if ((DateTime.TryParseExact(date, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tempDate))) return true;
            else return false;
        }


        public static string GetEdgeColor(ICBPart part)
        {
            string edgeDescription;
            if      (part.TopEdgeDescription != ""      && !IsEdgeMelamineHandle(part.TopEdgeDescription))      edgeDescription = part.TopEdgeDescription;
            else if (part.BottomEdgeDescription != ""   && !IsEdgeMelamineHandle(part.BottomEdgeDescription))   edgeDescription = part.BottomEdgeDescription;
            else if (part.LeftEdgeDescription != ""     && !IsEdgeMelamineHandle(part.LeftEdgeDescription))     edgeDescription = part.LeftEdgeDescription;
            else if (part.RightEdgeDescription != ""    && !IsEdgeMelamineHandle(part.RightEdgeDescription))    edgeDescription = part.RightEdgeDescription;
            else edgeDescription = string.Empty;

            return edgeDescription.ToLower().Replace("1mm", string.Empty).Replace("edge", string.Empty).Trim(); 
        }

        public static bool IsEdgeMelamineHandle(string edgeDescription)
        {
            List<string> melamineHandles = ["45Deg", "FPHA", "FPHBA"]; 
            return melamineHandles.Any(handle => edgeDescription.Contains(handle, StringComparison.OrdinalIgnoreCase));

        }

        public static bool IsMaterialHMRparticleBoard(string material)
        {
            if (string.Equals(material, HMRBOARD.WhiteHmrParticleBoard.ToString(), StringComparison.OrdinalIgnoreCase) || string.Equals(material, HMRBOARD.BlackHmrParticleBoard.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else { return false; }
        }


        //Round number conditionally
        public static double RoundDownNumberLessThanDecimalValueElseRoundUp(double number, double lessThanDecimal)
        {
            double decimalPart = Math.Round(number - Math.Truncate(number), 1);
            if (decimalPart >= lessThanDecimal) return Math.Ceiling(number);
            else return Math.Floor(number);
        }

        

    }
}
