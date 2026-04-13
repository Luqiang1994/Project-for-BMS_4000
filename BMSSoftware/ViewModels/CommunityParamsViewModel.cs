using BMSSoftware.Devices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static BMSSoftware.Devices.zlgcan;

namespace BMSSoftware.ViewModels
{
    public class CommunityParamsViewModel : ObservableObject
    {
        private DeviceInfo[] kDeviceType =
        {
            new DeviceInfo(Define.ZCAN_USBCAN1, 1),
            new DeviceInfo(Define.ZCAN_USBCAN2, 2),
            new DeviceInfo(Define.ZCAN_PCI9820I,2),
            new DeviceInfo(Define.ZCAN_USBCAN_E_U, 1),
            new DeviceInfo(Define.ZCAN_USBCAN_2E_U, 2),
            new DeviceInfo(Define.ZCAN_USBCAN_4E_U, 4),
            new DeviceInfo(Define.ZCAN_PCIECANFD_100U, 1),
            new DeviceInfo(Define.ZCAN_PCIECANFD_200U, 2),
            new DeviceInfo(Define.ZCAN_PCIECANFD_200U_EX,2),
            new DeviceInfo(Define.ZCAN_PCIECANFD_400U, 4),
            new DeviceInfo(Define.ZCAN_USBCANFD_200U, 2),
            new DeviceInfo(Define.ZCAN_USBCANFD_400U,4),
            new DeviceInfo(Define.ZCAN_USBCANFD_800U, 8),
            new DeviceInfo(Define.ZCAN_USBCANFD_100U, 1),
            new DeviceInfo(Define.ZCAN_USBCANFD_MINI, 1),
            new DeviceInfo(Define.ZCAN_CANETTCP, 1),
            new DeviceInfo(Define.ZCAN_CANETUDP, 1),
            new DeviceInfo(Define.ZCAN_CANWIFI_TCP, 1),
            new DeviceInfo(Define.ZCAN_CANFDNET_200U_TCP, 2),
            new DeviceInfo(Define.ZCAN_CANFDNET_200U_UDP, 2),
            new DeviceInfo(Define.ZCAN_CANFDNET_400U_TCP, 4),
            new DeviceInfo(Define.ZCAN_CANFDNET_400U_UDP, 4),
            new DeviceInfo(Define.ZCAN_CANFDNET_800U_TCP, 8),
            new DeviceInfo(Define.ZCAN_CANFDNET_800U_UDP, 8),
            new DeviceInfo(Define.ZCAN_CLOUD, 1)
        };
        private uint[] kBaudrate =
        {
            1000000,//1000kbps
            800000,//800kbps
            500000,//500kbps
            250000,//250kbps
            125000,//125kbps
            100000,//100kbps
            50000,//50kbps
            20000,//20kbps
            10000,//10kbps
            5000 //5kbps
        };
        private uint[] kUSBCANFDAbit =
        {
            1000000, // 1Mbps
            800000, // 800kbps
            500000, // 500kbps
            250000, // 250kbps
            125000, // 125kbps
            100000, // 100kbps
            50000, // 50kbps
            800000, // 800kbps
        };
        private uint[] kUSBCANFDDbit =
        {
            5000000, // 5Mbps
            4000000, // 4Mbps
            2000000, // 2Mbps
            1000000, // 1Mbps
            800000, // 800kbps
            500000, // 500kbps
            250000, // 250kbps
            125000, // 125kbps
            100000, // 100kbps
        };
        private uint[] kPCIECANFDAbit =
       {
            1000000, // 1Mbps
            800000, // 800kbps
            500000, // 500kbps
            250000, // 250kbps
        };
        private uint[] kPCIECANFDDbit =
       {
            8000000, // 8Mbps
            4000000, // 4Mbps
            2000000, // 2Mbps
        };


