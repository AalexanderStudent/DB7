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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private string WhereProfessor()
        {
            return "[Табельный номер] = @param1";
        }

        private List<String> GetProfessor()
        {
            List<String> res = new List<String>();
            List<String> qparams = new List<String>();
            qparams.Add(textBox1.Text);
            try
            {
                res = SelectFromDB("Преподаватель", WhereProfessor(), qparams, 7);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
            return res;
        }

        private void PushProfessor()
        {
            try
            {
                List<String> qparams = new List<string>();
                qparams.Add(textBox1.Text);
                qparams.Add(textBox2.Text);
                qparams.Add(textBox3.Text);
                qparams.Add(textBox4.Text);
                qparams.Add(textBox5.Text);
                qparams.Add(textBox6.Text);
                qparams.Add(textBox7.Text);
                InsertIntoDB("Преподаватель", qparams);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
        }

        private void UpdateProfessor()
        {
            string query = "UPDATE Преподаватель SET [Фамилия преподавателя] = @param2, [Имя преподавателя] = @param3" +
                ", [Отчество преподавателя] = @param4, [Должность преподавателя] = @param5, [Стаж работы] = @param6" +
                ", [Идентификатор кафедры] = @param7 WHERE " + WhereProfessor() + ";";
            List<String> qparams = new List<string>();
            qparams.Add(textBox2.Text);
            qparams.Add(textBox3.Text);
            qparams.Add(textBox4.Text);
            qparams.Add(textBox5.Text);
            qparams.Add(textBox6.Text);
            qparams.Add(textBox7.Text);
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

        private void DeleteProfessor()
        {
            List<String> qparams = new List<string>();
            qparams.Add(textBox1.Text);
            try
            {
                DeleteFromDB("Преподаватель", WhereProfessor(), qparams);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            HandleSelectResult(GetProfessor(), listBox1);
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            PushProfessor();
            listBox1.Items.Add("success"); // 
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            UpdateProfessor();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            DeleteProfessor();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 b3 = new Form3();
            b3.Show();
            b3.Location = this.Location;
            b3.StartPosition = FormStartPosition.Manual;
            b3.FormClosing += delegate { this.Show(); };
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 b1 = new Form1();
            b1.Show();
            b1.Location = this.Location;
            b1.StartPosition = FormStartPosition.Manual;
            b1.FormClosing += delegate { this.Show(); };
            this.Hide();
        }
    }
}
