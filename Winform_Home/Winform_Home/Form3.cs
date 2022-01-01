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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
                MessageBox.Show("Please dont leave the textbox blank. If you do not want to enter anything, Press Cancel. If you want to delete the added text, Right-Click to select, then press 'Delete' key on the keyboard");
            else
                this.DialogResult = DialogResult.OK;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        public string return_txt()
        {
            return textBox1.Text;
        }

        public float return_fontsize()
        {
            return (float)numericUpDown1.Value;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)
            {
                textBox1.TextAlign = HorizontalAlignment.Left;
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                textBox1.TextAlign = HorizontalAlignment.Center;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                textBox1.TextAlign =HorizontalAlignment.Right;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public StringAlignment return_txt_allignment()
        {
            if(radioButton3.Checked)
            {
                return StringAlignment.Far;
            }
            else if(radioButton2.Checked)
            {
                return StringAlignment.Center;
            }
            else
            {
                return StringAlignment.Near;
            }

        }
        public void get_data_form3(string s,float fsize,StringAlignment sf)
        {
            textBox1.Text = s;
            numericUpDown1.Value = (decimal)fsize;
            if(sf==StringAlignment.Center)
            {
                radioButton2.Checked = true;
                textBox1.TextAlign = HorizontalAlignment.Center;
            }
            else if (sf == StringAlignment.Near)
            {
                radioButton1.Checked = true;
                textBox1.TextAlign = HorizontalAlignment.Left;
            }
            else
                 
            {
                radioButton3.Checked = true;
                textBox1.TextAlign = HorizontalAlignment.Right;
            }


        }
    }
}
