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
    public partial class SignupForm : Form
    {
        private readonly string connectionString;

        public SignupForm()
        {
            InitializeComponent();

            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings["ScholarshipDB"] ?? throw new ConfigurationErrorsException("ScholarshipDB not found");

            if (settings == null || string.IsNullOrWhiteSpace(settings.ConnectionString))
            {
                throw new ConfigurationErrorsException(
                    "The connection string 'ScholarshipDB' was not found in the application configuration file.");
            }

            connectionString = settings.ConnectionString;
        }

        private void signupBtn_Click(object sender, EventArgs e)
        {
            string email = EmailBox.Text.Trim();
            string password = PasswordBox.Text.Trim();

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
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_RegisterStudent", con))
                    {
                        // tell the C# that we are execuing a stored procedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        //send the email to the stored procedure
                        cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = email;

                        //send the hashed password, not the orignal password
                        cmd.Parameters.Add("@password", SqlDbType.NVarChar, 255).Value = password;

                        // open connection
                        con.Open();

                        // execute the procedure and read its returned results
                        String result = Convert.ToString(cmd.ExecuteScalar());

                        //check whether the account was created
                        if (result == "SUCCESS")
                        {
                            MessageBox.Show("Registration was successful");
                            EmailBox.Clear();
                            PasswordBox.Clear();
                        }

                        // check whether the email is already registered
                        else if (result == "EXISTS")
                        {
                            MessageBox.Show("this Email already exists, please try again");

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Registration failed: " + ex.Message);
            }
        }

       
        private void redirectSignin_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginForm nf = new LoginForm();
            nf.Show();
            this.Hide();
        }
    }
}
    
