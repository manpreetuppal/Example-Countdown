using HashSoftwares;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiQPdf;

public partial class Company_Addpunchflyer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
     
            bindPunchOffer();
        }
    }
    private void bindPunchOffer()
    {
        SqlParameter[] sqlOffer = new SqlParameter[2];
        sqlOffer[0] = new SqlParameter("@CompanyId", Convert.ToInt32(Session["CompanyUserId"].ToString()));

        DataTable dt1 = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, CommandType.StoredProcedure, "USP_PunchCardOffers_By_Companyid", sqlOffer).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            drpPunchOffer.DataSource = dt1;
            drpPunchOffer.DataTextField = "Title";
            drpPunchOffer.DataValueField = "ID";
            drpPunchOffer.DataBind();
            ListItem licategory = new ListItem("-- Select Punch Offer --", "0");
            drpPunchOffer.Items.Insert(0, licategory);
        }
    }
    protected void btnaddflyer_Click(object sender, EventArgs e)
    {
        string url ="flyerpunch.aspx?flyer=" + 1 + "&CompanyId=" + Convert.ToInt32(Session["CompanyUserId"]) + "&offerid=" + drpPunchOffer.SelectedValue;
        Response.Write("<script>");
        Response.Write("window.open(" + "'" + url + "'" + ",'_blank')");
        Response.Write("</script>");
        //Response.Redirect("flyerpunch.aspx?flyer=" + txtflyers.Text + "&CompanyId=" + Convert.ToInt32(Session["CompanyUserId"]) + "&offerid=" + drpPunchOffer.SelectedValue);
    }
}