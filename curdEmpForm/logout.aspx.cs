using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace curdEmpForm
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Clear the session
            Session.Abandon();

            // Redirect to the login page
            Response.Redirect("login.aspx");
        }
    }
}