        //设置CANFD标准
        private bool setCANFDStandard(int canfd_standard)
        {
            string path = channel_index_ + "/canfd_standard";
            string value = canfd_standard.ToString();
            //char* pathCh = (char*)System.Runtime.InteropServices.Marshal.StringToHGlobalAnsi(path).ToPointer();
            //char* valueCh = (char*)System.Runtime.InteropServices.Marshal.StringToHGlobalAnsi(value).ToPointer();
            uint ret = Method.ZCAN_SetValue(device_handle_, path, Encoding.ASCII.GetBytes(value));
            return (ret == 1);
        }
        //设置波特率
        private bool setBaudrate(UInt32 baud)
        {
            string path = channel_index_ + "/baud_rate";
            string value = baud.ToString();
            //char* pathCh = (char*)System.Runtime.InteropServices.Marshal.StringToHGlobalAnsi(path).ToPointer();
            //char* valueCh = (char*)System.Runtime.InteropServices.Marshal.StringToHGlobalAnsi(value).ToPointer();
            return 1 == Method.ZCAN_SetValue(device_handle_, path, Encoding.ASCII.GetBytes(value));
        }

        private bool setFdBaudrate(UInt32 abaud, UInt32 dbaud)
        {
            string path = channel_index_ + "/canfd_abit_baud_rate";
            string value = abaud.ToString();
            if (1 != Method.ZCAN_SetValue(device_handle_, path, Encoding.ASCII.GetBytes(value)))
            {
                return false;
            }
            path = channel_index_ + "/canfd_dbit_baud_rate";
            value = dbaud.ToString();
            if (1 != Method.ZCAN_SetValue(device_handle_, path, Encoding.ASCII.GetBytes(value)))
            {
                return false;
            }
            return true;
        }
        //设置开启合并接收
        private bool setDataMerge()
        {
            byte merge_ = 0;
            //if (checkBox_merge.Checked) merge_ = 1;
            string path = channel_index_ + "/set_device_recv_merge";
            string value = merge_.ToString();
            return 1 == Method.ZCAN_SetValue(device_handle_, path, Encoding.ASCII.GetBytes(value));
        }

        private bool setFilter()
        {
            string path = channel_index_ + "/filter_clear";//清除滤波
            string value = "0";

            if (0 == Method.ZCAN_SetValue(device_handle_, path, Encoding.ASCII.GetBytes(value)))
            {
                return false;
            }

            path = channel_index_ + "/filter_mode";
            value = Standard2.ToString();

            if (value == "2")
            {
                return true;
            }
            if (0 == Method.ZCAN_SetValue(device_handle_, path, Encoding.ASCII.GetBytes(value)))
            {
                return false;
            }

            path = channel_index_ + "/filter_start";
            value = "0";
            if (0 == Method.ZCAN_SetValue(device_handle_, path, Encoding.ASCII.GetBytes(value)))
            {
                return false;
            }

            path = channel_index_ + "/filter_end";
            value = "FFFFFFFF";
            if (0 == Method.ZCAN_SetValue(device_handle_, path, Encoding.ASCII.GetBytes(value)))
            {
                return false;
            }

            path = channel_index_ + "/filter_ack";//滤波生效
            value = "0";
            if (0 == Method.ZCAN_SetValue(device_handle_, path, Encoding.ASCII.GetBytes(value)))
            {
                return false;
            }

            //如果要设置多条滤波，在清除滤波和滤波生效之间设置多条滤波即可
            return true;
        }


        public static IntPtr device_handle_;
        public static IntPtr channel_handle_;
        private ZlgCanDevice zlgCanDevice = new ZlgCanDevice();
        private int channel_index_;
        private uint device_type_index_;
        const int NULL = 0;

        public CommunityParamsViewModel()
        {
            DeviceType = new ObservableCollection<string>();
            BoudRate = new ObservableCollection<string>();
            DeviceIndex = new ObservableCollection<string>();
            CanIndex = new ObservableCollection<string>();
            ABITInfos = new ObservableCollection<string>();
            DBITInfos = new ObservableCollection<string>();
            for (int i = 0; i < zlgCanDevice.CardTypeList.Count; i++)
            {
                DeviceType.Add(zlgCanDevice.CardTypeList[i].TyPeName);
            }
            #region 创新can盒
            DeviceType.Clear();
            DeviceType.Add("DEV_USBCAN");DeviceType.Add("DEV_USBCAN2");
            #endregion
            for (int i = 0; i < zlgCanDevice.BoudRateList.Count; i++)
            {
                BoudRate.Add(zlgCanDevice.BoudRateList[i].RateName);
            }
            for (int i = 0; i < 4; i++)
            {
                DeviceIndex.Add(i.ToString());
            }
            DeviceTypeChangedCommand = new RelayCommand<ComboBox>(DeviceTypeChanged);
            ConnectCanCommand = new RelayCommand<string>(ConnectCan);
            CanIndex.Add("0");
            CanIndex.Add("1");
        }


