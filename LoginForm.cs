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
    public partial class LoginForm : Form
    {
        private readonly string Connstr = ConfigurationManager.ConnectionStrings["ScholarshipDB"]?.ConnectionString ?? 
            throw new ConfigurationErrorsException("ScholarshipDB not found");
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            // log in logic

            string email = GmailBox.Text.Trim();
            string password = PasswdBox.Text.Trim();

            //check whether the user left any field null

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {

                // tell the user to fill all the required fields
                MessageBox.Show("Please enter you email and password");
                return;
            }
            try
            {
                //connection using the con string
                using (SqlConnection con = new SqlConnection(Connstr))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_LoginStudent", con))
                    {
                        // tell the C# that we are execuing a stored procedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        //send the email to the stored procedure
                        cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = email;


                        // open connection
                        con.Open();

                        //read the account returned by the procedures
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                // use the same message for unknown email
                                MessageBox.Show("Invalid Email or Password");
                                return;
                            }

                            

                            //retrieve the auth user id
                            int student_ID = Convert.ToInt32(reader["student_ID"]);

                            //retrieve the role assigned to the user
                            string role = reader["Role"].ToString();

                            //declare the forn that will open after login
                            Form dashboard;
                            if (role == "Admin")
                            {
                                dashboard = new AdminDashboardForm();
                            }
                            else if (role == "Student")
                            {
                                dashboard = new StudentDashboardForm();
                            }
                            else
                            {
                                MessageBox.Show("Your role is not recognised");
                                return;
                            }

                            //keep the login form open when the dashbord closes
                            dashboard.FormClosed += (s, args) => this.Close();

                            //pass the logged in student id to the dashboard form
                            dashboard.Tag = student_ID;
                            //display the appropriate dashboard
                            dashboard.Show();

                            //hide the form after successfull log in
                            this.Hide();
                        }
                    }

                }
            }
            catch (SqlException)
            {

                MessageBox.Show("Unable to connect to the database");
            }

        }

        private void redirectSignup_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignupForm sf = new SignupForm();
            sf.Show();
            this.Hide();
        }
    }
}
    