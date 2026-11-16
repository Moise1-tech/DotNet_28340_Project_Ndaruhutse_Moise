using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ScholarshipPRO_28340
{
    public partial class StudentDashboardForm : Form
    {
        public StudentDashboardForm()
        {
            InitializeComponent();
        }

        private void viewApp_Click(object sender, EventArgs e)
        {
            if (this.Tag == null)
            {
                MessageBox.Show("No student is logged in. Please log in again.");
                return;
            }

            ApplicationForm af = new ApplicationForm();
            af.Tag = this.Tag;          // ⭐ the fix
            af.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyApplication ma = new MyApplication();

            ma.Tag = this.Tag;

            ma.ShowDialog();
        }
    }
}
