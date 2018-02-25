<%@ Application Language="C#" %>
<%@ Import Namespace="HashSoftwares" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Data" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs
        Exception exc = Server.GetLastError();

        HttpContext.Current.Response.Write(exc.Message.ToString() + "<br/>");

        HttpContext.Current.Response.Write(exc.InnerException + "<br/>");

        string ErrorUrl = (HttpContext.Current.Request.Url).ToString();

        string lastindex = ErrorUrl.Substring(ErrorUrl.LastIndexOf('/') + 1);

        //string messgae = (exc.Message).ToString();


        //string lastline = messgae.Substring(messgae.LastIndexOf('(') - 1);

        StackTrace st = new StackTrace(new StackFrame(true));

        StackFrame sf = st.GetFrame(o);



        SqlParameter[] SQL = new SqlParameter[7];

        if (exc.InnerException == null)

        {

            SQL[0] = new SqlParameter("@Name", (exc.Message).ToString());

        }

        else

        {

            SQL[0] = new SqlParameter("@Name", (exc.InnerException).ToString());

        }

        SQL[1] = new SqlParameter("@Description", (exc.Data).ToString());

        SQL[2] = new SqlParameter("@PageName", lastindex);

        SQL[3] = new SqlParameter("@PageUrl", ErrorUrl);

        SQL[4] = new SqlParameter("@LineNumber", (sf.GetFileLineNumber() - 1));


        SQL[5] = new SqlParameter("@ByUser_Id", '0');

        string ByUserType = "Guest";

        if (Request.Url.AbsoluteUri.Contains("/Admin")) ByUserType = "Admin";

        if (Request.Url.AbsoluteUri.Contains("/StaffMember")) ByUserType = "Staff Member";

        if (Request.Url.AbsoluteUri.Contains("/Patient")) ByUserType = "Patient";

        if (Request.Url.AbsoluteUri.Contains("/SalesRep")) ByUserType = "SalesRep";

        if (Request.Url.AbsoluteUri.Contains("/Practitioner")) ByUserType = "Practitioner";

        SQL[6] = new SqlParameter("@ByUserType", ByUserType);


        SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["con"].ConnectionString, CommandType.StoredProcedure, "USP_tblErrors_Insert", SQL);

        // Sending email to developer & admin of this website 

        //StreamReader sr = File.OpenText(Server.MapPath("Email_Templates/errormessage.htm"));

        //string strContents = sr.ReadToEnd();

        //strContents = strContents.Replace("[CONTACTNAME]", "Sourabh Sachdeva");

        //strContents = strContents.Replace("[ISSUE]", SQL[0].Value.ToString());

        //strContents = strContents.Replace("[DESCRIPTION]", (exc.Data).ToString());

        //strContents = strContents.Replace("[PAGENAME]", lastindex);

        //strContents = strContents.Replace("[PAGEURL]", ErrorUrl);




        //SendEmail.SendMail("sourabh@hashsoftwares.com", ConfigurationManager.AppSettings["MailFrom"].ToString(), "OOOPSS... !!! Someone facing issue on healthtrack. Lets try to find/solve this quickly !", strContents);

        //sr.Dispose();

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
