using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Sniffer
{
    public partial class Save : Form
    {
        public Save()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)//save按钮
        {
            if (mainform.saveflag == true)//saveflag=true 从主页面点击时进入
            {
                StreamWriter sw = new StreamWriter(textBox1.Text, true);
                for (int c = 0; c < mainform.numofpacketsaved; c++)
                {
                    string s1 = "********No." + (c + 1).ToString() + "********";
                    string s2 = mainform.Hexcode[c];
                    string s3 = mainform.Details[c];
                    sw.WriteLine(s1);
                    sw.Write(s2);
                    sw.WriteLine("");
                    sw.Write(s3);
                    sw.WriteLine(""); sw.WriteLine(""); sw.WriteLine("");
                    sw.Flush();
                }
                sw.Close();
                MessageBox.Show("Captured packet saved! See it in " + textBox1.Text + " !");
                this.Close();
            }
            else
            {//从IP fragment或TCP/UDP Stream界面进入
                StreamWriter sw = new StreamWriter(textBox1.Text, true);
                for (int c = 0; c < IPfragment.fragmentnum; c++)
                {
                    string s2 = mainform.Hexcode[IPfragment.fragmentindex[c]];
                    sw.Write(s2);
                    sw.WriteLine("");
                    sw.Flush();
                }
                sw.WriteLine(""); sw.WriteLine("");
                sw.Write(mainform.Details[IPfragment.fragmentindex[0]]);
                sw.WriteLine("");
                sw.Write("Num of Packets: " + IPfragment.fragmentnum.ToString());
                sw.Close();
                MessageBox.Show("Saved! See it in " + textBox1.Text + " !");
                this.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Save_Load(object sender, EventArgs e)
        {

        }
    }
}
