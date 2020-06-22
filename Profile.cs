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
    public partial class Profile : Form
    {
        string ed2 = "";
        private SQLiteConnection DB;
        public Profile()
        {
            InitializeComponent();
        }
        private void Profile_Load(object sender, EventArgs e)
        {
            if (ed2 != null && ed2 != "")
            {
                button1.Text = "Редактировать";
            }
            DB = new SQLiteConnection("Data Source=testBD.db; Version=3");
            DB.Open();
            SQLiteCommand CMD1 = DB.CreateCommand();

            
            CMD1.CommandText = "select Name from GRADE";
            SQLiteDataReader SQL = CMD1.ExecuteReader();
            while (SQL.Read())
            {
                comboBox1.Items.Add(SQL["Name"]);         //СтолбецТаблицы
            }
            SQLiteCommand CMD2 = DB.CreateCommand();
            CMD2.CommandText = "select Room from ROOMS";
            SQLiteDataReader SQL2 = CMD2.ExecuteReader();
            while (SQL2.Read())
            {
                comboBox2.Items.Add(SQL2["Room"]);         //СтолбецТаблицы
            }
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
                    check.CommandText = "select Subjects from TS WHERE Idteacher like '" + ed2 + "'";
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
            if (ed2 != null && ed2 != "")
            {
                SQLiteCommand CMD4 = DB.CreateCommand();
                CMD4.CommandText = "select * from TEACHERS where Id like '" + ed2 + "'";
                SQLiteDataReader SQL4 = CMD4.ExecuteReader();
                while (SQL4.Read())
                {
                    textBox1.Text = (SQL4["Fio"]).ToString();         //СтолбецТаблицы
                    textBox2.Text = (SQL4["Lesson"]).ToString();
                    comboBox1.Text = (SQL4["Grade"]).ToString();
                    comboBox2.Text = (SQL4["Room"]).ToString();
                }
                
                
            }
        }
        public string edit
        {
            get { return ed2; }
            set { ed2 = value; }
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                if (ed2 == "")
                {
                    SQLiteCommand CMD = DB.CreateCommand();
                    CMD.CommandText = "insert into TEACHERS (Fio, Lesson, Grade, Room) values( @fio ,@Lesson , @Grade, @Room )";
                    CMD.Parameters.Add("@fio", DbType.String).Value = textBox1.Text;
                    CMD.Parameters.Add("@Lesson", DbType.String).Value = textBox2.Text.ToUpper();


                    CMD.Parameters.Add("@Grade", DbType.String).Value = comboBox1.Text.ToUpper();
                    CMD.Parameters.Add("@Room", DbType.String).Value = comboBox2.Text.ToUpper();

                    CMD.ExecuteNonQuery();
                    this.Hide();
                    successfully succ = new successfully();
                    succ.ShowDialog();

                    SQLiteCommand CMD4 = DB.CreateCommand();
                    SQLiteCommand Teacherid = DB.CreateCommand();
                    Teacherid.CommandText = "select Id from TEACHERS where Fio LIKE @fio";
                    Teacherid.Parameters.Add("@fio", DbType.String).Value = textBox1.Text;
                    SQLiteDataReader SQL2 = Teacherid.ExecuteReader();
                    string q = "";
                    if (SQL2.HasRows)
                    {
                        while (SQL2.Read())
                        {
                            q = SQL2.GetValue(0).ToString();
                        }
                    }

                    //label6.Text = q;
                    foreach (string s in checkedListBox1.CheckedItems)
                    {

                        CMD4.CommandText = "insert into TS (Idteacher, Subjects) VALUES (@Sub, @Name)";
                        CMD4.Parameters.Add("@Sub", DbType.String).Value = q;
                        CMD4.Parameters.Add("@Name", DbType.String).Value = s;
                        CMD4.ExecuteNonQuery();

                    }
                    Form1 f1 = this.Owner as Form1;
                    f1.Update_Tab_2();
                    this.Close();
                }
                else
                {
                    SQLiteCommand del = DB.CreateCommand();
                    del.CommandText = "UPDATE TEACHERS set Fio = '', Lesson = NULL, Grade = NULL, Room = NULL WHERE Id = @Id; delete from TS where Idteacher = @id";

                    del.Parameters.Add("@Id", DbType.String).Value = ed2;
                    del.ExecuteNonQuery();

                    SQLiteCommand CMD = DB.CreateCommand();
                    CMD.CommandText = "UPDATE TEACHERS set Fio = @fio, Lesson = @Lesson, Grade = @Grade, Room = @Room where Id = '" + ed2 + "'";
                    CMD.Parameters.Add("@fio", DbType.String).Value = textBox1.Text;
                    CMD.Parameters.Add("@Lesson", DbType.String).Value = textBox2.Text.ToUpper();
                    CMD.Parameters.Add("@Grade", DbType.String).Value = comboBox1.Text.ToUpper();
                    CMD.Parameters.Add("@Room", DbType.String).Value = comboBox2.Text.ToUpper();
                    CMD.ExecuteNonQuery();

                    this.Hide();

                    successfully succ = new successfully();
                    succ.ShowDialog();

                    SQLiteCommand CMD4 = DB.CreateCommand();
                    SQLiteCommand Teacherid = DB.CreateCommand();
                    Teacherid.CommandText = "select Id from TEACHERS where Fio LIKE @fio";
                    Teacherid.Parameters.Add("@fio", DbType.String).Value = textBox1.Text;
                    SQLiteDataReader SQL2 = Teacherid.ExecuteReader();
                    string q = "";
                    if (SQL2.HasRows)
                    {
                        while (SQL2.Read())
                        {
                            q = SQL2.GetValue(0).ToString();
                        }
                    }

                    //label6.Text = q;
                    foreach (string s in checkedListBox1.CheckedItems)
                    {

                        CMD4.CommandText = "insert into TS (Idteacher, Subjects) VALUES (@Sub, @Name)";
                        CMD4.Parameters.Add("@Sub", DbType.String).Value = q;
                        CMD4.Parameters.Add("@Name", DbType.String).Value = s;
                        CMD4.ExecuteNonQuery();

                    }
                    Form1 f1 = this.Owner as Form1;
                    f1.Update_Tab_2();
                    this.Close();
                }
            }
        }

   
    }
}
