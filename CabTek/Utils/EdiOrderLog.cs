using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolytecOrderEDI
{
    class EdiOrderLog
    {
        public string PoNumber { get; private set; } = string.Empty;
        public string OrderedBy { get; private set; } = string.Empty;
        public string SentTo { get; private set; } = string.Empty;
        public string DateTime { get; private set; } = string.Empty;
        public string GoogleSheetId { get; private set; } = string.Empty;

        public EdiOrderLog() { }

        public EdiOrderLog(string poNumber, string orderedBy, string sentTo, string dateTime, string googleSheetId = "")
        {
            PoNumber = poNumber;
            OrderedBy = orderedBy;
            SentTo = sentTo;
            DateTime = dateTime;
            GoogleSheetId = googleSheetId;
        }

        public override string ToString()
        {
            return $"PoNumber:   {PoNumber}\n" +
                   $"Ordered by:  {OrderedBy}\n" +
                   $"Orderd on:   {DateTime} \n";
        }
    }
}
