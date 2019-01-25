using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RVISData
{
    public static class SpecialSettingData
    {
        /* SQL setting */
        public static string SqlAttachDbFilename { get; set; }
        public static string SqlDataSource { get; set; }

        /* Software Directory setting */
        public static string UIImageLoadPath { get; set; }
        public static string TMImageSavePath { get; set; }
        
    }
}
