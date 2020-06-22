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
    public partial class AddLesson : Form
    {
        private SQLiteConnection DB;

        public AddLesson()
        {
            InitializeComponent();
        }
        private void AddLesson_Load(object sender, EventArgs e)
        {
            
        }
            private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "")
                {
                    DB = new SQLiteConnection("Data Source=testBD.db; Version=3");
                    DB.Open();
                    SQLiteCommand AddtoTable = DB.CreateCommand();
                    AddtoTable.CommandText = "insert into LESSON (Name) values( @Name)";
                    AddtoTable.Parameters.Add("@Name", DbType.String).Value = textBox1.Text.ToUpper();
                    AddtoTable.ExecuteNonQuery();
                    Form1 form1 = this.Owner as Form1;
                    form1.Update_Tab_2();
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
    }
}
