<%@ Page Title="ImpresBi- View Results " Language="C#" MasterPageFile="~/Individual/Individual.master" AutoEventWireup="true" CodeFile="ViewResults.aspx.cs" Inherits="Individual_ManageUsers" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>

        $(function () {
            $("#btnClose").click(function () {
                $(".ui-dialog.ui-widget.ui-widget-content.ui-corner-all.ui-front.ui-draggable.ui-resizable").hide();
                $(".ui-widget-overlay.ui-front").hide();

            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--    <asp:UpdatePanel ID="upManageTest" runat="server">
        <ContentTemplate>--%>
    <%-- <asp:UpdateProgress runat="server" ID="PageUpdateProgress" AssociatedUpdatePanelID="upManageTest"
                DisplayAfter="1">
                <ProgressTemplate>
                    <div class="overlay" id="divProgress">
                        &nbsp;
                <asp:Image GenerateEmptyAlternateText="true" ID="Image1" runat="server" Width="50"
                    Height="40" ImageUrl="~/Admin/images/ajaxloading.gif" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>
    <h3 class="page-title">View Results
    </h3>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li>
                <i class="fa fa-home"></i>
                <a href="Index.aspx">Home</a>
                <i class="fa fa-angle-right"></i>
            </li>
            <li>
                <a href="ManageTestAssignment.aspx">View Results
                </a>
            </li>
        </ul>
    </div>

    <div class="row">
        <div class="col-md-12">
            <!-- BEGIN EXAMPLE TABLE PORTLET-->
            <div class="portlet box blue-hoki">
                <div class="portlet-title">
                    <div class="caption">View Results</div>
                    <div class="tools"></div>
                </div>
                <div class="portlet-body">
                    <div id="sample_1_wrapper" class="dataTables_wrapper no-footer">
                        <div class="row">
                        </div>
                        <div class="row">
                            <div class="col-md-12 col-sm-12">
                                <div id="sample_1_filter" class="dataTables_filter">
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label col-md-3" style="font-size: x-large">Test</label>
                                    <div class="col-md-9">
                                        <asp:Label ID="lblTestname" runat="server" BorderStyle="None" Font-Size="Larger" class="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label col-md-3" style="font-size: x-large">Date</label>
                                    <div class="col-md-9">
                                        <asp:Label ID="lblDate" runat="server" BorderStyle="None" Font-Size="Larger" class="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label col-md-3" style="font-size: x-large">Name </label>
                                    <div class="col-md-9">
                                        <asp:Label ID="lblUsername" runat="server" BorderStyle="None" Font-Size="Larger" class="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6" id="divResults" runat="server">
                                <div class="form-group">
                                    <label class="control-label col-md-3" style="font-size: x-large">Filter By</label>
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="drpFilter" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="drpFilter_SelectedIndexChanged" AutoPostBack="true" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <asp:Label ID="lblErrormsg" runat="server" Text="No Record Found!!" ForeColor="Red" Visible="false"></asp:Label>
                            </div>

                        </div>
                        <%--    <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button ID="btnRadar" class="control-label col-md-3" runat="server" Text="Spider Chart" OnClick="btnRadar_Click1"></asp:Button>
                                            <div class="col-md-9">
                                                <asp:Button ID="btnChart" runat="server" class="control-label col-md-3"  Text="Bar Chart" OnClick="btnChart_Click"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>--%>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div id="divliteral" runat="server" style="border: none">
                                        <asp:Chart ID="Chart1" runat="server" Height="550px" style="margin-left: 30px; border: none; background: none;" Width="1000px">

                                            <series>
                    <asp:Series Name="Average" chartArea="ChartArea1" YValueMembers="Average" XValueMember="QuestionCategory"  BackImageTransparentColor="White"  BackSecondaryColor="Orange" Color="#FF6600" ShadowColor="Orange" IsValueShownAsLabel="True" Legend="Average" >
                                                    </asp:Series>
<asp:Series Name="Individual" ShadowColor="Red" ChartArea="ChartArea1" YValueMembers="PercentageCompletion" XValueMember="QuestionCategory"  Color="#17375E" BackImageTransparentColor="White" BackSecondaryColor="Blue" IsValueShownAsLabel="True" Legend="Average">
                                                    </asp:Series>                     
                          </series>
                                            <chartareas>
                        <asp:ChartArea Name="ChartArea1" ></asp:ChartArea>
                    </chartareas>
                                            <legends>
                                                    <asp:Legend BackColor="White" LegendStyle="Row" Name="Average" Alignment="Center" BackImageAlignment="Top" DockedToChartArea="ChartArea1" Docking="Bottom" Font="Microsoft Sans Serif, 8.25pt, style=Bold" IsDockedInsideChartArea="False" IsEquallySpacedItems="True" IsTextAutoFit="False" TitleAlignment="Near">
                                                    </asp:Legend>
                                                </legends>
                                            <titles>
                                                    <asp:Title Name="Results" >
                                                    </asp:Title>
                                                </titles>
                                        </asp:Chart>
                                    </div>
                                    <div runat="server" id="divTable" visible="false">
                                        <%--        <asp:Chart Id="chart2" runat="server" Height="1000px" style="margin-left: 30px;" Width="1000px">
                                                    <series>
                    <asp:Series Name="Average" chartArea="ChartArea2" YValueMembers="Average" XValueMember="QuestionCategory"  BackImageTransparentColor="White"  BackSecondaryColor="Orange" Color="#FF6600" ShadowColor="Orange" IsValueShownAsLabel="True" Legend="Average" ChartType="Radar" CustomProperties="RadarDrawingStyle=Line, CircularLabelsStyle=Horizontal, AreaDrawingStyle=Polygon" IsXValueIndexed="True" >
                                                    </asp:Series>
<asp:Series Name="Individual" ShadowColor="Red" ChartArea="ChartArea2" YValueMembers="PercentageCompletion" XValueMember="QuestionCategory"  Color="#17375E" BackImageTransparentColor="White" BackSecondaryColor="Blue" IsValueShownAsLabel="True" Legend="Average" ChartType="Radar" CustomProperties="RadarDrawingStyle=Line, CircularLabelsStyle=Horizontal, AreaDrawingStyle=Polygon">
                                                    </asp:Series>                     
                          </series>
                                                    <chartareas>
                        <asp:ChartArea Name="ChartArea2" AlignWithChartArea="ChartArea1" ></asp:ChartArea>
                                        <asp:ChartArea Name="ChartArea1">
                                        </asp:ChartArea>
                    </chartareas>
                                                    <legends>
                                                    <asp:Legend BackColor="White" LegendStyle="Row" Name="Average" Alignment="Center" BackImageAlignment="Top" DockedToChartArea="ChartArea1" Docking="Bottom" Font="Microsoft Sans Serif, 8.25pt, style=Bold" IsDockedInsideChartArea="False" IsEquallySpacedItems="True" IsTextAutoFit="False" TitleAlignment="Near">
                                                    </asp:Legend>
                                                </legends>
                                                    <titles>
                                                    <asp:Title Name="Results" >
                                                    </asp:Title>
                                                </titles>
                                                </asp:Chart>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="row" id="divsummary" runat="server">
        <div class="col-md-12">
            <div class="form-group">
                <label class="control-label col-md-12"  font-style: italic" > <span >Overall Result:   </span>  
                   Your results indicate an overall score of
                                                <asp:Label ID="lblResult" runat="server" ></asp:Label>
                    which means you are ranked as Business Improvement</label>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <asp:Label class="control-label col-md-12" ID="lblSummary" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnNumberofquestions" runat="server" />
    <asp:HiddenField ID="hdnIndividual" runat="server" />
    <asp:HiddenField runat="server" ID="hdnTotal" />
    <asp:HiddenField runat="server" ID="hdnTestId" />
    <asp:HiddenField runat="server" ID="Education" />
    <asp:HiddenField runat="server" ID="Country" />
    <asp:HiddenField runat="server" ID="Industry" />
    <asp:HiddenField runat="server" ID="Qualification" />
    <asp:HiddenField runat="server" ID="BestDescribes" />
    <asp:HiddenField runat="server" ID="Maturity" />
    <div class="table-scrollable" id="divgrid" runat="server">
        <table class="table table-striped table-bordered table-hover dataTable no-footer" style="border: 1px solid #ddd" id="sample_1" role="grid" aria-describedby="sample_1_info">
            <thead>
                <tr role="row" style="border: 1px solid #ddd">
                    <asp:GridView ID="grdComments" DataKeyNames="Category" AllowPaging="true" HeaderStyle-BackColor="#FAC090" OnPageIndexChanging="grdComments_PageIndexChanging" OnRowDataBound="grdComments_RowDataBound" PageSize="10" Font-Names="Open Sans, sans-serif" class="sorting" HeaderStyle-Height="20px" BorderColor="#dddddd" TabIndex="0" runat="server" AutoGenerateColumns="false" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Category" HeaderStyle-Width="219px" ItemStyle-BackColor="#FAC090" HeaderStyle-Height="20px">
                                <ItemTemplate>
                                    <%#Eval("Category")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Starter" HeaderStyle-Width="219px" HeaderStyle-Height="20px">
                                <ItemTemplate>
                                    <%#Eval("Starter").ToString().Split('#')[0].ToString()%>
                                    <asp:Label ID="lblStarter" runat="server" Text='<%#Eval("Starter")%>' Visible="false"></asp:Label>
                                    <asp:LinkButton ID="lnkSeemoreStarter" runat="server" ToolTip="Click here to read full description!!" ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Intermediate" HeaderStyle-Width="219px" HeaderStyle-Height="20px">
                                <ItemTemplate>
                                    <%#Eval("Intermediate").ToString().Split('#')[0].ToString() %>
                                    <asp:Label ID="lblIntermediate" runat="server" Text='<%#Eval("Intermediate")%>' Visible="false"></asp:Label>
                                    <asp:LinkButton ID="lnkSeemoreIntermediate" runat="server" ToolTip="Click here to read full description!!" ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Advanced" HeaderStyle-Width="219px" HeaderStyle-Height="20px">
                                <ItemTemplate>
                                    <%#Eval("Advanced").ToString().Split('#')[0].ToString() %>
                                    <asp:Label ID="lblAdvanced" runat="server" Text='<%#Eval("Advanced")%>' Visible="false"></asp:Label>
                                    <asp:LinkButton ID="lnkSeemoreAdvanced" runat="server" ToolTip="Click here to read full description!!"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Master" HeaderStyle-Width="219px" HeaderStyle-Height="20px">
                                <ItemTemplate>
                                    <%#Eval("Master").ToString().Split('#')[0].ToString()%>
                                    <asp:Label ID="lblMaster" runat="server" Text='<%#Eval("Master")%>' Visible="false"></asp:Label>
                                    <asp:LinkButton ID="lnkSeemoreMaster" runat="server" ToolTip="Click here to read full description!!" ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </tr>
            </thead>
        </table>
        <div class="row">
            <div class="col-md-12 fntsz">
                <div class="form-group">
                    <asp:DataList ID="dlsummary" runat="server" BorderStyle="None" RepeatDirection="Vertical">
                        <ItemTemplate>
                            <table style="border: none">
                                <tr>
                                    <td colspan="2">
                                        <b>
                                            <asp:Label ID="lblQuestionCategory" runat="server" Text='<%# Eval("QuestionCategory")%>'></asp:Label></b>
                                        <b>Result:
                                                    <asp:Label ID="lblPercentageCompletion" runat="server" Text='<%# Eval("PercentageCompletion","{0:0.00}")%>'></asp:Label>%</b><br />
                                        <asp:Label ID="lblOverallSummary" runat="server" Text=' <%# Eval("OverallSummary") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <b>
                                            <asp:Label ID="lblQuestionCategory1" runat="server" Text='<%# Eval("QuestionCategory")%>'></asp:Label>:</b>     <b>Personal Development</b><br />
                                        <asp:Label ID="PersonalDevelopment" runat="server" Text='<%# Eval("PersonalDevelopment") %>'></asp:Label></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
    </div>
    <div id="dialog" style="width: 600px; display: none">
        <button type="button" id="btnClose" class="ui-dialog-titlebar-close">x</button>
        <div class="row">
            <div class="col-lg-12">
                <div class="col-lg-12">
                    <p class="text-left">
                        <asp:Label ID="lblDescription" runat="server"></asp:Label>
                    </p>
                </div>
            </div>
        </div>
    </div>
    <script src="//code.jquery.com/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.4/jquery-ui.min.js"></script>

    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

