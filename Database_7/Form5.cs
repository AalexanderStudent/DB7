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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private string WhereGroup()
        {
            return "[Факультет] = @param1 and [Номер группы] = @param2";
        }

        private List<String> GetGroup()
        {
            List<String> res = new List<String>();
            List<String> qparams = new List<String>();
            qparams.Add(textBox1.Text);
            qparams.Add(textBox2.Text);
            try
            {
                res = SelectFromDB("Группа", WhereGroup(), qparams, 3);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
            return res;
        }

        private void PushGroup()
        {
            try
            {
                List<String> qparams = new List<String>();
                qparams.Add(textBox1.Text);
                qparams.Add(textBox2.Text);
                qparams.Add(textBox3.Text);
                InsertIntoDB("Группа", qparams);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
        }

        private void UpdateGroup()
        {
            string query = "UPDATE Группа SET [Количество студентов] = @param3 WHERE " + WhereGroup() + ";";
            List<String> qparams = new List<String>();
            qparams.Add(textBox3.Text);
            qparams.Add(textBox1.Text);
            qparams.Add(textBox2.Text);
            try
            {
                ExecuteQuery(query, qparams);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
        }

        private void DeleteGroup()
        {
            List<String> qparams = new List<String>();
            qparams.Add(textBox1.Text);
            qparams.Add(textBox2.Text);
            qparams.Add(textBox3.Text);
            try
            {
                DeleteFromDB("Группа", WhereGroup(), qparams); 
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            HandleSelectResult(GetGroup(), listBox1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            PushGroup();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            UpdateGroup();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            DeleteGroup();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form6 b6 = new Form6();
            b6.Location = this.Location;
            b6.StartPosition = FormStartPosition.Manual;
            b6.FormClosing += delegate { this.Show(); };
            b6.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form4 b4 = new Form4();
            b4.Show();
            b4.Location = this.Location;
            b4.StartPosition = FormStartPosition.Manual;
            b4.FormClosing += delegate { this.Show(); };
            this.Hide();
        }
    }
}
 