using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace curdEmpForm
{
    public partial class login : System.Web.UI.Page
    {
        sampleEmpDBEntities db=new sampleEmpDBEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void btnlogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (sampleEmpDBEntities db = new sampleEmpDBEntities())
                {
                    // Fetch the user by email and password
                    var user = db.Users
                                 .Where(x => x.UserEmail == txtEmail.Text && x.PassWord == txtPassword.Text)
                                 .FirstOrDefault();

                    if (user != null)
                    {
                        // Fetch the user's role using the UserId and UserRoles relationship
                        var userRole = db.UserRoles
                                         .Where(ur => ur.UserId == user.UserId)
                                         .Select(ur => ur.Role.RoleName)
                                         .FirstOrDefault(); // Assuming each user has one role

                        // Store username and role in session after successful login
                        Session["UserName"] = user.UserName;
                        Session["UserRole"] = userRole; // Store the role in the session

                        // Redirect to the homepage or dashboard
                        Response.Redirect("~/");
                    }
                    else
                    {
                        lblMessage.Text = "Invalid username or password";
                    }
                }
            }
        }



    }
}