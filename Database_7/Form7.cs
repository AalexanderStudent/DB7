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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private List<String> GetAssistants()
        {
            List<String> res = new List<String>();
            List<String> qparams = new List<String>();
            try
            {
                res = SelectFromDB("Ассистенты", "1=1", qparams, 6);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
            return res;
        }

        private List<String> GetEmptySubfaculties()
        {
            List<String> res = new List<String>();
            List<String> qparams = new List<String>();
            try
            {
                res = SelectFromDB("[Кафедры без преподавателей SQL6]", "1=1", qparams, 2);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
            return res;
        }

        private List<String> GetLessonsAfter4PM()
        {
            List<String> res = new List<String>();
            List<String> qparams = new List<String>();
            try
            {
                res = SelectFromDB("[Занятия после 4 PM]", "1=1", qparams, 11);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
            return res;
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<String> qparam = new List<String>();
            qparam.Add("Ассистенты");
            if (SystemDatabaseReader("[Name] = @param1", qparam)?.Any() == true)
            { 
                listBox1.Items.Clear();
                HandleSelectResult(GetAssistants(), listBox1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<String> qparam = new List<String>();
            qparam.Add("Кафедры без преподавателей SQL6");
            if (SystemDatabaseReader("[Name] = @param1", qparam)?.Any() == true)
            {
                listBox1.Items.Clear();
                HandleSelectResult(GetEmptySubfaculties(), listBox1);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<String> qparam = new List<String>();
            qparam.Add("Занятия после 4 PM");
            if (SystemDatabaseReader("[Name] = @param1", qparam)?.Any() == true)
            {
                listBox1.Items.Clear();
                HandleSelectResult(GetLessonsAfter4PM(), listBox1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form8 b8 = new Form8();
            b8.Location = this.Location;
            b8.StartPosition = FormStartPosition.Manual;
            b8.FormClosing += delegate { this.Show(); };
            this.Hide();
            b8.Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form6 b6 = new Form6();
            b6.Location = this.Location;
            b6.StartPosition = FormStartPosition.Manual;
            b6.FormClosing += delegate { this.Show(); };
            this.Hide();
            b6.Show();
        }
    }
}
