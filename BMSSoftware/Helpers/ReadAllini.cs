using BMSSoftware.Devices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSSoftware.Helpers
{
    public class ReadAllini
    {
        //系统标定文件
        public static string SysCalibration_CanPath = Directory.GetCurrentDirectory() + "\\" + "SettingFiles" + "\\" + "储能" + "\\" + "SysCalibration_Can.ini";
        public static List<MessageStruct> SysCalibration = new List<MessageStruct>();

        public static string HSD_IOPath = Directory.GetCurrentDirectory() + "\\" + "SettingFiles" + "\\" + "储能" + "\\" + "HSD_IO.ini";
        public static List<MessageStruct> HSD_IO = new List<MessageStruct>();

        #region  储能项目特有配置文件

        #endregion


        static Dictionary<string, List<MessageStruct>> dic_Path_MessageStructs = new Dictionary<string, List<MessageStruct>>();
        public static void InitDictionary()
        {
            dic_Path_MessageStructs.Add(SysCalibration_CanPath, SysCalibration);
            dic_Path_MessageStructs.Add(HSD_IOPath, HSD_IO);
        }

        public static void GetFormat(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);
            string lineinfo = "";

            while (!sr.EndOfStream)
            {
                MessageStruct messageStruct = new MessageStruct();
                lineinfo = sr.ReadLine();
                if (lineinfo == "")
                {
                    continue;
                }

                string[] lineinfodata = lineinfo.Split(',');
                messageStruct._messageName = lineinfodata[0];
                messageStruct._idFormat = lineinfodata[1] == "Extended" ? 1 : 0;
                messageStruct._msgID = /*int.Parse(lineinfodata[2])*/Convert.ToInt32(lineinfodata[2], 16);
                messageStruct._DLC = int.Parse(lineinfodata[3]);
                messageStruct._TX = lineinfodata[4];
                messageStruct._RX = lineinfodata[5];
                messageStruct._TXMethod = lineinfodata[6];
                messageStruct._cycleTime = int.Parse(lineinfodata[7]);
                messageStruct._messageComment = lineinfodata[8];
                messageStruct._signalName = lineinfodata[9];
                messageStruct._Startbit = int.Parse(lineinfodata[10]);
                messageStruct._Length = int.Parse(lineinfodata[11]);
                messageStruct._Factor = float.Parse(lineinfodata[12]);
                messageStruct._Offset = int.Parse(lineinfodata[13]);
                messageStruct._initialValue = int.Parse(lineinfodata[14]);
                messageStruct._Min = float.Parse(lineinfodata[15]);
                messageStruct._Max = float.Parse(lineinfodata[16]);
                messageStruct._Unit = lineinfodata[17];
                messageStruct._signalComment = lineinfodata[18];
                messageStruct._byteOrder = lineinfodata[19];
                messageStruct._Value = -1;
                if (lineinfodata.Length == 21)
                {
                    messageStruct._NeedSave = 0;
                }
                else
                    messageStruct._NeedSave = int.Parse(lineinfodata[21]);

                SetMessageStruct(path, messageStruct);

            }

            sr.Dispose();
            sr.Close();
        }

        private static void SetMessageStruct(string path, MessageStruct message)
        {
            dic_Path_MessageStructs[path].Add(message);
        }
    }
}
