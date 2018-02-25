<%@ Page Title="" Language="C#" MasterPageFile="~/Company/Company.master" AutoEventWireup="true" CodeFile="Addpunchflyer.aspx.cs" Inherits="Company_Addpunchflyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
      
        function validation() {
            var IsError = '';
            var invalid = " "; //
            IsError += ValidateDropdown(document.getElementById('<%=drpPunchOffer.ClientID%>'), "Please select offer!");

              if (IsError.length > 0) {
                  alert(IsError);
                  return false;
              }
           
             
              return true;
          }
          function isNumberKey(evt, obj) {

              var charCode = (evt.which) ? evt.which : event.keyCode
              var value = obj.value;
              var dotcontains = value.indexOf(".") != -1;
              if (dotcontains)
                  if (charCode == 46) return false;
              if (charCode == 46) return true;
              if (charCode > 31 && (charCode < 48 || charCode > 57))
                  return false;
              return true;
          }
     
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section>

        <div class="container">

            <!-- Top Row Start -->
            <div class="row">
                <div class="main-my-profile-hd">
                    <div class="col-md-6 ">
                        <h3>Create PunchCard Flyers</h3>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="dash-board">
                        <ul class="breadcrumb">
                            <li><a href="dashboard.aspx">Home </a></li>
                            <li class="active">Create PunchCard Flyers </li>
                        </ul>
                    </div>
                </div>
            </div>
            <!-- Top Row End -->
            <!-- main cont area  Start -->
            <div class="row">
                <div class="col-md-12">
                    <div class="ibox float-e-margins" style="min-height:300px;">
                        <div class="ibox-content">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-4 col-md-2 control-label">Choose Punch Offer</label>
                                    <div class="col-sm-8 col-md-10">
                                        <asp:DropDownList ID="drpPunchOffer"  Width="300" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                          <%--      <div class="form-group">
                                    <label class="col-sm-4 col-md-2 control-label">PunchCard Flyres</label>
                                    <div class="col-sm-8 col-md-10">
                                        <asp:TextBox ID="txtflyers" runat="server" Width="300" CssClass="form-control" placeholder="No of flyers" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                    </div>
                                </div>--%>
                                <div class="hr-line-dashed"></div>
                                <div class="form-group">
                                    <div class="col-sm-4 col-sm-offset-2">
                                        <asp:Button ID="btnaddflyer" runat="server" Text="Create Flyers" CssClass="btn btn-success" OnClientClick="return validation();" OnClick="btnaddflyer_Click"   />

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>




        </div>
        <!-- main cont area  End -->


    </section>
</asp:Content>

