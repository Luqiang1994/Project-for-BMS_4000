using BMSSoftware.Devices;
using BMSSoftware.Helpers;
using BMSSoftware.Models;
using BMSSoftware.OwnUserControls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static BMSSoftware.Devices.zlgcan;

namespace BMSSoftware.ViewModels
{
    public class StoredEnergyViewModel : ObservableObject
    {
        public CancellationTokenSource tokenSource_Eol = new CancellationTokenSource();//声明令牌
        CancellationToken cancellationToken_Eol;
        public CancellationTokenSource tokenSource_Control = new CancellationTokenSource();//声明令牌
        CancellationToken cancellationToken_Control;
        private int all_us_HSDFltSt = 0;


        public StoredEnergyViewModel()
        {
            PageIndex = 0;
            ChangeTabControlCommand = new RelayCommand<string>(ChangeTabControl);
            WeakReferenceMessenger.Default.Register<List<DBCFileModel>>(this, OnReceive);
            CalbIOCommand = new RelayCommand(DoCalbIO);
            SysParamCalbCommand = new RelayCommand<MessageStruct>(SysParamCalb);
            CancelSysParamCalbCommand = new RelayCommand(CancelSysParamCalb);
            EnterEolCommand = new RelayCommand(EnterEol);
            ChangeDeviceCommand = new RelayCommand<object>(ChangeDeviceState);
            EnterControlCommand = new RelayCommand(EnterControl);
            MainCards = new List<string>()
            {
                "更换主板",
                "换包"
            };
            EolStatus = "进入EOL模式";
            ControlModeStep = "进入强控模式";
            InitBalanceControls();
            InitTargetControlAddres();
            WeakReferenceMessenger.Default.Register<List<MessageStruct>, string>(this, "LoadSysCalb", GetSysCalbInfos);
            //簇控高边驱动/IO状态读取
            //HighDeviceIOStates = DBCParser.Messages.Find(m => m.ID == 0x30c).Signals;
        }

        private void GetSysCalbInfos(object recipient, List<MessageStruct> message)
        {
            foreach (var item in ReadAllini.SysCalibration)
            {
                item._TX = "-1";
            }
            SetInfos = ReadAllini.SysCalibration;
        }

        private void EnterControl()
        {
            if (ControlModeStep == "进入强控模式")
            {
                if (ControlPassWord == "0xFF2024" || ControlPassWord == "FF2024")
                {
                    ControlModeStep = "退出强控模式";
                    ZCAN_Transmit_Data can_data = new ZCAN_Transmit_Data();
                    can_data.frame.can_id = Method.MakeCanId(0x2FF, 0, 0, 0);
                    can_data.frame.data = new byte[8];
                    can_data.frame.data[0] = 0x02;
                    can_data.frame.data[2] = BitConverter.GetBytes(0xFF2024)[0];
                    can_data.frame.data[3] = BitConverter.GetBytes(0xFF2024)[1];
                    can_data.frame.data[4] = BitConverter.GetBytes(0xFF2024)[2];
                    can_data.frame.can_dlc = 8;
                    can_data.transmit_type = 0;

                    Task.Factory.StartNew(() =>
                    {
                        while (!cancellationToken_Control.IsCancellationRequested)
                        {
                            can_data.frame.data[1] = Convert.ToByte(CurTargetControlAddres1.Split(" ")[0], 16);
                            can_data.frame.data[5] = (byte)all_us_HSDFltSt;
                            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(can_data));
                            Marshal.StructureToPtr(can_data, ptr, true);
                            uint result = Method.ZCAN_Transmit(CommunityParamsViewModel.channel_handle_, ptr, 1);
                            Marshal.FreeHGlobal(ptr);
                            Thread.Sleep(1000);
                        }
                    }, cancellationToken_Control);
                }
                else
                    MessageBox.Error("密码输入错误");
            }
            else
            {
                ControlModeStep = "进入强控模式";
                tokenSource_Control.Cancel();
            }
                
        }


        /// <summary>
        /// 改变高边驱动状态
        /// </summary>
        /// <param name="obj"></param>
        private void ChangeDeviceState(object? obj)
        {
            object[] objects = (object[])obj;
            if ((bool)objects[0] == true)
            {
                all_us_HSDFltSt &= ~(1 << int.Parse(objects[1].ToString()));
            }
            else
                all_us_HSDFltSt |= (1 << int.Parse(objects[1].ToString()));
        }

