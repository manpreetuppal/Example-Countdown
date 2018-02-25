using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using HashSoftwares;
using System.Text;
using System.IO;


public partial class ExpertAvailability : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ExpertId"] == null)
            Response.Redirect("Default.aspx");
        if (!IsPostBack)
        {
            BindAvailability();
        }

    }

    private void BindAvailability()
    {
        SqlParameter[] sql = new SqlParameter[1];
        sql[0] = new SqlParameter("@Expert_Id", Convert.ToInt32(Session["ExpertId"]));
        DataTable dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_tblExpertAvailability_Select_By_Id", sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            //hdnDbDay.Value = dt.Rows[0]["Availability"].ToString();
            hdnDay.Value = dt.Rows[0]["Availability"].ToString();
        }
        //else
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("$(function(){ $('#LoginMessage').modal('show');});");
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), true);
        //}
      
        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string Message = "";
        if (!string.IsNullOrEmpty(hdnDay.Value))
        {
            SqlParameter[] sql = new SqlParameter[3];
            sql[0] = new SqlParameter("@Expert_Id", Convert.ToInt32(Session["ExpertId"]));
            sql[1] = new SqlParameter("@Availability", hdnDay.Value);
            sql[2] = new SqlParameter("@CreatedByUser_Id", Convert.ToInt32(Session["ExpertId"]));
            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_tblExpertAvailability_Insert", sql);
            hdnDay.Value = "";
            Response.Redirect("AddSocialLinks.aspx");
            //BindAvailability();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", "$(function(){showmsg('Record saved successfully !')});", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", "$(function(){showmsg('To set availability please make selection from calendar area !')});", true);

        }
    }
}