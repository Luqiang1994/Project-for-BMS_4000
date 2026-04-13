using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSSoftware.Devices
{
    /// <summary>
    /// 单体采集配置
    /// </summary>
    public class SingleVoltTempSet
    {
        public static int AFENums { get; set; }
        public static int VoltChanels { get; set; }
        public static int TempChanels { get; set; }
        public static int EachFormVolts { get; set; }
        public static int EachFormTemps { get; set; }
        public static int MaxVoltIndex { get; set; }
        public static int MaxTempFormIndex { get; set; }
        public static int VoltEffectivenessNums { get; set; }
        public static int TempEffectivenessNums { get; set; }
    }
}
