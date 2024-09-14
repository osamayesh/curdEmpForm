using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace curdEmpForm
{
    public partial class Admin : System.Web.UI.Page
    {
        
        sampleEmpDBEntities db = new sampleEmpDBEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindUsers();
                BindRoles();

            }

            if (Session["UserRole"] == null || !Session["UserRole"].ToString().Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                // If not an admin, redirect to the Unauthorized page
                Response.Redirect("Unauthorized.aspx");
            }



        }

        

        // Bind users and their roles to the GridView
        private void BindUsers()
        {
            // Step 1: Fetch the data from the database without string.Join
            var users = db.Users
                .Select(u => new
                {
                    u.UserId,
                    u.UserName,
                    u.UserEmail,
                    Roles = u.UserRoles.Select(ur => ur.Role.RoleName).ToList() // Fetch list of roles
                })
                .ToList(); // Fetch data into memory

            // Step 2: Use string.Join after the data is loaded into memory
            var userWithRoles = users.Select(u => new
            {
                u.UserId,
                u.UserName,
                u.UserEmail,
                Roles = string.Join(",", u.Roles) // Apply string.Join here in-memory
            }).ToList();

            gvUsers.DataSource = userWithRoles;
            gvUsers.DataBind();
        }


        // Bind available roles to the dropdown list
        private void BindRoles()
        {
            var roles = db.Roles.ToList();
            ddlRoles.DataSource = roles;
            ddlRoles.DataTextField = "RoleName";
            ddlRoles.DataValueField = "RoleId";
            ddlRoles.DataBind();
        }

        // Assign role to the user
        protected void btnAssignRole_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int userId = Convert.ToInt32(button.CommandArgument);
            int roleId = Convert.ToInt32(ddlRoles.SelectedValue);

            var userRole = new UserRole { UserId = userId, RoleId = roleId };
            db.UserRoles.Add(userRole);
            db.SaveChanges();

            // Rebind users to reflect the role changes
            BindUsers();
        }

        // New method to handle role removal
        protected void btnRemoveRole_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int userRoleId = Convert.ToInt32(button.CommandArgument); // Assuming you have UserRoleId in the button CommandArgument

            // Find the UserRole record in the database
            var userRole = db.UserRoles.FirstOrDefault(ur => ur.UserId == userRoleId);

            if (userRole != null)
            {
                // Remove the UserRole entry from the database
                db.UserRoles.Remove(userRole);
                db.SaveChanges();



                // Rebind users to reflect the role changes
                BindUsers();
            }


        }
    }
}