using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HashSoftwares;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Drawing;

public partial class Individual_ManageUsers : System.Web.UI.Page
{
    Decimal Total = 0;
    Decimal starter;
    Decimal Intermediate;
    Decimal Advanced;
    Decimal Master;
    string StarterSeemore;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Subscription"] != null)
            {
                Response.Redirect("ManageTestAssignment.aspx");
            }
            bindTest();
            bindFilterList();
            CreateBarGraph();
            Reports();
            KeyDevelopment();
        }
    }
    protected void bindFilterList()
    {
        SqlParameter[] sqlSearch = new SqlParameter[1];
        sqlSearch[0] = new SqlParameter("@ContactId", Convert.ToInt32(Session["ContactId"]));
        DataTable dt1 = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "Usp_tblContacts_SelectBy_User", sqlSearch).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            ListItem lst = new ListItem("--Select--", "0");
            drpFilter.Items.Insert(drpFilter.Items.Count - 0, lst);
            ListItem lstCountry = new ListItem("By Country - " + dt1.Rows[0]["Country"].ToString(), "1");
            drpFilter.Items.Insert(drpFilter.Items.Count - 0, lstCountry);
            Country.Value = dt1.Rows[0]["Country"].ToString();
            ListItem lstOrganization = new ListItem("By Role in Organization - " + dt1.Rows[0]["BestDescribes"].ToString(), "2");
            if (lstOrganization.Text == "By Role in Organization - --Select--")
                lstOrganization.Attributes.Add("display", "none");

            else if (lstOrganization.Text == "By Role in Organization - ")
                lstOrganization.Attributes.Add("display", "none");
            else
            {
                drpFilter.Items.Insert(drpFilter.Items.Count - 0, lstOrganization);
                BestDescribes.Value = dt1.Rows[0]["BestDescribes"].ToString();
            }
            ListItem lstIndustry = new ListItem("By Industry - " + dt1.Rows[0]["Industry"].ToString(), "3");
            if (lstIndustry.Text == "By Industry - ")
                lstIndustry.Attributes.Add("display", "none");
            else
            {
                drpFilter.Items.Insert(drpFilter.Items.Count - 0, lstIndustry);
                Industry.Value = dt1.Rows[0]["Industry"].ToString();
            }
            ListItem lstQualification = new ListItem("By BI Qualification - " + dt1.Rows[0]["ImprovementQualification"].ToString(), "4");
            if (lstQualification.Text == "By BI Qualification - --select--")
                lstQualification.Attributes.Add("display", "none");
            else if (lstQualification.Text == "By BI Qualification - ")
                lstQualification.Attributes.Add("display", "none");
            else
            {
                drpFilter.Items.Insert(drpFilter.Items.Count - 0, lstQualification);
                Qualification.Value = dt1.Rows[0]["ImprovementQualification"].ToString();
            }
            ListItem lstEducation = new ListItem("By Education Level - " + dt1.Rows[0]["EducationLevel"].ToString(), "5");
            if (lstEducation.Text == "By Education Level - --select--")
                lstEducation.Attributes.Add("display", "none");
            else if (lstEducation.Text == "By Education Level - ")
                lstEducation.Attributes.Add("display", "none");
            else
            {
                drpFilter.Items.Insert(drpFilter.Items.Count - 0, lstEducation);
                Education.Value = dt1.Rows[0]["EducationLevel"].ToString();
            }
            ListItem lstBIMaturityperceive = new ListItem("By Six Sigma Training - " + dt1.Rows[0]["BIMaturityperceive"].ToString(), "6");
            if (lstBIMaturityperceive.Text == "By Six Sigma Training - --select--")
                lstBIMaturityperceive.Attributes.Add("display", "none");
            else if (lstBIMaturityperceive.Text == "By Six Sigma Training - ")
                lstBIMaturityperceive.Attributes.Add("display", "none");
            else
            {
                drpFilter.Items.Insert(drpFilter.Items.Count - 0, lstBIMaturityperceive);
                Maturity.Value = dt1.Rows[0]["BIMaturityperceive"].ToString();
            }
        }
    }
    private void bindTest()
    {
        SqlParameter[] sqlQuestions = new SqlParameter[1];
        sqlQuestions[0] = new SqlParameter("@TAId", Request.QueryString["TAId"]);
        DataTable dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_tblTestAttempt_ViewResults_By_TAId", sqlQuestions).Tables[0];
        if (dt.Rows.Count > 0)
        {
            lblUsername.Text = dt.Rows[0]["Name"].ToString();
            lblDate.Text = Convert.ToDateTime(dt.Rows[0]["DateCreated"]).ToString("dd-MM-yyyy");
            lblTestname.Text = dt.Rows[0]["TestName"].ToString();
            hdnTestId.Value = dt.Rows[0]["Test_Id"].ToString();
            Session["TestId"] = hdnTestId.Value;
            string type = dt.Rows[0]["MembershipType"].ToString();
            if (type == "Trial" || type=="")
                divResults.Visible = false;
            else
                divResults.Visible = true;
        }
    }
    private void Reports()
    {
        SqlParameter[] sqlResponse = new SqlParameter[1];
        sqlResponse[0] = new SqlParameter("@TAID", Request.QueryString["TAId"]);
        DataTable dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_Show_Comments", sqlResponse).Tables[0];
        if (dt.Rows.Count > 0)
        {
            grdComments.DataSource = dt;
            grdComments.DataBind();
        }
    }
    private void KeyDevelopment()
    {
        SqlParameter[] sqlKeyDevelopment = new SqlParameter[1];
        sqlKeyDevelopment[0] = new SqlParameter("@TAId", Request.QueryString["TAId"]);
        DataTable dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_tblTestAttempt_Show_Results_By_Lowest_Category_with_Membership_Type", sqlKeyDevelopment).Tables[0];
        if (dt.Rows.Count > 0)
        {
            dlsummary.DataSource = dt;
            dlsummary.DataBind();
        }
    }
    private void CreateBarGraph()
    {
        SqlParameter[] sqlQuestions = new SqlParameter[1];
        sqlQuestions[0] = new SqlParameter("@TAId", Request.QueryString["TAId"]);
        DataTable dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_tblTestAttempt_Show_Results_By_ID", sqlQuestions).Tables[0];
        SqlParameter[] sqlReports = new SqlParameter[2];
        sqlReports[0] = new SqlParameter("@TA_Id", Request.QueryString["TAId"]);
        sqlReports[1] = new SqlParameter("@Test_Id", hdnTestId.Value);
        DataTable dt1 = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_Select_All_Results_By_Average", sqlReports).Tables[0];
        int j = 0;

        for (int i = 0; i < dt1.Rows.Count; i++)
        {

            Chart1.Series["Average"].Points.AddXY(dt1.Rows[i]["QuestionCategory"].ToString(), Math.Round(Convert.ToDecimal(dt1.Rows[i]["Average"].ToString()), 2));
            Chart1.Series["Average"].ToolTip = "#VALX, #VALY";

            //chart2.Series["Average"].Points.AddXY(dt1.Rows[i]["QuestionCategory"].ToString(), dt1.Rows[i]["Average"].ToString().Substring(0, 5));
            //chart2.Series["Average"].ToolTip = "#VALX, #VALY";

        }
        for (j = 0; j < dt.Rows.Count; j++)
        {

            Chart1.Series["Individual"].Points.AddXY(dt.Rows[j]["QuestionCategory"].ToString(), Math.Round(Convert.ToDecimal(dt.Rows[j]["PercentageCompletion"].ToString()), 2));
            Chart1.Series["Individual"].ToolTip = "#VALX, #VALY";
             decimal Percentage=Convert.ToDecimal(dt.Rows[j]["PercentageCompletion"].ToString());
             Total = Math.Round(Total + Percentage);         
            //chart2.Series["Individual"].Points.AddXY(dt.Rows[j]["QuestionCategory"].ToString(), dt.Rows[j]["PercentageCompletion"].ToString().Substring(0, 5));
            //chart2.Series["Individual"].ToolTip = "#VALX, #VALY";
        }
        int Percent = (Convert.ToInt32(Total) / Convert.ToInt32(j.ToString()));
        lblResult.Text = Convert.ToInt32(Percent).ToString()+"%";

        Chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 100;
        Chart1.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
        Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
        Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
        //chart2.ChartAreas["ChartArea2"].AxisY.Maximum = 100;
        //chart2.ChartAreas["ChartArea2"].AxisY.Minimum = 0;
        //chart2.ChartAreas["ChartArea2"].Area3DStyle.Enable3D = false;
        //chart2.ChartAreas["ChartArea2"].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
        //chart2.ChartAreas["ChartArea2"].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
    }
    private void bindChartBySearch()
    {
        SqlParameter[] sqlQuestions = new SqlParameter[1];
        sqlQuestions[0] = new SqlParameter("@TAId", Request.QueryString["TAId"]);
        DataTable dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_tblTestAttempt_Show_Results_By_ID", sqlQuestions).Tables[0];
        SqlParameter[] sqlSearch = new SqlParameter[3];
        sqlSearch[0] = new SqlParameter("@Test_Id", Convert.ToInt32(Session["TestId"]));
        sqlSearch[1] = new SqlParameter("@Type", drpFilter.SelectedValue);
        sqlSearch[2] = new SqlParameter("@ContactId", Convert.ToInt32(Session["ContactId"]));
        DataTable dt1 = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_tblTestAttempt_Average_ByFilters", sqlSearch).Tables[0];
        int j = 0;
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            Chart1.Series["Average"].Points.AddXY(dt1.Rows[i]["Category"].ToString(), dt1.Rows[i]["Average"].ToString().Substring(0, 5));
            Chart1.Series["Average"].ToolTip = "#VALX, #VALY";
        }
        for (j = 0; j < dt.Rows.Count; j++)
        {
            Chart1.Series["Individual"].Points.AddXY(dt.Rows[j]["QuestionCategory"].ToString(), dt.Rows[j]["PercentageCompletion"].ToString().Substring(0, 5));
            Chart1.Series["Individual"].ToolTip = "#VALX, #VALY";
        }
        Chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 100;
        Chart1.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
        Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
        Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;

    }
    private void CreateBarGraphEducation()
    {
        SqlParameter[] sqlQuestions = new SqlParameter[1];
        sqlQuestions[0] = new SqlParameter("@TAId", Request.QueryString["TAId"]);
        DataTable dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_tblTestAttempt_Show_Results_By_ID", sqlQuestions).Tables[0];
        SqlParameter[] sqlReports = new SqlParameter[2];
        sqlReports[0] = new SqlParameter("@EducationLevel", Education.Value);
        sqlReports[1] = new SqlParameter("@Test_Id", hdnTestId.Value);
        DataTable dt1 = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_tblTestAttempt_Filter_By_EducationLevel", sqlReports).Tables[0];
        int j = 0;
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            Chart1.Series["Average"].Points.AddXY(dt1.Rows[i]["Category"].ToString(), Math.Round(Convert.ToDecimal(dt1.Rows[i]["Average"].ToString()), 2));
            Chart1.Series["Average"].ToolTip = "#VALX, #VALY";
        }
        for (j = 0; j < dt.Rows.Count; j++)
        {
            Chart1.Series["Individual"].Points.AddXY(dt.Rows[j]["QuestionCategory"].ToString(), Math.Round(Convert.ToDecimal(dt.Rows[j]["PercentageCompletion"].ToString()), 2));
            Chart1.Series["Individual"].ToolTip = "#VALX, #VALY";
        }
        Chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 100;
        Chart1.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
        Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
        Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
    }
    private void CreateBarGraphCountry()
    {
        SqlParameter[] sqlQuestions = new SqlParameter[1];
        sqlQuestions[0] = new SqlParameter("@TAId", Request.QueryString["TAId"]);
        DataTable dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_tblTestAttempt_Show_Results_By_ID", sqlQuestions).Tables[0];
        SqlParameter[] sqlReports = new SqlParameter[2];
        sqlReports[0] = new SqlParameter("@Country", Country.Value);
        sqlReports[1] = new SqlParameter("@Test_Id", hdnTestId.Value);
        DataTable dt1 = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_tblTestAttempt_Filter_By_Country", sqlReports).Tables[0];
        int j = 0;
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            Chart1.Series["Average"].Points.AddXY(dt1.Rows[i]["Category"].ToString(), Math.Round(Convert.ToDecimal(dt1.Rows[i]["Average"].ToString()), 2));
            Chart1.Series["Average"].ToolTip = "#VALX, #VALY";
        }
        for (j = 0; j < dt.Rows.Count; j++)
        {
            Chart1.Series["Individual"].Points.AddXY(dt.Rows[j]["QuestionCategory"].ToString(), Math.Round(Convert.ToDecimal(dt.Rows[j]["PercentageCompletion"].ToString()), 2));
            Chart1.Series["Individual"].ToolTip = "#VALX, #VALY";
        }
        Chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 100;
        Chart1.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
        Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
        Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
    }
    private void CreateBarGraphOrganization()
    {
        SqlParameter[] sqlQuestions = new SqlParameter[1];
        sqlQuestions[0] = new SqlParameter("@TAId", Request.QueryString["TAId"]);
        DataTable dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_tblTestAttempt_Show_Results_By_ID", sqlQuestions).Tables[0];
        SqlParameter[] sqlReports = new SqlParameter[2];
        sqlReports[0] = new SqlParameter("@BestDescribes", BestDescribes.Value);
        sqlReports[1] = new SqlParameter("@Test_Id", hdnTestId.Value);
        DataTable dt1 = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_tblTestAttempt_Filter_By_BestDescribes", sqlReports).Tables[0];
        int j = 0;
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            Chart1.Series["Average"].Points.AddXY(dt1.Rows[i]["Category"].ToString(), Math.Round(Convert.ToDecimal(dt1.Rows[i]["Average"].ToString()), 2));
            Chart1.Series["Average"].ToolTip = "#VALX, #VALY";
        }
        for (j = 0; j < dt.Rows.Count; j++)
        {
            Chart1.Series["Individual"].Points.AddXY(dt.Rows[j]["QuestionCategory"].ToString(), Math.Round(Convert.ToDecimal(dt.Rows[j]["PercentageCompletion"].ToString()), 2));
            Chart1.Series["Individual"].ToolTip = "#VALX, #VALY";
        }
        Chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 100;
        Chart1.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
        Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
        Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
    }
    private void CreateBarGraphIndustry()
    {
        SqlParameter[] sqlQuestions = new SqlParameter[1];
        sqlQuestions[0] = new SqlParameter("@TAId", Request.QueryString["TAId"]);
        DataTable dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_tblTestAttempt_Show_Results_By_ID", sqlQuestions).Tables[0];
        SqlParameter[] sqlReports = new SqlParameter[2];
        sqlReports[0] = new SqlParameter("@Industry", Industry.Value);
        sqlReports[1] = new SqlParameter("@Test_Id", hdnTestId.Value);
        DataTable dt1 = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_tblTestAttempt_Filter_By_Industry", sqlReports).Tables[0];
        int j = 0;
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            Chart1.Series["Average"].Points.AddXY(dt1.Rows[i]["Category"].ToString(), Math.Round(Convert.ToDecimal(dt1.Rows[i]["Average"].ToString()), 2));
            Chart1.Series["Average"].ToolTip = "#VALX, #VALY";
        }
        for (j = 0; j < dt.Rows.Count; j++)
        {
            Chart1.Series["Individual"].Points.AddXY(dt.Rows[j]["QuestionCategory"].ToString(), Math.Round(Convert.ToDecimal(dt.Rows[j]["PercentageCompletion"].ToString()), 2));
            Chart1.Series["Individual"].ToolTip = "#VALX, #VALY";
        }
        Chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 100;
        Chart1.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
        Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
        Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
    }
    private void CreateBarQualification()
    {
        SqlParameter[] sqlQuestions = new SqlParameter[1];
        sqlQuestions[0] = new SqlParameter("@TAId", Request.QueryString["TAId"]);
        DataTable dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_tblTestAttempt_Show_Results_By_ID", sqlQuestions).Tables[0];
        SqlParameter[] sqlReports = new SqlParameter[2];
        sqlReports[0] = new SqlParameter("@ImprovementQualification", Qualification.Value);
        sqlReports[1] = new SqlParameter("@Test_Id", hdnTestId.Value);
        DataTable dt1 = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_tblTestAttempt_Filter_By_ImprovementQualification", sqlReports).Tables[0];
        int j = 0;
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            Chart1.Series["Average"].Points.AddXY(dt1.Rows[i]["Category"].ToString(), Math.Round(Convert.ToDecimal(dt1.Rows[i]["Average"].ToString()), 2));
            Chart1.Series["Average"].ToolTip = "#VALX, #VALY";
        }
        for (j = 0; j < dt.Rows.Count; j++)
        {
            Chart1.Series["Individual"].Points.AddXY(dt.Rows[j]["QuestionCategory"].ToString(), Math.Round(Convert.ToDecimal(dt.Rows[j]["PercentageCompletion"].ToString()), 2));
            Chart1.Series["Individual"].ToolTip = "#VALX, #VALY";
        }
        Chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 100;
        Chart1.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
        Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
        Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
    }
    private void CreateBarBIMaturityperceive()
    {
        SqlParameter[] sqlQuestions = new SqlParameter[1];
        sqlQuestions[0] = new SqlParameter("@TAId", Request.QueryString["TAId"]);
        DataTable dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_tblTestAttempt_Show_Results_By_ID", sqlQuestions).Tables[0];
        SqlParameter[] sqlReports = new SqlParameter[2];
        sqlReports[0] = new SqlParameter("@BIMaturityperceive", Maturity.Value);
        sqlReports[1] = new SqlParameter("@Test_Id", hdnTestId.Value);
        DataTable dt1 = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_tblTestAttempt_Filter_By_BIMaturityperceive", sqlReports).Tables[0];
        int j = 0;
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            Chart1.Series["Average"].Points.AddXY(dt1.Rows[i]["Category"].ToString(), Math.Round(Convert.ToDecimal(dt1.Rows[i]["Average"].ToString()), 2));
            Chart1.Series["Average"].ToolTip = "#VALX, #VALY";
        }
        for (j = 0; j < dt.Rows.Count; j++)
        {
            Chart1.Series["Individual"].Points.AddXY(dt.Rows[j]["QuestionCategory"].ToString(), Math.Round(Convert.ToDecimal(dt.Rows[j]["PercentageCompletion"].ToString()), 2));
            Chart1.Series["Individual"].ToolTip = "#VALX, #VALY";
        }
        Chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 100;
        Chart1.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
        Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
        Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
        Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
    }
    protected void grdComments_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdComments.PageIndex = e.NewPageIndex;
        Reports();
    }
    protected void grdComments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStarter = (Label)e.Row.FindControl("lblStarter");
            LinkButton lnkseemore = (LinkButton)e.Row.FindControl("lnkSeemoreStarter");
            string text = lblStarter.Text;
            if (text == "")
            {
                lblStarter.Text = "No question of this category is attempted";
            }
            else
            {
                string integers = text.Split('#', '@')[1].ToString();
                starter = Convert.ToDecimal(integers);
            }
            Label lblIntermediate = (Label)e.Row.FindControl("lblIntermediate");
            string textlblIntermediate = lblIntermediate.Text;

            if (textlblIntermediate == "")
            {
                lblIntermediate.Text = "No question of this category is attempted";
            }
            else
            {
                string integerslblIntermediate = textlblIntermediate.Split('#', '@')[1].ToString();
                Intermediate = Convert.ToDecimal(integerslblIntermediate);
            }
            Label lblAdvanced = (Label)e.Row.FindControl("lblAdvanced");
            string textlblAdvanced = lblAdvanced.Text;
            if (textlblAdvanced == "")
            {
                lblAdvanced.Text = "No question of this category is attempted";
            }
            else
            {
                string integerslblAdvanced = textlblAdvanced.Split('#', '@')[1].ToString();
                Advanced = Convert.ToDecimal(integerslblAdvanced);
            }
            Label lblMaster = (Label)e.Row.FindControl("lblMaster");
            string textlblMaster = lblMaster.Text;
            if (textlblMaster == "")
            {
                lblMaster.Text = "No question of this category is attempted";
            }
            else
            {
                string integerslblMaster = textlblMaster.Split('#', '@')[1].ToString();
                Master = Convert.ToDecimal(integerslblMaster);
            }
            Total = Convert.ToDecimal(starter + Intermediate + Advanced + Master) / 4;
            if (Total <= Convert.ToDecimal(25.000000))
            {
                e.Row.Cells[1].Attributes.Add("style", "color: #000; background-color: #FF6600;");
                lblStarter.Attributes.Add("style", "color: #000;background-color: #FF6600;");
            }
            else if (Total <= Convert.ToDecimal(50.000000))
            {
                e.Row.Cells[2].Attributes.Add("style", "color: #000;background-color: #FF6600;");
                lblIntermediate.Attributes.Add("style", "color: #000;background-color: #FF6600;");
            }
            else if (Total <= Convert.ToDecimal(75.000000))
            {
                e.Row.Cells[3].Attributes.Add("style", "color: #000;background-color: #FF6600;");
                lblAdvanced.Attributes.Add("style", "color: #000;background-color: #FF6600;");
            }
            else if (Total <= Convert.ToDecimal(100.000000))
            {
                e.Row.Cells[4].Attributes.Add("style", "color: #000;background-color: #FF6600;");
                lblMaster.Attributes.Add("style", "color: #000;background-color: #FF6600;");
            }
            else
            {
                e.Row.Cells[4].BackColor = Color.White;
                lblMaster.BackColor = Color.White;
            }
        }
    }
    protected void drpFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpFilter.SelectedItem.Text == "--Select--")
            CreateBarGraph();
        else if (drpFilter.SelectedValue == "6")
            CreateBarBIMaturityperceive();
        else if (drpFilter.SelectedValue == "5")
            CreateBarGraphEducation();
        else if (drpFilter.SelectedValue == "4")
            CreateBarQualification();
        else if (drpFilter.SelectedValue == "3")
            CreateBarGraphIndustry();
        else if (drpFilter.SelectedValue == "2")
            CreateBarGraphOrganization();
        else if (drpFilter.SelectedValue == "1")
            CreateBarGraphCountry();
    }
    //protected void btnRadar_Click1(object sender, EventArgs e)
    //{
    //    divTable.Visible = true;
    //    divliteral.Visible = false;
    //    CreateBarGraph();
    //}
    //protected void btnChart_Click(object sender, EventArgs e)
    //{
    //    divTable.Visible = false;
    //    divliteral.Visible = true;
    //    CreateBarGraph();
    //}

}