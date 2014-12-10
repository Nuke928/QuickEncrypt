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

namespace Encryption
{
    public partial class Form1 : Form
    {
        int seed = 0;

        public Form1()
        {
            InitializeComponent();
            Random rnd = new Random();
            seed = rnd.Next(1, 999);
            label1.Text = "Current seed : " + seed.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(folderBrowserDialog1.SelectedPath))
            {
                encryptFolder("encrypt", folderBrowserDialog1.SelectedPath);
            }
            else if (File.Exists(openFileDialog1.FileName))
            {
                encryptFile("encrypt", openFileDialog1.FileName);
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
                encryptFolder("decrypt", folderBrowserDialog1.SelectedPath);
            }
            else if (File.Exists(openFileDialog1.FileName))
            {
                encryptFile("decrypt", openFileDialog1.FileName);
            }
            else
            {
                button2.Text = "File not found :c";
                button2.Enabled = false;
                button1.Enabled = false;
            }
        }

        private void encryptFolder(String mode, string folderName)
        {
            var folderItems = Directory.GetFiles(folderName, "*", SearchOption.AllDirectories);
            foreach (String f in folderItems)
            {
                encryptFile(mode, f);
            }
        }

        private void encryptFile(String mode, string fileName)
        {
            var fileByte = File.ReadAllBytes(fileName);

            int i = 0;
            if (mode == "encrypt")
            {
                foreach (byte j in fileByte)
                {
                    fileByte[i] += (byte)seed;
                    i++;
                }
            }
            else if(mode == "decrypt")
            {
                foreach (byte j in fileByte)
                {
                    fileByte[i] -= (byte)int.Parse(textBox2.Text);
                    i++;
                }
            }

            File.WriteAllBytes(fileName, fileByte);
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
