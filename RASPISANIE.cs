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
    public partial class RASPISANIE : Form
    {
        private SQLiteConnection DB;
        string h,w,g = "";
        int ed = 0;
        public RASPISANIE()
        {
            InitializeComponent();
        }
        public string hour
        {
            get { return h; }
            set { h = value; }
        }
        public string grade
        {
            get { return g; }
            set { g = value; }
        }

        int[] myArr = new int[100];

        private void RASPISANIE_Load(object sender, EventArgs e)
        {
            
            int i = 0;
            DB = new SQLiteConnection("Data Source=testBD.db; Version=3");
            DB.Open();

            SQLiteCommand CMD2 = DB.CreateCommand();

            CMD2.CommandText = "select s.Lesson, s.Grade, s.Time, s.Weekday, s.Room, T.Fio, s.Teacher from RASPISANIE s left join TEACHERS T on s.Teacher = T.Id where s.Grade = @G and s.Time = @H and s.Weekday = @W";
            CMD2.Parameters.Add("@W", DbType.String).Value = week;
            CMD2.Parameters.Add("@H", DbType.String).Value = hour;
            CMD2.Parameters.Add("@G", DbType.String).Value = grade;
            SQLiteDataReader SQL2 = CMD2.ExecuteReader();
            if (SQL2.HasRows)
            {
                while (SQL2.Read())
                {
                    comboBox1.Items.Add(SQL2["Lesson"].ToString() + String.Concat(Enumerable.Repeat("..", 20 - SQL2["Lesson"].ToString().Length)) + SQL2["Fio"] + String.Concat(Enumerable.Repeat("..", 20 - SQL2["Fio"].ToString().Length)) + SQL2["Room"].ToString());         //СтолбецТаблицы
                    ed = 1;
                    comboBox1.SelectedIndex = 0;
                    myArr[i] = Convert.ToInt32(SQL2["Teacher"]);
                }
            }
            else
            {
                SQLiteCommand CMD1 = DB.CreateCommand();

                CMD1.CommandText = "select s.*, T.Fio from TEACHERSPLAN s left join TEACHERS T on s.Idteacher = T.Id where s.Grade = @G";
                CMD1.Parameters.Add("@G", DbType.String).Value = grade;
                SQLiteDataReader SQL = CMD1.ExecuteReader();
                while (SQL.Read())
                {
                    SQLiteCommand CMD3 = DB.CreateCommand();

                    CMD3.CommandText = "select COUNT(*) from RASPISANIE where Grade like @G and Lesson like @L";
                    CMD3.Parameters.Add("@G", DbType.String).Value = grade;
                    CMD3.Parameters.Add("@L", DbType.String).Value = SQL["Lesson"].ToString();
                    SQLiteDataReader SQL3 = CMD3.ExecuteReader();
                    
                    if (SQL3.HasRows)
                    {
                        while (SQL3.Read())
                        {
                            if (Convert.ToInt32(SQL["Hoursaweek"]) > Convert.ToInt32(SQL3.GetValue(0)))
                            {
                                comboBox1.Items.Add(SQL["Lesson"] + String.Concat(Enumerable.Repeat("..", 20 - SQL["Lesson"].ToString().Length)) + SQL["Fio"] + String.Concat(Enumerable.Repeat("..", 20 - SQL["Fio"].ToString().Length)) + SQL["Room"]);
                                myArr[i] = Convert.ToInt32(SQL["Id"]);
                                i++;
                            }
                        }
                    }
                    else
                    {
                        comboBox1.Items.Add(SQL["Lesson"]);
                    }
                }
            }
            if (ed == 1)
            {
                button1.Hide();
                button2.Show();
            }    
        }


        public string week
        {
            get { return w; }
            set { w = value; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int t = myArr[comboBox1.SelectedIndex];
                SQLiteCommand CMD4 = DB.CreateCommand();
                CMD4.CommandText = "delete from RASPISANIE where Grade = @G and Weekday = @W and Time = @T";
                CMD4.Parameters.Add("@T", DbType.String).Value = hour;
                CMD4.Parameters.Add("@W", DbType.String).Value = week;
                CMD4.Parameters.Add("@G", DbType.String).Value = grade;
                CMD4.ExecuteNonQuery();
                Form1 f1 = this.Owner as Form1;
                f1.panel1.Refresh();
                this.Close();

            }
            catch
            {
                //pass
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
            int t = myArr[comboBox1.SelectedIndex];
            SQLiteCommand CMD4 = DB.CreateCommand();
            CMD4.CommandText = "select * from RASPISANIE where Time = @T and Weekday = @W and Room = (select Room FROM TEACHERSPLAN WHERE Id like @Te)";
            CMD4.Parameters.Add("@T", DbType.String).Value = hour;
            CMD4.Parameters.Add("@W", DbType.String).Value = week;
            CMD4.Parameters.Add("@Te", DbType.String).Value = t.ToString();
            SQLiteDataReader SQL4 = CMD4.ExecuteReader();

            if (SQL4.HasRows)
            {
                DialogResult dialogResult = MessageBox.Show("Этот кабинет уже заянт в данное время. Вы все равно хотите добавить ?", "Предупреждение", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {                     
                    add_ras(t);
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                add_ras(t);
            }
            }
            catch
            {
                DialogResult dialogResult = MessageBox.Show("err");
                this.Close();
            }
        }
        public void add_ras(int t)
        {

            SQLiteCommand CMD2 = DB.CreateCommand();
            CMD2.CommandText = "insert into RASPISANIE (Lesson, Grade, Room, Teacher) SELECT Lesson, Grade, Room, Idteacher FROM TEACHERSPLAN WHERE Id like @Te;";
            CMD2.Parameters.Add("@Te", DbType.String).Value = t.ToString();

            CMD2.ExecuteNonQuery();

            SQLiteCommand CMD3 = DB.CreateCommand();
            CMD3.CommandText = "update RASPISANIE set Time = @Ti, Weekday = @W where Time is NULL";
            CMD3.Parameters.Add("@Ti", DbType.String).Value = hour;
            CMD3.Parameters.Add("@W", DbType.String).Value = week;
            CMD3.ExecuteNonQuery();
            Form1 f1 = this.Owner as Form1;
            f1.panel1.Refresh();
            DB.Close();
            this.Close();
        }
    }
}
