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
    public partial class AdminDashboardForm : Form
    {
        private readonly string Connstr = ConfigurationManager.ConnectionStrings["ConnectionSC"].ConnectionString;

        private DataTable masterTable;
        public AdminDashboardForm()
        {
            InitializeComponent();
        }

        private void AdminDashboardForm_Load(object sender, EventArgs e)
        {
            // Populate the ComboBox with filter options
            cmbFilterStatus.Items.Clear();
            cmbFilterStatus.Items.AddRange(new string[] { "All", "Pending", "Approved", "Rejected" });
            cmbFilterStatus.SelectedIndex = 0; // Default to "All"

            LoadApps();
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            LoadApps();
        }

        // --- Helper Method to Load Data from SQL ---
        private void LoadApps()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connstr))
                using (SqlCommand cmd = new SqlCommand("usp_AdminGetApps", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        masterTable = new DataTable();
                        adapter.Fill(masterTable);
                        adminDgv.DataSource = masterTable;
                    }
                }

                adminDgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                adminDgv.MultiSelect = false;
                adminDgv.AllowUserToAddRows = false;
                adminDgv.ReadOnly = true;

                // Reset filters after loading
                txtSearch.Clear();
                cmbFilterStatus.SelectedIndex = 0;
                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading applications: " + ex.Message);
            }
        }

        // --- Filtering Logic ---
        private void ApplyFilter()
        {
            if (masterTable == null) return;

            string filterQuery = "";

            // 1. Text Search Filter (Search by Name, Email, or Department)
            string searchText = txtSearch.Text.Trim().Replace("'", "''"); // Escape single quotes
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filterQuery += $"(full_name LIKE '%{searchText}%' OR email LIKE '%{searchText}%' OR department LIKE '%{searchText}%')";
            }

            // 2. Status Filter (Pending, Approved, Rejected)
            string selectedStatus = cmbFilterStatus.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedStatus) && selectedStatus != "All")
            {
                if (filterQuery != "") filterQuery += " AND "; // Combine with text search
                filterQuery += $"status = '{selectedStatus}'";
            }

            // Apply the filter to the grid
            DataView dv = masterTable.DefaultView;
            dv.RowFilter = filterQuery;
            adminDgv.DataSource = dv;


        }

        private void filterBtn_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void showAllBtn_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbFilterStatus.SelectedIndex = 0; // "All"
            ApplyFilter();
        }

        private void showSelected_TextChanged(object sender, EventArgs e)
        {
            // Optional: Automatically filter as they type (Live Search)
            // ApplyFilter(); 
        }

        private void cmbFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Optional: Automatically filter when ComboBox changes
            // ApplyFilter();
        }

        // --- Approve / Reject Logic ---
        private void approveBtn_Click(object sender, EventArgs e)
        {
            updateStatus("Approved");
        }

        private void rejectBtn_Click(object sender, EventArgs e)
        {
            updateStatus("Rejected");
        }

        private void updateStatus(string newStatus)
        {
            if (adminDgv.CurrentRow == null)
            {
                MessageBox.Show("Please select an application from the list first.");
                return;
            }

            int appID = Convert.ToInt32(adminDgv.CurrentRow.Cells["application_ID"].Value);
            string currentStatus = adminDgv.CurrentRow.Cells["status"].Value.ToString();

            if (currentStatus != "Pending")
            {
                MessageBox.Show("You can only approve or reject 'Pending' applications.");
                return;
            }

            DialogResult confirm = MessageBox.Show($"Are you sure you want to mark this application as {newStatus}?",
                                                   "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No) return;

            try
            {
                using (SqlConnection con = new SqlConnection(Connstr))
                using (SqlCommand cmd = new SqlCommand("usp_AdminUpdateStatus", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@application_ID", SqlDbType.Int).Value = appID;
                    cmd.Parameters.Add("@status", SqlDbType.NVarChar, 50).Value = newStatus;

                    con.Open();
                    int rowsAffected = Convert.ToInt32(cmd.ExecuteScalar());

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Application {newStatus.ToLower()} successfully!");
                        LoadApps(); // Refresh the grid to show the new status
                    }
                    else
                    {
                        MessageBox.Show("Could not update status. The application may have already been processed.");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
        }

        // --- Unused Event Handlers ---
        private void IncreaseTotalWithFilter_Click(object sender, EventArgs e)
        {
            // Can be deleted if not used
        }

        private void adminDgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Can be deleted if not used
        }
    }
}