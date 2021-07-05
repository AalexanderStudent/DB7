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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }
        private string WhereLesson()
        {
            return "[День проведения] = @param1 and [Время проведения] = @param2 and [Идентификатор предмета] = " +
                "@param3 and [Табельный номер преподавателя] = @param4 and [Идентификатор кафедры] = @param5 " +
                "and [Корпус] = @param6 and [Этаж] = @param7 and [Номер аудитории] = @param8 and [Факультет] = " +
                "@param9 and [Номер группы] = @param10";
        }

        private List<String> GetLesson() 
        {
            List<String> res = new List<String>();
            List<String> qparams = new List<String>();
            qparams.Add(textBox1.Text);
            qparams.Add(textBox2.Text);
            qparams.Add(textBox3.Text);
            qparams.Add(textBox4.Text);
            qparams.Add(textBox5.Text);
            qparams.Add(textBox6.Text);
            qparams.Add(textBox7.Text);
            qparams.Add(textBox8.Text);
            qparams.Add(textBox9.Text);
            qparams.Add(textBox10.Text);
            try
            {
                res = SelectFromDB("Занятие", WhereLesson(), qparams, 11);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
                Console.WriteLine(e);
            }
            return res;
        }

        private void PushLesson()
        {
            string date = "convert(datetime, " + textBox1.Text + ", 3)";
            string time = "convert(datetime, " + textBox2.Text + ", 8)";
            List<String> qparams = new List<String>();
            qparams.Add(textBox1.Text);
            qparams.Add(textBox2.Text);
            qparams.Add(textBox3.Text);
            qparams.Add(textBox4.Text);
            qparams.Add(textBox5.Text);
            qparams.Add(textBox6.Text);
            qparams.Add(textBox7.Text);
            qparams.Add(textBox8.Text);
            qparams.Add(textBox9.Text);
            qparams.Add(textBox10.Text);
            qparams.Add(textBox11.Text);
            foreach (string i in qparams)
                Console.WriteLine(i);
            try
            {
                InsertIntoDB("Занятие", qparams);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
        }

        private void UpdateLesson()
        {
            string query = "UPDATE Занятие SET [Тип] = @param11 WHERE [День проведения] = @param1 and " +
                "[Время проведения] = @param2 and [Идентификатор предмета] = @param3 and " +
                "[Табельный номер преподавателя] = @param4 and [Идентификатор кафедры] = @param5 " +
                "and [Корпус] = @param6 and [Этаж] = @param7 and [Номер аудитории] = @param8 and [Факультет] = " +
                "@param9 and [Номер группы] = @param10;";
            //string date = "convert(datetime, " + textBox1.Text + ", 3)";
            //string time = "convert(datetime, " + textBox2.Text + ", 8)";
            List<String> qparams = new List<String>();
            qparams.Add(textBox11.Text);
            qparams.Add(textBox1.Text);
            qparams.Add(textBox2.Text);
            qparams.Add(textBox3.Text);
            qparams.Add(textBox4.Text);
            qparams.Add(textBox5.Text);
            qparams.Add(textBox6.Text);
            qparams.Add(textBox7.Text);
            qparams.Add(textBox8.Text);
            qparams.Add(textBox9.Text);
            qparams.Add(textBox10.Text);
            try
            {
                ExecuteQuery(query, qparams);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
        }

        private void DeleteLesson()
        {
            //string date = "convert(datetime, " + textBox1.Text + ", 3)";
            //string time = "convert(datetime, " + textBox2.Text + ", 8)";
            List<String> qparams = new List<String>();
            qparams.Add(textBox1.Text);
            qparams.Add(textBox2.Text);
            qparams.Add(textBox3.Text);
            qparams.Add(textBox4.Text);
            qparams.Add(textBox5.Text);
            qparams.Add(textBox6.Text);
            qparams.Add(textBox7.Text);
            qparams.Add(textBox8.Text);
            qparams.Add(textBox9.Text);
            qparams.Add(textBox10.Text);
            try
            {
                DeleteFromDB("Занятие", WhereLesson(), qparams);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            HandleSelectResult(GetLesson(), listBox1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            PushLesson();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            UpdateLesson();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            DeleteLesson();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form5 b5 = new Form5();
            b5.Show();
            b5.Location = this.Location;
            b5.StartPosition = FormStartPosition.Manual;
            b5.FormClosing += delegate { this.Show(); };
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
