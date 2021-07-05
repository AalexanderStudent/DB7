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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }
        private void Form8_Load(object sender, EventArgs e)
        {

        }

        private List<String> GetClassroomByBuilding()
        {
            List<String> res = new List<String>();
            List<String> qparams = new List<String>();
            qparams.Add(textBox1.Text);
            try
            {
                res = SelectFromDB("Аудитория", "[Корпус] = @param1", qparams, 5);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
            return res;
        }

        private List<String> GetGroupsByDepartment()
        {
            List<String> res = new List<String>();
            List<String> qparams = new List<String>();
            qparams.Add(textBox2.Text);
            try
            {
                res = SelectFromDB("Группа", "[Факультет] = @param1", qparams, 3);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
            return res;
        }
        private List<String> GetLessonsByGroup()
        {
            List<String> res = new List<String>();
            List<String> qparams = new List<String>();
            qparams.Add(textBox3.Text);
            qparams.Add(textBox4.Text);
            try
            {
                res = SelectFromDB("Занятие", "[Факультет] = @param1 and [Номер группы] = @param2", qparams, 11);
            }
            catch (OleDbException e)
            {
                ReportOleDbExceptionMessage(e, listBox1);
            }
            return res;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<String> qparam = new List<String>();
            qparam.Add("Аудитории по корпусу");
            if (SystemDatabaseReader("[Name] = @param1", qparam)?.Any() == true)
            {
                listBox1.Items.Clear();
                HandleSelectResult(GetClassroomByBuilding(), listBox1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<String> qparam = new List<String>();
            qparam.Add("Группы по факультету");
            if (SystemDatabaseReader("[Name] = @param1", qparam)?.Any() == true)
            {
                listBox1.Items.Clear();
                HandleSelectResult(GetGroupsByDepartment(), listBox1);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<String> qparam = new List<String>();
            qparam.Add("Занятия по группе");
            if (SystemDatabaseReader("[Name] = @param1", qparam)?.Any() == true)
            {
                listBox1.Items.Clear();
                HandleSelectResult(GetLessonsByGroup(), listBox1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form9 b9 = new Form9();
            b9.Location = this.Location;
            b9.StartPosition = FormStartPosition.Manual;
            b9.FormClosing += delegate { this.Show(); };
            this.Hide();
            b9.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form7 b7 = new Form7();
            b7.Location = this.Location;
            b7.StartPosition = FormStartPosition.Manual;
            b7.FormClosing += delegate { this.Show(); };
            this.Hide();
            b7.Show();
        }
    }
}
