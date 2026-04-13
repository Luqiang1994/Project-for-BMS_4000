using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSSoftware.Devices
{
    #region CAN设备信息类
    [Serializable]
    public class CardType
    {
        public string TyPeName { get; set; }
        public uint TypeId { get; set; }
    }
    #endregion

    #region 波特率信息类
    [Serializable]
    public class BoudRoute
    {
        public string RateName { get; set; }
        public UInt32 RateValue { get; set; }
    }
    #endregion

    #region 波特率定时器

    public class BoudRouteTiming
    {
        public string BoudRoute { get; set; }
        public Byte Timing0 { get; set; }
        public Byte Timing1 { get; set; }
    }
    #endregion

    [Serializable]
    public class ZlgCanDevice
    {
        #region CAN配置参数
        //设备类型
        public uint DevType { get; set; }
        //设备索引
        public uint DevID { get; set; }
        //CAN通道
        public uint CANID { get; set; }
        public uint BoudRate { get; set; }
        public uint SendTimeout { get; set; }
        #endregion

        #region CAN初始化参数
        public uint AccCode { get; set; }
        public uint AccMask { get; set; }
        public uint Reserved { get; set; }
        public byte Filter { get; set; }
        public byte Timing0 { get; set; }
        public byte Timing1 { get; set; }
        public byte Mode { get; set; }
        #endregion

        #region 设备、波特率及定时器信息列表
        public List<CardType> CardTypeList { get; set; }
        public List<BoudRoute> BoudRateList { get; set; }
        public List<BoudRouteTiming> BrRouteTimingsList { get; set; }
        #endregion

        public ZlgCanDevice(uint devType, uint devID, uint boudRate, uint sendTimeout, uint canID = 0)
        {
            this.DevType = devType;
            this.DevID = devID;
            this.CANID = canID;
            this.BoudRate = boudRate;
            this.SendTimeout = sendTimeout;
        }

        public ZlgCanDevice()
        {
            //初始化设备列表
            this.CardTypeList = new List<CardType>();
            this.CardTypeList.Add(new CardType { TyPeName = "USBCAN-I/I+", TypeId = 3 });
            this.CardTypeList.Add(new CardType { TyPeName = "USBCAN-II/II+", TypeId = 4 });
            this.CardTypeList.Add(new CardType { TyPeName = "PCI-9820I", TypeId = 16 });
            this.CardTypeList.Add(new CardType { TyPeName = "USBCAN_E_U", TypeId = 20 });
            this.CardTypeList.Add(new CardType { TyPeName = "USBCAN_2E_U", TypeId = 21 });
            this.CardTypeList.Add(new CardType { TyPeName = "USBCAN-4E-U", TypeId = 31 });
            this.CardTypeList.Add(new CardType { TyPeName = "PCIE-CANFD-100U", TypeId = 38 });
            this.CardTypeList.Add(new CardType { TyPeName = "PCIE-CANFD-200U", TypeId = 39 });
            this.CardTypeList.Add(new CardType { TyPeName = "PCIE-CANFD-200U-EX", TypeId = 37 });
            this.CardTypeList.Add(new CardType { TyPeName = "PCIE-CANFD-400U", TypeId = 40 });
            this.CardTypeList.Add(new CardType { TyPeName = "USBCANFD-200U", TypeId = 41 });
            this.CardTypeList.Add(new CardType { TyPeName = "USBCANFD-400U", TypeId = 76 });
            this.CardTypeList.Add(new CardType { TyPeName = "USBCANFD-800U", TypeId = 59 });
            this.CardTypeList.Add(new CardType { TyPeName = "USBCANFD-100U", TypeId = 42 });
            this.CardTypeList.Add(new CardType { TyPeName = "USBCANFD-MINI", TypeId = 43 });
            this.CardTypeList.Add(new CardType { TyPeName = "PCI-9810I", TypeId = 2 });
            this.CardTypeList.Add(new CardType { TyPeName = "PCI-9820", TypeId = 5 });
            this.CardTypeList.Add(new CardType { TyPeName = "PCI-9840I", TypeId = 14 });
            this.CardTypeList.Add(new CardType { TyPeName = "PC104-CAN2I", TypeId = 15 });
            this.CardTypeList.Add(new CardType { TyPeName = "PCI-5010-U", TypeId = 19 });
            this.CardTypeList.Add(new CardType { TyPeName = "PCI-5020-U", TypeId = 22 });
            this.CardTypeList.Add(new CardType { TyPeName = "PCIe-9221", TypeId = 24 });
            this.CardTypeList.Add(new CardType { TyPeName = "PCIe-9120I", TypeId = 27 });
            this.CardTypeList.Add(new CardType { TyPeName = "PCIe-9110I", TypeId = 28 });
            this.CardTypeList.Add(new CardType { TyPeName = "PCIe-9140I", TypeId = 29 });
            this.CardTypeList.Add(new CardType { TyPeName = "USBCAN-8E-U", TypeId = 34 });



            //初始化波特率表
            this.BoudRateList = new List<BoudRoute>();
            this.BoudRateList.Add(new BoudRoute { RateName = "1000Kbps", RateValue = 0x060003 });
            this.BoudRateList.Add(new BoudRoute { RateName = "800Kbps", RateValue = 0x060004 });
            this.BoudRateList.Add(new BoudRoute { RateName = "500Kbps", RateValue = 0x060007 });
            this.BoudRateList.Add(new BoudRoute { RateName = "250Kbps", RateValue = 0x1C0008 });
            this.BoudRateList.Add(new BoudRoute { RateName = "125Kbps", RateValue = 0x1C0011 });
            this.BoudRateList.Add(new BoudRoute { RateName = "100Kbps", RateValue = 0x160023 });
            this.BoudRateList.Add(new BoudRoute { RateName = "50Kbps", RateValue = 0x1C002C });
            this.BoudRateList.Add(new BoudRoute { RateName = "20Kbps", RateValue = 0x1600B3 });
            this.BoudRateList.Add(new BoudRoute { RateName = "10Kbps", RateValue = 0x1C00E0 });
            this.BoudRateList.Add(new BoudRoute { RateName = "5Kbps", RateValue = 0x1C01C1 });

            //波特率定时器
            this.BrRouteTimingsList = new List<BoudRouteTiming>();
            this.BrRouteTimingsList.Add(new BoudRouteTiming { BoudRoute = "1000Kbps", Timing0 = 0x00, Timing1 = 0x14 });
            this.BrRouteTimingsList.Add(new BoudRouteTiming { BoudRoute = "800Kbps", Timing0 = 0x00, Timing1 = 0x16 });
            this.BrRouteTimingsList.Add(new BoudRouteTiming { BoudRoute = "500Kbps", Timing0 = 0x00, Timing1 = 0x1C });
            this.BrRouteTimingsList.Add(new BoudRouteTiming { BoudRoute = "250Kbps", Timing0 = 0x01, Timing1 = 0x1C });
            this.BrRouteTimingsList.Add(new BoudRouteTiming { BoudRoute = "125Kbps", Timing0 = 0x03, Timing1 = 0x1C });
            this.BrRouteTimingsList.Add(new BoudRouteTiming { BoudRoute = "100Kbps", Timing0 = 0x04, Timing1 = 0x1C });
            this.BrRouteTimingsList.Add(new BoudRouteTiming { BoudRoute = "50Kbps", Timing0 = 0x09, Timing1 = 0x1C });
            this.BrRouteTimingsList.Add(new BoudRouteTiming { BoudRoute = "20Kbps", Timing0 = 0x18, Timing1 = 0x1C });
            this.BrRouteTimingsList.Add(new BoudRouteTiming { BoudRoute = "10Kbps", Timing0 = 0x31, Timing1 = 0x1C });
            this.BrRouteTimingsList.Add(new BoudRouteTiming { BoudRoute = "5Kbps", Timing0 = 0xBF, Timing1 = 0xFF });
        }
    }
}
