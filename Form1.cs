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
using System.Deployment.Application;


namespace vkr
{
    public partial class Form1 : Form
    {
        private SQLiteConnection DB;
        //global start
        string treeView_AfterSelec = ""; 
        //globag end
        public Form1()
        {
            InitializeComponent();
            tabControl_main.Selecting += tabControl_main_Selecting;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer3.Panel1MinSize = 217; //минимальный размер сужения splitContainer3 во вкладке классы
            LoadData();
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //длинна столбцов заполняет все место
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;    // выделяет всю строку

            dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView5.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; 
            dataGridView5.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView6.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView6.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView7.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView7.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView9.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView9.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView8.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView8.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView10.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView10.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView11.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView11.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void LoadData()
        {
            DB = new SQLiteConnection("Data Source=testBD.db; Version=3");
            DB.Open();
            Update_BD();
        }
        
        public void Update_Tab_2()
        {
            treeView4.Nodes[0].Nodes.Clear();
            dataGridView4.Rows.Clear();
            SQLiteCommand comm = DB.CreateCommand();
            comm.CommandText = "select * from LESSON";
            SQLiteDataReader SQL = comm.ExecuteReader();
            if (SQL.HasRows)
            {
                while (SQL.Read())
                {
                    treeView4.Nodes[0].Nodes.Add(new TreeNode(SQL.GetValue(0).ToString()));
                    dataGridView4.Rows.Add(new object[]
                    {
                            SQL.GetValue(0),
                    });                   
                }               
            }

            dataGridView4.Rows.Clear();
            SQLiteCommand comm1 = DB.CreateCommand();
            comm1.CommandText = "select * from TEACHERS";
            SQLiteDataReader SQL1 = comm1.ExecuteReader();            
            try
            {
                int i = 0;
                if (SQL1.HasRows)
                {
                    while (SQL1.Read())
                    {
                        string s = null;
                        i++;

                        SQLiteCommand addtime = DB.CreateCommand();
                        addtime.CommandText = "SELECT sum(Hoursaweek) FROM TEACHERSPLAN WHERE Idteacher like @Id";
                        addtime.Parameters.Add("Id", DbType.String).Value = SQL1.GetValue(0).ToString();
                        SQLiteDataReader SQLtime = addtime.ExecuteReader();

                        if (SQLtime.HasRows)
                        {
                            while (SQLtime.Read())
                            {
                                s = SQLtime.GetValue(0).ToString();
                            }
                        }
                        dataGridView4.Rows.Add(new object[]
                        {
                            
                            SQL1.GetValue(1),
                            SQL1.GetValue(3),
                            SQL1.GetValue(4),
                            SQL1.GetValue(5),
                            SQL1.GetValue(6),
                            SQL1.GetValue(7),
                            s,
                            SQL1.GetValue(0)
                        });                       
                    }                    
                }
            }
            catch
            {
                //pass
            }
        }

        public void Update_Room()
        {
            treeView1.Nodes[0].Nodes.Clear();
            dataGridView9.Rows.Clear();
           
            SQLiteCommand comm1 = DB.CreateCommand();
            comm1.CommandText = "select * from ROOMS";
            SQLiteDataReader SQL1 = comm1.ExecuteReader();

            try
            {
                int i = 0;
                if (SQL1.HasRows)
                {


                    while (SQL1.Read())
                    {
                        string s = null;
                        i++;

                        SQLiteCommand addtime = DB.CreateCommand();
                        addtime.CommandText = "SELECT sum(Hoursaweek) FROM TEACHERSPLAN WHERE Room like @Id";
                        addtime.Parameters.Add("Id", DbType.String).Value = SQL1.GetValue(1).ToString();
                        SQLiteDataReader SQLtime = addtime.ExecuteReader();

                        if (SQLtime.HasRows)
                        {
                            while (SQLtime.Read())
                            {
                                s = SQLtime.GetValue(0).ToString();
                            }
                        }

                        dataGridView9.Rows.Add(new object[]
                        {
                            SQL1.GetValue(1),
                            SQL1.GetValue(2),                           
                            s,
                            SQL1.GetValue(0),
                        });                        
                    }
                    
                }
            }
            catch
            {
                //pass
            }
        }

        public void Update_Room2()
        {
            try
            {                
                string a = dataGridView9[0, dataGridView9.CurrentRow.Index].Value.ToString();
                dataGridView10.Rows.Clear();
                dataGridView11.Rows.Clear();
                SQLiteConnection updateBD = new SQLiteConnection("Data Source=testBD.db; Version=3");
                updateBD.Open();

                SQLiteCommand comm2 = updateBD.CreateCommand();
                comm2.CommandText = "select * from TEACHERSPLAN WHERE Lesson in (select Subjects from RS WHERE Idroom like '" + a + "') and Room is null";
                
                SQLiteDataReader SQL2 = comm2.ExecuteReader();
                if (SQL2.HasRows)
                {
                    while (SQL2.Read())
                    {
                        dataGridView10.Rows.Add(new object[]
                        {
                            SQL2.GetValue(1),

                            SQL2.GetValue(4),
                            SQL2.GetValue(6),
                            SQL2.GetValue(3),
                            SQL2.GetValue(2),
                            SQL2.GetValue(7)
                        });
                    }                    
                }

                SQLiteCommand comm3 = updateBD.CreateCommand();
                comm3.CommandText = "select * from TEACHERSPLAN WHERE Room like '" + a + "'";
                
                SQLiteDataReader SQL3 = comm3.ExecuteReader();
                if (SQL3.HasRows)
                {
                    while (SQL3.Read())
                    {
                        dataGridView11.Rows.Add(new object[]
                        {
                            SQL3.GetValue(1),

                            SQL3.GetValue(4),
                            SQL3.GetValue(6),
                            SQL3.GetValue(3),
                            SQL3.GetValue(2),
                            SQL3.GetValue(7)
                        });
                    }                   
                }
                treeView1.Nodes[0].Nodes.Clear();
                
                SQLiteCommand comm = DB.CreateCommand();
                comm.CommandText = "select Subjects from RS WHERE Idroom like '" + a + "'";
                SQLiteDataReader SQL = comm.ExecuteReader();
                if (SQL.HasRows)
                {
                    while (SQL.Read())
                    {
                        treeView1.Nodes[0].Nodes.Add(new TreeNode(SQL.GetValue(0).ToString()));                      
                    }
                }
                treeView1.ExpandAll();
            }
            catch
            {
                //pass
            }
        }

        private void tabControl_main_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == 2)
            {
                treeView4.Nodes[0].Nodes.Clear();
                Update_Tab_2();
            }

