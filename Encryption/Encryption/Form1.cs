using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using EncryptLib;

namespace Encryption
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Encrypt.Seed = (byte)new Random().Next(1, 256);
            label1.Text = "Current seed : " + Encrypt.Seed.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(folderBrowserDialog1.SelectedPath))
            {
                foreach (String file in Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*", SearchOption.AllDirectories))
                {
                    Encrypt.EncryptFile(file);
                }
            }
            else if (File.Exists(openFileDialog1.FileName))
            {
                Encrypt.EncryptFile(openFileDialog1.FileName);
            }
            else
            {
                button1.Text = "File not found :c";
                button2.Enabled = false;
                button1.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(Directory.Exists(folderBrowserDialog1.SelectedPath))
            {
                foreach (String file in Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*", SearchOption.AllDirectories))
                {
                    Encrypt.DecryptFile(file);
                }
            }
            else if (File.Exists(openFileDialog1.FileName))
            {
                Encrypt.DecryptFile(openFileDialog1.FileName);
            }
            else
            {
                button2.Text = "File not found :c";
                button2.Enabled = false;
                button1.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox1.Text = openFileDialog1.FileName;
            button1.Enabled = true;
            button1.Text = "Encrypt";
            textBox2_TextChanged(null, null);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int temp;
            if(textBox2.TextLength > 0 && button1.Enabled == true && int.TryParse(textBox2.Text, out temp))
            {
                Encrypt.Seed = (byte)temp;
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            if(folderBrowserDialog1.SelectedPath.Length > 0)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
                button1.Enabled = true;
            }
        }
    }
}
