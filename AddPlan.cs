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
    public partial class AddPlan : Form
    {
        string ed, ed1, ed2, id,rd = "";

        public AddPlan()
        {
            InitializeComponent();
        }

        private void AddPlan_Load(object sender, EventArgs e)
        {
            SQLiteConnection DB = new SQLiteConnection("Data Source=testBD.db; Version=3");
            DB.Open();
            SQLiteCommand CMD = DB.CreateCommand();
            CMD.CommandText = "select NAME from LESSON";
            SQLiteDataReader SQL = CMD.ExecuteReader();
            while (SQL.Read())
            {
                comboBox2.Items.Add(SQL["NAME"]);        
            }
            comboBox1.SelectedIndex = 0;
            if (ed != null && ed != "" )
            {
                comboBox2.Text = ed;
                numericUpDown1.Value = Convert.ToInt32(ed1);
                comboBox1.Text = ed2;
            }
            if (rd != "")
            {
                button1.Hide();
                button2.Show();
                label1.Text = rd;
            }
        }
        public string reedit
        {
            get { return rd; }
            set { rd = value; }
        }
        public string InputText
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        public string edit
        {
            get { return ed; }
            set { ed = value; }
        }
        public string edit1
        {
            get { return ed1; }
            set { ed1 = value; }
        }
        public string edit2
        {
            get { return ed2; }
            set { ed2 = value; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SQLiteConnection DB = new SQLiteConnection("Data Source=testBD.db; Version=3");
            DB.Open();
            SQLiteCommand AddtoTable = DB.CreateCommand();
            AddtoTable.CommandText = "insert into TEACHERSPLAN (Lesson, Hoursaweek, Grade) values( @Name, @Houraweek, @Year)";
            AddtoTable.Parameters.Add("@Name", DbType.String).Value = comboBox2.Text.ToUpper();
            AddtoTable.Parameters.Add("@Houraweek", DbType.String).Value = numericUpDown1.Text;
            AddtoTable.Parameters.Add("@Year", DbType.String).Value = rd;
            AddtoTable.ExecuteNonQuery();
            Form1 f1 = this.Owner as Form1;
            f1.Update_BD_1();
            successfully succ = new successfully();
            f1.Update_Grid6();
            this.Hide();
            succ.ShowDialog();
            this.Close();
        }

        public string id1
        {
            get { return id; }
            set { id = value; }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ed == null || ed == "")
            {
                SQLiteConnection DB = new SQLiteConnection("Data Source=testBD.db; Version=3");
                DB.Open();
                SQLiteCommand AddtoTable = DB.CreateCommand();
                AddtoTable.CommandText = "insert into PLAN (Name, Houraweek, Year, Grouptype) values( @Name, @Houraweek, @Year, @Grouptype)";
                AddtoTable.Parameters.Add("@Name", DbType.String).Value = comboBox2.Text.ToUpper();
                AddtoTable.Parameters.Add("@Houraweek", DbType.String).Value = numericUpDown1.Text;
                AddtoTable.Parameters.Add("@Year", DbType.String).Value = label1.Text.Split()[0];
                AddtoTable.Parameters.Add("@Grouptype", DbType.String).Value = comboBox1.Text;
                AddtoTable.ExecuteNonQuery();
                
            }
            else
            {
                SQLiteConnection DB = new SQLiteConnection("Data Source=testBD.db; Version=3");
                DB.Open();
                SQLiteCommand AddtoTable = DB.CreateCommand();
                AddtoTable.CommandText = "UPDATE PLAN set Name = @Name, Houraweek = @Houraweek, Grouptype = @Grouptype WHERE Id = @Id";
                AddtoTable.Parameters.Add("@Name", DbType.String).Value = comboBox2.Text.ToUpper();
                AddtoTable.Parameters.Add("@Houraweek", DbType.String).Value = numericUpDown1.Text;
                AddtoTable.Parameters.Add("@Id", DbType.String).Value = id;
                AddtoTable.Parameters.Add("@Grouptype", DbType.String).Value = comboBox1.Text;
                AddtoTable.ExecuteNonQuery();
               
            }
            Form1 f1 = this.Owner as Form1;
            f1.Update_BD_1();
            successfully succ = new successfully();
            this.Hide();
            succ.ShowDialog();
            this.Close();

        }
    }
}