        private void SendConnectStatus(string status)
        {
            WeakReferenceMessenger.Default.Send<string,string>(status,"ConnectStatus");
        }


        private void ConnectCan(string step)
        {
            if (step == "连接")
            {
                device_handle_ = Method.ZCAN_OpenDevice(kDeviceType[device_type_index_].device_type, (uint)device_index_, 0);
                if (NULL == (int)device_handle_)
                {
                    //连接失败处理逻辑
                    SendConnectStatus("NG");
                    return;
                }
                if (kDeviceType[device_type_index_].device_type == 41)
                {
                    string path = "0/get_cn/1";
                    byte[] sn_ = new byte[30];
                    IntPtr sn = Method.ZCAN_GetValue(device_handle_, path);
                    Marshal.Copy(sn, sn_, 0, 30);
                }

                //连接成功处理逻辑
                uint type = kDeviceType[device_type_index_].device_type;
                bool netDevice = type == Define.ZCAN_CANETTCP || type == Define.ZCAN_CANETUDP || type == Define.ZCAN_CANWIFI_TCP ||
                    type == Define.ZCAN_CANFDNET_400U_TCP || type == Define.ZCAN_CANFDNET_400U_UDP ||
                    type == Define.ZCAN_CANFDNET_200U_TCP || type == Define.ZCAN_CANFDNET_200U_UDP || type == Define.ZCAN_CANFDNET_800U_TCP ||
                    type == Define.ZCAN_CANFDNET_800U_UDP;
                bool canfdnetDevice = type == Define.ZCAN_CANFDNET_400U_TCP || type == Define.ZCAN_CANFDNET_400U_UDP ||
                    type == Define.ZCAN_CANFDNET_200U_TCP || type == Define.ZCAN_CANFDNET_200U_UDP || type == Define.ZCAN_CANFDNET_800U_TCP ||
                    type == Define.ZCAN_CANFDNET_800U_UDP;
                bool pcieCanfd = type == Define.ZCAN_PCIECANFD_100U ||
                    type == Define.ZCAN_PCIECANFD_200U ||
                    type == Define.ZCAN_PCIECANFD_400U ||
                    type == Define.ZCAN_PCIECANFD_200U_EX;
                bool usbCanfd = type == Define.ZCAN_USBCANFD_100U ||
                    type == Define.ZCAN_USBCANFD_200U ||
                    type == Define.ZCAN_USBCANFD_400U ||
                    type == Define.ZCAN_USBCANFD_MINI ||
                    type == Define.ZCAN_USBCANFD_800U;
                bool canfdDevice = usbCanfd || pcieCanfd;
                if (usbCanfd && (type != Define.ZCAN_USBCANFD_800U))
                {
                    if (!setCANFDStandard(Standard))//设置CANFD标准
                    {
                        //设置失败逻辑，即通讯失败，即连接失败
                        SendConnectStatus("NG");
                        return;
                    }
                }
                if (!canfdDevice)
                {
                    if (!setBaudrate(kBaudrate[CurBoudRate]))
                    {
                        //设置失败逻辑，即通讯失败，即连接失败
                        SendConnectStatus("NG");
                        return;
                    }
                }
                else
                {
                    bool result = true;
                    if (usbCanfd)
                    {
                        if (type == Define.ZCAN_USBCANFD_200U || type == Define.ZCAN_USBCANFD_400U || type == Define.ZCAN_USBCANFD_800U)
                        {
                            if (!setDataMerge())
                            {
                                //指令发送失败逻辑，即通讯失败，即连接失败
                                SendConnectStatus("NG");
                                return;
                            }
                        }
                        result = setFdBaudrate(kUSBCANFDAbit[CurABIT], kUSBCANFDDbit[CurDBIT]);
                    }
                    else if (pcieCanfd)
                    {
                        result = setFdBaudrate(kPCIECANFDAbit[CurABIT], kPCIECANFDDbit[CurDBIT]);
                        if (type == Define.ZCAN_PCIECANFD_400U || type == Define.ZCAN_PCIECANFD_200U_EX)
                        {
                            if (!setDataMerge())
                            {
                                //指令发送失败逻辑，即通讯失败，即连接失败
                                SendConnectStatus("NG");
                                return;
                            }
                        }
                    }
                    if (!result)
                    {
                        //指令发送失败逻辑，即通讯失败，即连接失败
                        SendConnectStatus("NG");
                        return;
                    }
                }
                ZCAN_CHANNEL_INIT_CONFIG config_ = new ZCAN_CHANNEL_INIT_CONFIG();
                if (usbCanfd)
                {
                    config_.can_type = Define.TYPE_CANFD;
                    config_.canfd.mode = 0;
                }
                else if (pcieCanfd)
                {
                    config_.can_type = Define.TYPE_CANFD;
                    config_.canfd.filter = 0;
                    config_.canfd.acc_code = 0;
                    config_.canfd.acc_mask = 0xFFFFFFFF;
                    config_.canfd.mode = 0;
                }
                else
                {
                    config_.can_type = Define.TYPE_CAN;
                    config_.can.filter = 0;
                    config_.can.acc_code = 0;
                    config_.can.acc_mask = 0xFFFFFFFF;
                    config_.can.mode = 0;
                }
                IntPtr pConfig = Marshal.AllocHGlobal(Marshal.SizeOf(config_));
                Marshal.StructureToPtr(config_, pConfig, true);
                channel_handle_ = Method.ZCAN_InitCAN(device_handle_, (uint)channel_index_, pConfig);
                Marshal.FreeHGlobal(pConfig);
                if (NULL == (int)channel_handle_)
                {
                    //连接失败逻辑处理
                    SendConnectStatus("NG");
                    return;
                }
                if (Method.ZCAN_StartCAN(channel_handle_) != Define.STATUS_OK)
                {
                    //连接失败逻辑处理
                    SendConnectStatus("NG");
                    return;
                }
                type = kDeviceType[device_type_index_].device_type;   //增加CANFDNET滤波,CANFDNET滤波必须在startcan之后进行。
                canfdnetDevice = type == Define.ZCAN_CANFDNET_400U_TCP || type == Define.ZCAN_CANFDNET_400U_UDP ||
                                 type == Define.ZCAN_CANFDNET_200U_TCP || type == Define.ZCAN_CANFDNET_200U_UDP || type == Define.ZCAN_CANFDNET_800U_TCP ||
                                 type == Define.ZCAN_CANFDNET_800U_UDP;
                if (canfdnetDevice && !setFilter())
                {
                    //连接失败逻辑处理
                    SendConnectStatus("NG");
                    return;
                }

                //连接成功逻辑
                SendConnectStatus("OK");
            }
            else
            {
                Method.ZCAN_CloseDevice(device_handle_);
                //断开连接后操作
                SendConnectStatus("NG");
            }
        }

