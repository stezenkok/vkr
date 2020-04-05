using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimetableEditor
{
    public partial class Week : Form
    {
        public Week()
        {
            InitializeComponent();
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuItemTeachers_Click(object sender, EventArgs e)
        {
            Form f = new Teachers();
            f.ShowDialog();
        }

        private void menuItemRooms_Click(object sender, EventArgs e)
        {
            Form f = new Rooms();
            f.ShowDialog();
        }

        private void menuItemGroups_Click(object sender, EventArgs e)
        {
            Form f = new Groups();
            f.ShowDialog();
        }
    }
}
