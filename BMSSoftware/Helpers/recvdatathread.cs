using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Markup;
using BMSSoftware.Devices;
using BMSSoftware.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using static BMSSoftware.Devices.zlgcan;

namespace BMSSoftware.Helpers
{
    public class recvdatathread
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void RecvCANDataEventHandler(zlgcan.ZCAN_Receive_Data[] data, uint len);//CAN数据接收事件委托

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void RecvFDDataEventHandler(zlgcan.ZCAN_ReceiveFD_Data[] data, uint len);//CANFD数据接收事件委托

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void RecvDataEventHandler(zlgcan.ZCANDataObj[] data, uint len);//CANFD数据接收事件委托

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void RecvLINDataEventHandler(zlgcan.ZCAN_LIN_MSG[] data, uint len);//CANFD数据接收事件委托


        const int TYPE_CAN = 0;
        const int TYPE_CANFD = 1;

        bool m_bStart;
        IntPtr lin_channel_handle_;
        IntPtr channel_handle_;
        IntPtr device_handle_;
        byte merge_ = 0;//初始不开启合并
        Thread recv_thread_;
        static object locker = new object();
        public RecvCANDataEventHandler OnRecvCANDataEvent;
        public RecvFDDataEventHandler OnRecvFDDataEvent;
        public RecvDataEventHandler OnRecvDataEvent;
        public RecvLINDataEventHandler OnRecvLINDataEvent;
        public recvdatathread()
        {
            
        }

        public event RecvCANDataEventHandler RecvCANData
        {
            add { OnRecvCANDataEvent += new RecvCANDataEventHandler(value); }
            remove { OnRecvCANDataEvent -= new RecvCANDataEventHandler(value); }
        }

        public event RecvFDDataEventHandler RecvFDData
        {
            add { OnRecvFDDataEvent += new RecvFDDataEventHandler(value); }
            remove { OnRecvFDDataEvent -= new RecvFDDataEventHandler(value); }
        }

        public event RecvDataEventHandler RecvData
        {
            add { OnRecvDataEvent += new RecvDataEventHandler(value); }
            remove { OnRecvDataEvent -= new RecvDataEventHandler(value); }
        }


        public event RecvLINDataEventHandler RecvLINData
        {
            add { OnRecvLINDataEvent += new RecvLINDataEventHandler(value); }
            remove { OnRecvLINDataEvent -= new RecvLINDataEventHandler(value); }
        }

        public void setStart(bool start)
        {
            m_bStart = start;
            if (start)
            {
                recv_thread_ = new Thread(RecvDataFunc);
                recv_thread_.IsBackground = true;
                recv_thread_.Start();
            }
            else
            {
                recv_thread_.Join();
                recv_thread_ = null;
            }
        }

        public void setChannelHandle(IntPtr channel_handle)
        {
            lock (locker)
            {
                channel_handle_ = channel_handle;
            }
        }

        public void setDeviceHandle(IntPtr device_handle)
        {
            lock (locker)
            {
                device_handle_ = device_handle;
            }
        }


        public void setMerge_flag(Byte merge)
        {
            lock (locker)
            {
                merge_ = merge;
            }
        }


        public void setLINChannelHandle(IntPtr lin_channel_handle)
        {
            lock (locker)
            {
                lin_channel_handle_ = lin_channel_handle;
            }
        }


