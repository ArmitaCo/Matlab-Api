using System;

namespace Matlab.DataModel
{
    [Flags]
    public enum UsePackageState
    {
        Owned=1,
        Vas=2,
        Buy=4,
        Finished=8
    }
}