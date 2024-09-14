using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace curdEmpForm
{
    public partial class sginup : System.Web.UI.Page
    {
        sampleEmpDBEntities db = new sampleEmpDBEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {

            // Check if the page is valid (checks for the validators in the form)
            if (Page.IsValid)
            {
              
                    // Check if the username or email already exists in the database
                    var existingUser = db.Users.FirstOrDefault(u => u.UserName == txtUsername.Text || u.UserEmail == txtEmail.Text);

                    if (existingUser != null)
                    {
                        lblMessage.Text = "Username or Email already exists. Please try a different one.";
                        return;
                    }

                    // Ensure passwords match (this is checked with CompareValidator but included here for extra security)
                    if (txtPassword.Text != txtConfirmPassword.Text)
                    {
                        lblMessage.Text = "Passwords do not match.";
                        return;
                    }

                    // Create a new user object
                    User user = new User
                    {
                        FirstName = txtFirstName.Text,
                        UserName = txtUsername.Text,
                        UserEmail = txtEmail.Text,
                        // Hash the password before saving it to the database
                        PassWord = HashPassword(txtPassword.Text) // Implement password hashing
                    };

                    // Add the new user to the database and save changes
                    db.Users.Add(user);
                    db.SaveChanges();

                    // Redirect to login page after successful registration
                    Response.Redirect("Login.aspx");
                }
            
        }


        // Simple password hashing function (replace with a stronger hashing mechanism like BCrypt)
        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }


    }
}