//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace PolytecOrderEDI
{
    class OrderDetailsForGoogleApi
    {
        public JOBTYPE JobType { get; private set; } = JOBTYPE.None;
        public string PoNumber { get; private set; } = string.Empty;
        public string OrderedBy { get; private set; } = string.Empty;
        public string RequestedDate { get; set; } = string.Empty;
        public List<VinylPart> VinylProducts { get; private set; } = [];
        public List<Cabinet> MelamineCabinets { get; private set; } = [];   

        public OrderDetailsForGoogleApi()
        {
            JobType = GlobalVariable.JobType;
            PoNumber = (GlobalVariable.PoNumber.Length > 0) ? GlobalVariable.PoNumber : string.Empty;
            OrderedBy = (GlobalVariable.CurrentUserName.Length > 0) ? GlobalVariable.CurrentUserName : string.Empty;
            RequestedDate = GlobalVariable.RequestedDate;
            VinylProducts = VinylJob.LstVinylParts;
            MelamineCabinets = ICB.Cabinets;
        }
    }
}
