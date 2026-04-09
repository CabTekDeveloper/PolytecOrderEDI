

using System;

namespace PolytecOrderEDI
{
    public enum HINGETYPE
    {
        None,
        Blum,
        Hettich,
        Blum11,
        BlumLdf,
        BlumRdf,
        Hole35mm
    }
}



// Blum     : Standard Blum hinge
// Hettich  : Standard Hettich hinge
// Blum11   : Customized Blum hinge with different hole depts to standard blum hinge
// BlumLdf  : Add standard Blum hinge to Left of Drawer front provided the hinge insets and hole positions are provided.
// BlumRdf  : Add standard Blum hinge to Right of Drawer front provided the hinge insets and hole positions are provided.
//Hole35mm  : Add 35mm wide and 13mm deep hinge cup hole
