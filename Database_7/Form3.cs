using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;

using static Database_7.Program;

namespace Database_7
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private string WhereSubject()
        {
            return "[Идентификатор предмета] = @param1";
        }

        private List<String> GetSubject()
        {
            List<String> res = new List<String>();
            List<String> qparams = new List<String>();
            qparams.Add(textBox1.Text);
            try
            {
                res = SelectFromDB("Предмет", WhereSubject(), qparams, 4);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
            return res;
        }

        private void PushSubject()
        {
            try
            {
                List<String> qparams = new List<string>();
                qparams.Add(textBox1.Text);
                qparams.Add(textBox2.Text);
                qparams.Add(textBox3.Text);
                qparams.Add(textBox4.Text);
                InsertIntoDB("Предмет", qparams);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
        }

        private void UpdateSubject()
        {
            string query = "UPDATE Предмет SET [Название предмета] = @param2, [Короткое название предмета] = " +
                "@param3, [Количество часов] = @param4 WHERE " + WhereSubject() + ";";
            List<String> qparams = new List<string>();
            qparams.Add(textBox2.Text);
            qparams.Add(textBox3.Text);
            qparams.Add(textBox4.Text);
            qparams.Add(textBox1.Text);
            try
            {
                ExecuteQuery(query, qparams);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
        }

        private void DeleteSubject()
        {
            List<String> qparams = new List<string>();
            qparams.Add(textBox1.Text);
            qparams.Add(textBox2.Text);
            qparams.Add(textBox3.Text);
            qparams.Add(textBox4.Text);
            try
            {
                DeleteFromDB("Предмет", WhereSubject(), qparams);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            HandleSelectResult(GetSubject(), listBox1);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            PushSubject();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            UpdateSubject();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            DeleteSubject();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Form4 b4 = new Form4();
            b4.Location = this.Location;
            b4.StartPosition = FormStartPosition.Manual;
            b4.FormClosing += delegate { this.Show(); };
            b4.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 b2 = new Form2();
            b2.Show();
            b2.Location = this.Location;
            b2.StartPosition = FormStartPosition.Manual;
            b2.FormClosing += delegate { this.Show(); };
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
