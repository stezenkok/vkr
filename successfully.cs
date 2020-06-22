using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vkr
{
    public partial class successfully : Form
    {
        public successfully()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (keyData.ToString() == "Escape")
            {
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
