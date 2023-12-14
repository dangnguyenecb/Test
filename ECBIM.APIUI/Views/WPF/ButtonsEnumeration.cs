using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECBIM.APIUI
{
    [Flags]
    public enum Button : int
    {
        None = 0,
        Ok = 1,
        Cancel = 2,
        No = 4,
        Yes = 8
    }
}
