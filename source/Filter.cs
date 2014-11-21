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
    public partial class Filter : Form
    {
        public delegate void SetMainFormTopMostHandle(bool topmost);//定义委托函数
        public event SetMainFormTopMostHandle SetMainFormTopMost;//设置委托函数的实例，用来之后跨窗口调用
        static string r = "";//r为过滤字符串
        public Filter()
        {
            InitializeComponent();
       
        }

        private void Filter_Load(object sender, EventArgs e)
        {
            Expression.Enabled = false;
        }//阻止用户输入

        //生成协议的过滤字符串
        private void GenerateRule()
        {
            r = "";
            //ARP 和 ICMP
            if (ARP.Checked)
                r = "arp";
            if (ICMP.Checked)
            {
                if (r == "")
                    r = "icmp";
                else
                    r = r + " or icmp";
            }
            if (IGMP.Checked)
            {
                if (r == "")
                    r = "igmp";
                else
                    r = r + " or igmp";
            }
            
            if (IPV4.Checked || IPV6.Checked)
            {
                if ((!TCP.Checked) && (!UDP.Checked))
                {
                    if (IPV4.Checked)
                    {
                        if (r == "")
                            r = "ip";
                        else
                            r = r + " or ip";
                    }
                    if (IPV6.Checked)
                    {
                        if (r == "")
                            r = "ip6";
                        else
                            r = r + " or ip6";
                    }
                }
                else
                {
                    if (r == "")
                        r = "X and Y";
                    else
                        r = r + " or (X and Y)";

                    if (IPV4.Checked && IPV6.Checked)
                        r = r.Replace("X", "(ip or ip6)");
                    else if (IPV4.Checked)
                        r = r.Replace("X", "ip");
                    else
                        r = r.Replace("X", "ip6");


                    if (TCP.Checked && UDP.Checked)
                        r = r.Replace("Y", "(tcp or udp)");
                    else if (TCP.Checked)
                        r = r.Replace("Y", "tcp");
                    else
                        r = r.Replace("Y", "udp");

                }
            }
            else
            {
                if (TCP.Checked)
                {
                    if (r == "")
                        r = "tcp";
                    else
                        r = r + " or tcp";
                }
                if (UDP.Checked)
                {
                    if (r == "")
                        r = "udp";
                    else
                        r = r + " or udp";
                }
            }
            //端口和源、目的地址的过滤字符串
            string temp = "";
                if (MACAdrS.Text.Trim() != "")
                    temp = temp + " and ether src " + MACAdrS.Text.Trim().ToString();
                if (IPAdrS.Text.Trim() != "")
                    temp = temp + " and src host " + IPAdrS.Text.Trim().ToString();
                if (PORTNumS.Text.Trim() != "")
                    temp = temp + " and src port " + PORTNumS.Text.Trim().ToString();
                if (MACAdrD.Text.Trim() != "")
                    temp = temp + " and ether dst " + MACAdrD.Text.Trim();
                if (IPAdrD.Text.Trim() != "")
                    temp = temp + " and dst host " + IPAdrD.Text.Trim().ToString();
                if (PORTNumD.Text.Trim() != "")
                    temp = temp + " and dst port " + PORTNumD.Text.Trim().ToString();
           
            if (temp != "")
                if (r == "")
                    r = temp.Substring(5, temp.Length - 5);
                else if (r.IndexOf(" or ") == -1)
                    r = r + temp;
                else
                    r = "(" + r + ")" + temp;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        //点击apply，显示框显示过滤字符串
        private void button1_Click(object sender, EventArgs e)
        {
            GenerateRule();
            Expression.Text = r;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void MACAdrD_TextChanged(object sender, EventArgs e)
        {

        }

        //点击ok，应用过滤字符串
        private void OK_Click(object sender, EventArgs e)
        {
            if (Expression.Text == "")
            {
                MessageBox.Show("No filter rules applied!");
                this.Close();
            }
            mainform.filterrules = Expression.Text.Trim();
            //委托函数，用来跨窗口调用
            SetMainFormTopMost(true);
            this.Close();
        }

        //清除所有勾选规则
        private void Clear_Click(object sender, EventArgs e)
        {
            r = "";
            ARP.Checked = false;
            ICMP.Checked = false;
            IPV4.Checked = false;
            IPV6.Checked = false;
            TCP.Checked = false;
            UDP.Checked = false;
            MACAdrS.Clear();
            MACAdrD.Clear();
            IPAdrS.Clear();
            IPAdrD.Clear();
            PORTNumS.Clear();
            PORTNumD.Clear();

        }

        private void CLOSE_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
