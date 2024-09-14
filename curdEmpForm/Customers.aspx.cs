using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace curdEmpForm
{
    public partial class Customers : System.Web.UI.Page
    { sampleEmpDBEntities db=new sampleEmpDBEntities();
        private string uploadDirectory = "~/Uploads/";

        protected void Page_Load(object sender, EventArgs e)
        {
            var list = from c in db.Customers select c;
            grd.DataSource = list.ToList();
            grd.DataBind();

        }
      

        protected void sreach_Click(object sender, EventArgs e)
        {
            var mycustomers = db.Customers.ToList();
            if (txtsearching.Text == "")
            {
                grd.DataSource = mycustomers;
                grd.DataBind();
            }
            else
            {
                 grd.DataSource = mycustomers.Where(x => x.Country == txtsearching.Text || x.City == txtsearching.Text || x.CustomerID == txtsearching.Text).ToList();
                grd.DataBind();
            }





        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var list =from c in db.Customers select new { c.CustomerID,c.CompanyName,c.ContactName,c.Country,c.Phone };
            grd.DataSource = list.ToList();
            grd.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var list =from c in db.Products orderby c.UnitPrice descending select c;
            grd.DataSource = list.ToList();
            grd.DataBind(); 

        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFiles)
            {
                string physicalPath = Server.MapPath(uploadDirectory);
                if (!Directory.Exists(physicalPath))
                {
                    Directory.CreateDirectory(physicalPath);
                }

                foreach (HttpPostedFile uploadedFile in fileUpload.PostedFiles)
                {
                    string filePath = Path.Combine(physicalPath, uploadedFile.FileName);
                    uploadedFile.SaveAs(filePath);
                }

                lblUploadStatus.Text = $"{fileUpload.PostedFiles.Count} file(s) uploaded successfully.";
                lblUploadStatus.ForeColor = System.Drawing.Color.Green;
                LoadFiles();
            }
            else
            {
                lblUploadStatus.Text = "Please select file(s) to upload.";
                lblUploadStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void LoadFiles()
        {
            string[] filePaths = Directory.GetFiles(Server.MapPath(uploadDirectory));
            DataTable dt = new DataTable();
            dt.Columns.Add("FileName");
            dt.Columns.Add("FileSize");

            foreach (string filePath in filePaths)
            {
                FileInfo file = new FileInfo(filePath);
                dt.Rows.Add(file.Name, file.Length);
            }

            gvFiles.DataSource = dt;
            gvFiles.DataBind();
        }

        protected void gvFiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DownloadFile")
            {
                string fileName = e.CommandArgument.ToString();
                string filePath = Path.Combine(Server.MapPath(uploadDirectory), fileName);

                if (File.Exists(filePath))
                {
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
                    Response.TransmitFile(filePath);
                    Response.End();
                }
            }
        }
    }
}