        //数据接收函数
        protected void RecvDataFunc()
        {
            zlgcan.ZCAN_Receive_Data[] can_data = new zlgcan.ZCAN_Receive_Data[20000];
            zlgcan.ZCAN_ReceiveFD_Data[] canfd_data = new zlgcan.ZCAN_ReceiveFD_Data[10000];
            zlgcan.ZCANDataObj[] data_obj = new zlgcan.ZCANDataObj[10000];
            zlgcan.ZCAN_LIN_MSG[] lin_data = new zlgcan.ZCAN_LIN_MSG[10000];
            uint len = 0;
            while (m_bStart)
            {
                lock (locker)
                {
                    if (merge_ != 1)
                    { //分开接收

                        len = zlgcan.Method.ZCAN_GetReceiveNum(channel_handle_, TYPE_CAN);

                        if (len > 0)
                        {
                            int size = Marshal.SizeOf(typeof(zlgcan.ZCAN_Receive_Data));
                            IntPtr ptr = Marshal.AllocHGlobal((int)len * size);
                            len = zlgcan.Method.ZCAN_Receive(channel_handle_, ptr, len, 50);

                            ZCAN_Receive_Data candata = new ZCAN_Receive_Data();
                            for (int i = 0; i < len; ++i)
                            {
                                //if (i >= can_data.Length)
                                //{
                                //    continue;
                                //}
                                //can_data[i] = (zlgcan.ZCAN_Receive_Data)Marshal.PtrToStructure(
                                //    (IntPtr)((Int64)ptr + i * size), typeof(zlgcan.ZCAN_Receive_Data));

                                candata = (zlgcan.ZCAN_Receive_Data)Marshal.PtrToStructure(
                                    (IntPtr)((Int64)ptr + i * size), typeof(zlgcan.ZCAN_Receive_Data));

                                //当选项为储能项目时，需要发送心跳信号
                                if (MainViewModel.CurProject == ProjectType.StoredEnergy)
                                {
                                    if (candata.frame.can_id == 0x300)
                                    {
                                        WeakReferenceMessenger.Default.Send<string,string>("trig","0x300");
                                    }
                                }

                                if (DBCParser.Messages.Find(t => t.ID == candata.frame.can_id) != null)
                                {
                                    for (int j = 0; j < DBCParser.Messages[i].Signals.Count; j++)
                                    {
                                        DBCParser.Messages[i].Signals[j].Value = Method.getByteVal(DBCParser.Messages[i].Signals[j].StartBit, (uint)DBCParser.Messages[i].Signals[j].Length,
                                            candata.frame.data, (float)DBCParser.Messages[i].Signals[j].Factor, (int)DBCParser.Messages[i].Signals[j].Offset, 0,
                                            (int)DBCParser.Messages[i].Signals[j].Min, (float)DBCParser.Messages[i].Signals[j].Max);
                                    }
                                }
                                if (ReadAllini.HSD_IO.Find(t => t._msgID == candata.frame.can_id) != null )
                                {
                                    foreach (var item in ReadAllini.HSD_IO)
                                    {
                                        if (item._msgID == candata.frame.can_id)
                                        {
                                            item._Value = Method.getByteVal(item._Startbit, (uint)item._Length, candata.frame.data, item._Factor, (int)item._Offset, item._initialValue, (int)item._Min, item._Max);
                                        }
                                    }
                                }
                            }
                            //OnRecvCANDataEvent(can_data, len);
                            Marshal.FreeHGlobal(ptr);
                        }

                        len = zlgcan.Method.ZCAN_GetReceiveNum(channel_handle_, TYPE_CANFD);
                        if (len > 0)
                        {
                            int size = Marshal.SizeOf(typeof(zlgcan.ZCAN_ReceiveFD_Data));
                            IntPtr ptr = Marshal.AllocHGlobal((int)len * size);
                            len = zlgcan.Method.ZCAN_ReceiveFD(channel_handle_, ptr, len, 50);
                            for (int i = 0; i < len; ++i)
                            {
                                canfd_data[i] = (zlgcan.ZCAN_ReceiveFD_Data)Marshal.PtrToStructure(
                                    (IntPtr)((Int64)ptr + i * size), typeof(zlgcan.ZCAN_ReceiveFD_Data));
                            }
                            //OnRecvFDDataEvent(canfd_data, len);
                            Marshal.FreeHGlobal(ptr);
                        }

                        {
                            int size = Marshal.SizeOf(typeof(zlgcan.ZCAN_LIN_MSG));
                            IntPtr ptr = Marshal.AllocHGlobal(50 * size);
                            len = zlgcan.Method.ZCAN_ReceiveLIN(lin_channel_handle_, ptr, 50, 0);
                            if (len > 0)
                            {
                                for (int i = 0; i < len; ++i)
                                {
                                    lin_data[i] = (zlgcan.ZCAN_LIN_MSG)Marshal.PtrToStructure(
                                        (IntPtr)((Int64)ptr + i * size), typeof(zlgcan.ZCAN_LIN_MSG));
                                }
                                //OnRecvLINDataEvent(lin_data, len);

                            }
                            Marshal.FreeHGlobal(ptr);
                        }
                    }
                    else
                    { //合并接收
                        len = zlgcan.Method.ZCAN_GetReceiveNum(channel_handle_, 2); //合并接收类型type为2
                        if (len > 0)
                        {
                            int size = Marshal.SizeOf(typeof(zlgcan.ZCANDataObj));
                            IntPtr ptr = Marshal.AllocHGlobal((int)len * size);
                            len = zlgcan.Method.ZCAN_ReceiveData(device_handle_, ptr, len, 50);         //传设备的句柄
                            for (int i = 0; i < len; ++i)
                            {
                                data_obj[i] = (zlgcan.ZCANDataObj)Marshal.PtrToStructure(
                                    (IntPtr)((Int64)ptr + i * size), typeof(zlgcan.ZCANDataObj));
                            }
                            OnRecvDataEvent(data_obj, len);
                            Marshal.FreeHGlobal(ptr);
                        }

                    }
                }


                Thread.Sleep(1);
            }
        }
    }
}
