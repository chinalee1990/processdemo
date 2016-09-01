using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tested
{
    public partial class Form1 : Form
    {
        string[] strarrCmdLine;
        public Form1()
        {
            InitializeComponent();

            
        }

        public void SetCmdLine(string[] args)
        {
            strarrCmdLine = new string[args.Length];
            strarrCmdLine = args;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var item in strarrCmdLine)
            {
                richTextBox1.Text += item.ToString()+"\r\n";
            }
        }

    }
}
