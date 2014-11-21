using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sniffer
{
    public partial class Search : Form
    {
        public delegate void SetMainFormTopMostHandle2(bool topmost2);//声明委托函数
        public event SetMainFormTopMostHandle2 SetMainFormTopMost2;
        private static bool flag = false;
        public static List<int> SearchIndex;
        public Search()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int strlen = textBox1.Text.Length;
            //搜索每一个包的ASCII码数据部分 记录包序号
            for (int c = 0; c < mainform.numofpacketsaved; c++)
            {
                flag = false;
                var packet = PacketDotNet.Packet.ParsePacket(mainform.CapturePacketlist[c].LinkLayerType, mainform.CapturePacketlist[c].Data);
                //var ethernetPacket = (PacketDotNet.EthernetPacket)packet;
                var ip = PacketDotNet.IpPacket.GetEncapsulated(packet);
                try
                {
                    string temp = new String(System.Text.Encoding.Default.GetChars(ip.Bytes));
                    for (int d = 0; d < temp.Length - strlen + 1; d++)
                    {
                        if (string.Equals(temp.Substring(d, strlen), textBox1.Text) == true) { flag = true; break; }
                    }
                }
                catch (System.Exception ex)
                {
                   
          
                }
                
                
                if (flag == true) { SearchIndex.Add(c + 1); }
            }
            MessageBox.Show("Done!");
            SetMainFormTopMost2(true);
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int c = 0; c < mainform.numofpacketsaved; c++) { SearchIndex.Add(c + 1); }
            MessageBox.Show("Done!");
            SetMainFormTopMost2(true);
            this.Hide();
        }

        private void Search_Load(object sender, EventArgs e)
        {
            SearchIndex = new List<int>();
            SearchIndex.Clear();
        }
    }
}