        private void DeviceTypeChanged(ComboBox? obj)
        {
            CanIndex.Clear();
            setChannelCombobox(0, (int)kDeviceType[obj.SelectedIndex].channel_count, 0);
            uint type = kDeviceType[obj.SelectedIndex].device_type;
            bool pcieCanfd = type == Define.ZCAN_PCIECANFD_100U ||
                type == Define.ZCAN_PCIECANFD_200U ||
                type == Define.ZCAN_PCIECANFD_400U;
            bool usbCanfd = type == Define.ZCAN_USBCANFD_100U ||
                type == Define.ZCAN_USBCANFD_200U ||
                type == Define.ZCAN_USBCANFD_400U ||
                type == Define.ZCAN_USBCANFD_MINI ||
                type == Define.ZCAN_USBCANFD_800U;

            if (pcieCanfd)
            {
                ABITInfos.Clear();
                ABITInfos.Add("1Mbps 80%");
                ABITInfos.Add("800kbps 80%");
                ABITInfos.Add("500kbps 80%");
                ABITInfos.Add("250kbps 80%");
                DBITInfos.Clear();
                DBITInfos.Add("8Mbps 75%");
                DBITInfos.Add("4Mbps 75%");
                DBITInfos.Add("2Mbps 75%");

            }
            else if (usbCanfd)
            {
                ABITInfos.Clear();
                ABITInfos.Add("1Mbps 80%");
                ABITInfos.Add("800kbps 80%");
                ABITInfos.Add("500kbps 80%");
                ABITInfos.Add("250kbps 80%");
                ABITInfos.Add("125kbps 80%");
                ABITInfos.Add("100kbps 80%");
                ABITInfos.Add("50kbps 80%");
                DBITInfos.Clear();
                DBITInfos.Add("5Mbps 75%");
                DBITInfos.Add("4Mbps 75%");
                DBITInfos.Add("2Mbps 75%");
                DBITInfos.Add("1Mbps 75%");
                DBITInfos.Add("800kbps 75%");
                DBITInfos.Add("500kbps 75%");
                DBITInfos.Add("250kbps 75%");
                DBITInfos.Add("125kbps 75%");
                DBITInfos.Add("100kbps 75%");
            }
            EnableSet(obj.SelectedIndex);
            device_type_index_ = (uint)obj.SelectedIndex;
        }

