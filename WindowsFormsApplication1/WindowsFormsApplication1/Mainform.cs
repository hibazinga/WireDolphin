using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Resources;
using System.IO;
using SharpPcap;
using SharpPcap.LibPcap;
using SharpPcap.AirPcap;
using SharpPcap.WinPcap;


namespace Sniffer
{
    public partial class mainform : Form
    {
        
        ICaptureDevice device;
        private static int NumOfPacket = 0;
        public static int numofpacketsaved = 0; 
        CaptureDeviceList devices = CaptureDeviceList.Instance;
        RawCapture CapturedPacket;
        public static List<RawCapture> CapturePacketlist;
        public static List<String> Hexcode;
        public static List<String> Details;
        public static string filterrules = ""; //过滤字符串
        public static bool saveflag;
        public static int Selectedindex = 0;
        public class DoubleBufferListView : ListView
        {
            public DoubleBufferListView()
            {
                SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
                UpdateStyles();
            }
        }
        public mainform()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            textBox3.Enabled = false;
        }
        private void mainform_Load(object sender, EventArgs e)
        {
            CapturePacketlist = new List<RawCapture>();//新建抓包的list
            Hexcode = new List<String>();
            Details = new List<String>();
            if (devices.Count < 1)
            {
            
                interfacelist.Items.Add("No device");
                return;
            }
            else
            {
                foreach (var dev in devices)
                {
                    interfacelist.Items.Add(dev.Description.ToString());
                }

            }
        }
        private void Form1_SelectedIndexChanged(object sender, EventArgs e)
        {
            device = devices[interfacelist.SelectedIndex];
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            if (listView1.SelectedItems.Count == 0)
            {
                return;
            }
            else
            {
                ListView sel = (ListView)sender;
                Selectedindex = Convert.ToInt32(sel.SelectedItems[0].Text.ToString());
                RawCapture selectpacket = CapturePacketlist[Selectedindex-1];
                if (selectpacket == null)
                {
                    return;
                }
                var selpacket = PacketDotNet.Packet.ParsePacket(selectpacket.LinkLayerType, selectpacket.Data);
                textBox1.Text = selpacket.PrintHex();
                var ip = PacketDotNet.IpPacket.GetEncapsulated(selpacket);
                var icmpv4 = PacketDotNet.ICMPv4Packet.GetEncapsulated(selpacket);
                var icmpv6 = PacketDotNet.ICMPv6Packet.GetEncapsulated(selpacket);
                var igmp = PacketDotNet.IGMPv2Packet.GetEncapsulated(selpacket);
                var tcp = PacketDotNet.TcpPacket.GetEncapsulated(selpacket);
                var udp = PacketDotNet.UdpPacket.GetEncapsulated(selpacket);
                var arp = PacketDotNet.ARPPacket.GetEncapsulated(selpacket);

                if (ip != null)
                {

                    if (icmpv4 != null)
                    {
                        textBox2.Text = selpacket.ToString();
                    }

                    else if (igmp != null)
                    {
                        textBox2.Text = selpacket.ToString();

                    }
                    else if (tcp != null)
                    {
                        textBox2.Text = selpacket.ToString() + "[seq:" + tcp.SequenceNumber + "]" + "[WindowSize:" + tcp.WindowSize + "]" +
                            "[URG:" + tcp.Urg + "]" + "[ACK:" + tcp.Ack + "]" + "[PSH:" + tcp.Psh + "]" + "[RST:" + tcp.Rst + "]" + "[SYN:" + tcp.Syn + "]" + "[FIN:" + tcp.Fin + "]";

                    }
                    else if (udp != null)
                    {
                        textBox2.Text = selpacket.ToString() + "[UdpPacket Length:" + udp.Length + " bytes]" + "[checksum:" + udp.Checksum + "]";
                    }
                    else
                    {
                        try
                        {
                            textBox2.Text = selpacket.ToString();
                        }
                        catch (System.Exception ex)
                        {
                            textBox2.Text = ex.ToString();
                        }

                    }
                }
                else if (arp != null)
                {
                    textBox2.Text = selpacket.ToString();
                }
                else
                {
                    textBox2.Text = selpacket.ToString();
                }

            }
        }
        private void start_button_Click(object sender, EventArgs e)
        {

            if (device == null) MessageBox.Show("Please choose a network adaptor!");
            else
            {
                numofpacketsaved = 0;
                CapturePacketlist.Clear();
                Hexcode.Clear();
                Details.Clear();
                CapturePacketlist = new List<RawCapture>();
                Hexcode = new List<String>();
                Details = new List<String>();
                Hexcode.Clear();
                Details.Clear();
                listView1.Items.Clear();
                device.OnPacketArrival += new PacketArrivalEventHandler(device_OnPacketArrival);//回调函数（sharppcap的example中例子）
                int readTimeoutMilliseconds = 1000;
                if (device is AirPcapDevice)
                {
                    // NOTE: AirPcap devices cannot disable local capture
                    var airPcap = device as AirPcapDevice;
                    airPcap.Open(SharpPcap.WinPcap.OpenFlags.DataTransferUdp, readTimeoutMilliseconds);
                }
                else if (device is WinPcapDevice)
                {
                    var winPcap = device as WinPcapDevice;
                    winPcap.Open(SharpPcap.WinPcap.OpenFlags.DataTransferUdp | SharpPcap.WinPcap.OpenFlags.NoCaptureLocal, readTimeoutMilliseconds);
                }
                else if (device is LibPcapLiveDevice)
                {
                    var livePcapDevice = device as LibPcapLiveDevice;
                    livePcapDevice.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);
                }
                else
                {
                    throw new System.InvalidOperationException("unknown device type of " + device.GetType().ToString());
                }
                device.Filter = filterrules;
                //device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);
                textBox3.Text = filterrules.ToString();
                device.StartCapture();
            }
        }
        private void Save_Click(object sender, EventArgs e)
        {
            Save save = new Save();
            saveflag = true;
            save.Show();
        }
        private void Stop_Click(object sender, EventArgs e)
        {
            try
            {
                device.StopCapture();
                device.Close();
                textBox1.Clear();
                textBox2.Clear();
                NumOfPacket = 0;
            }
            catch (System.Exception ex)
            {
            }

        }
        private void device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            //包的数量从1开始计数，因此先++
            NumOfPacket++;
            numofpacketsaved = NumOfPacket;
            var time = e.Packet.Timeval.Date.Date.ToString() + e.Packet.Timeval.Date.Millisecond.ToString();
            var len = e.Packet.Data.Length;
            CapturedPacket = e.Packet;
            CapturePacketlist.Add(CapturedPacket);
            var packet = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
            var ethernetPacket = (PacketDotNet.EthernetPacket)packet;
            ListViewItem a = new ListViewItem();
            a.SubItems[0].Text = NumOfPacket.ToString();
            a.SubItems.Add(time);
            try
            {
                Hexcode.Add(packet.PrintHex());
                Details.Add(packet.ToString());
            }
            catch (System.Exception ex)
            {
                Hexcode.Add(packet.PrintHex());
                Details.Add("unknown");
            }

            var ip = PacketDotNet.IpPacket.GetEncapsulated(packet);
            var ip6 = PacketDotNet.IPv6Packet.GetEncapsulated(packet);
            var icmpv4 = PacketDotNet.ICMPv4Packet.GetEncapsulated(packet);
            var icmpv6 = PacketDotNet.ICMPv6Packet.GetEncapsulated(packet);
            var igmp = PacketDotNet.IGMPv2Packet.GetEncapsulated(packet);
            var tcp = PacketDotNet.TcpPacket.GetEncapsulated(packet);
            var udp = PacketDotNet.UdpPacket.GetEncapsulated(packet);
            var arp = PacketDotNet.ARPPacket.GetEncapsulated(packet);



            if (ip != null)
            {
                a.SubItems.Add(ip.SourceAddress.ToString());
                a.SubItems.Add(ip.DestinationAddress.ToString());
                if (icmpv4 != null)
                {
                    //protocol 为icmpv4
                    a.SubItems.Add("ICMPv4");
                    //添加长度
                    a.SubItems.Add(len.ToString());
                    //info里添加源、目的地址
                    a.SubItems.Add(ip.SourceAddress.ToString() + "=>" + ip.DestinationAddress.ToString());
                    //北京设为粉色
                    a.BackColor = Color.Pink;

                }
                else if (icmpv6 != null)
                {
                    a.SubItems.Add("ICPMv6");
                    a.SubItems.Add(len.ToString());
                    a.SubItems.Add(ip.SourceAddress.ToString() + "=>" + ip.DestinationAddress.ToString());
                    a.BackColor = Color.Pink;

                }
                else if (igmp != null)
                {
                    a.SubItems.Add("IGMP");
                    a.SubItems.Add(len.ToString());
                    a.SubItems.Add(ip.SourceAddress.ToString() + "=>" + ip.DestinationAddress.ToString());
                    a.BackColor = Color.Orange;
                }
                else if (tcp != null)
                {
                    a.SubItems.Add("TCP");
                    a.SubItems.Add(len.ToString());
                    a.SubItems.Add("Src.Port: " + tcp.SourcePort.ToString() + " Dst.Port: " + tcp.DestinationPort.ToString());
                    a.BackColor = Color.LightGreen;
                }
                else if (udp != null)
                {
                    a.SubItems.Add("UDP");
                    a.SubItems.Add(len.ToString());
                    a.SubItems.Add("Src.Port: " + udp.SourcePort.ToString() + " Dst.Port: " + udp.DestinationPort.ToString());
                    a.BackColor = Color.Yellow;
                }
                else
                {
                    a.SubItems.Add("Unknown");
                    a.SubItems.Add(len.ToString());
                    a.SubItems.Add(ip.SourceAddress.ToString() + "=>" + ip.DestinationAddress.ToString());
                    a.BackColor = Color.Snow;
                }
            }
            else if (arp != null)
            {
                a.SubItems.Add(arp.SenderProtocolAddress.ToString());
                a.SubItems.Add(arp.TargetProtocolAddress.ToString());
                a.SubItems.Add("ARP");
                a.SubItems.Add(len.ToString());
                a.SubItems.Add(arp.SenderProtocolAddress.ToString() + "=> " + arp.TargetProtocolAddress.ToString());
                a.BackColor = Color.Black;
                a.ForeColor = Color.White;
            }
            else
            {
                //其他协议为分析
                a.SubItems.Add("UNKNOWN");
                a.SubItems.Add("UNKNOWN");
                a.SubItems.Add("UNKNOWN");
                a.SubItems.Add("UNKNOWN");
                a.SubItems.Add("UNKNOWN");
                a.SubItems.Add("UNKNOWN");
                a.SubItems.Add("UNKNOWN");
            }
            //写入listview
            listView1.Items.Add(a);
            listView1.Items[listView1.Items.Count - 1].EnsureVisible();
        }
        //点击filter窗口
        private void filter_Click(object sender, EventArgs e)
        {
            Filter filter = new Filter();
            filter.SetMainFormTopMost += new Filter.SetMainFormTopMostHandle(filter_SetMainFormTopMost);
            filter.Show();
        }

        void filter_SetMainFormTopMost(bool topmost)
        {
            textBox3.Text = filterrules.ToString();
            //throw new NotImplementedException();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Search search = new Search();
            search.SetMainFormTopMost2 += new Search.SetMainFormTopMostHandle2(search_SetMainFormTopMost2);
            search.Show();
        }

        void search_SetMainFormTopMost2(bool topmost2)
        {
            textBox1.Clear();
            textBox2.Clear();
            listView1.Items.Clear();
            try
            {
                foreach (int index in Search.SearchIndex)
                {
                    var e = CapturePacketlist[index];
                    var time = e.Timeval.Date.Date.ToString() + e.Timeval.Date.Millisecond.ToString();
                    var len = e.Data.Length;
                    var packet = PacketDotNet.Packet.ParsePacket(e.LinkLayerType, e.Data);
                    var ethernetPacket = (PacketDotNet.EthernetPacket)packet;
                    ListViewItem a = new ListViewItem();
                    a.SubItems[0].Text = index.ToString();
                    a.SubItems.Add(time);
                    var ip = PacketDotNet.IpPacket.GetEncapsulated(packet);
                    var icmpv4 = PacketDotNet.ICMPv4Packet.GetEncapsulated(packet);
                    var icmpv6 = PacketDotNet.ICMPv6Packet.GetEncapsulated(packet);
                    var igmp = PacketDotNet.IGMPv2Packet.GetEncapsulated(packet);
                    var tcp = PacketDotNet.TcpPacket.GetEncapsulated(packet);
                    var udp = PacketDotNet.UdpPacket.GetEncapsulated(packet);
                    var arp = PacketDotNet.ARPPacket.GetEncapsulated(packet);
                    if (ip != null)
                    {
                        a.SubItems.Add(ip.SourceAddress.ToString());
                        a.SubItems.Add(ip.DestinationAddress.ToString());
                        if (icmpv4 != null)
                        {
                            a.SubItems.Add("ICMPv4");
                            a.SubItems.Add(len.ToString());
                            a.SubItems.Add(ip.SourceAddress.ToString() + "=>" + ip.DestinationAddress.ToString());
                            a.BackColor = Color.Pink;
                        }
                        else if (icmpv6 != null)
                        {
                            a.SubItems.Add("ICPMv6");
                            a.SubItems.Add(len.ToString());
                            a.SubItems.Add(ip.SourceAddress.ToString() + "=>" + ip.DestinationAddress.ToString());
                            a.BackColor = Color.Pink;
                        }
                        else if (igmp != null)
                        {
                            a.SubItems.Add("IGMP");
                            a.SubItems.Add(len.ToString());
                            a.SubItems.Add(ip.SourceAddress.ToString() + "=>" + ip.DestinationAddress.ToString());
                            a.BackColor = Color.Orange;
                        }
                        else if (tcp != null)
                        {
                            a.SubItems.Add("TCP");
                            a.SubItems.Add(len.ToString());
                            a.SubItems.Add("Src.Port: " + tcp.SourcePort.ToString() + " Dst.Port: " + tcp.DestinationPort.ToString());
                            a.BackColor = Color.LightGreen;
                        }
                        else if (udp != null)
                        {
                            a.SubItems.Add("UDP");
                            a.SubItems.Add(len.ToString());
                            a.SubItems.Add("Src.Port: " + udp.SourcePort.ToString() + " Dst.Port: " + udp.DestinationPort.ToString());
                            a.BackColor = Color.Yellow;
                        }
                        else
                        {
                            a.SubItems.Add("Unknown");
                            a.SubItems.Add(len.ToString());
                            a.SubItems.Add(ip.SourceAddress.ToString() + "=>" + ip.DestinationAddress.ToString());
                            a.BackColor = Color.Snow;
                        }
                    }
                    else if (arp != null)
                    {
                        a.SubItems.Add(arp.SenderProtocolAddress.ToString());
                        a.SubItems.Add(arp.TargetProtocolAddress.ToString());
                        a.SubItems.Add("ARP");
                        a.SubItems.Add(len.ToString());
                        a.SubItems.Add(arp.SenderProtocolAddress.ToString() + "=> " + arp.TargetProtocolAddress.ToString());
                        a.BackColor = Color.Black;
                        a.ForeColor = Color.White;
                    }
                    else
                    {
                        a.SubItems.Add("UNKNOWN");
                        a.SubItems.Add("UNKNOWN");
                        a.SubItems.Add("UNKNOWN");
                        a.SubItems.Add("UNKNOWN");
                        a.SubItems.Add("UNKNOWN");
                        a.SubItems.Add("UNKNOWN");
                        a.SubItems.Add("UNKNOWN");
                    }
                    listView1.Items.Add(a);
                }
            }
            catch (System.Exception ex)
            {
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Selectedindex == 0) { MessageBox.Show("Please choose a IP packet!"); }
            else
            {
                IPfragment fragment = new IPfragment();
                fragment.Show();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Selectedindex == 0) { MessageBox.Show("Please choose a TCP/UDP packet!"); }
            else
            {
                Stream follow = new Stream();
                follow.Show();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
 
            if (CapturePacketlist.Count() == 0)
            {
                MessageBox.Show("No packets!");
                return;
            }
            else
            {
                //选中listview1中项目的序号
                int i = int.Parse(listView1.SelectedItems[0].Text)-1;
                RawCapture target = CapturePacketlist[i];
                var pac = PacketDotNet.Packet.ParsePacket(target.LinkLayerType, target.Data);
                var tcp = PacketDotNet.TcpPacket.GetEncapsulated(pac);
                var ip = PacketDotNet.IpPacket.GetEncapsulated(pac);
                //检测是否为tcp包
                if (tcp != null)
                {
                    //新建一个tcppacket类型的stream
                    List<PacketDotNet.TcpPacket> stream = new List<PacketDotNet.TcpPacket>();
                    foreach (var single in CapturePacketlist)
                    {
                        var pac2 = PacketDotNet.Packet.ParsePacket(single.LinkLayerType, single.Data);
                        var tcp2 = PacketDotNet.TcpPacket.GetEncapsulated(pac2);
                        var ip2 = PacketDotNet.IpPacket.GetEncapsulated(pac2);
                        if (tcp2 != null)
                        {
                            var tcp_target = PacketDotNet.TcpPacket.GetEncapsulated(pac);
                            var tcp_temp = PacketDotNet.TcpPacket.GetEncapsulated(pac2);
                            //检测源目的地址是否相同，相同添加
                            if (tcp_temp.SourcePort.Equals(tcp_target.SourcePort) && ip.SourceAddress.Equals(ip2.SourceAddress))
                            {
                                //检测是否有相同序号的包，seq不相同的包加入stream
                                if (stream.Find(x => x.SequenceNumber.Equals(tcp_temp.SequenceNumber)) == null)
                                {
                                    //添加入stream
                                    stream.Add(tcp_temp);
                                    
                                }
                            }
                        }
                    }
                    //对stream中的包进行排序，用lambda表达式
                    stream.Sort((y,x) => x.SequenceNumber.CompareTo(y.SequenceNumber));
                    SaveFileDialog sf = new SaveFileDialog();
                    sf.Filter = "txt文件|*.txt|所有文件|*.*";
                    sf.AddExtension = true;
                    sf.Title = "写文件";
                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        FileStream fs = new FileStream(sf.FileName, FileMode.Create);
                        foreach (PacketDotNet.TcpPacket temp in stream)
                        {
                            //把包中data写入文件
                            fs.Write(temp.PayloadData, 0, temp.PayloadData.Length);
                        }
                        //关闭流
                        fs.Flush();
                        fs.Close();
                    }
                }
                else
               {
                   MessageBox.Show("It is not a TCP packet!");
                    }
                }
            
        }
    }
}