            if (e.TabPageIndex == 3)
            {
                Update_Room();
            }
            if (e.TabPageIndex == 4)
            {
                treeView6.Nodes[0].Nodes.Clear();
                SQLiteCommand comm = DB.CreateCommand();
                comm.CommandText = "select Name from GRADE";
                SQLiteDataReader SQL = comm.ExecuteReader();
                if (SQL.HasRows)
                {
                    while (SQL.Read())
                    {
                        treeView6.Nodes[0].Nodes.Add(new TreeNode(SQL.GetValue(0).ToString()));
                        dataGridView4.Rows.Add(new object[]
                        {
                            SQL.GetValue(0)
                        });
                    }                    
                }
            }
        }

        private void treeView2_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            treeView_AfterSelec = treeView2.SelectedNode.FullPath;
            if (treeView_AfterSelec.Contains('\\') == false)
            {
                treeView_AfterSelec = "";
            }
            Update_BD();
        }

        private void treeView5_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent != null)
            {
                treeView_AfterSelec = e.Node.Text;
                Update_BD_1();
            }            
        }

        public void Update_BD_1()
        {
            SQLiteConnection bd = new SQLiteConnection("Data Source=testBD.db; Version=3");
            bd.Open();
            dataGridView5.Rows.Clear();
            SQLiteCommand comm1 = bd.CreateCommand();
            comm1.CommandText = "select * from PLAN WHERE Year LIKE '" + treeView_AfterSelec.Split()[0] + "'";
            //label8.Text = treeView_AfterSelec.Split()[0];
            try
            {
                SQLiteDataReader SQL1 = comm1.ExecuteReader();
                if (SQL1.HasRows)
                {
                    while (SQL1.Read())
                    {
                        dataGridView5.Rows.Add(new object[]
                        {
                            SQL1.GetValue(0),
                            SQL1.GetValue(1),
                            SQL1.GetValue(3),
                            SQL1.GetValue(4)
                        });
                    }                     
                }                
            }
            catch
            {
                dataGridView5.Rows.Add("Нет данных");
            }
        }

        public void Update_BD()
        {
            SQLiteConnection updateBD = new SQLiteConnection("Data Source=testBD.db; Version=3");
            updateBD.Open();
            dataGridView2.Rows.Clear();
            SQLiteCommand comm = updateBD.CreateCommand();
            comm.CommandText = "select * from GRADE";
            SQLiteDataReader SQL = comm.ExecuteReader();
            if (SQL.HasRows)
            {
                while (SQL.Read())
                {
                    dataGridView2.Rows.Add(new object[]
                    {
                            SQL.GetValue(0),
                            SQL.GetValue(1),
                            SQL.GetValue(2),
                            SQL.GetValue(3),
                            SQL.GetValue(4),
                            SQL.GetValue(5)
                    });                   
                }                
            }            
        }     

        private void buttonAddGrade_Click(object sender, EventArgs e)
        {
            if (treeView_AfterSelec != "")
            {
                Form2 create = new Form2();
                create.Owner = this;
                create.textBox1.Text = treeView2.SelectedNode.Text.Split()[0];
                create.textBox2.Text = treeView2.SelectedNode.FullPath.Split('\\')[0];
                create.ShowDialog();                
            }            
        } 

        private void button6_Click(object sender, EventArgs e)
        {
            try 
            {
                int delet = dataGridView2.SelectedCells[0].RowIndex;
                SQLiteCommand CMD = DB.CreateCommand();
                CMD.CommandText = "delete from GRADE where Name = @Name; delete from TEACHERSPLAN where Grade = @Name; delete from RASPISANIE where Grade = @Name";
                CMD.Parameters.Add("@Name", DbType.String).Value = dataGridView2[0, delet].Value.ToString();
                CMD.ExecuteNonQuery();
                Update_BD();
                successfully succ = new successfully();
                succ.ShowDialog();
            }
            catch
            {
                error er = new error();
                er.ShowDialog();
            }
         }

        private void button14_Click(object sender, EventArgs e)
        {
            AddLesson add = new AddLesson();
            add.Owner = this;
            add.ShowDialog();
        }

        private void treeView6_AfterSelect(object sender, TreeViewEventArgs e)
        {
            treeView_AfterSelec = treeView6.SelectedNode.FullPath;
            if (treeView_AfterSelec.Contains('\\') == false)
            {                
            }
            else
            {                
                if (treeView_AfterSelec != "")
                {                     
                    panel1.Refresh();                    
                }
            }            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var p = sender as Panel;
            var g = e.Graphics;
            Pen pen = new Pen(Color.Black, 1);
            for (int i = 1; i <= 8; i++)
            {
                e.Graphics.DrawLine(pen, 0, Convert.ToInt32(p.Height / 9 * i) , p.Width , Convert.ToInt32(p.Height / 9 * i));
                
            }
            for (int i = 1; i <= 6; i++)
            {
                e.Graphics.DrawLine(pen, Convert.ToInt32(p.Width / 7 * i), 0, Convert.ToInt32(p.Width / 7 * i), p.Height);
            }       
            
            try
            {
                if (treeView6.SelectedNode != null)
                {
                    
                    SQLiteConnection updateBD = new SQLiteConnection("Data Source=testBD.db; Version=3");
                    updateBD.Open();
                    SQLiteCommand comm = updateBD.CreateCommand();
                    comm.CommandText = "select s.Lesson, s.Grade, s.Time, s.Weekday, s.Room, T.Fio from RASPISANIE s left join TEACHERS T on s.Teacher = T.Id where s.Grade = @Name";
                    comm.Parameters.Add("@Name", DbType.String).Value = treeView6.SelectedNode.Text;
                    SQLiteDataReader SQL = comm.ExecuteReader();
                    int i = 0;

                    if (SQL.HasRows)
                    {

                        while (SQL.Read())
                        {                            
                            var weekday = Int32.Parse(SQL.GetValue(3).ToString()) - 1;
                            var hour = Int32.Parse(SQL.GetValue(2).ToString()) - 9;
                            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(55, Color.Purple)),
                                Convert.ToInt32(p.Width / 7 * weekday), Convert.ToInt32(p.Height / 9 * hour),
                                Convert.ToInt32(p.Width / 7), Convert.ToInt32(p.Height / 9)
                               );
                            addnewlabel(SQL, p.Width, p.Height, i);
                            i++;
                        }
                         
                        for (int j = i; j < mylab.Length; j++)
                        {
                            if (mylab[j] != null)
                            {
                                this.Controls.Remove(mylab[j]);
                                mylab[j].Dispose();
                                mylab[j] = null;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int j = i; j < mylab.Length; j++)
                        {
                            if (mylab[j] != null)
                            {
                                this.Controls.Remove(mylab[j]);
                                mylab[j].Dispose();
                                mylab[j] = null;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
                //pass
            }
            
        }
        Label[] mylab = new Label[100];

        public void addnewlabel(IDataReader SQL, double W, double H, int i)
        {
            var w = Int32.Parse(SQL.GetValue(3).ToString()) - 1;
            var h = Int32.Parse(SQL.GetValue(2).ToString()) - 9;
            if (mylab[i] == null)
            {
                mylab[i] = new Label();
            }

            mylab[i].Location = new Point(Convert.ToInt32(W / 7 * w), Convert.ToInt32(H / 9 * h));
            mylab[i].Text = SQL.GetValue(0).ToString() + "\r\n" + SQL.GetValue(5).ToString() + "\r\n" + SQL.GetValue(4).ToString();
            mylab[i].AutoSize = true;
            mylab[i].Font = new Font("Calibri", 9);
            mylab[i].ForeColor = Color.Green;             
            panel1.Controls.Add(mylab[i]);            
        }
        private void panel1_Click(object sender, EventArgs e)
        {
            try
            {
                var p = sender as Panel;
                var e1 = e as MouseEventArgs;
                var hour = Convert.ToInt32(Convert.ToDouble(e1.Y) / p.Height * 9 + 8.5);
                var week = Convert.ToInt32(Convert.ToDouble(e1.X) / p.Width * 7 + 0.5);
                string grade = treeView6.SelectedNode.Text;

                RASPISANIE ras = new RASPISANIE();
                ras.Owner = this;

                ras.hour = hour.ToString();
                ras.week = week.ToString();
                ras.grade = grade;

                ras.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Выберите класс в списке справа", "Предупреждение", MessageBoxButtons.OK);
            }
        }              

        private void button19_Click(object sender, EventArgs e)
        {
            AddPlan plan = new AddPlan();
            plan.Owner = this;
            plan.InputText = treeView_AfterSelec.ToString();
            plan.ShowDialog();
        }

        private void button20_Click(object sender, EventArgs e)
        {            
            try
            {
                int delet = dataGridView5.SelectedCells[0].RowIndex;
                SQLiteCommand deleted_5 = DB.CreateCommand();
                deleted_5.CommandText = "DELETE FROM PLAN WHERE Year like @Year AND Name like '%' || @Name || '%'";
                deleted_5.Parameters.Add("@Year", DbType.String).Value = treeView_AfterSelec.ToString().Split()[0];
                deleted_5.Parameters.Add("@Name", DbType.String).Value = dataGridView5[0, delet].Value.ToString();
                
                deleted_5.ExecuteNonQuery();
                
                Update_BD_1();

                successfully succ = new successfully();
                succ.ShowDialog();
            }
            catch
            {
                error er = new error();
                er.ShowDialog();
            }
        }

        public void Update_Grid6()
        {
            try
            {
                dataGridView6.Rows.Clear();
                SQLiteConnection updateBD = new SQLiteConnection("Data Source=testBD.db; Version=3");
                updateBD.Open();
                string sel = this.dataGridView2.CurrentRow.Cells[0].Value.ToString();

                SQLiteCommand comm2 = updateBD.CreateCommand();
                comm2.CommandText = "select TP.Lesson, T.Fio, TP.Room, TP.Amount, TP.Grade, TP.Hoursaweek, TP.Id from TEACHERSPLAN TP left join TEACHERS T on TP.Idteacher = T.Id where TP.Grade like '" + sel + "'";
                comm2.Parameters.Add("@phone", DbType.String).Value = sel;
                SQLiteDataReader SQL2 = comm2.ExecuteReader();
                if (SQL2.HasRows)
                {
                    while (SQL2.Read())
                    {
                        dataGridView6.Rows.Add(new object[]
                        {
                            SQL2.GetValue(0),
                            SQL2.GetValue(1),
                            SQL2.GetValue(2),
                            SQL2.GetValue(3),
                            SQL2.GetValue(4),
                            SQL2.GetValue(5),
                            SQL2.GetValue(6)
                        });
                    }                    
                }              
            }
            catch
            {

            }
        }

            private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {            
            Update_Grid6();            
        }        

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                int delet = dataGridView4.SelectedCells[0].RowIndex;
                SQLiteCommand CMD = DB.CreateCommand();
                CMD.CommandText = "delete from TEACHERS where Fio = @Name; delete from TS where Idteacher = @Id; UPDATE TEACHERSPLAN set Idteacher = NULL WHERE Idteacher = @Id";
                CMD.Parameters.Add("@Name", DbType.String).Value = dataGridView4[0, delet].Value.ToString();
                CMD.Parameters.Add("@Id", DbType.String).Value = dataGridView4[7, delet].Value.ToString();
                CMD.ExecuteNonQuery();
                Update_Tab_2();
                successfully succ = new successfully();
                succ.ShowDialog();
            }
            catch
            {
                error er = new error();
                er.ShowDialog();
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Profile create = new Profile();
            create.Owner = this;            
            create.ShowDialog();
        }

        public void Update_BD_3()
        {            
            try
            {
                string a = dataGridView4[0, dataGridView4.CurrentRow.Index].Value.ToString();
                dataGridView7.Rows.Clear();
                dataGridView8.Rows.Clear();
                SQLiteConnection updateBD = new SQLiteConnection("Data Source=testBD.db; Version=3");
                updateBD.Open();

                SQLiteCommand comm2 = updateBD.CreateCommand();
                comm2.CommandText = "select * from TEACHERSPLAN WHERE Lesson in (select Subjects from TS WHERE Idteacher like (select Id from TEACHERS where Fio like '" + a + "')) and Idteacher is null";
               
                SQLiteDataReader SQL2 = comm2.ExecuteReader();
                if (SQL2.HasRows)
                {
                    while (SQL2.Read())
                    {
                        dataGridView7.Rows.Add(new object[]
                        {
                            SQL2.GetValue(1),                            
                            SQL2.GetValue(4),
                            SQL2.GetValue(6),
                            SQL2.GetValue(3),
                            SQL2.GetValue(2),
                            SQL2.GetValue(7)
                        });
                    }                    
                }

                SQLiteCommand comm1 = updateBD.CreateCommand();
                comm1.CommandText = "select * from TEACHERSPLAN WHERE Idteacher like (select Id from TEACHERS where Fio like '" + a + "')";
                SQLiteDataReader SQL1 = comm1.ExecuteReader();
                if (SQL1.HasRows)
                {
                    while (SQL1.Read())
                    {
                        dataGridView8.Rows.Add(new object[]
                        {
                            SQL1.GetValue(1),
                           
                            SQL1.GetValue(4),
                            SQL1.GetValue(6),
                            SQL1.GetValue(3),
                            SQL1.GetValue(2),
                            SQL1.GetValue(7)
                        });
                    }                    
                }                
            }
            catch
            {
                //pass
            }
        }

            private void dataGridView4_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {            
            Update_BD_3();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var k = dataGridView7.SelectedRows.Cast<DataGridViewRow>().Select(z => z.Index).ToList();
            string a = dataGridView4[7, dataGridView4.CurrentRow.Index].Value.ToString();
            foreach (int s in k)
            {
                SQLiteCommand AddtoTable2 = DB.CreateCommand();
                AddtoTable2.CommandText = "UPDATE TEACHERSPLAN set Idteacher = " + a + " WHERE Id = '" + dataGridView7[5, s].Value.ToString() + "'";                
                AddtoTable2.ExecuteNonQuery();                
            }
            Update_BD_3();
            Update_Tab_2();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            var k = dataGridView8.SelectedRows.Cast<DataGridViewRow>().Select(z => z.Index).ToList();
            
            foreach (int s in k)
            {
                SQLiteCommand AddtoTable2 = DB.CreateCommand();
                AddtoTable2.CommandText = "UPDATE TEACHERSPLAN set Idteacher = NULL WHERE Id = '" + dataGridView8[5, s].Value.ToString() + "'";                
                AddtoTable2.ExecuteNonQuery();
            }
            Update_BD_3();
            Update_Tab_2();
        }

        private void button17_Click(object sender, EventArgs e)
        {
           string ed = dataGridView4[7, dataGridView4.CurrentRow.Index].Value.ToString();

            Profile pro = new Profile();
            pro.Owner = this;
            pro.edit = ed;
            pro.ShowDialog();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                string ed = dataGridView5[0, dataGridView5.CurrentRow.Index].Value.ToString();
                string ed1 = (dataGridView5[1, dataGridView5.CurrentRow.Index].Value.ToString());
                string ed2 = dataGridView5[2, dataGridView5.CurrentRow.Index].Value.ToString();
                string id = dataGridView5[3, dataGridView5.CurrentRow.Index].Value.ToString();
                AddPlan plan = new AddPlan();
                plan.Owner = this;
                plan.InputText = treeView_AfterSelec.ToString();
                plan.edit = ed;
                plan.edit1 = ed1;
                plan.edit2 = ed2;
                plan.id1 = id;
                plan.ShowDialog();
            }
            catch
            {
                MessageBox.Show("error - попробуйте еще раз");
            }
        }

        private void dataGridView9_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Update_Room2();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            var k = dataGridView10.SelectedRows.Cast<DataGridViewRow>().Select(z => z.Index).ToList();
            string a = dataGridView9[0, dataGridView9.CurrentRow.Index].Value.ToString();
            foreach (int s in k)
            {
                SQLiteCommand AddtoTable2 = DB.CreateCommand();
                AddtoTable2.CommandText = "UPDATE TEACHERSPLAN set Room = " + a + " WHERE Id = '" + dataGridView10[5, s].Value.ToString() + "'";                
                AddtoTable2.ExecuteNonQuery();                
            }
            Update_Room2();
            Update_Room();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            var k = dataGridView11.SelectedRows.Cast<DataGridViewRow>().Select(z => z.Index).ToList();

            foreach (int s in k)
            {
                SQLiteCommand AddtoTable2 = DB.CreateCommand();
                AddtoTable2.CommandText = "UPDATE TEACHERSPLAN set Room = NULL WHERE Id = '" + dataGridView11[5, s].Value.ToString() + "'";
                AddtoTable2.ExecuteNonQuery();
            }
            Update_Room2();
            Update_Room();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                string a = dataGridView2[0, dataGridView2.CurrentRow.Index].Value.ToString();
                AddPlan create = new AddPlan();
                create.Owner = this;
                create.reedit = a;
                create.ShowDialog();
            }           
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                int delet = dataGridView6.SelectedCells[0].RowIndex;
                SQLiteCommand CMD = DB.CreateCommand();
                CMD.CommandText = "delete from TEACHERSPLAN where Id = @Name;";
                CMD.Parameters.Add("@Name", DbType.String).Value = dataGridView6[6, delet].Value.ToString();
                CMD.ExecuteNonQuery();
                Update_Grid6();                
            }
            catch
            {
                error er = new error();
                er.ShowDialog();
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            Addroom create = new Addroom();
            create.Owner = this;
            create.ShowDialog();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            string ed2 = dataGridView9[0, dataGridView9.CurrentRow.Index].Value.ToString();
            string ed1 = dataGridView9[1, dataGridView9.CurrentRow.Index].Value.ToString();
            Addroom create = new Addroom();
            create.Owner = this;
            create.edit = ed2;
            create.edit1 = ed1;
            create.ShowDialog();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            try
            {
                int delet = dataGridView9.SelectedCells[0].RowIndex;
                SQLiteCommand CMD = DB.CreateCommand();
                CMD.CommandText = "delete from ROOMS where Id = @Id; delete from RS where Idroom = @Name; UPDATE TEACHERSPLAN set Room = NULL WHERE Room = @Name";
                CMD.Parameters.Add("@Name", DbType.String).Value = dataGridView9[0, delet].Value.ToString();
                CMD.Parameters.Add("@Id", DbType.String).Value = dataGridView9[3, delet].Value.ToString();
                CMD.ExecuteNonQuery();
                Update_Room();
                Update_Room2();
                successfully succ = new successfully();
                succ.ShowDialog();
            }
            catch
            {
                error er = new error();
                er.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView4.Rows.Count > 0)
            {

                Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
                xcelApp.Application.Workbooks.Add(Type.Missing);

                for (int i = 1; i < dataGridView4.Columns.Count + 1; i++)
                {
                    xcelApp.Cells[1, i] = dataGridView4.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < dataGridView4.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView4.Columns.Count; j++)
                    {
                        xcelApp.Cells[i + 2, j + 1] = dataGridView4.Rows[i].Cells[j].Value.ToString();
                    }
                }
                xcelApp.Columns.AutoFit();
                xcelApp.Visible = true;
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