        /// <summary>
        /// 执行进入EOL模式
        /// </summary>
        private void EnterEol()
        {
            if (EolStatus == "进入EOL模式")
            {
                if (EolPassWord == "0xEE2024" || EolPassWord == "EE2024")
                {
                    EolStatus = "退出EOL模式";
                    //开启EOL模式线程
                    ZCAN_Transmit_Data can_data = new ZCAN_Transmit_Data();
                    can_data.frame.can_id = Method.MakeCanId(0x2FF, 0, 0, 0);
                    can_data.frame.data = new byte[8];
                    can_data.frame.data[0] = 0x01;
                    can_data.frame.data[2] = BitConverter.GetBytes(0xEE2024)[0];
                    can_data.frame.data[3] = BitConverter.GetBytes(0xEE2024)[1];
                    can_data.frame.data[4] = BitConverter.GetBytes(0xEE2024)[2];
                    can_data.frame.can_dlc = 8;
                    can_data.transmit_type = 0;

                    cancellationToken_Eol = tokenSource_Eol.Token;
                    Task.Factory.StartNew(() =>
                    {
                        while (!cancellationToken_Eol.IsCancellationRequested)
                        {
                            //发送EOL指令
                            can_data.frame.data[1] = Convert.ToByte(CurTargetControlAddres.Split(" ")[0], 16);
                            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(can_data));
                            Marshal.StructureToPtr(can_data, ptr, true);
                            uint result = Method.ZCAN_Transmit(CommunityParamsViewModel.channel_handle_, ptr, 1);
                            Marshal.FreeHGlobal(ptr);
                            Thread.Sleep(1000);
                        }
                    }, cancellationToken_Eol);
                }
                else
                {
                    MessageBox.Error("密码输入错误");
                }
            }
            else
            {
                EolStatus = "进入EOL模式";
                tokenSource_Eol.Cancel();
            }

        }

        private void InitBalanceControls()
        {
            BalanceControlModes = new ObservableCollection<BalanceControlMode>();
            for (int i = 0; i < 70; i++)
            {
                BalanceControlModes.Add(new BalanceControlMode()
                {
                    Byte1 = "0B",
                    Byte2 = (i + 1).ToString("X"),
                    Byte3 = "00",
                    Byte4 = "00",
                    Byte5 = "00",
                    Byte6 = "00",
                    Byte7 = "00",
                    Byte8 = "00",
                }) ;
            }
        }

        private void InitTargetControlAddres()
        {
            TargetControlAddres = new List<string>();
            for (int i = 0; i < 8; i++)
            {
                string item = "";
                item = "0x" + (0x30+i).ToString("X") + "\"Cluster" + (i+1).ToString() + "\"";
                TargetControlAddres.Add(item);
            }
            for (int i = 0; i < 8; i++)
            {
                string item = "";
                item = "0x" + (0x40 + i).ToString("X") + "\"Cluster" + (i + 9).ToString() + "\"";
                TargetControlAddres.Add(item);
            }


        }

        /// <summary>
        /// 取消系统参数标定
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void CancelSysParamCalb()
        {
            ZCAN_Transmit_Data can_data = new ZCAN_Transmit_Data();
            can_data.frame.can_id = Method.MakeCanId(0x201, 0, 0, 0);
            can_data.frame.data = new byte[8];
            can_data.frame.can_dlc = 8;
            can_data.transmit_type = 0;
            can_data.frame.data[0] = Convert.ToByte(CurCalbSelectIndex + 1);
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(can_data));
            Marshal.StructureToPtr(can_data, ptr, true);
            uint result = Method.ZCAN_Transmit(CommunityParamsViewModel.channel_handle_, ptr, 1);
            Marshal.FreeHGlobal(ptr);
        }

        /// <summary>
        /// 标定系统参数，由于需要标定时间等特殊值，因此不能绑定_Value属性，改为绑定_Tx属性
        /// </summary>
        private void SysParamCalb(MessageStruct messageStruct)
        {
            Console.WriteLine($"---{CurCalbSelectIndex} --{SetInfos[CurCalbSelectIndex]._TX}");

            uint inputValue = 0;

            ZCAN_Transmit_Data can_data = new ZCAN_Transmit_Data();
            can_data.frame.can_id = Method.MakeCanId(0x201, 0, 0, 0);
            can_data.frame.data = new byte[8];
            can_data.frame.can_dlc = 8;
            can_data.transmit_type = 0;
            can_data.frame.data[1] = 0x80;

            //写入信息
            if (ReadAllini.SysCalibration[CurCalbSelectIndex]._byteOrder == "XX" || ReadAllini.SysCalibration[CurCalbSelectIndex]._byteOrder == "01")
            {
                inputValue = (uint)((float.Parse(SetInfos[CurCalbSelectIndex]._TX) - SetInfos[CurCalbSelectIndex]._Offset)
                / SetInfos[CurCalbSelectIndex]._Factor);
                can_data.frame.data[0] = Convert.ToByte(CurCalbSelectIndex + 1);
                can_data.frame.data[2] = Convert.ToByte(inputValue);
            }
            else if (ReadAllini.SysCalibration[CurCalbSelectIndex]._byteOrder == "01 00 00")
            {
                can_data.frame.data[0] = Convert.ToByte(CurCalbSelectIndex + 1);
                string[] paramValue = SetInfos[CurCalbSelectIndex]._TX.Split('.');
                can_data.frame.data[2] = (byte)(Convert.ToUInt16(paramValue[0]) >> 8);
                can_data.frame.data[3] = (byte)(Convert.ToUInt16(paramValue[0]));
                can_data.frame.data[4] = (byte)(Convert.ToUInt16(paramValue[1]) >> 8);
                can_data.frame.data[5] = (byte)(Convert.ToUInt16(paramValue[1]));
            }
            else if (ReadAllini.SysCalibration[CurCalbSelectIndex]._byteOrder == "XX XX")
            {
                inputValue = (uint)((float.Parse(SetInfos[CurCalbSelectIndex]._TX) - SetInfos[CurCalbSelectIndex]._Offset)
                / SetInfos[CurCalbSelectIndex]._Factor);
                can_data.frame.data[0] = Convert.ToByte(CurCalbSelectIndex + 1);
                can_data.frame.data[2] = (byte)(Convert.ToUInt16(inputValue) >> 8);
                can_data.frame.data[3] = (byte)(Convert.ToUInt16(inputValue));
            }
            else
            {
                if (ReadAllini.SysCalibration[CurCalbSelectIndex]._signalComment == "清楚NVM数据")
                {
                    can_data.frame.data[0] = 0x2D;
                    can_data.frame.data[2] = 0x5A;
                }
                else if (ReadAllini.SysCalibration[CurCalbSelectIndex]._signalComment == "RTC时间标定")
                {
                    can_data.frame.data[0] = 0x41;
                    string[] paramValue = SetInfos[CurCalbSelectIndex]._TX.Split('.');
                    can_data.frame.data[2] = (byte)(int.Parse(paramValue[0]) - 2000);//年
                    can_data.frame.data[3] = (byte)int.Parse(paramValue[1]);//月
                    can_data.frame.data[4] = (byte)int.Parse(paramValue[2]);//日
                    can_data.frame.data[5] = (byte)int.Parse(paramValue[3]);//时
                    can_data.frame.data[6] = (byte)int.Parse(paramValue[4]);//分
                    can_data.frame.data[7] = (byte)int.Parse(paramValue[5]);//秒
                }
            }

            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(can_data));
            Marshal.StructureToPtr(can_data, ptr, true);
            uint result = Method.ZCAN_Transmit(CommunityParamsViewModel.channel_handle_, ptr, 1);
            Marshal.FreeHGlobal(ptr);
        }

        private void DoCalbIO()
        {
            ZCAN_Transmit_Data can_data = new ZCAN_Transmit_Data();
            can_data.frame.can_id = Method.MakeCanId(0x201, 0, 0, 0);
            can_data.frame.data = new byte[8];
            can_data.frame.can_dlc = 8;
            can_data.transmit_type = 0;
            for (int i = 0; i < BalanceControlModes.Count; i++)
            {
                can_data.frame.data[0] = Convert.ToByte(BalanceControlModes[i].Byte1, 16);
                can_data.frame.data[1] = Convert.ToByte(BalanceControlModes[i].Byte2, 16);
                can_data.frame.data[2] = Convert.ToByte(BalanceControlModes[i].Byte3, 16);
                can_data.frame.data[3] = Convert.ToByte(BalanceControlModes[i].Byte4, 16);
                can_data.frame.data[4] = Convert.ToByte(BalanceControlModes[i].Byte5, 16);
                can_data.frame.data[5] = Convert.ToByte(BalanceControlModes[i].Byte6, 16);
                can_data.frame.data[6] = Convert.ToByte(BalanceControlModes[i].Byte7, 16);
                can_data.frame.data[7] = Convert.ToByte(BalanceControlModes[i].Byte8, 16);

                IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(can_data));
                Marshal.StructureToPtr(can_data, ptr, true);
                uint result = Method.ZCAN_Transmit(CommunityParamsViewModel.channel_handle_, ptr, 1);
                Marshal.FreeHGlobal(ptr);
            }
            can_data.frame.data[0] = 0x0B; can_data.frame.data[1] = 0x80; can_data.frame.data[2] = 0;
            IntPtr ptr1 = Marshal.AllocHGlobal(Marshal.SizeOf(can_data));
            Marshal.StructureToPtr(can_data, ptr1, true);
            uint result1 = Method.ZCAN_Transmit(CommunityParamsViewModel.channel_handle_, ptr1, 1);
            Marshal.FreeHGlobal(ptr1);
        }

        private void OnReceive(object recipient, List<DBCFileModel> message)
        {
            DBCFileModels = message;
        }

        private void ChangeTabControl(string index)
        {
            PageIndex = int.Parse(index);
        }

        private int _pageIndex;
        public int PageIndex
        {
            get { return _pageIndex; }
            set => SetProperty(ref _pageIndex, value);
        }

        private List<DBCFileModel> dBCFileModels;
        public List<DBCFileModel> DBCFileModels
        {
            get { return dBCFileModels; }
            set { SetProperty(ref dBCFileModels, value); }
        }

        private List<Signal> signalInfos;
        public List<Signal> SignalInfos
        {
            get { return signalInfos; }
            set { SetProperty(ref signalInfos, value); }
        }

        private int _curSeletcIndex;
        public int CurSeletcIndex
        {
            get => _curSeletcIndex;
            set
            {
                SetProperty(ref _curSeletcIndex, value);
                SignalInfos = DBCParser.Messages[_curSeletcIndex].Signals;
            }
        }


        private List<MessageStruct> _setInfos;
        public List<MessageStruct> SetInfos
        {
            get => _setInfos;
            set => SetProperty(ref _setInfos, value);
        }

        private int _curCalbSelectIndex;
        public int CurCalbSelectIndex
        {
            get => _curCalbSelectIndex;
            set
            {
                SetProperty(ref _curCalbSelectIndex, value);
            }
        }

        private ObservableCollection<BalanceControlMode> _balanceControlModes;
        public ObservableCollection<BalanceControlMode> BalanceControlModes
        {
            get => _balanceControlModes;
            set => SetProperty(ref _balanceControlModes, value);
        }

        private string _eolPassWord;
        public string EolPassWord
        {
            get => _eolPassWord;
            set => SetProperty(ref _eolPassWord, value);
        }

        private List<string> _targetControlAddres;
        public List<string> TargetControlAddres
        {
            get => _targetControlAddres;
            set => SetProperty(ref _targetControlAddres, value);
        }

        private string _curTargetControlAddres;
        public string CurTargetControlAddres
        {
            get => _curTargetControlAddres;
            set => SetProperty(ref _curTargetControlAddres, value);
        }

        private string _curTargetControlAddres1;
        public string CurTargetControlAddres1
        {
            get => _curTargetControlAddres1;
            set => SetProperty(ref _curTargetControlAddres1, value);
        }

        //簇控高边驱动/io状态
        private List<Signal> highDeviceIOStates;
        public List<Signal> HighDeviceIOStates
        {
            get => highDeviceIOStates;
            set => SetProperty(ref highDeviceIOStates, value);
        }

        private List<string> mainCards;
        public List<string> MainCards
        {
            get => mainCards;
            set => SetProperty(ref mainCards, value);
        }
        private string eolStatus;
        public string EolStatus
        {
            get => eolStatus;
            set => SetProperty(ref eolStatus, value);
        }

        private string controlModeStep;
        public string ControlModeStep
        {
            get => controlModeStep;
            set => SetProperty(ref controlModeStep, value);
        }
        private string controlPassWord;
        public string ControlPassWord
        {
            get => controlPassWord;
            set => SetProperty(ref controlPassWord, value);
        }
        public RelayCommand<string> ChangeTabControlCommand { get; set; }

        public RelayCommand CalbIOCommand { get; set; }

        public RelayCommand<MessageStruct> SysParamCalbCommand { get; set; }

        public RelayCommand CancelSysParamCalbCommand { get; set; }

        public RelayCommand EnterEolCommand { get;set; }

        public RelayCommand<object> ChangeDeviceCommand { get; set; }
        public RelayCommand EnterControlCommand { get; set; }
    }
}
