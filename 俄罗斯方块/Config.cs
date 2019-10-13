using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Pluz;
namespace Pluz
{
    public partial class Config : Form
    {
        public Config()
        {
            InitializeComponent();
        }

        private void Config_Load(object sender, EventArgs e)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load("Config.xml");
            XmlNode root = xml.DocumentElement;
            textBox1.Text = root["change"].InnerText;
            textBox2.Text = root["down"].InnerText;
            textBox3.Text = root["left"].InnerText;
            textBox4.Text = root["right"].InnerText;
            textBox5.Text = root["shoot"].InnerText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(textBox7.Text) || string.IsNullOrEmpty(textBox8.Text) || string.IsNullOrEmpty(textBox9.Text) || string.IsNullOrEmpty(textBox10.Text))
            {
                MessageBox.Show("不能为空！");
            }
            else
            {
                XmlDocument xml = new XmlDocument();
                xml.Load("Config.xml");
                XmlNode root = xml.DocumentElement;
                root["change"].InnerText = textBox6.Text.ToLower() ;
                root["down"].InnerText = textBox7.Text.ToLower();
                root["left"].InnerText = textBox8.Text.ToLower();
                root["right"].InnerText = textBox9.Text.ToLower();
                root["shoot"].InnerText = textBox10.Text.ToLower();
                xml.Save("Config.xml");
                MessageBox.Show("修改完成");
            }
        }

        private void Config_FormClosing(object sender, FormClosingEventArgs e)
        {

            DialogResult dr = MessageBox.Show("是否关闭？");
            if (dr == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(textBox7.Text) || string.IsNullOrEmpty(textBox8.Text) || string.IsNullOrEmpty(textBox9.Text) || string.IsNullOrEmpty(textBox10.Text))
                {
                    MessageBox.Show("没有做任何修改");
                }
                else
                {
                    XmlDocument xml = new XmlDocument();
                    xml.Load("Config.xml");
                    XmlNode root = xml.DocumentElement;
                    root["change"].InnerText = textBox6.Text;
                    root["down"].InnerText = textBox7.Text;
                    root["left"].InnerText = textBox8.Text;
                    root["right"].InnerText = textBox9.Text;
                    root["shoot"].InnerText = textBox10.Text;
                    xml.Save("Config.xml");
                    MessageBox.Show("保存完成");
                }
            }
        }
    }
}