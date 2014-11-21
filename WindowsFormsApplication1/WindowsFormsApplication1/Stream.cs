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
    public partial class Stream : Form
    {
        private static ushort ID;
        private static int index;
        private static string sourceaddress;
        private static string destinationaddress;
        private static string sourceport;
        private static string destinationport;
        public Stream()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            IPfragment.fragmentindex = new List<int>();
            IPfragment.fragmentnum=0;
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
                    sourceaddress=selipv4.SourceAddress.ToString();
                    destinationaddress=selipv4.DestinationAddress.ToString();
                    if (selicmpv4 != null)
                    {
                        MessageBox.Show("Error!ICMPv4 Packet!");
                        this.Close();
                    }
                    else if (selicmpv6 != null)
                    {
                        MessageBox.Show("Error!ICMPv6 Packet!");
                        this.Close();
                    }
                    else if (seligmp != null)
                    {
                        MessageBox.Show("Error!IGMP Packet!");
                        this.Close();
                    }
                    else if (seltcp != null)
                    {
                        sourceport=seltcp.SourcePort.ToString();
                        destinationport=seltcp.DestinationPort.ToString();
                    }
                    else if (seludp != null)
                    {
                        sourceport=seludp.SourcePort.ToString();
                        destinationport=seludp.DestinationPort.ToString();
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
                    if (sourceport == tcp.SourcePort.ToString() && destinationport == tcp.DestinationPort.ToString() && sourceaddress == ipv4.SourceAddress.ToString() && destinationaddress == ipv4.DestinationAddress.ToString()) { IPfragment.fragmentindex.Add(index);IPfragment.fragmentnum++; }
                }
                else if (seludp != null && udp !=null)
                {
                    if (sourceport == udp.SourcePort.ToString() && destinationport == udp.DestinationPort.ToString() && sourceaddress == ipv4.SourceAddress.ToString() && destinationaddress == ipv4.DestinationAddress.ToString()) { IPfragment.fragmentindex.Add(index);IPfragment.fragmentnum++; }
                }
            }
            for (int i = 0; i < IPfragment.fragmentnum; i++) {
                for (int j = 0; j < IPfragment.fragmentnum-1; j++) { 
                    var packet = PacketDotNet.Packet.ParsePacket(mainform.CapturePacketlist[j].LinkLayerType, mainform.CapturePacketlist[j].Data);
                    var ipv4 =(PacketDotNet.IPv4Packet) PacketDotNet.IPv4Packet.GetEncapsulated(packet);
                    var packet_1 = PacketDotNet.Packet.ParsePacket(mainform.CapturePacketlist[j+1].LinkLayerType, mainform.CapturePacketlist[j+1].Data);
                    var ipv4_1 =(PacketDotNet.IPv4Packet) PacketDotNet.IPv4Packet.GetEncapsulated(packet_1);
                    if (ipv4.Id > ipv4_1.Id) {
                        int temp = IPfragment.fragmentindex[j + 1];
                        IPfragment.fragmentindex[j + 1] = IPfragment.fragmentindex[j];
                        IPfragment.fragmentindex[j] = temp;
                    }
                }
            }
            for (int k = 0; k < IPfragment.fragmentnum; k++) {
                var packet = PacketDotNet.Packet.ParsePacket(mainform.CapturePacketlist[k].LinkLayerType, mainform.CapturePacketlist[k].Data);
                var ipv4 = (PacketDotNet.IPv4Packet)PacketDotNet.IPv4Packet.GetEncapsulated(packet);
                textBox1.Text += ipv4.PrintHex();
                textBox1.Text += "\r\n";
            }

            textBox2.Text = selipv4.ToString() + "\r\n"+"***[Num of Packets : " + IPfragment.fragmentnum.ToString()+"]***";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Save save = new Save();
            mainform.saveflag = false;
            save.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
