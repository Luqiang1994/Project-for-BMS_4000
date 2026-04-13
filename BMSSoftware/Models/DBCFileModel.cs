using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSSoftware.Models
{
    public class DBCFileModel : ObservableObject
    {
        public uint ID { get; set; } // 消息ID
        public string Name { get; set; } // 消息名称

        private int dLC;
        public int DLC
        {
            get { return dLC; }
            set
            {
                SetProperty(ref dLC, value);
            }
        }// 数据长度
        public string Transmitter { get; set; } // 发送节点

        private List<Signal> signals = new List<Signal>();
        public List<Signal> Signals
        {
            get => signals;
            set => SetProperty(ref signals, value);
        }
    }

    public class Signal : ObservableObject
    {
        public string Name { get; set; } // 信号名称
        public int StartBit { get; set; } // 起始位
        public int Length { get; set; } // 信号长度
        public double Factor { get; set; } // 缩放因子
        public double Offset { get; set; } // 偏移量
        public double Min { get; set; } // 最小值
        public double Max { get; set; } // 最大值
        public string Unit { get; set; } // 单位
        public string Receiver { get; set; } // 接收节点

        private double _value;
        public double Value
        {
            get { return _value; }
            set => SetProperty(ref _value, value);
        }//信号值
    }

    public class Node : ObservableObject
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }// 节点名称
    }
}
