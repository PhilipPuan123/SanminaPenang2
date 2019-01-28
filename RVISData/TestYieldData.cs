using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RVISData
{
    public static class TestYieldData
    {
        public static uint TotalTestedUnits { get; set; } = 0;
        public static uint TotalPassedUnits { get; set; } = 0;
        public static double PassingRate
        {
            get
            {
                if (TotalPassedUnits > 0) return (TotalPassedUnits / TotalTestedUnits);
                else return 0;
            }
        }
    }
}
