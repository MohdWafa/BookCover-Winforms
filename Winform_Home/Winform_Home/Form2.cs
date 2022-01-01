using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform_Home
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.DialogResult = DialogResult.OK;
        }

        public int return_numeric1()
        {
            return (int)numericUpDown1.Value;
        }
        public int return_numeric2()
        {
            return (int)numericUpDown2.Value;
        }
        public int return_numeric3()
        {
            return (int)numericUpDown3.Value;
        }
    }
}
