using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace vkr
{
    public partial class Form2 : Form
    {
        private SQLiteConnection DB;
        public Form2()
        {
            InitializeComponent();
        }

        string year = "";

        private void Form2_Load(object sender, EventArgs e)
        {          
            
            DB = new SQLiteConnection("Data Source=testBD.db; Version=3");
            DB.Open();
            SQLiteCommand CMD = DB.CreateCommand();
            CMD.CommandText = "select Fio from TEACHERS";
            SQLiteDataReader SQL = CMD.ExecuteReader();
            while (SQL.Read())
            {
                comboBox2.Items.Add(SQL["Fio"]);         //СтолбецТаблицы
            }                

            SQLiteCommand CMD2 = DB.CreateCommand();
            CMD2.CommandText = "select Name from GRADE where Name like @Name || '%'";
            CMD2.Parameters.Add("@Name", DbType.String).Value = textBox1.Text + " ";
            SQLiteDataReader SQL2 = CMD2.ExecuteReader();
            int j = 0;
            if (SQL2.HasRows)
            {
                while (SQL2.Read())
                {
                    j++;
                }

            }
            char[] arr = Enumerable.Range(0, 32).Select((x, i) => (char)('а' + i)).ToArray();
            year = textBox1.Text;
            textBox1.Text += " " + arr[j].ToString().ToUpper();

            SQLiteCommand CMD3 = DB.CreateCommand();
            CMD3.CommandText = "select Room from ROOMS";
            SQLiteDataReader SQL3 = CMD3.ExecuteReader();
            if (SQL3.HasRows)
            {
                while (SQL3.Read())
                {
                    comboBox4.Items.Add(SQL3["Room"]);         //СтолбецТаблицы
                }
            }            
        }            

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                if (textBox1.Text != "")
                {
                    SQLiteCommand AddtoTable = DB.CreateCommand();
                    AddtoTable.CommandText = "insert into GRADE (Name, Shift, Academic_Plan, Supervisor, Room,Number_Of_Students) values( @Name, @Shift, @Academic_Plan, @Supervisor, @Room, @Number_Of_Students )";
                    AddtoTable.Parameters.Add("@Name", DbType.String).Value = textBox1.Text.ToUpper();
                    AddtoTable.Parameters.Add("@Shift", DbType.String).Value = comboBox1.Text.ToUpper();
                    AddtoTable.Parameters.Add("@Academic_Plan", DbType.String).Value = textBox2.Text.ToUpper();
                    AddtoTable.Parameters.Add("@Supervisor", DbType.String).Value = comboBox2.Text.ToUpper();
                    AddtoTable.Parameters.Add("@Room", DbType.String).Value = comboBox4.Text.ToUpper();
                    AddtoTable.Parameters.Add("@Number_Of_Students", DbType.String).Value = numericUpDown1.Text.ToUpper();
                    AddtoTable.ExecuteNonQuery();
                    SQLiteCommand AddtoTable1 = DB.CreateCommand();
                    AddtoTable1.CommandText = "INSERT INTO TEACHERSPLAN(Lesson, Hoursaweek, Grade)SELECT Name, Houraweek, Year FROM PLAN WHERE Year like @name";
                    AddtoTable1.Parameters.Add("@Name", DbType.String).Value = year;
                    AddtoTable1.ExecuteNonQuery();
                    SQLiteCommand AddtoTable2 = DB.CreateCommand();
                    AddtoTable2.CommandText = "UPDATE TEACHERSPLAN set Grade = @Name WHERE GRADE = @Year";
                    AddtoTable2.Parameters.Add("@Year", DbType.String).Value = year;
                    AddtoTable2.Parameters.Add("@Name", DbType.String).Value = textBox1.Text.ToUpper();
                    AddtoTable2.ExecuteNonQuery();
                    Form1 f1 = this.Owner as Form1;
                    f1.Update_BD();
                    successfully succ = new successfully();
                    this.Hide();
                    succ.ShowDialog();
                    this.Close();
                }
            }
            catch
            {
                error er = new error();
                er.ShowDialog();
            }
            
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData.ToString() == "Escape")
            {
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }       

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
