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
using System.Data.OleDb;
using System.Globalization;


public partial class Import : System.Web.UI.Page
{
    DataSet ds;
    DataTable Dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        ImporttoDatatable();
    }
    private void ImporttoDatatable()
    {

        if (FlUploadcsv.HasFile)
        {

            lblmsg.Visible = false;

            string[] filesPath = Directory.GetFiles(Server.MapPath("Files/"));
            List<ListItem> files = new List<ListItem>();
            //foreach (string path__1 in filesPath)
            //{

            //    if (path__1.Contains(".xls") | path__1.Contains(".xlsm"))
            //    {
            //        System.DateTime fl = System.DateTime.Now;
            //        //dynamic rslt = fl.ToString("dd");
            //        string tst = path__1.Substring(path__1.LastIndexOf("\\") + 3, 2);
            //        //dynamic ydate = path__1.Substring(path__1.LastIndexOf("\\") + 3, 2);
            //        if (rslt != ydate)
            //        {
            //            System.IO.File.Delete(path__1);
            //        }
            //    }
            //}



            OleDbConnection conn = new OleDbConnection();
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter da = new OleDbDataAdapter();
            //DataSet ds = new DataSet();
            string query = null;
            string connString = "";
            string strFileName = DateTime.Now.ToString("MMddyyyhhmmss");
            string strFileType = System.IO.Path.GetExtension(FlUploadcsv.FileName).ToString().ToLower();

            if (strFileType == ".xls" || strFileType == ".xlsm")
            {
                FlUploadcsv.SaveAs(Server.MapPath("Files/" + strFileName + strFileType));
            }

            string strNewPath = Server.MapPath("Files/" + strFileName + strFileType);
            if (strFileType.Trim() == ".xls")
            {
                OleDbConnection OleDbcon = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strNewPath + ";Extended Properties=Excel 12.0;");
                connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strNewPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
            }
            else if (strFileType.Trim() == ".xlsm")
            {
                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strNewPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }

            conn = new OleDbConnection(connString);
            if (conn.State == ConnectionState.Closed) conn.Open();
            string SpreadSheetName = "";
            DataTable ExcelSheets = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

            SpreadSheetName = ExcelSheets.Rows[0]["TABLE_NAME"].ToString();
            query = "SELECT * FROM [" + SpreadSheetName + "]";
            cmd = new OleDbCommand(query, conn);
            da = new OleDbDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "tab1");
            Dt = ds.Tables[0];
            InsertData();

        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Please browse to select a file to upload!";
            return;
        }



    }
    private void InsertData()
    {


        for (int i = 0; i < Dt.Rows.Count; i++)
        {
            if (Dt.Rows[i][0].ToString() != "")
            {
                DataRow row = Dt.Rows[i];
                int columnCount = Dt.Columns.Count;
                string[] columns = new string[columnCount];
                for (int j = 0; j < columnCount; j++)
                {


                    columns[j] = row[j].ToString();
                }

                SqlParameter[] sql = new SqlParameter[38];
                //    sql[0] = new SqlParameter("@LOANID", columns[0]);
                // string tst = columns[1].Substring(1, columns[1].Length - 1);
                if (columns[1] != "")
                {
                    sql[0] = new SqlParameter("@Revenuesincltax", Convert.ToDecimal(columns[1]));
                }
                else
                {
                    sql[0] = new SqlParameter("@Revenuesincltax", 0);
                }
                // string tst = columns[1];
                if (columns[2] != "")
                {
                    sql[1] = new SqlParameter("@Revenuesexcltax", Convert.ToDecimal(columns[2]));
                }
                else
                {
                    sql[1] = new SqlParameter("@Revenuesexcltax", 0);
                }
                if (columns[12] != "")
                {
                    sql[2] = new SqlParameter("@ATM", Convert.ToDecimal(columns[12]));
                }
                else
                {
                    sql[2] = new SqlParameter("@ATM", 0);
                }

                if (columns[13] != "")
                {
                    sql[3] = new SqlParameter("@CreditCards", Convert.ToDecimal(columns[13]));
                }
                else
                {
                    sql[3] = new SqlParameter("@CreditCards", 0);
                }

                if (columns[15] != "")
                {
                    sql[4] = new SqlParameter("@Deductiondeposit", Convert.ToDecimal(columns[15].Replace("-","0").Replace("("," ").Replace(")","")));
                }
                else
                {
                    sql[4] = new SqlParameter("@Deductiondeposit", 0);
                }

                if (columns[17] != "")
                {
                    sql[5] = new SqlParameter("@CustomerAmount", Convert.ToDecimal(columns[17]));
                }
                else
                {
                    sql[5] = new SqlParameter("@CustomerAmount", 0);
                }

                if (columns[19] != "")
                {
                    sql[6] = new SqlParameter("@hours", Convert.ToDecimal(columns[19]));
                }
                else
                {
                    sql[6] = new SqlParameter("@hours", 0);
                }

                if (columns[23] != "")
                {
                    sql[7] = new SqlParameter("@SickLeave", Convert.ToDecimal(columns[23]));
                }
                else
                {
                    sql[7] = new SqlParameter("@SickLeave", 0);
                }

                if (columns[24] != "")
                {
                    sql[8] = new SqlParameter("@Vacationleave", Convert.ToDecimal(columns[24]));
                }
                else
                {
                    sql[8] = new SqlParameter("@Vacationleave", 0);
                }

                if (columns[28] != "")
                {
                    sql[9] = new SqlParameter("@Staffmeal", Convert.ToDecimal(columns[28]));
                }
                else
                {
                    sql[9] = new SqlParameter("@Staffmeal", 0);
                }

                if (columns[30] != "")
                {
                    sql[10] = new SqlParameter("@Waste", Convert.ToDecimal(columns[30]));
                }
                else
                {
                    sql[10] = new SqlParameter("@Waste", 0);
                }

                if (columns[32] != "")
                {
                    sql[11] = new SqlParameter("@AmountStorno", Convert.ToDecimal(columns[32]));
                }
                else
                {
                    sql[11] = new SqlParameter("@AmountStorno", 0);
                }

                if (columns[33] != "")
                {
                    sql[12] = new SqlParameter("@valuestorno", Convert.ToDecimal(columns[33]));
                }
                else
                {
                    sql[12] = new SqlParameter("@valuestorno", 0);
                }

                if (columns[35] != "")
                {
                    sql[13] = new SqlParameter("@Drivecustomeramount", Convert.ToDecimal(columns[35]));
                }
                else
                {
                    sql[13] = new SqlParameter("@Drivecustomeramount", 0);
                }

                if (columns[37] != "")
                {
                    sql[14] = new SqlParameter("@DriveCustomerRevenue", Convert.ToDecimal(columns[37]));
                }
                else
                {
                    sql[14] = new SqlParameter("@DriveCustomerRevenue", 0);
                }

                if (columns[40] != "")
                {
                    sql[15] = new SqlParameter("@Driveshare", Convert.ToDecimal(columns[40]));
                }
                else
                {
                    sql[15] = new SqlParameter("@Driveshare", 0);
                }

                if (columns[41] != "")
                {
                    sql[16] = new SqlParameter("@Minuten", Convert.ToDecimal(columns[41]));
                }
                else
                {
                    sql[16] = new SqlParameter("@Minuten", 0);
                }

                if (columns[42] != "")
                {
                    sql[17] = new SqlParameter("@Seconds", Convert.ToDecimal(columns[42]));
                }
                else
                {
                    sql[17] = new SqlParameter("@Seconds", 0);
                }

                if (columns[0] != "")
                {
                    sql[18] = new SqlParameter("@CreateDate", Convert.ToDateTime(columns[0]));
                }
                else
                {
                    sql[18] = new SqlParameter("@CreateDate", "");
                }

                if (columns[10] != "")
                {
                    sql[19] = new SqlParameter("@Revenuesexcltaxlastyear", Convert.ToDecimal(columns[10]));
                }
                else
                {
                    sql[19] = new SqlParameter("@Revenuesexcltaxlastyear", 0);
                }

                if (columns[11] != "")
                {
                    sql[20] = new SqlParameter("@Deviationperday", Convert.ToDecimal(columns[11].Replace("E-05","0")));
                }
                else
                {
                    sql[20] = new SqlParameter("@Deviationperday", 0);
                }

                if (columns[14] != "")
                {
                    sql[21] = new SqlParameter("@BankDepositTarget", Convert.ToDecimal(columns[14].Replace("#Value!", "")));
                }
                else
                {
                    sql[21] = new SqlParameter("@BankDepositTarget", 0);
                }

                if (columns[16] != "")
                {
                    sql[22] = new SqlParameter("@BankdepositIS", Convert.ToDecimal(columns[16].Replace("#Value!","")));
                }
                else
                {
                    sql[22] = new SqlParameter("@BankdepositIS", 0);
                }
                if (columns[18] != "")
                {
                    sql[23] = new SqlParameter("@SalesCustomer", Convert.ToDecimal(columns[18].Replace("kA", "0")));
                }
                else
                {
                    sql[23] = new SqlParameter("@SalesCustomer", 0);
                }
                if (columns[20] != "")
                {
                    sql[24] = new SqlParameter("@TicketsPerHour", Convert.ToDecimal(columns[20]));
                }
                else
                {
                    sql[24] = new SqlParameter("@TicketsPerHour", 0);
                }

                if (columns[21] != "")
                {
                    sql[25] = new SqlParameter("@Salesperhour", Convert.ToDecimal(columns[21]));
                }
                else
                {
                    sql[25] = new SqlParameter("@Salesperhour", 0);

                }

                if (columns[22] != "")
                {
                    sql[26] = new SqlParameter("@PersCosts", Convert.ToDecimal(columns[22]));
                }
                else
                {
                    sql[26] = new SqlParameter("@PersCosts", 0);
                }

                if (columns[25] != "")
                {
                    sql[27] = new SqlParameter("@PersCostReal", Convert.ToDecimal(columns[25]));
                }
                else
                {
                    sql[27] = new SqlParameter("@PersCostReal", 0);
                }
                if (columns[26] != "")
                {
                    sql[28] = new SqlParameter("@Payroll", Convert.ToDecimal(columns[26]));
                }
                else
                {
                    sql[28] = new SqlParameter("@Payroll", 0);
                }

                if (columns[27] != "")
                {
                    sql[29] = new SqlParameter("@Payrollreal", Convert.ToDecimal(columns[27]));
                }
                else
                {
                    sql[29] = new SqlParameter("@Payrollreal", 0);
                }

                if (columns[29] != "")
                {
                    sql[30] = new SqlParameter("@Staffmeals", Convert.ToDecimal(columns[29]));
                }
                else
                {
                    sql[30] = new SqlParameter("@Staffmeals", 0);
                }

                if (columns[31] != "")
                {
                    sql[31] = new SqlParameter("@Wastes", Convert.ToDecimal(columns[31]));
                }
                else
                {
                    sql[31] = new SqlParameter("@Wastes", 0);
                }
                if (columns[34] != "")
                {
                    sql[32] = new SqlParameter("@Cancellations", Convert.ToDecimal(columns[34]));
                }
                else
                {
                    sql[32] = new SqlParameter("@Cancellations", 0);
                }

                if (columns[36] != "")
                {
                    sql[33] = new SqlParameter("@DrivecustomeramountIn", Convert.ToDecimal(columns[36]));
                }
                else
                {
                    sql[33] = new SqlParameter("@DrivecustomeramountIn", 0);
                }


                if (columns[38] != "")
                {
                    sql[34] = new SqlParameter("@DrivecustomerrevenueIn", Convert.ToDecimal(columns[38]));
                }
                else
                {
                    sql[34] = new SqlParameter("@DrivecustomerrevenueIn", 0);
                }




                if (columns[39] != "")
                {
                    String timeText = columns[39]; // The String array contans 21:31:00

                    DateTime time = Convert.ToDateTime(timeText);
                    sql[35] = new SqlParameter("@SOSdrive", time);
                }
                else
                {
                    sql[35] = new SqlParameter("@SOSdrive", "");
                }
                sql[36] = new SqlParameter("@U_ID",42 );

                if (columns[3] != "")
                {
                    sql[37] = new SqlParameter("@Checksandgifts", Convert.ToDecimal(columns[3]));
                }
                else
                {
                    sql[37] = new SqlParameter("@Checksandgifts", 0);
                }


                SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_tblDaily_Insert", sql).ToString();
                Page.RegisterStartupScript("script", "<script language='javascript'>alert('Records has been Inserted');</script>");
            }
           
        }
    }

}