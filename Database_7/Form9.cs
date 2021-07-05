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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {

        }

        private void PushStandardClassroom()
        {
            try
            {
                List<String> qparams = new List<string>();
                qparams.Add(textBox1.Text);
                qparams.Add(textBox2.Text);
                qparams.Add(textBox3.Text);
                string columns = "Корпус, Этаж, Номер, [Тип аудитории], Вместимость";
                string complexPart = "VALUES (@param1, @param2, @param3, " + Txt("Для семинаров") + ", 32)";
                InsertIntoDBComplex("Аудитория", columns, complexPart, qparams);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
        }

        private void AddStudent()
        {
            string query = "UPDATE Группа SET [Количество студентов] = [Количество студентов] + 1 " +
                "WHERE Факультет = @param1 And [Номер группы] = @param2;";
            List<String> qparams = new List<string>();
            qparams.Add(textBox4.Text);
            qparams.Add(textBox5.Text);
            try
            {
                ExecuteQuery(query, qparams);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
        }

        private void DeleteLessonsByDay()
        {
            string condition = "[День проведения] = @param1";
            List<String> qparams = new List<string>();
            qparams.Add(textBox6.Text);
            try
            {
                DeleteFromDB("Занятие", condition, qparams);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<String> qparam = new List<String>();
            qparam.Add("insert стандартная аудитория");
            if (SystemDatabaseReader("[Name] = @param1", qparam)?.Any() == true)
            {
                PushStandardClassroom();
                listBox1.Items.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<String> qparam = new List<String>();
            qparam.Add("update 5 add student SQL6");
            if (SystemDatabaseReader("[Name] = @param1", qparam)?.Any() == true)
            {
                AddStudent();
                listBox1.Items.Clear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<String> qparam = new List<String>();
            qparam.Add("Удалить уроки в день");
            if (SystemDatabaseReader("[Name] = @param1", qparam)?.Any() == true)
            {
                DeleteLessonsByDay();
                listBox1.Items.Clear();
            }
            else
            {
                Console.WriteLine("Query not found");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form8 b8 = new Form8();
            b8.Location = this.Location;
            b8.StartPosition = FormStartPosition.Manual;
            b8.FormClosing += delegate { this.Show(); };
            this.Hide();
            b8.Show();
        }
    }
}