        private void EnableSet(int index)
        {
            uint type = kDeviceType[index].device_type;
            bool cloudDevice = type == Define.ZCAN_CLOUD;
            bool netDevice = type == Define.ZCAN_CANETTCP || type == Define.ZCAN_CANETUDP || type == Define.ZCAN_CANWIFI_TCP ||
                type == Define.ZCAN_CANFDNET_400U_TCP || type == Define.ZCAN_CANFDNET_400U_UDP ||
                type == Define.ZCAN_CANFDNET_200U_TCP || type == Define.ZCAN_CANFDNET_200U_UDP || type == Define.ZCAN_CANFDNET_800U_TCP ||
                type == Define.ZCAN_CANFDNET_800U_UDP;
            bool canfdnetDevice = type == Define.ZCAN_CANFDNET_400U_TCP || type == Define.ZCAN_CANFDNET_400U_UDP ||
                type == Define.ZCAN_CANFDNET_800U_TCP || type == Define.ZCAN_CANFDNET_800U_UDP ||
                type == Define.ZCAN_CANFDNET_200U_TCP || type == Define.ZCAN_CANFDNET_200U_UDP;
            bool pcieCanfd = type == Define.ZCAN_PCIECANFD_100U ||
                type == Define.ZCAN_PCIECANFD_200U ||
                type == Define.ZCAN_PCIECANFD_400U ||
                type == Define.ZCAN_PCIECANFD_200U_EX;
            bool usbCanfd = type == Define.ZCAN_USBCANFD_100U ||
                type == Define.ZCAN_USBCANFD_200U ||
                type == Define.ZCAN_USBCANFD_400U ||
                type == Define.ZCAN_USBCANFD_MINI ||
                type == Define.ZCAN_USBCANFD_800U;
            bool canfdDevice = usbCanfd || pcieCanfd;
            bool usbCAN = type == Define.ZCAN_USBCAN1 || type == Define.ZCAN_USBCAN2 || type == Define.ZCAN_PCI9820I || type == Define.ZCAN_USBCAN_4E_U;
            bool accFilter = usbCAN || pcieCanfd;
            bool lin_device = type == Define.ZCAN_USBCANFD_200U || type == Define.ZCAN_USBCANFD_400U;


            CanFDEnable = canfdDevice;
            CanFDEnable = canfdDevice;
            CanFDEnable = canfdDevice;
            BaudrateEnable = !canfdDevice && !cloudDevice && !netDevice;
            ResistanceEnable = usbCanfd;
            StandardEnable = !cloudDevice && !netDevice && !accFilter || canfdnetDevice;
            DeviceIndexEnable = !cloudDevice && !netDevice;
            

        }

