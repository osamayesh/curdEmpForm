using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace curdEmpForm
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        
        //retiver data 
        private void BindGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connection_"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employee", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvResults.DataSource = dt;
                gvResults.DataBind(); //render data 
            }
        }

        protected void modal_Click(object sender, EventArgs e)
        {
            ClearForm();
            ViewState["EmployeeId"] = null;  // Clear the ViewState to indicate a new record
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "$('#mymodal').modal('show');", true);
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connection_"].ConnectionString;

            // Validate the input fields before proceeding with database operations
            if (ddlGender.SelectedValue == "Select Gender")
            {
                lblmsg.Text = "Please select a valid gender.";
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "$('#mymodal').modal('show');", true);
                return;
            }

            DateTime dateOfBirth;
            if (!DateTime.TryParseExact(txtDateOfBirth.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth))
            {
                lblmsg.Text = "Please enter a valid date of birth in the format yyyy-MM-dd.";
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "$('#mymodal').modal('show');", true);
                return;
            }

            decimal salary;
            if (!decimal.TryParse(TxtSalary.Text, out salary))
            {
                lblmsg.Text = "Please enter a valid salary.";
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "$('#mymodal').modal('show');", true);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd;

                // Check if we're adding a new record or updating an existing one
                if (ViewState["EmployeeId"] != null)
                {
                    // Update existing employee
                    cmd = new SqlCommand("UPDATE Employee SET EmpNumber = @EmpNumber, EmpName = @EmpName, Gender = @Gender, Date_of_birth = @Date_of_birth, Position = @Position, Salary = @Salary WHERE Id = @Id", conn);
                    cmd.Parameters.AddWithValue("@Id", ViewState["EmployeeId"]);
                }
                else
                {
                    // Add new employee
                    cmd = new SqlCommand("INSERT INTO Employee (EmpNumber, EmpName, Gender, Date_of_birth, Position, Salary) VALUES (@EmpNumber, @EmpName, @Gender, @Date_of_birth, @Position, @Salary)", conn);
                }

                cmd.Parameters.AddWithValue("@EmpNumber", txtEmpNumber.Text);
                cmd.Parameters.AddWithValue("@EmpName", txtEmpName.Text);
                cmd.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue);
                cmd.Parameters.AddWithValue("@Date_of_birth", dateOfBirth);
                cmd.Parameters.AddWithValue("@Position", TxtPosition.Text);
                cmd.Parameters.AddWithValue("@Salary", salary);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    lblmsg.Text = ViewState["EmployeeId"] != null ? "Employee updated successfully" : "Employee added successfully";
                }
                else
                {
                    lblmsg.Text = "Error while saving data";
                }
            }
            //used to execure a javascript function on client side from server-side code in an asp web forms application
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "$('#mymodal').modal('hide');", true);
            ClearForm();
            BindGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connection_"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employee WHERE EmpName LIKE @EmpName OR Id = @Id", conn);
                cmd.Parameters.AddWithValue("@EmpName", "%" + txtSearch.Text + "%");

                int id;
                if (int.TryParse(txtSearch.Text, out id))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Id", DBNull.Value);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvResults.DataSource = dt;
                gvResults.DataBind();
            }
        }


        protected void btndlt_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["connection_"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Employee WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    lblmsg.Text = "Data Deleted Successfully";
                }
                else
                {
                    lblmsg.Text = "Error while deleting data";
                }
            }

            BindGrid();
        }

        protected void btnupdate_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["connection_"].ConnectionString;

            // Fetch employee details based on the provided ID
            FetchEmployeeDetails(id, connectionString);

            // Store the ID in ViewState to update this record later
            ViewState["EmployeeId"] = id;

            // Show the modal with the fetched details
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModalScript", "$('#mymodal').modal('show');", true);
        }

        private void FetchEmployeeDetails(string id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT EmpNumber, EmpName, Gender, Date_of_birth, Position, Salary FROM Employee WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        txtEmpNumber.Text = dataReader["EmpNumber"].ToString();
                        txtEmpName.Text = dataReader["EmpName"].ToString();
                        ddlGender.SelectedValue = dataReader["Gender"].ToString();
                        txtDateOfBirth.Text = Convert.ToDateTime(dataReader["Date_of_birth"]).ToString("yyyy-MM-dd");
                        TxtPosition.Text = dataReader["Position"].ToString();
                        TxtSalary.Text = dataReader["Salary"].ToString(); // Convert to string for TextBox
                    }
                }
            }
        }

        private void ClearForm()
        {
            txtEmpNumber.Text = "";
            txtEmpName.Text = "";
            ddlGender.SelectedIndex = 0;
            txtDateOfBirth.Text = "";
            TxtPosition.Text = "";
            TxtSalary.Text = "";
            ViewState["EmployeeId"] = null;
        }
    }
}
