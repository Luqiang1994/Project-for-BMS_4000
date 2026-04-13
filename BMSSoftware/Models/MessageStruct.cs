using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSSoftware.Devices
{
    public class MessageStruct : ObservableObject
    {
        public string _messageName { get; set; }  //报文名称
        public int _idFormat { get; set; }   //帧格式
        public int _msgID { get; set; }   //帧ID
        public int _DLC { get; set; }    //数据长度

        private string _tx;
        public string _TX
        {
            get { return _tx; }
            set => SetProperty(ref _tx, value);
        }
        public string _RX { get; set; }
        public string _TXMethod { get; set; }
        public int _cycleTime { get; set; }

        private string _messagecomment;
        public string _messageComment
        {
            get { return _messagecomment; }
            set => SetProperty(ref _messagecomment, value);
        }
        public string _signalName { get; set; }
        public int _Startbit { get; set; }
        public int _Length { get; set; }
        public float _Factor { get; set; }
        public float _Offset { get; set; }
        public int _initialValue { get; set; }
        public float _Min { get; set; }
        public float _Max { get; set; }
        public string _Unit { get; set; }
        private string _signalcomment;
        public string _signalComment
        {
            get { return _signalcomment; }
            set => SetProperty(ref _signalcomment, value);
        }
        public string _byteOrder { get; set; }
        private float _value;
        public float _Value
        {
            get { return _value; }
            set => SetProperty(ref _value, value);
        }
        public int _NeedSave { get; set; }
    }

    public class BalanceControlMode : ObservableObject
    {
        private string byte1;
        public string Byte1
        {
            get { return byte1; }
            set=> SetProperty(ref byte1, value);
        }
        private string byte2;
        public string Byte2
        {
            get => byte2;
            set=> SetProperty(ref byte2, value);
        }
        private string byte3;
        public string Byte3
        {
            get => byte3;
            set=> SetProperty(ref byte3, value);
        }
        private string byte4;
        public string Byte4
        {
            get => byte4;
            set=> SetProperty(ref byte4, value);
        }
        private string byte5;
        public string Byte5
        {
            get => byte5;
            set=> SetProperty(ref byte5, value);
        }
        private string byte6;
        public string Byte6
        {
            get => byte6; 
            set => SetProperty(ref byte6, value);
        }
        private string byte7;
        public string Byte7
        {
            get => byte7; 
            set => SetProperty(ref byte7, value);
        }
        private string byte8;
        public string Byte8
        {
            get => byte8; set => SetProperty(ref byte8, value);
        }
    }
}
