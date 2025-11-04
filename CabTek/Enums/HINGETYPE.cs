

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
        BlumRdf
    }
}



// Blum     : Standard Blum hinge
// Hettich  : Standard Hettich hinge
// Blum11   : Customized Blum hinge with different hole depts to standard blum hinge
// BlumLdf  : Add standard Blum hinge to Left of Drawer front provided the hinge insets and hole positions are provided.
// BlumRdf  : Add standard Blum hinge to Right of Drawer front provided the hinge insets and hole positions are provided.
