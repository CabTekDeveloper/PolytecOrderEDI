//using BorgEdi.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace PolytecOrderEDI
{
    class BuildParameter_Door
    {
        public HINGETYPE HingeType { get;  set; }    //HCLS: 0=Blum, 1=Hettich
        public int PNGC { get; private set; }            //Glass frame door cutouts     
        public double HTOD { get; private set; }
        public double HingeCupInset { get; private set; }
        public double HingeBlockInset { get; private set; }
        public double BifoldHingeCupInset { get; private set; }
        public double Hole1FromBot { get; private set; }
        public double Hole2FromTop { get; private set; }
        public double Hole3FromTop { get; private set; }
        public double Hole4FromTop { get; private set; }
        public double Hole5FromTop { get; private set; }
        public double Hole6FromTop { get; private set; }
        public int NumHoles { get; private set; }


        //Default instace
        public BuildParameter_Door() { }


        public BuildParameter_Door(CabinetPart part)
        {
            PARTNAME partName = part.PartName;
            var height = part.Height;
            var dict_param = HelperMethods.SplitParameter(part.Parameter);
           
            if (dict_param.Count > 0 && partName != PARTNAME.None)
            {
                double paramValue;

                var hcls    = dict_param.TryGetValue("HCLS", out paramValue) ? (int)paramValue : -1;    //HCLS: Hinge brand (0=Blum, 1=Hettich) Here, we cannot assing default paramValue = 0, since HCLS=0 is Blum hinge.
                var pngc    = dict_param.TryGetValue("PNGC", out paramValue) ? (int)paramValue : 0;     //PNGC: Glass frame door cutout (1=1 cutout)
                var htod    = dict_param.TryGetValue("HTOD", out paramValue) ? paramValue : 0;          //HTOD: Top door Gap of 770 style leaf door

                var hand    = dict_param.TryGetValue("HAND", out paramValue) ? (int)paramValue : 0; //HAND: (Left: 1=On,3=Off) (Right: 2=On,4=Off)
                var htyp    = dict_param.TryGetValue("HTYP", out paramValue) ? (int)paramValue : 0; //HTYP: Hinge drilling type (1=HMX, 2=HNH) 
                var hmx     = dict_param.TryGetValue("HMX", out paramValue)  ? paramValue : 0;      //HMX : Maximum spacing between hinges 
                var hin     = dict_param.TryGetValue("HIN", out paramValue)  ? paramValue : 0;      //HIN : Hinge Inset
                var hsp     = dict_param.TryGetValue("HSP", out paramValue)  ? paramValue : 0;      //HSP : Hinge start position
                var hep     = dict_param.TryGetValue("HEP", out paramValue)  ? paramValue : 0;      //HEP : Hinge end position

                var hngm    = dict_param.TryGetValue("HNGM", out paramValue) ? paramValue : 0;  //HNGM: Maximum spacing between hinge blocks
                var yhng    = dict_param.TryGetValue("YHNG", out paramValue) ? paramValue : 0;  //YHNG: Hinge block side Inset
                var bhng    = dict_param.TryGetValue("BHNG", out paramValue) ? paramValue : 0;  //BHNG: Hinge block from bottom
                var thng    = dict_param.TryGetValue("THNG", out paramValue) ? paramValue : 0;  //THNG: Hinge block from top

                var hnsd    = dict_param.TryGetValue("HNSD", out paramValue) ? (int)paramValue : 0; //HNSD: Non standard hinge nos.
                var h1      = dict_param.TryGetValue("H1", out paramValue)   ? paramValue : 0;      //H1: Hinge 1 position
                var h2      = dict_param.TryGetValue("H2", out paramValue)   ? paramValue : 0;      //H2: Hinge 2 position
                var h3      = dict_param.TryGetValue("H3", out paramValue)   ? paramValue : 0;      //H3: Hinge 3 position
                var h4      = dict_param.TryGetValue("H4", out paramValue)   ? paramValue : 0;      //H4: Hinge 4 position
                var h5      = dict_param.TryGetValue("H5", out paramValue)   ? paramValue : 0;      //H5: Hinge 5 position
                var h6      = dict_param.TryGetValue("H6", out paramValue)   ? paramValue : 0;      //H6: Hinge 6 position



                //if the current part don't have HCLS param (0 or 1), get it from other parts that belong to same cabinet
                if (hcls == -1)
                {
                    foreach (var cabinet in ICB.Cabinets)
                    {
                        if (cabinet.CabinetName == part.CabinetName)
                        {
                            var tempVal = cabinet.GetHingeType();
                            hcls = (tempVal == HINGETYPE.Blum) ? 0 : (tempVal == HINGETYPE.Hettich) ? 1 : -1;
                            break;
                        }
                    }
                }

                //Set
                HingeType = (hcls == 0) ? HINGETYPE.Blum : (hcls == 1) ? HINGETYPE.Hettich : HINGETYPE.None;
                PNGC = pngc;
                HTOD = htod;

                //Set Hinge Cup Inset
                if (partName ==PARTNAME.Left_Leaf_770 || partName == PARTNAME.Right_Leaf_770)
                {
                    HingeCupInset = 22;
                }
                else
                {
                    HingeCupInset = hin;
                }


                //Set Hinge Block Inset
                if (partName.ToString().Contains("770"))
                {
                    HingeBlockInset = yhng;
                }
                else if (partName == PARTNAME.Right_Leaf  || partName == PARTNAME.Left_Leaf )
                {
                    HingeBlockInset = 37;
                }
                else if (partName == PARTNAME.Left_Blind_Panel || partName == PARTNAME.Right_Blind_Panel)
                {
                    HingeBlockInset = (hcls == 0) ? 22 : (hcls == 1) ? 33 : 0;
                }


                //Set Bi-Fold Hinge Cup Inset
                if (partName == PARTNAME.Left_Bifold || partName == PARTNAME.Right_Bifold)
                {
                    BifoldHingeCupInset = (hcls == 0) ? 12.5 : (hcls == 1) ? 22 : 0;
                }


                //Set Hinge positions
                if (hnsd >= 2 && h1 != 0 && h2 != 0)
                {
                    Workout_NonStangdard_HingePositions(height, hnsd, h1, h2, h3, h4, h5, h6);
                }
                else if (hsp > 0 && hep > 0 && hmx > 0)
                {
                    Workout_StandardHolePositions(height, hsp, hep, hmx);
                }


                //Set Hinge Block Positions
                if (partName == PARTNAME.Left_Blind_Panel || partName == PARTNAME.Right_Blind_Panel || partName == PARTNAME.Left_Leaf_770 || partName == PARTNAME.Right_Leaf_770)
                {
                    if (bhng > 0 && thng > 0 && hngm > 0) Workout_StandardHolePositions(height, bhng, thng, hngm);
                }

                //Set Number of hinges, set this only after setting the Hinge Positions.
                NumHoles = (Hole1FromBot > 0 ? 1 : 0) + (Hole2FromTop > 0 ? 1 : 0) + (Hole3FromTop > 0 ? 1 : 0) + (Hole4FromTop > 0 ? 1 : 0) + (Hole5FromTop > 0 ? 1 : 0) + (Hole6FromTop > 0 ? 1 : 0);


                //Finally, Reset properties that are not required
                if (partName == PARTNAME.Right_Leaf  || partName == PARTNAME.Left_Leaf )
                {
                    HingeCupInset = 0;
                    HingeType = HINGETYPE.None;
                }
                else if (partName == PARTNAME.Left_Leaf_770 || partName == PARTNAME.Right_Leaf_770)
                {
                    HingeType = HINGETYPE.None;
                }
                //End Reset value
            }
        }

        private void Workout_StandardHolePositions(double height, double startHolePos, double endHolePos, double maxHoleGap)
        {
            var totalHoles = (int)(((height - startHolePos - endHolePos) / maxHoleGap) + 2);

            Hole1FromBot = startHolePos;
            Hole2FromTop = endHolePos;

            if (totalHoles > 2)
            {
                double holeGap = (height - startHolePos - endHolePos) / (totalHoles - 1);
                Hole3FromTop = Hole2FromTop + holeGap;
                Hole4FromTop = (totalHoles > 3) ? Hole3FromTop + holeGap : 0;
                Hole5FromTop = (totalHoles > 4) ? Hole4FromTop + holeGap : 0;
                Hole6FromTop = (totalHoles > 5) ? Hole5FromTop + holeGap : 0;
            }
        }
     
        private void Workout_NonStangdard_HingePositions(double height, int hnsd, double h1, double h2, double h3, double h4, double h5, double h6)
        {
            Hole1FromBot = h1;
            if (hnsd == 6)
            {
                Hole2FromTop = height - h6;
                Hole3FromTop = height - h5;
                Hole4FromTop = height - h4;
                Hole5FromTop = height - h3;
                Hole6FromTop = height - h2;
            }
            else if (hnsd == 5)
            {
                Hole2FromTop = height - h5;
                Hole3FromTop = height - h4;
                Hole4FromTop = height - h3;
                Hole5FromTop = height - h2;
            }
            else if (hnsd == 4)
            {
                Hole2FromTop = height - h4;
                Hole3FromTop = height - h3;
                Hole4FromTop = height - h2;
            }
            else if (hnsd == 3)
            {
                Hole2FromTop = height - h3;
                Hole3FromTop = height - h2;
            }
            else if (hnsd == 2)
            {
                Hole2FromTop = height - h2;
            }

        }

      

        //End of Methods

    }//End of class

}//End of namespace

