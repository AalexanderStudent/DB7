using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database_7
{
    public partial class Form0 : Form
    {
        public Form0()
        {
            InitializeComponent();
            listBox1.Items.Add("Pick part");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 b1 = new Form1();
            b1.Location = this.Location;
            b1.StartPosition = FormStartPosition.Manual;
            b1.FormClosing += delegate { this.Show(); };
            this.Hide();
            b1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form7 b7 = new Form7();
            b7.Location = this.Location;
            b7.StartPosition = FormStartPosition.Manual;
            b7.FormClosing += delegate { this.Show(); };
            this.Hide();
            b7.Show();
        }

        private void Form0_Load(object sender, EventArgs e)
        {

        }
    }
}
