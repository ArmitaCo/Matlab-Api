using System;

namespace Matlab.DataModel
{
    [Flags]
    public enum UserPackageState
    {
        Owned=1,
        Vas=2,
        Buy=4,
        Finished=8
    }
}