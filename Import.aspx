<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Import.aspx.cs" Inherits="Import" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
 <div id="page-wrapper">
                <div class="row">
                    <div class="col-lg-12">
                        <h1>Import <small> Loan</small></h1>
                        <ol class="breadcrumb">
                            <li><a href="index.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
                            <li class="active"><i class="fa fa-edit"></i>Import /Loan</li>
                        </ol>
                    </div>
                </div>

                <div class="row">
                    <!-- left section starts -->
                    <div class="col-lg-8 adduser">
                        <div class="form-group">
                            <label class="col-lg-5 text-right"> </label>
                            <%--<input placeholder="Name" class="form-control col-lg-7">--%>
                                                   
                                  <asp:FileUpload ID="FlUploadcsv" runat="server" />
                            <asp:Label ID="lblmsg" runat ="server" Text="" ForeColor="Red" Visible="false"  ></asp:Label>
                </div>


                        </div>
                        

                      

                       
            <asp:GridView ID="gvEmployee" runat="server" width="100%">                    
                    <HeaderStyle BackColor="#89A0FE" />
                 </asp:GridView>


                        <div class="form-group">
                            <span class="col-lg-5"></span>
                            <%--    <button class="btn btn-default" type="submit">Submit</button>--%>
                            <asp:Button ID="btnsubmit" runat="server" class="btn btn-default" Text="Upload" OnClick="btnsubmit_Click" OnClientClick="return Validations();" />
                            <asp:Button ID="btnupdate" runat="server" class="btn btn-default" Text="Update" OnClientClick="return Validations();" Style="display: none" />
                        </div>
                    </div>
                    <!-- left section finsih -->

                </div>
        
            <!-- /#page-wrapper -->


    <script type="text/javascript" language="javascript">
        function Validations() {
            var IsError = '';
            var invalid = " "; // Invalid character is a space



            if (IsError.length > 0) {
                alert(IsError);
                return false;
            }
            return true;
        }
    </script>
    </form>
</body>
</html>
