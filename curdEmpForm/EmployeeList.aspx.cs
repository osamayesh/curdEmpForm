using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace curdEmpForm
{
    public partial class About : Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connection_"].ConnectionString;
        sampleEmpDBEntities db = new sampleEmpDBEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserName"] == null || (Session["RoleName"] != null && !Session["RoleName"].ToString().ToLower().Equals("Admin")))
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                BindGridView();
            }


            if (!IsPostBack) // Check if the GridView  cant rpeat data when button click or refersh site 
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Employee", con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvEmployees.DataSource = dt;
                    gvEmployees.DataBind();
                }
            }
        }

        protected void btnAddNewEmployee_Click(object sender, EventArgs e)
        {
            ClearForm();
            hdnEmployeeId.Value = string.Empty;
            btnSaveEmployee.Text = "Add Employee";
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openEmployeeModal();", true);
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            LinkButton lnkUpdate = (LinkButton)sender;
            int employeeId = Convert.ToInt32(lnkUpdate.CommandArgument);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Employee WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", employeeId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        hdnEmployeeId.Value = reader["Id"].ToString();
                        txtEmpName.Text = reader["EmpName"].ToString();
                        txtEmpNumber.Text = reader["EmpNumber"].ToString();
                        ddlGender.SelectedValue = reader["Gender"].ToString();
                        txtDateOfBirth.Text = Convert.ToDateTime(reader["Date_of_birth"]).ToString("yyyy-MM-dd");
                        txtSalary.Text = reader["Salary"].ToString();
                        txtPosition.Text = reader["Position"].ToString();
                    }
                }
            }

            btnSaveEmployee.Text = "Update Employee";
            ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openEmployeeModal();", true);
        }

        protected void btnSaveEmployee_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hdnEmployeeId.Value))
            {
                InsertEmployee();
            }
            else
            {
                UpdateEmployee();
            }

            BindGridView();
            ScriptManager.RegisterStartupScript(this, GetType(), "CloseModal", "$('#employeeModal').modal('hide');", true);
        }

        private void InsertEmployee()
        {
            string query = "INSERT INTO Employee (EmpName, EmpNumber, Gender, Date_of_birth, Salary, Position) " +
                           "VALUES (@EmpName, @EmpNumber, @Gender, @DateOfBirth, @Salary, @Position)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@EmpName", txtEmpName.Text);
                    cmd.Parameters.AddWithValue("@EmpNumber", txtEmpNumber.Text);
                    cmd.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue);
                    cmd.Parameters.AddWithValue("@DateOfBirth", Convert.ToDateTime(txtDateOfBirth.Text));
                    cmd.Parameters.AddWithValue("@Salary", Convert.ToDecimal(txtSalary.Text));
                    cmd.Parameters.AddWithValue("@Position", txtPosition.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateEmployee()
        {
            string query = "UPDATE Employee SET EmpName = @EmpName, EmpNumber = @EmpNumber, Gender = @Gender, " +
                           "Date_of_birth = @DateOfBirth, Salary = @Salary, Position = @Position WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(hdnEmployeeId.Value));
                    cmd.Parameters.AddWithValue("@EmpName", txtEmpName.Text);
                    cmd.Parameters.AddWithValue("@EmpNumber", txtEmpNumber.Text);
                    cmd.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue);
                    cmd.Parameters.AddWithValue("@DateOfBirth", Convert.ToDateTime(txtDateOfBirth.Text));
                    cmd.Parameters.AddWithValue("@Salary", Convert.ToDecimal(txtSalary.Text));
                    cmd.Parameters.AddWithValue("@Position", txtPosition.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnkDelete = (LinkButton)sender;
            int employeeId = Convert.ToInt32(lnkDelete.CommandArgument);

            string query = "DELETE FROM Employee WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", employeeId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            BindGridView();
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

                BindGridView();
                gvEmployees.DataSource = dt;
                gvEmployees.DataBind();
            } 
        }
        private void ClearForm()
        {
            hdnEmployeeId.Value = string.Empty;
            txtEmpName.Text = string.Empty;
            txtEmpNumber.Text = string.Empty;
            ddlGender.SelectedIndex = 0;
            txtDateOfBirth.Text = string.Empty;
            txtSalary.Text = string.Empty;
            txtPosition.Text = string.Empty;
        }
    }
}