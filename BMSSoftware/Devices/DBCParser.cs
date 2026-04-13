using BMSSoftware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BMSSoftware.Devices
{
    public class DBCParser
    {
        public static List<DBCFileModel> Messages { get; private set; } = new List<DBCFileModel>();
        public static List<Node> Nodes { get; private set; } = new List<Node>();
        public static string filePath = "";

        public static void Parse()
        {
            string[] lines = System.IO.File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                ParseLine(line);
            }
        }

        private static void ParseLine(string line)
        {
            if (line.StartsWith("BO_"))
            {
                ParseMessage(line);
            }
            else if (line.StartsWith(" SG_"))
            {
                ParseSignal(line);
            }
            else if (line.StartsWith("BU_"))
            {
                ParseNode(line);
            }
        }

        private static void ParseMessage(string line)
        {
            // 示例行：BO_ 100 MessageName: 8 Transmitter
            var match = Regex.Match(line, @"BO_ (\d+) (\w+): (\d+) (\w+)");
            if (match.Success)
            {
                var message = new DBCFileModel
                {
                    ID = uint.Parse(match.Groups[1].Value),
                    Name = match.Groups[2].Value,
                    DLC = int.Parse(match.Groups[3].Value),
                    Transmitter = match.Groups[4].Value
                };
                Messages.Add(message);
            }
        }

        private static void ParseSignal(string line)
        {
            // 示例行：SG_ SignalName : 8|16@1+ (1,0) [0|255] "Unit" Receiver
            var match = Regex.Match(line, @"SG_ (\w+) : (\d+)\|(\d+)@(\d+)([+-]) \((\d+),(\d+)\) \[(\d+)\|(\d+)\] ""(.*)"" (\w+)");
            Match match1 = Regex.Match(line, @"SG_ (\w+) : (\d+)\|(\d+)@(\d+)([+-]) \(([\d.]+),([-+]?\d+)\) \[([-+]?\d+)\|([-+]?\d+)\] ""(.*)"" (\w+)");
            Match match2 = Regex.Match(line, @"SG_ (\w+) : (\d+)\|(\d+)@(\d+)([+-]) \(([\d.]+),([\d.-]+)\) \[([\d.]+)\|([\d.]+)\] ""(.*)"" (\w+)");
            if (match.Success)
            {
                var signal = new Signal
                {
                    Name = match.Groups[1].Value,
                    StartBit = int.Parse(match.Groups[2].Value),
                    Length = int.Parse(match.Groups[3].Value),
                    Factor = double.Parse(match.Groups[6].Value),
                    Offset = double.Parse(match.Groups[7].Value),
                    Min = double.Parse(match.Groups[8].Value),
                    Max = double.Parse(match.Groups[9].Value),
                    Unit = match.Groups[10].Value,
                    Receiver = match.Groups[11].Value
                };

                // 将信号添加到最后一个消息中
                if (Messages.Count > 0)
                {
                    Messages[Messages.Count - 1].Signals.Add(signal);
                }
            }
            else if (match1.Success)
            {
                var signal = new Signal
                {
                    Name = match1.Groups[1].Value,
                    StartBit = int.Parse(match1.Groups[2].Value),
                    Length = int.Parse(match1.Groups[3].Value),
                    Factor = double.Parse(match1.Groups[6].Value),
                    Offset = double.Parse(match1.Groups[7].Value),
                    Min = double.Parse(match1.Groups[8].Value),
                    Max = double.Parse(match1.Groups[9].Value),
                    Unit = match1.Groups[10].Value,
                    Receiver = match1.Groups[11].Value
                };

                // 将信号添加到最后一个消息中
                if (Messages.Count > 0)
                {
                    Messages[Messages.Count - 1].Signals.Add(signal);
                }
            }
            else if (match2.Success)
            {
                var signal = new Signal
                {
                    Name = match2.Groups[1].Value,
                    StartBit = int.Parse(match2.Groups[2].Value),
                    Length = int.Parse(match2.Groups[3].Value),
                    Factor = double.Parse(match2.Groups[6].Value),
                    Offset = double.Parse(match2.Groups[7].Value),
                    Min = double.Parse(match2.Groups[8].Value),
                    Max = double.Parse(match2.Groups[9].Value),
                    Unit = match2.Groups[10].Value,
                    Receiver = match2.Groups[11].Value
                };

                // 将信号添加到最后一个消息中
                if (Messages.Count > 0)
                {
                    Messages[Messages.Count - 1].Signals.Add(signal);
                }
            }

        }

        private static void ParseNode(string line)
        {
            // 示例行：BU_: Node1 Node2 Node3
            var match = Regex.Match(line, @"BU_: (.*)");
            if (match.Success)
            {
                var nodeNames = match.Groups[1].Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var name in nodeNames)
                {
                    Nodes.Add(new Node { Name = name });
                }
            }
        }
    }
}
