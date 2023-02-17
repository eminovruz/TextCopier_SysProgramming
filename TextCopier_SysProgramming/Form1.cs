using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextCopier_SysProgramming
{
    public partial class Form1 : Form
    {
        bool isSuspensed = false;
        bool isAborted = false;
        Thread t = null;


        public Form1()
        {
            InitializeComponent();
            t = new Thread(Copy);
        }

        private async void Copy()
        {
            string srcPath = textBox1.Text;
            string destPath = textBox2.Text;

            if (!File.Exists(srcPath))
            {
                MessageBox.Show("File not exists");
                return;
            }

            using (FileStream fsRead = new FileStream(srcPath, FileMode.Open, FileAccess.Read))
            {
                using (FileStream fsWrite = new FileStream(destPath, FileMode.Create, FileAccess.Write))
                {
                    var len = 10;
                    var fileSize = fsRead.Length;
                    byte[] buffer = new byte[len];

                    do
                    {
                        len = fsRead.Read(buffer, 0, buffer.Length);
                        fsWrite.Write(buffer, 0, len);

                        fileSize -= len;

                        Thread.Sleep(5);

                    } while (len != 0);
                }
            }

            await Task.Delay(2000);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            textBox2.Text = openFileDialog1.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                t.Resume();
                MessageBox.Show("Thread Resuming!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong?!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            t.Start();
            progressBar1.Value = 30;
            Thread.Sleep(2000);
            progressBar1.Value = 90;
            Thread.Sleep(1000);
            progressBar1.Value = progressBar1.Maximum;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                t.Suspend();
                MessageBox.Show("Thread Suspended!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong?!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                t.Abort();
                MessageBox.Show("Thread Aborted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong?!");
            }
        }
    }
}
