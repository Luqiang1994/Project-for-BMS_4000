using BMSSoftware.Devices;
using BMSSoftware.Helpers;
using BMSSoftware.Models;
using BMSSoftware.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HandyControl.Controls;
using HandyControl.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using static BMSSoftware.Devices.zlgcan;

namespace BMSSoftware.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private System.Windows.Media.Color NG_state = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#8C2B2B");
        private System.Windows.Media.Color OK_state = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#329765");

        private SolidColorBrush bmsClolr;
        private SolidColorBrush canClolor;
        private bool ConnectState = false;
        private recvdatathread recv_data_thread_;
        private DateTime dt_300;
        private CancellationTokenSource SendHeartTokenSource = new CancellationTokenSource();
        private CancellationToken SendHeartToken;

        private CommunityParamsView communityParamsView = new CommunityParamsView();
        public static ProjectType CurProject = ProjectType.StoredEnergy;
        public MainViewModel()
        {
            bmsClolr = new SolidColorBrush(NG_state);
            canClolor = new SolidColorBrush(NG_state);
            //bmsClolr.Color = OK_state;
            
            BMSState = bmsClolr;
            CanState = canClolor;
            ChangePageCommand = new RelayCommand<string>(ChangePage);
            ReadDBCCommand = new RelayCommand(ReadDBCFile);
            OpenConnectPageCommand = new RelayCommand(OpenConnectPage);
            ReceiveDatasCommand = new RelayCommand(ReceiveDatas);
            ChooseProjectTypeCommand = new RelayCommand<string>(ChooseProjectType);
            CurrentPage = AllPages["StoredEnergy"];
            HardWareVer = "--";
            SoftWareVer = "--";
            ReceivtState = "接收数据";

            //读取配置文件
            ReadAllini.InitDictionary();
            ReadAllini.GetFormat(ReadAllini.SysCalibration_CanPath);
            WeakReferenceMessenger.Default.Register<string, string>(this, "ConnectStatus", ShowConnectStatus);
            WeakReferenceMessenger.Default.Register<string, string>(this, "0x300", HeartBeat);
            WeakReferenceMessenger.Default.Send<List<MessageStruct>, string>(ReadAllini.SysCalibration, "LoadSysCalb");
        }

        private void HeartBeat(object recipient, string message)
        {
            if (DateTime.Now.Subtract(dt_300).TotalMilliseconds > 1000)
            {
                bmsClolr.Color = NG_state;
                BMSState = bmsClolr;
                return;
            }
            bmsClolr.Color = OK_state;
            BMSState = bmsClolr;
        }

        private void ChooseProjectType(string? obj)
        {
            if (obj == "储能")
            {
                CurProject = ProjectType.StoredEnergy;
            }
        }

        private void ShowConnectStatus(object recipient, string message)
        {
            if (message == "OK")
            {
                ConnectState = true;
                canClolor.Color = OK_state;
                CanState = canClolor;
            }
            if (message == "NG")
            {
                ConnectState = false;
                canClolor.Color = NG_state;
                CanState = canClolor;
            }
            communityParamsView.Hide();
        }

        private void ReceiveDatas()
        {
            if (ReceivtState == "接收数据")
            {
                if (!ConnectState)
                {
                    //弹框提示连接失败,暂时用原生，需要抽空编写一个自定义消息弹窗
                    MessageBox.Error("Can设备连接失败，请检查通讯", "请注意");
                    return;
                }

                ReceivtState = "停止接收";
                if (CurProject == ProjectType.StoredEnergy)
                {
                    dt_300 = DateTime.Now;
                    //储能需要发送心跳信号
                    SendHeartTokenSource = new CancellationTokenSource();
                    SendHeartToken = SendHeartTokenSource.Token;
                    Task.Factory.StartNew(() =>
                    {
                        Send_200();
                        Thread.Sleep(1000);
                    });
                }
                

                if (null == recv_data_thread_)
                {
                    recv_data_thread_ = new recvdatathread();
                    recv_data_thread_.setChannelHandle(CommunityParamsViewModel.channel_handle_);
                    recv_data_thread_.setStart(true);
                    recv_data_thread_.setDeviceHandle(CommunityParamsViewModel.device_handle_);
                }
                else
                {
                    recv_data_thread_.setChannelHandle(CommunityParamsViewModel.channel_handle_);
                    recv_data_thread_.setDeviceHandle(CommunityParamsViewModel.device_handle_);
                }

            }
            else
            {
                ReceivtState = "接收数据";
                recv_data_thread_.setStart(false);
                SendHeartTokenSource.Cancel();
            }
        }

        private void Send_200()
        {
            ZCAN_Transmit_Data can_data = new ZCAN_Transmit_Data();
            can_data.frame.can_id = Method.MakeCanId(0x200, 0, 0, 0);
            can_data.frame.data = new byte[8];
            can_data.frame.data[0] = 0x55;
            can_data.frame.can_dlc = 8;
            can_data.transmit_type = 0;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(can_data));
            Marshal.StructureToPtr(can_data, ptr, true);
            uint result = Method.ZCAN_Transmit(CommunityParamsViewModel.channel_handle_, ptr, 1);
            Marshal.FreeHGlobal(ptr);
        }

        private void OpenConnectPage()
        {
            communityParamsView.ShowDialog();
        }

        private void ReadDBCFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择DBC文件";
            if (openFileDialog.ShowDialog() == true)
            {
                DBCParser.filePath = openFileDialog.FileName;
                DBCParser.Parse();

                WeakReferenceMessenger.Default.Send<List<DBCFileModel>>(DBCParser.Messages);
            }
        }

        private void ChangePage(string? pageName)
        {
            if (AllPages.ContainsKey(pageName))
            {
                CurrentPage = AllPages[pageName];
            }
        }

        private Brush _BMSState;
        public Brush BMSState
        {
            get => _BMSState;
            set => SetProperty(ref _BMSState, value);
        }

        private Brush _canState;
        public Brush CanState
        {
            get => _canState;
            set => SetProperty(ref _canState, value);
        }

        private string _hardWareVer;
        public string HardWareVer
        {
            get => _hardWareVer;
            set => SetProperty(ref _hardWareVer, value);
        }

        private string _softWareVer;
        public string SoftWareVer
        {
            get => _softWareVer;
            set => SetProperty(ref _softWareVer, value);
        }

        public Dictionary<string, Page> AllPages = new Dictionary<string, Page>()
        {
            {"StoredEnergy",new StoredEnergy() },

        };

        private object _currPage;
        public object CurrentPage
        {
            get => _currPage;
            set => SetProperty(ref _currPage, value);
        }
        private string receivtState;
        public string ReceivtState
        {
            get => receivtState;
            set => SetProperty(ref receivtState, value);
        }
        public RelayCommand<string> ChangePageCommand { get; set; }

        public RelayCommand ReadDBCCommand { get; set; }

        public RelayCommand OpenConnectPageCommand { get; set; }

        public RelayCommand ReceiveDatasCommand { get; set; }
        public RelayCommand<string> ChooseProjectTypeCommand { get; set; }

    }

    public enum ProjectType
    {
        StoredEnergy,
        MiniCar
    }

}
