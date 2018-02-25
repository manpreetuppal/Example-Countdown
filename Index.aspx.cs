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
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using InfoSoftGlobal;
using System.Drawing;
using System.Xml;
using System.Collections;

public partial class Admin_AddGroup : System.Web.UI.Page
{
    Decimal total;
    Decimal total1;
    Decimal total2;
    Decimal total3;
    Decimal total4;
    Decimal totalChart1;
    Decimal totalChart2;
    Decimal totalChart3;
    Decimal totalChart4;
    Decimal totalChart5;
    Decimal totalChart6;
    Decimal WBHTotal1;
    Decimal WBHTotal2;
    Decimal WBHTotal3;
    Decimal WBHTotal4;
    Decimal WBHTotal5;
    Decimal WBHTotal01;
    Decimal WBHTotal02;
    Decimal WBHTotal03;
    Decimal WBHTotal04;
    Decimal WBHTotal05;
    Decimal WBHTotal06;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            ChartPlans();
            ChartPlans1();
            CreateBarGraph1();
            CreateBarGraph();

        }

    }
    private void ChartPlans()
    {

        DataTable dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_Select_PlanCharts_V2").Tables[0];
        if (dt.Rows.Count > 0)
        {
            grdChartPlans.DataSource = dt;
            grdChartPlans.DataBind();



        }
        else
        {
            grdChartPlans.DataSource = dt;
            grdChartPlans.DataBind();

        }
    }
    private void ChartPlans1()
    {

        DataTable dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_Select_PlanCharts_V3").Tables[0];
        if (dt.Rows.Count > 0)
        {

            grdChartPlans1.DataSource = dt;
            grdChartPlans1.DataBind();


        }
        else
        {
            grdChartPlans1.DataSource = dt;
            grdChartPlans1.DataBind();

        }
    }
    private void CreateBarGraph()
    {
        DataTable dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_Select_PlanCharts_V2").Tables[0];

        string xmlData, categories, Plan, Revenue, RevenueLastYear;
        //Initialize <chart> element
        xmlData = @"<graph caption='Stores' numberPrefix='' formatNumberScale='1' rotateValues='1'  decimals='0' >";
        //Initialize <categories> element - necessary to generate a multi-series chart
        categories = "<categories>";
        //Initiate <dataset> elements
        Plan = "<dataset seriesName='Plan Amount' color='#559EE0'>";
        Revenue = "<dataset seriesName='Revenue Exl Tax' color='#C80000'>";
        RevenueLastYear = "<dataset seriesName='Revenue ExTax Last Year' color='#74B44A'>";
        //Iterate through the data
        int i = 0;
        foreach (DataRow DR in dt.Rows)
        {
            //Append <category name='...' /> to strCategories
            categories += "<category name='" + DR["StoreName"].ToString() + "' />";

            //Add <set value='...' /> to both the datasets
            Plan += "<set value='" + DR["PlanAmount"].ToString() + "' />";
            Revenue += "<set value='" + DR["RevenueExlTax"].ToString() + "' />";
            RevenueLastYear += "<set value='" + DR["RevenueExTaxLastYear"].ToString() + "' />";
            i++;
        }
        //Close <categories> element
        categories += "</categories>";

        //Close <dataset> elements
        Plan += "</dataset>";
        Revenue += "</dataset>";
        RevenueLastYear += "</dataset>";

        //Assemble the entire XML now
        xmlData += categories + Plan + Revenue + RevenueLastYear + "</graph>";

        //Create the chart - MS Column 3D Chart with data contained in xmlData
        //  return FusionCharts.RenderChart("../../FusionCharts/MSColumn3D.swf", "", xmlData, "productSales", "600", "300", false, false);

        Literal1.Text = FusionCharts.RenderChartHTML(
             "FusionCharts/FCF_MSColumn3D.swf", // Path to chart's SWF
             "",                              // Leave blank when using Data String method
             xmlData,                          // xmlStr contains the chart data
             "productSales",                      // Unique chart ID
             "800", "400",                   // Width & Height of chart
             false
             );
    }
    private void CreateBarGraph1()
    {
        DataTable dt = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_Select_PlanCharts_V3").Tables[0];

        string xmlData, categories, CustomerAmount, Amount;
        //Initialize <chart> element
        xmlData = @"<graph caption='Stores' numberPrefix='' formatNumberScale='1' rotateValues='1'  decimals='0' >";
        //Initialize <categories> element - necessary to generate a multi-series chart
        categories = "<categories>";
        //Initiate <dataset> elements
        CustomerAmount = "<dataset seriesName='Customer Amount' color='#559EE0'>";
        Amount = "<dataset seriesName='Amount' color='#C80000'>";
        //Iterate through the data
        int i = 0;
        foreach (DataRow DR in dt.Rows)
        {
            //Append <category name='...' /> to strCategories
            categories += "<category name='" + DR["StoreName"].ToString() + "' />";

            //Add <set value='...' /> to both the datasets
            CustomerAmount += "<set value='" + DR["CustomerAmount"].ToString() + "' />";
            Amount += "<set value='" + DR["CustomerAmountLastYear"].ToString() + "' />";
            i++;
        }
        //Close <categories> element
        categories += "</categories>";

        //Close <dataset> elements
        CustomerAmount += "</dataset>";
        Amount += "</dataset>";

        //Assemble the entire XML now
        xmlData += categories + CustomerAmount + Amount + "</graph>";

        //Create the chart - MS Column 3D Chart with data contained in xmlData
        //  return FusionCharts.RenderChart("../../FusionCharts/MSColumn3D.swf", "", xmlData, "productSales", "600", "300", false, false);

        Literal2.Text = FusionCharts.RenderChartHTML(
             "FusionCharts/FCF_MSColumn3D.swf", // Path to chart's SWF
             "",                              // Leave blank when using Data String method
             xmlData,                          // xmlStr contains the chart data
             "productSales",                      // Unique chart ID
             "550", "400",                   // Width & Height of chart
             false
             );
    }
    protected void grdChartPlans_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblparent = (Label)e.Row.FindControl("lblDifference");
            if (lblparent.Text == "")
            {
                lblparent.Text = "0";
            }
            if (Convert.ToDouble(lblparent.Text) < 0.00)
            {
                e.Row.Cells[3].ForeColor = Color.Red;
                lblparent.ForeColor = Color.Red;
            }
            else
            {
                e.Row.Cells[3].ForeColor = Color.Black;
                lblparent.ForeColor = Color.Black;
            }

            Label lblRevenueLastYearDifference = (Label)e.Row.FindControl("lblRevenueLastYearDifference");
            if (lblRevenueLastYearDifference.Text == "")
            {
                lblRevenueLastYearDifference.Text = "0";
            }
            if (Convert.ToDouble(lblRevenueLastYearDifference.Text) < 0.00)
            {
                e.Row.Cells[3].ForeColor = Color.Red;
                lblRevenueLastYearDifference.ForeColor = Color.Red;
            }
            else
            {
                e.Row.Cells[3].ForeColor = Color.Black;
                lblRevenueLastYearDifference.ForeColor = Color.Black;
            }
            if (lblparent.Text == "0")
            {
                lblparent.Text = "kA";
            }


            if (lblRevenueLastYearDifference.Text == "0")
            {
                lblRevenueLastYearDifference.Text = "kA";
            }


            Label lblRevenueExTaxLastYear = (Label)e.Row.FindControl("lblRevenueExTaxLastYear");

            if (lblRevenueExTaxLastYear.Text == "")
            {
                lblRevenueExTaxLastYear.Text = "kA";
            }


            Label lblPlan = (Label)e.Row.FindControl("lblPlanAmount");
            decimal qty = decimal.Parse(lblPlan.Text);
            total = total + qty;


            Label lblRevenueExcTax = (Label)e.Row.FindControl("lblRevenueExlTax");
            if (lblRevenueExcTax.Text == "")
            {
                lblRevenueExcTax.Text = "0";
            }
            decimal qty1 = decimal.Parse(lblRevenueExcTax.Text);
            total1 = total1 + qty1;


            Label lblDifference = (Label)e.Row.FindControl("lblDifference");
            if (lblDifference.Text == "kA")
            {
                lblDifference.Text = "0";
            }

            decimal qty2 = decimal.Parse(lblDifference.Text);

            total2 = total2 + qty2;

            Label lblRevenueTaxLastYear = (Label)e.Row.FindControl("lblRevenueExTaxLastYear");
            if (lblRevenueTaxLastYear.Text == "kA")
            {
                lblRevenueTaxLastYear.Text = "0";
            }
            decimal qty3 = decimal.Parse(lblRevenueTaxLastYear.Text);
            total3 = total3 + qty3;

            Label lblLastYearDifference = (Label)e.Row.FindControl("lblRevenueLastYearDifference");
            if (lblLastYearDifference.Text == "kA")
            {
                lblLastYearDifference.Text = "0";
            }
            decimal qty4 = decimal.Parse(lblLastYearDifference.Text);
            total4 = total4 + qty4;

            Label lblStoreName = (Label)e.Row.FindControl("lblStoreName");

            if (lblStoreName.Text == "WBH" || lblStoreName.Text == "WND")
            {
                Label lblPlanex = (Label)e.Row.FindControl("lblPlanAmount");
                decimal qty0 = decimal.Parse(lblPlanex.Text);
                qty0 = 0;

                Label lblRevenueExcTaxex = (Label)e.Row.FindControl("lblRevenueExlTax");
                if (lblRevenueExcTaxex.Text == "")
                {
                    lblRevenueExcTaxex.Text = "0";
                }
                decimal qty01 = decimal.Parse(lblRevenueExcTaxex.Text);
                qty01 = 0;

                Label lblDifferenceex = (Label)e.Row.FindControl("lblDifference");
                if (lblDifferenceex.Text == "kA")
                {
                    lblDifferenceex.Text = "0";
                }
                decimal qty02 = decimal.Parse(lblDifferenceex.Text);
                qty02 = 0;

                Label lblRevenueTaxLastYearex = (Label)e.Row.FindControl("lblRevenueExTaxLastYear");
                if (lblRevenueTaxLastYearex.Text == "kA")
                {
                    lblRevenueTaxLastYearex.Text = "0";
                }
                decimal qty03 = decimal.Parse(lblRevenueTaxLastYearex.Text);
                qty03 = 0;

                Label lblLastYearDifferenceex = (Label)e.Row.FindControl("lblRevenueLastYearDifference");
                if (lblLastYearDifferenceex.Text == "kA")
                {
                    lblLastYearDifferenceex.Text = "0";
                }
                decimal qty04 = decimal.Parse(lblLastYearDifferenceex.Text);
                qty04 = 0;
            }
            else
            {
                Label lblPlanex = (Label)e.Row.FindControl("lblPlanAmount");
                decimal qty0 = decimal.Parse(lblPlanex.Text);
                WBHTotal1 = WBHTotal1 + qty0;


                Label lblRevenueExcTaxex = (Label)e.Row.FindControl("lblRevenueExlTax");
                if (lblRevenueExcTaxex.Text == "")
                {
                    lblRevenueExcTaxex.Text = "0";
                }
                decimal qty01 = decimal.Parse(lblRevenueExcTaxex.Text);
                WBHTotal2 = WBHTotal2 + qty01;

                Label lblDifferenceex = (Label)e.Row.FindControl("lblDifference");
                if (lblDifferenceex.Text == "kA")
                {
                    lblDifferenceex.Text = "0";
                }

                decimal qty02 = decimal.Parse(lblDifferenceex.Text);

                WBHTotal3 = WBHTotal3 + qty02;

                Label lblRevenueTaxLastYearex = (Label)e.Row.FindControl("lblRevenueExTaxLastYear");
                if (lblRevenueTaxLastYearex.Text == "kA")
                {
                    lblRevenueTaxLastYearex.Text = "0";
                }
                decimal qty03 = decimal.Parse(lblRevenueTaxLastYearex.Text);
                WBHTotal4 = WBHTotal4 + qty03;

                Label lblLastYearDifferenceex = (Label)e.Row.FindControl("lblRevenueLastYearDifference");
                if (lblLastYearDifferenceex.Text == "kA")
                {
                    lblLastYearDifferenceex.Text = "0";
                }
                decimal qty04 = decimal.Parse(lblLastYearDifferenceex.Text);
                WBHTotal5 = WBHTotal5 + qty04;



            }

            lblparent.Text = lblparent.Text + "%";
            lblRevenueLastYearDifference.Text = lblRevenueLastYearDifference.Text + "%";

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblGrandTotal = (Label)e.Row.FindControl("lblGrandTotal");
            if (lblGrandTotal.Text == "")
            {
            }
            else
            {
                Label lblTotalPlan = (Label)e.Row.FindControl("lblTotalPlan");
                lblTotalPlan.Text = total.ToString();

                Label lblTotal1 = (Label)e.Row.FindControl("lblTotalRevenueExlTax");
                lblTotal1.Text = total1.ToString();

                Label lblTotal2 = (Label)e.Row.FindControl("lblTotalDifference");
                lblTotal2.Text = total2.ToString();
                lblTotal2.Text = lblTotal2.Text + "%";

                Label lblTotal3 = (Label)e.Row.FindControl("lblTotalRevenueExTaxLastYear");
                lblTotal3.Text = total3.ToString();

                Label lblTotal4 = (Label)e.Row.FindControl("lblTotalRevenueLastYearDifference");
                lblTotal4.Text = total4.ToString();
                lblTotal4.Text = lblTotal4.Text + "%";
            }


            Label lblExclTotal = (Label)e.Row.FindControl("lblExclTotal");
            if (lblExclTotal.Text == "")
            {

            }
            else
            {
                Label lblTotalPlan = (Label)e.Row.FindControl("lblExcPlan");
                lblTotalPlan.Text = WBHTotal1.ToString();

                Label lblTotal1 = (Label)e.Row.FindControl("lblExcTotalRevenueExlTax");
                lblTotal1.Text = WBHTotal2.ToString();

                Label lblTotal2 = (Label)e.Row.FindControl("lblExclTotalDifference");
                lblTotal2.Text = WBHTotal3.ToString();
                lblTotal2.Text = lblTotal2.Text + "%";

                Label lblTotal3 = (Label)e.Row.FindControl("lblExclTotalRevenueExTaxLastYear");
                lblTotal3.Text = WBHTotal4.ToString();

                Label lblTotal4 = (Label)e.Row.FindControl("lblExclTotalRevenueLastYearDifference");
                lblTotal4.Text = WBHTotal5.ToString();
                lblTotal4.Text = lblTotal4.Text + "%";
            }


        }
    }
    protected void grdChartPlans1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCustomerAmountDifference = (Label)e.Row.FindControl("lblCustomerAmountDifference");

            if (lblCustomerAmountDifference.Text == "")
            {
                lblCustomerAmountDifference.Text = "0";
            }
            if (Convert.ToDouble(lblCustomerAmountDifference.Text) < 0.00)
            {
                e.Row.Cells[2].ForeColor = Color.Red;
                lblCustomerAmountDifference.ForeColor = Color.Red;
            }
            else
            {
                e.Row.Cells[2].ForeColor = Color.Black;
                lblCustomerAmountDifference.ForeColor = Color.Black;
            }

            if (lblCustomerAmountDifference.Text == "0")
            {
                lblCustomerAmountDifference.Text = "kA";
            }
            Label lblCustomerAmountLastYearDifference = (Label)e.Row.FindControl("lblCustomerAmountLastYearDifference");
            if (lblCustomerAmountLastYearDifference.Text == "")
            {
                lblCustomerAmountLastYearDifference.Text = "0";
            }
            if (Convert.ToDouble(lblCustomerAmountLastYearDifference.Text) < 0.00)
            {
                e.Row.Cells[2].ForeColor = Color.Red;
                lblCustomerAmountLastYearDifference.ForeColor = Color.Red;
            }
            else
            {
                e.Row.Cells[2].ForeColor = Color.Black;
                lblCustomerAmountLastYearDifference.ForeColor = Color.Black;
            }
            if (lblCustomerAmountLastYearDifference.Text == "0")
            {
                lblCustomerAmountLastYearDifference.Text = "kA";
            }
            Label lblCustomerAmountLastYear = (Label)e.Row.FindControl("lblCustomerAmountLastYear");
            if (lblCustomerAmountLastYear.Text == "")
            {
                lblCustomerAmountLastYear.Text = "kA";
            }
            Label lblRevenueLastYear = (Label)e.Row.FindControl("lblRevenueLastYear");
            if (lblRevenueLastYear.Text == "")
            {
                lblRevenueLastYear.Text = "kA";
            }
            Label lblPlan = (Label)e.Row.FindControl("lblCustomer");
            if (lblPlan.Text == "")
            {
                lblPlan.Text = "0";
            }
            decimal qty = decimal.Parse(lblPlan.Text);
            totalChart1 = totalChart1 + qty;

            Label lblRevenueExcTax = (Label)e.Row.FindControl("lblCustomerAmountLastYear");
            if (lblRevenueExcTax.Text == "kA")
            {
                lblRevenueExcTax.Text = "0";
            }
            decimal qty1 = decimal.Parse(lblRevenueExcTax.Text);
            totalChart2 = totalChart2 + qty1;

            Label lblDifference = (Label)e.Row.FindControl("lblCustomerAmountDifference");
            if (lblDifference.Text == "kA")
            {
                lblDifference.Text = "0";
            }
            decimal qty2 = decimal.Parse(lblDifference.Text);
            totalChart3 = totalChart3 + qty2;
            Label lblRevenueTaxLastYear = (Label)e.Row.FindControl("lblRevenue");
            if (lblRevenueTaxLastYear.Text == "")
            {
                lblRevenueTaxLastYear.Text = "0";
            }
            decimal qty3 = decimal.Parse(lblRevenueTaxLastYear.Text);
            totalChart4 = totalChart4 + qty3;
            Label lblLastYearDifference = (Label)e.Row.FindControl("lblRevenueLastYear");
            if (lblLastYearDifference.Text == "kA")
            {
                lblLastYearDifference.Text = "0";
            }
            decimal qty4 = decimal.Parse(lblLastYearDifference.Text);
            totalChart5 = totalChart5 + qty4;
            Label lblAmountLastYearDifference = (Label)e.Row.FindControl("lblCustomerAmountLastYearDifference");
            if (lblAmountLastYearDifference.Text == "kA")
            {
                lblAmountLastYearDifference.Text = "0";
            }
            decimal qty5 = decimal.Parse(lblAmountLastYearDifference.Text);
            totalChart6 = totalChart6 + qty5;
            Label lblStoreName1 = (Label)e.Row.FindControl("lblStores");
            if (lblStoreName1.Text == "WBH" || lblStoreName1.Text == "WND")
            {
                Label lblPlanex = (Label)e.Row.FindControl("lblCustomer");
                if (lblPlanex.Text == "")
                {

                    lblPlanex.Text = "0";
                }
                decimal qty00 = decimal.Parse(lblPlanex.Text);
                qty00 = 0;

                Label lblRevenueExcTaxex1 = (Label)e.Row.FindControl("lblCustomerAmountLastYear");
                if (lblRevenueExcTaxex1.Text == "kA")
                {
                    lblRevenueExcTaxex1.Text = "0";
                }
                decimal qty001 = decimal.Parse(lblRevenueExcTaxex1.Text);
                qty001 = 0;

                Label lblDifferenceex1 = (Label)e.Row.FindControl("lblCustomerAmountDifference");
                if (lblDifferenceex1.Text == "kA")
                {
                    lblDifferenceex1.Text = "0";
                }
                decimal qty002 = decimal.Parse(lblDifferenceex1.Text);
                qty002 = 0;

                Label lblRevenueTaxLastYearex1 = (Label)e.Row.FindControl("lblRevenue");
                if (lblRevenueTaxLastYearex1.Text == "")
                {
                    lblRevenueTaxLastYearex1.Text = "0";
                }
                decimal qty003 = decimal.Parse(lblRevenueTaxLastYearex1.Text);
                qty003 = 0;

                Label lblLastYearDifferenceex1 = (Label)e.Row.FindControl("lblRevenueLastYear");
                if (lblLastYearDifferenceex1.Text == "kA")
                {
                    lblLastYearDifferenceex1.Text = "0";
                }
                decimal qty004 = decimal.Parse(lblLastYearDifferenceex1.Text);
                qty004 = 0;
                Label lblLastYearDifferenceexc1 = (Label)e.Row.FindControl("lblCustomerAmountLastYearDifference");
                if (lblLastYearDifferenceexc1.Text == "kA")
                {
                    lblLastYearDifferenceexc1.Text = "0";
                }
                decimal qty005 = decimal.Parse(lblLastYearDifferenceexc1.Text);
                qty005 = 0;
            }
            else
            {
                Label lblPlanex = (Label)e.Row.FindControl("lblCustomer");
                decimal qty00 = decimal.Parse(lblPlanex.Text);
                WBHTotal01 = WBHTotal01 + qty00;
                Label lblRevenueExcTaxex = (Label)e.Row.FindControl("lblCustomerAmountLastYear");
                if (lblRevenueExcTaxex.Text == "")
                {
                    lblRevenueExcTaxex.Text = "0";
                }
                decimal qty001 = decimal.Parse(lblRevenueExcTaxex.Text);
                WBHTotal02 = WBHTotal02 + qty001;

                Label lblDifferenceex = (Label)e.Row.FindControl("lblCustomerAmountDifference");
                if (lblDifferenceex.Text == "kA")
                {
                    lblDifferenceex.Text = "0";
                }
                decimal qty002 = decimal.Parse(lblDifferenceex.Text);
                WBHTotal03 = WBHTotal03 + qty002;
                Label lblRevenueTaxLastYearex = (Label)e.Row.FindControl("lblRevenue");
                if (lblRevenueTaxLastYearex.Text == "kA")
                {
                    lblRevenueTaxLastYearex.Text = "0";
                }
                decimal qty003 = decimal.Parse(lblRevenueTaxLastYearex.Text);
                WBHTotal04 = WBHTotal04 + qty003;

                Label lblLastYearDifferenceex = (Label)e.Row.FindControl("lblRevenueLastYear");
                if (lblLastYearDifferenceex.Text == "kA")
                {
                    lblLastYearDifferenceex.Text = "0";
                }
                decimal qty004 = decimal.Parse(lblLastYearDifferenceex.Text);
                WBHTotal05 = WBHTotal05 + qty004;
                Label lblLastYearDifferenceex1 = (Label)e.Row.FindControl("lblCustomerAmountLastYearDifference");
                if (lblLastYearDifferenceex1.Text == "kA")
                {
                    lblLastYearDifferenceex1.Text = "0";
                }
                decimal qty005 = decimal.Parse(lblLastYearDifferenceex1.Text);
                WBHTotal06 = WBHTotal06 + qty005;
            }
            lblCustomerAmountDifference.Text = lblCustomerAmountDifference.Text + "%";

            lblCustomerAmountLastYearDifference.Text = lblCustomerAmountLastYearDifference.Text + "%";
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblGrandTotal1 = (Label)e.Row.FindControl("lblgrndTotal");
            if (lblGrandTotal1.Text == "")
            {
            }
            else
            {
                Label lblTotalPlan = (Label)e.Row.FindControl("lblCustomerAmount");
                lblTotalPlan.Text = totalChart1.ToString();
                Label lblTotal1 = (Label)e.Row.FindControl("lblTotalCustomerAmountLastYear");
                lblTotal1.Text = totalChart2.ToString();
                Label lblTotal2 = (Label)e.Row.FindControl("lblTotalCustomerAmountDifference");
                lblTotal2.Text = totalChart3.ToString();
                lblTotal2.Text = lblTotal2.Text + "%";
                Label lblTotal3 = (Label)e.Row.FindControl("lblTotalRevenue");
                lblTotal3.Text = totalChart4.ToString();
                Label lblTotal4 = (Label)e.Row.FindControl("lblTotalRevenueLastYear");
                lblTotal4.Text = totalChart5.ToString();
                Label lblTotal5 = (Label)e.Row.FindControl("lblTotalCustomerAmountLastYearDifference");
                lblTotal5.Text = totalChart6.ToString();
                lblTotal5.Text = lblTotal5.Text + "%";
            }
            Label lblGrandTotal01 = (Label)e.Row.FindControl("lblExcluTotal");
            if (lblGrandTotal01.Text == "")
            {
            }
            else
            {
                Label lblTotalPlan1 = (Label)e.Row.FindControl("lblExcCustomerAmount");
                lblTotalPlan1.Text = WBHTotal01.ToString();
                Label lblTotal01 = (Label)e.Row.FindControl("lblExcTotalCustomerAmountLastYear");
                lblTotal01.Text = WBHTotal02.ToString();
                Label lblTotal02 = (Label)e.Row.FindControl("lblExcTotalCustomerAmountDifference");
                lblTotal02.Text = WBHTotal03.ToString();
                lblTotal02.Text = lblTotal02.Text + "%";
                Label lblTotal03 = (Label)e.Row.FindControl("lblExcTotalRevenue");
                lblTotal03.Text = WBHTotal04.ToString();
                Label lblTotal04 = (Label)e.Row.FindControl("lblExcTotalRevenueLastYear");
                lblTotal04.Text = WBHTotal05.ToString();
                Label lblTotal05 = (Label)e.Row.FindControl("lblExcTotalCustomerAmountLastYearDifference");
                lblTotal05.Text = WBHTotal06.ToString();
                lblTotal05.Text = lblTotal05.Text + "%";
            }
        }
    }
}