        private void setChannelCombobox(int start, int end, int current)
        {
            //CanIndex = new ObservableCollection<string>();
            for (int i = start; i < end; ++i)
            {
                CanIndex.Add(i.ToString());
            }
        }
        #region 属性/命令绑定
        private ObservableCollection<string> deviceType;
        public ObservableCollection<string> DeviceType
        {
            get => deviceType;
            set => SetProperty(ref deviceType, value);
        }

        private ObservableCollection<string> boudRate;
        public ObservableCollection<string> BoudRate
        {
            get => boudRate;
            set => SetProperty(ref boudRate, value);
        }

        private ObservableCollection<string> deviceIndex;
        public ObservableCollection<string> DeviceIndex
        {
            get => deviceIndex;
            set => SetProperty(ref deviceIndex, value);
        }

        private ObservableCollection<string> aBITInfos;
        public ObservableCollection<string> ABITInfos
        {
            get => aBITInfos;
            set => SetProperty(ref aBITInfos, value);
        }

        private ObservableCollection<string> dBITInfos;
        public ObservableCollection<string> DBITInfos
        {
            get => dBITInfos;
            set => SetProperty(ref dBITInfos, value);
        }
        private ObservableCollection<string> canIndex;
        public ObservableCollection<string> CanIndex
        {
            get => canIndex;
            set => SetProperty(ref canIndex, value);
        }
        private int channel_Index;
        public int Channel_Index
        {
            get => channel_Index;
            set => SetProperty(ref channel_Index, value);
        }

        private bool canFDEnable;
        public bool CanFDEnable
        {
            get => canFDEnable;
            set => SetProperty(ref canFDEnable, value);
        }

        private bool baudrateEnable;
        public bool BaudrateEnable
        {
            get => baudrateEnable;
            set => SetProperty(ref baudrateEnable, value);
        }
        private bool resistanceEnable;
        public bool ResistanceEnable
        {
            get => resistanceEnable;
            set => SetProperty(ref resistanceEnable, value);
        }

        private bool standardEnable;
        public bool StandardEnable
        {
            get => standardEnable;
            set => SetProperty(ref standardEnable, value);
        }
        private bool deviceIndexEnable;
        public bool DeviceIndexEnable
        {
            get => deviceIndexEnable;
            set => SetProperty(ref deviceIndexEnable, value);
        }

        private bool resistanceIsChecked;
        public bool ResistanceIsChecked
        {
            get => resistanceIsChecked;
            set => SetProperty(ref resistanceIsChecked, value);
        }
        private int _device_index_;
        public int device_index_
        {
            get => _device_index_;
            set => SetProperty(ref _device_index_, value);
        }

        private int standard;
        public int Standard
        {
            get => standard;
            set => SetProperty(ref standard, value);
        }

        private int standard2;
        public int Standard2
        {
            get => standard2;
            set => SetProperty(ref standard2, value);
        }
        private int curBoudRate;
        public int CurBoudRate
        {
            get => curBoudRate;
            set => SetProperty(ref curBoudRate, value);
        }
        private int curABIT;
        public int CurABIT
        {
            get => curABIT;
            set => SetProperty(ref curABIT, value);
        }
        private int curDBIT;
        public int CurDBIT
        {
            get => curDBIT;
            set => SetProperty(ref curDBIT, value);
        }

        #region 创新盒子初始化属性
        private string accCode;
        public string AccCode
        {
            get => accCode;
            set => SetProperty(ref accCode, value);
        }
        private string accMask;
        public string AccMask
        {
            get => accMask;
            set => SetProperty(ref accMask, value);
        }
        private string timer0;
        public string Timer0
        {
            get => timer0;
            set => SetProperty(ref timer0, value);
        }
        private string timer1;
        public string Timer1
        {
            get => timer1;
            set => SetProperty(ref timer1, value);
        }
        #endregion

        public RelayCommand<ComboBox> DeviceTypeChangedCommand { get; set; }

        public RelayCommand<string> ConnectCanCommand { get; set; }
        #endregion

    }
}
