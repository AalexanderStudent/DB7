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
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private string WhereSubfaculty()
        {
            return "[Идентификатор кафедры] = @param1";
        }

        private List<String> GetSubfaculty()
        {
            List<String> res = new List<String>();
            List<String> qparams = new List<String>();
            qparams.Add(textBox1.Text);
            try
            {
                res = SelectFromDB("Кафедра", WhereSubfaculty(), qparams, 4);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
            return res;
        }

        private void PushSubfaculty()
        {
            try
            {
                List<String> qparams = new List<string>();
                qparams.Add(textBox1.Text);
                qparams.Add(textBox2.Text);
                qparams.Add(textBox3.Text);
                qparams.Add(textBox4.Text);
                InsertIntoDB("Кафедра", qparams);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
        }

        private void UpdateSubfaculty()
        {
            string query = "UPDATE Кафедра SET [Название кафедры] = @param2, [Сокращённое название] = @param3" +
                ", [Количество ставок] = @param4 WHERE [Идентификатор кафедры] = @param1;";
            try
            {
                OleDbConnection cn = ConnectToMyDatabase();
                try
                {
                    List<String> qparams = new List<string>();
                    qparams.Add(textBox2.Text);
                    qparams.Add(textBox3.Text);
                    qparams.Add(textBox4.Text);
                    qparams.Add(textBox1.Text);
                    cn.Open();
                    OleDbCommand cmd = CreateOleDbCommand(cn, query);
                    cmd = AddParams(cmd, qparams);
                    //Console.WriteLine("{0}", cmd.CommandText); //
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    cn.Close();
                }
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
        }

        private void DeleteSubfaculty()
        {
            List<String> qparams = new List<string>();
            qparams.Add(textBox1.Text);
            try
            {
                DeleteFromDB("Кафедра", WhereSubfaculty(), qparams);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            HandleSelectResult(GetSubfaculty(), listBox1);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            PushSubfaculty();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            UpdateSubfaculty();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            DeleteSubfaculty();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Form2 b2 = new Form2();
            b2.Location = this.Location;
            b2.StartPosition = FormStartPosition.Manual;
            b2.FormClosing += delegate { this.Show(); };
            this.Hide();
            b2.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form0 b0 = new Form0();
            b0.Location = this.Location;
            b0.StartPosition = FormStartPosition.Manual;
            b0.FormClosing += delegate { this.Show(); };
            this.Hide();
            b0.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

// buttons uppercase? --
// function with _ - wrong \/
// exception handling \/
// previous \/
// next are done, right \/
// text boxes names \/
// parsing datetime \/
// inline + wherefaculty \/
// if select empty
// success messages / check with data in the fields
// success even when exception
// add exit
// delete with none corresponding to where - no message
// - is excessive if it is after space
// finish form6. It is not finished
// date and time conversion exceptions in Form6
// add "back" on Task1... 
// copy new queries back in lab6
    // " Для определения наличия в БД соответствующих запросов использовать выборку из таблицы MSysObjects. "
    // add checks accordingly
