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
    public partial class ApplicationForm : Form
    {
        private readonly string Connstr = ConfigurationManager.ConnectionStrings["ScholarshipDB"].ConnectionString;
        public ApplicationForm()
        {
            InitializeComponent();
            Load += ApplicationForm_Load;
        }
        int Student_ID;

        private void ApplicationForm_Load(object sender, EventArgs e)
        {
            if (this.Tag == null || !int.TryParse(this.Tag.ToString(), out Student_ID) || Student_ID <= 0)
            {
                MessageBox.Show("No student is logged in. Please log in again.");
                Close();
                return;
            }

            scholarshipCmb.DropDownStyle = ComboBoxStyle.DropDownList;

            try
            {
                LoadScholarships();
                scholarshipCmb.SelectedIndex = -1;
                scholarshipCmb.Text = "Select scholarship";
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Unable to load scholarship categories: " + ex.Message);
            }
        }
        private void LoadScholarships()
        {
            using (SqlConnection con = new SqlConnection(Connstr))
            {
                using (SqlCommand cmd = new SqlCommand("usp_GetAvailableScholarships", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        sda.Fill(table);
                        scholarshipCmb.DisplayMember = "scholarship_name";
                        scholarshipCmb.ValueMember = "scholarship_ID";
                        scholarshipCmb.DataSource = table;
                    }
                }
            }
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Student_ID = " + Student_ID + "  |  Tag = " + (this.Tag ?? "<null>"));
            // 1. Required fields
            if (string.IsNullOrWhiteSpace(FnameBox.Text)
             || string.IsNullOrWhiteSpace(DeptBox.Text)
             || string.IsNullOrWhiteSpace(semesterBox.Text)
             || string.IsNullOrWhiteSpace(AvgScoreBox.Text)
             || string.IsNullOrWhiteSpace(incomeBox.Text)
             || string.IsNullOrWhiteSpace(reasonBox.Text))
            {
                MessageBox.Show("Please complete all fields.");
                return;
            }

            if (scholarshipCmb.SelectedIndex < 0 || scholarshipCmb.SelectedValue == null)
            {
                MessageBox.Show("Please select a scholarship category.");
                return;
            }

            // 2. Numeric parsing
            if (!int.TryParse(semesterBox.Text, out int semester))
            {
                MessageBox.Show("Semester must be a whole number.");
                return;
            }
            if (!decimal.TryParse(AvgScoreBox.Text, out decimal average))
            {
                MessageBox.Show("Average score must be a number (e.g. 14.50).");
                return;
            }
            if (!decimal.TryParse(incomeBox.Text, out decimal income))
            {
                MessageBox.Show("Please enter a valid monthly income amount.");
                return;
            }

            // 3. Client-side eligibility (server re-checks — keep both)
            if (semester < 2 || average < 14)
            {
                MessageBox.Show("You are not eligible. You must have completed semester 2 and have an average of 14 or above.");
                return;
            }
            if (income < 5)
            {
                MessageBox.Show("Monthly income must be at least 5 RWF.");
                return;
            }

            int scholarship_ID = Convert.ToInt32(scholarshipCmb.SelectedValue);

            try
            {
                using (var con = new SqlConnection(Connstr))
                using (var cmd = new SqlCommand("usp_SubmitScolarshipApplication", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@student_ID", SqlDbType.Int).Value = Student_ID;
                    cmd.Parameters.Add("@full_name", SqlDbType.NVarChar, 50).Value = FnameBox.Text.Trim();
                    cmd.Parameters.Add("@department", SqlDbType.NVarChar, 50).Value = DeptBox.Text.Trim();
                    cmd.Parameters.Add("@semester", SqlDbType.Int).Value = semester;

                    var pAvg = cmd.Parameters.Add("@current_average_score", SqlDbType.Decimal);
                    pAvg.Precision = 4; pAvg.Scale = 2; pAvg.Value = average;

                    var pInc = cmd.Parameters.Add("@monthly_family_income", SqlDbType.Decimal);
                    pInc.Precision = 15; pInc.Scale = 2; pInc.Value = income;

                    cmd.Parameters.Add("@scholarship_ID", SqlDbType.Int).Value = scholarship_ID;
                    cmd.Parameters.Add("@reason", SqlDbType.NVarChar, 500).Value = reasonBox.Text.Trim();

                    con.Open();
                    string Result = Convert.ToString(cmd.ExecuteScalar()) ?? "";

                    switch (Result)
                    {
                        case "SUCCESS":
                            MessageBox.Show("Your application has been submitted successfully.");
                            FnameBox.Clear();
                            DeptBox.Clear();
                            semesterBox.Clear();
                            AvgScoreBox.Clear();
                            incomeBox.Clear();
                            reasonBox.Clear();
                            LoadScholarships();
                            break;

                        case "CATEGORY_UNAVAILABLE":
                            MessageBox.Show("This category is no longer available.");
                            LoadScholarships();
                            break;

                        case "NOT_ELIGIBLE":
                            MessageBox.Show("You are not eligible for this scholarship.");
                            break;

                        default:
                            MessageBox.Show("Unexpected response from database: '" + Result + "'");
                            break;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error: " + ex.Message);
            }
        }

        private void scholarshipCmb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
