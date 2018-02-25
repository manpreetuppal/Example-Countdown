using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class excel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
            string FilePath = Server.MapPath(FolderPath + FileName);
            FileUpload1.SaveAs(FilePath);
            GetExcelSheets(FilePath, Extension, "Yes");
        }
    }
    private void GetExcelSheets(string FilePath, string Extension, string isHDR)
    {
        string conStr = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                         .ConnectionString;
                break;
            case ".xlsx": //Excel 07
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                         .ConnectionString;
                break;
        }

        //Get the Sheets in Excel WorkBoo
        conStr = String.Format(conStr, FilePath, isHDR);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        cmdExcel.Connection = connExcel;
        connExcel.Open();

        //Bind the Sheets to DropDownList
        ddlSheets.Items.Clear();
        ddlSheets.Items.Add(new ListItem("--Select Sheet--", ""));
        ddlSheets.DataSource = connExcel
                 .GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        ddlSheets.DataTextField = "TABLE_NAME";
        ddlSheets.DataValueField = "TABLE_NAME";
        ddlSheets.DataBind();
        connExcel.Close();
        txtTable.Text = "";
        lblFileName.Text = Path.GetFileName(FilePath);
        Panel2.Visible = true;
        Panel1.Visible = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string FileName = lblFileName.Text;
        string Extension = Path.GetExtension(FileName);
        string FolderPath = Server.MapPath(ConfigurationManager
                           .AppSettings["FolderPath"]);
        string CommandText = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                CommandText = "spx_ImportFromExcel03";
                break;
            case ".xlsx": //Excel 07
                CommandText = "spx_ImportFromExcel07";
                break;
        }
        //Read Excel Sheet using Stored Procedure
        //And import the data into Database Table
        String strConnString = ConfigurationManager
                     .ConnectionStrings["conString"].ConnectionString;
        SqlConnection con = new SqlConnection(strConnString);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = CommandText;
        cmd.Parameters.Add("@SheetName", SqlDbType.VarChar).Value =
                       ddlSheets.SelectedItem.Text;
        cmd.Parameters.Add("@FilePath", SqlDbType.VarChar).Value =
                       FolderPath + FileName;
        cmd.Parameters.Add("@HDR", SqlDbType.VarChar).Value =
                       rbHDR.SelectedItem.Text;
        cmd.Parameters.Add("@TableName", SqlDbType.VarChar).Value =
                       txtTable.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            object count = cmd.ExecuteNonQuery();
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = count.ToString() + " records inserted.";
        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = ex.Message;
        }
        finally
        {
            con.Close();
            con.Dispose();
            Panel1.Visible = true;
            Panel2.Visible = false;
        }
    }
}