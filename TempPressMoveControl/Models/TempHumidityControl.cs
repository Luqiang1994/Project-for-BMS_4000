using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempPressMoveControl.Models
{
    /// <summary>
    /// 温湿度控制类
    /// </summary>
    public class TempHumidityControl
    {
        #region 属性
        public float CurTemp { get; set; }
        public float CurHumidity { get; set; }
        #endregion
    }
}
