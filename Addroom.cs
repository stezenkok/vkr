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
    public partial class Addroom : Form
    {
        string ed2,ed1 = "";
        private SQLiteConnection DB;
        public Addroom()
        {
            InitializeComponent();
        }
        public string edit
        {
            get { return ed2; }
            set { ed2 = value; }
        }
        public string edit1
        {
            get { return ed1; }
            set { ed1 = value; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {

                if (ed2 == "" || ed2 == null)
                {
                    SQLiteCommand CMD = DB.CreateCommand();
                    CMD.CommandText = "insert into ROOMS (Room, Type) values(@Room, @Type )";

                    CMD.Parameters.Add("@Type", DbType.String).Value = textBox2.Text.ToUpper();
                    CMD.Parameters.Add("@Room", DbType.String).Value = textBox1.Text.ToUpper();
                    CMD.ExecuteNonQuery();

                    this.Hide();
                    successfully succ = new successfully();
                    succ.ShowDialog();

                    SQLiteCommand CMD4 = DB.CreateCommand();
                    foreach (string s in checkedListBox1.CheckedItems)
                    {

                        CMD4.CommandText = "insert into RS (Idroom, Subjects) VALUES (@Sub, @Name)";
                        CMD4.Parameters.Add("@Sub", DbType.String).Value = textBox1.Text.ToUpper();
                        CMD4.Parameters.Add("@Name", DbType.String).Value = s;
                        CMD4.ExecuteNonQuery();

                    }
                    Form1 f1 = this.Owner as Form1;
                    f1.Update_Room();
                    this.Close();
                }
                else
                {
                    
                    SQLiteCommand CMD = DB.CreateCommand();
                    CMD.CommandText = "UPDATE ROOMS set Room = @R, Type = @T where Room = '" + ed2 + "';delete from RS where Idroom = @Room";
                    CMD.Parameters.Add("@R", DbType.String).Value = textBox1.Text.ToUpper();
                    CMD.Parameters.Add("@Room", DbType.String).Value = ed2;
                    CMD.Parameters.Add("@T", DbType.String).Value = textBox2.Text.ToUpper();

                    CMD.ExecuteNonQuery();

                    this.Hide();

                    successfully succ = new successfully();
                    succ.ShowDialog();

                    SQLiteCommand CMD4 = DB.CreateCommand();
                 
                    //label6.Text = q;
                    foreach (string s in checkedListBox1.CheckedItems)
                    {

                        CMD4.CommandText = "insert into RS (Idroom, Subjects) VALUES (@Sub, @Name)";
                        CMD4.Parameters.Add("@Sub", DbType.String).Value = textBox1.Text.ToUpper();
                        CMD4.Parameters.Add("@Name", DbType.String).Value = s;
                        CMD4.ExecuteNonQuery();

                    }
                    Form1 f1 = this.Owner as Form1;
                    f1.Update_Room2();
                    f1.Update_Room();

                    this.Close();
                }
            }
        }

        private void Addroom_Load(object sender, EventArgs e)
        {
            if (ed2 != null && ed2 != "")
            {
                button1.Text = "Редактировать";
                textBox1.Text = ed2;
                textBox2.Text = ed1;
            }
            DB = new SQLiteConnection("Data Source=testBD.db; Version=3");
            DB.Open();

           
            SQLiteCommand CMD3 = DB.CreateCommand();
            CMD3.CommandText = "select NAME from LESSON";
            SQLiteDataReader SQL3 = CMD3.ExecuteReader();


            while (SQL3.Read())
            {
                if (ed2 == null && ed2 == "")
                {
                    checkedListBox1.Items.Add(SQL3["NAME"]);
                }
                else
                {
                    SQLiteCommand check = DB.CreateCommand();
                    check.CommandText = "select Subjects from RS WHERE Idroom like '" + ed2 + "'";
                    SQLiteDataReader SQLcheck = check.ExecuteReader();
                    int q = 0;
                    while (SQLcheck.Read())
                    {
                        if (SQL3["NAME"].ToString() == SQLcheck["Subjects"].ToString())
                        {
                            q = 1;
                            checkedListBox1.Items.Add(SQL3["NAME"], CheckState.Checked);
                            break;
                        }
                    }
                    if (q == 0)
                    {
                        checkedListBox1.Items.Add(SQL3["NAME"]);
                    }
                }
            }
           
        }
    }
}
