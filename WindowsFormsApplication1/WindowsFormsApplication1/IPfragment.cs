using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpPcap;
using SharpPcap.LibPcap;
using SharpPcap.AirPcap;
using SharpPcap.WinPcap;

namespace Sniffer
{
    public partial class IPfragment : Form
    {
        private static ushort ID;
        private static int index;
        public static int fragmentnum;
        public static List<int> fragmentindex;
        public IPfragment()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Save save = new Save();
            mainform.saveflag = false;
            save.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            fragmentindex = new List<int>();
            fragmentnum=0;
            textBox1.Clear();
            textBox2.Clear();
            var selpacket = PacketDotNet.Packet.ParsePacket(mainform.CapturePacketlist[mainform.Selectedindex-1].LinkLayerType, mainform.CapturePacketlist[mainform.Selectedindex-1].Data);
            var selethernetPacket = (PacketDotNet.EthernetPacket)selpacket;
            var selipv4 = (PacketDotNet.IPv4Packet)PacketDotNet.IPv4Packet.GetEncapsulated(selpacket);
            var selicmpv4 = PacketDotNet.ICMPv4Packet.GetEncapsulated(selpacket);
            var selicmpv6 = PacketDotNet.ICMPv6Packet.GetEncapsulated(selpacket);
            var seligmp = PacketDotNet.IGMPv2Packet.GetEncapsulated(selpacket);
            var seltcp = PacketDotNet.TcpPacket.GetEncapsulated(selpacket);
            var seludp = PacketDotNet.UdpPacket.GetEncapsulated(selpacket);
            var selarp = PacketDotNet.ARPPacket.GetEncapsulated(selpacket);
                if (selipv4 != null)
                {
                    ID = selipv4.Id;
                    if (selicmpv4 != null)
                    {
                    }
                    else if (selicmpv6 != null)
                    {
                    }
                    else if (seligmp != null)
                    {
                    }
                    else if (seltcp != null)
                    {
                    }
                    else if (seludp != null)
                    {
                    }
                    else
                    {
                        MessageBox.Show("Error!Unknown Packet!");
                        this.Close();
                    }
                }
                else if (selarp != null)
                {
                    MessageBox.Show("Error!This is an ARP packet!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error!Unknown Packet!");
                    this.Close();
                }


            for (index = 0; index < mainform.numofpacketsaved;index++ )
            {
                //if (index == mainform.Selectedindex - 1) { continue; }
                var packet = PacketDotNet.Packet.ParsePacket(mainform.CapturePacketlist[index].LinkLayerType, mainform.CapturePacketlist[index].Data);
                var ipv4 =(PacketDotNet.IPv4Packet) PacketDotNet.IPv4Packet.GetEncapsulated(packet);
                var tcp = PacketDotNet.TcpPacket.GetEncapsulated(packet);
                var udp = PacketDotNet.UdpPacket.GetEncapsulated(packet);
                var icmpv4 = PacketDotNet.ICMPv4Packet.GetEncapsulated(packet);
                var icmpv6 = PacketDotNet.ICMPv6Packet.GetEncapsulated(packet);
                var igmp = PacketDotNet.IGMPv2Packet.GetEncapsulated(packet);
                if (seltcp != null && tcp !=null)
                {
                    if (ID == ipv4.Id) { fragmentindex.Add(index);fragmentnum++; }
                }
                else if (seludp != null && udp !=null)
                {
                    if (ID == ipv4.Id) { fragmentindex.Add(index);fragmentnum++; }
                }
                else if (selicmpv4 != null && icmpv4 != null) 
                { 
                    if (ID == ipv4.Id) { fragmentindex.Add(index); fragmentnum++;} 
                }
                else if (selicmpv6 != null && icmpv6 != null)
                {
                    if (ID == ipv4.Id) { fragmentindex.Add(index);fragmentnum++; }
                }
                else if (seligmp != null && igmp != null)
                {
                    if (ID == ipv4.Id) { fragmentindex.Add(index); fragmentnum++;}
                }
            }
            for (int i = 0; i < fragmentnum; i++) {
                for (int j = 0; j < fragmentnum-1; j++) { 
                    var packet = PacketDotNet.Packet.ParsePacket(mainform.CapturePacketlist[j].LinkLayerType, mainform.CapturePacketlist[j].Data);
                    var ipv4 =(PacketDotNet.IPv4Packet) PacketDotNet.IPv4Packet.GetEncapsulated(packet);
                    var packet_1 = PacketDotNet.Packet.ParsePacket(mainform.CapturePacketlist[j+1].LinkLayerType, mainform.CapturePacketlist[j+1].Data);
                    var ipv4_1 =(PacketDotNet.IPv4Packet) PacketDotNet.IPv4Packet.GetEncapsulated(packet_1);
                    if (ipv4.FragmentOffset > ipv4_1.FragmentOffset) {
                        int temp = fragmentindex[j + 1];
                        fragmentindex[j + 1] = fragmentindex[j];
                        fragmentindex[j] = temp;
                    }
                }
            }
            for (int k = 0; k < fragmentnum; k++) {
                var packet = PacketDotNet.Packet.ParsePacket(mainform.CapturePacketlist[k].LinkLayerType, mainform.CapturePacketlist[k].Data);
                var ipv4 = (PacketDotNet.IPv4Packet)PacketDotNet.IPv4Packet.GetEncapsulated(packet);
                textBox1.Text += ipv4.PrintHex();
                textBox1.Text += "\r\n";
            }

            textBox2.Text = selipv4.ToString() + "\r\n"+"***[Num of Fragments : " + fragmentnum.ToString()+"]***";
        }
    }
}