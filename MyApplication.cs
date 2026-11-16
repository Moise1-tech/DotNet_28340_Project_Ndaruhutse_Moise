using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScholarshipPRO_28340
{
    public partial class MyApplication : Form
    {
        private readonly string Connstr = ConfigurationManager.ConnectionStrings["ScholarshipDB"]?.ConnectionString ?? throw new ConfigurationErrorsException("Connection string ScholarshipDB not found.");

        public MyApplication()
        {
            InitializeComponent();
        }

        private void MyApplication_Load(object sender, EventArgs e)
        {
         
            if (this.Tag == null || !int.TryParse(this.Tag.ToString(), out int studentID) || studentID <= 0)
            {
                MessageBox.Show("No student is logged in. Please log in again.");
                this.BeginInvoke(new Action(this.Close));
                return;
            }

            LoadGridData(studentID);

            ApplicationGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ApplicationGrid.MultiSelect = false;
            ApplicationGrid.AllowUserToAddRows = false;
        }

        private void LoadGridData(int studentID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connstr))
                using (SqlCommand cmd = new SqlCommand("usp_GetMyApplication", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@student_ID", SqlDbType.Int).Value = studentID;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        ApplicationGrid.DataSource = table;
                    }
                }

                // Make ID, Scholarship Name, and Status READ-ONLY
                if (ApplicationGrid.Columns["application_ID"] != null) ApplicationGrid.Columns["application_ID"].ReadOnly = true;
                if (ApplicationGrid.Columns["scholarship_name"] != null) ApplicationGrid.Columns["scholarship_name"].ReadOnly = true;
                if (ApplicationGrid.Columns["status"] != null) ApplicationGrid.Columns["status"].ReadOnly = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (ApplicationGrid.CurrentRow == null)
            {
                MessageBox.Show("Please select an application to update.");
                return;
            }

            // Get values directly from the selected row in the grid
            int applicationID = Convert.ToInt32(ApplicationGrid.CurrentRow.Cells["application_ID"].Value);

            if (this.Tag == null || !int.TryParse(this.Tag.ToString(), out int studentID))
            {
                MessageBox.Show("Session error. Please log in again.");
                return;
            }

            // Get the values the user typed into the grid cells
            string fullName = Convert.ToString(ApplicationGrid.CurrentRow.Cells["full_name"].Value);
            string department = Convert.ToString(ApplicationGrid.CurrentRow.Cells["department"].Value);
            string reason = Convert.ToString(ApplicationGrid.CurrentRow.Cells["reason"].Value);

            // Safely parse the numbers from the grid
            if (!int.TryParse(Convert.ToString(ApplicationGrid.CurrentRow.Cells["semester"].Value), out int semester) ||
                !decimal.TryParse(Convert.ToString(ApplicationGrid.CurrentRow.Cells["current_average_score"].Value), out decimal average) ||
                !decimal.TryParse(Convert.ToString(ApplicationGrid.CurrentRow.Cells["monthly_family_income"].Value), out decimal income))
            {
                MessageBox.Show("Please enter valid numbers for semester, average, and income inside the grid.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(Connstr))
                using (SqlCommand cmd = new SqlCommand("usp_UpdateMyApplication", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@application_ID", SqlDbType.Int).Value = applicationID;
                    cmd.Parameters.Add("@student_ID", SqlDbType.Int).Value = studentID;
                    cmd.Parameters.Add("@full_name", SqlDbType.NVarChar, 50).Value = fullName;
                    cmd.Parameters.Add("@department", SqlDbType.NVarChar, 50).Value = department;
                    cmd.Parameters.Add("@semester", SqlDbType.Int).Value = semester;

                    var pAvg = cmd.Parameters.Add("@current_average_score", SqlDbType.Decimal);
                    pAvg.Precision = 4; pAvg.Scale = 2; pAvg.Value = average;

                    var pInc = cmd.Parameters.Add("@monthly_family_income", SqlDbType.Decimal);
                    pInc.Precision = 15; pInc.Scale = 2; pInc.Value = income;

                    cmd.Parameters.Add("@reason", SqlDbType.NVarChar, 500).Value = reason;

                    con.Open();
                    string result = Convert.ToString(cmd.ExecuteScalar()) ?? "";

                    if (result == "SUCCESS")
                    {
                        MessageBox.Show("Application updated successfully!");
                        LoadGridData(studentID); // Refresh grid
                    }
                    else if (result == "NOT_ALLOWED")
                    {
                        MessageBox.Show("You cannot edit this application. It has already been processed.");
                    }
                    else
                    {
                        MessageBox.Show("Unexpected result: " + result);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (ApplicationGrid.CurrentRow == null)
            {
                MessageBox.Show("Please select an application to delete.");
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this application?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.No) return;

            int applicationID = Convert.ToInt32(ApplicationGrid.CurrentRow.Cells["application_ID"].Value);

            if (this.Tag == null || !int.TryParse(this.Tag.ToString(), out int studentID))
            {
                MessageBox.Show("Session error. Please log in again.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(Connstr))
                using (SqlCommand cmd = new SqlCommand("usp_DeleteMyApplication", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@application_ID", SqlDbType.Int).Value = applicationID;
                    cmd.Parameters.Add("@student_ID", SqlDbType.Int).Value = studentID;

                    con.Open();
                    string result = Convert.ToString(cmd.ExecuteScalar()) ?? "";

                    if (result == "SUCCESS")
                    {
                        MessageBox.Show("Application deleted successfully.");
                        LoadGridData(studentID); // Refresh grid
                    }
                    else if (result == "NOT_ALLOWED")
                    {
                        MessageBox.Show("You cannot delete this. It may have already been processed.");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
        }
    }
}

