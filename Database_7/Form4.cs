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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private string WhereClassroom()
        {
            return "[Корпус] = @param1 and [Этаж] = @param2 and [Номер] = @param3";
        }

        private List<String> GetClassroom()
        {
            List<String> res = new List<String>();
            List<String> qparams = new List<String>();
            qparams.Add(textBox1.Text);
            qparams.Add(textBox2.Text);
            qparams.Add(textBox3.Text);
            try
            {
                res = SelectFromDB("Аудитория", WhereClassroom(), qparams, 5);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
            return res;
        }

        private void PushClassroom()
        {
            try
            {
                List<String> qparams = new List<string>();
                qparams.Add(textBox1.Text);
                qparams.Add(textBox2.Text);
                qparams.Add(textBox3.Text);
                qparams.Add(textBox4.Text);
                qparams.Add(textBox5.Text);
                InsertIntoDB("Аудитория", qparams);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
        }

        private void UpdateClassroom()
        {
            string query = "UPDATE Аудитория SET [Тип аудитории] = @param4, [Вместимость] = " +
                "@param5 WHERE " + WhereClassroom() + ";";
            List<String> qparams = new List<string>();
            qparams.Add(textBox4.Text);
            qparams.Add(textBox5.Text);
            qparams.Add(textBox1.Text);
            qparams.Add(textBox2.Text);
            qparams.Add(textBox3.Text);
            try
            {
                ExecuteQuery(query, qparams);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
        }

        private void DeleteClassroom()
        {
            List<String> qparams = new List<string>();
            qparams.Add(textBox1.Text);
            qparams.Add(textBox2.Text);
            qparams.Add(textBox3.Text);
            qparams.Add(textBox4.Text);
            qparams.Add(textBox5.Text);
            try
            {
                DeleteFromDB("Аудитория", WhereClassroom(), qparams);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            HandleSelectResult(GetClassroom(), listBox1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            PushClassroom();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            UpdateClassroom();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            DeleteClassroom();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 b5 = new Form5();
            b5.Location = this.Location;
            b5.StartPosition = FormStartPosition.Manual;
            b5.FormClosing += delegate { this.Show(); };
            b5.Show();
            
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form3 b3 = new Form3();
            b3.Show();
            b3.Location = this.Location;
            b3.StartPosition = FormStartPosition.Manual;
            b3.FormClosing += delegate { this.Show(); };
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
