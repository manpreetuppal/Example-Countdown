<%@ Page Title="Burger King-DashBoard" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Admin_AddGroup" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .table thead tr th {
            color: white !important;
        }

    
    </style>
    <h3 class="page-title">Dashboard 
    </h3>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li>
                <i class="fa fa-home"></i>
                <a href="Index.aspx">Home</a>
                <i class="fa fa-angle-right"></i>
            </li>
            <li>
                <a href="Index.aspx">Dashboard</a>

            </li>

        </ul>
        <div class="page-toolbar">
            <div class="btn-group pull-right">
                <button type="button" class="btn btn-fit-height grey-salt dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-delay="1000" data-close-others="true">
                    Actions <i class="fa fa-angle-down"></i>
                </button>
                <ul class="dropdown-menu pull-right" role="menu">
                    <li>
                        <a href="#">Action</a>
                    </li>
                    <li>
                        <a href="#">Another action</a>
                    </li>
                    <li>
                        <a href="#">Something else here</a>
                    </li>
                    <li class="divider"></li>
                    <li>
                        <a href="#">Separated link</a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <!-- /.modal -->
    <!-- END SAMPLE PORTLET CONFIGURATION MODAL FORM-->
    <!-- BEGIN STYLE CUSTOMIZER -->

    <!-- END STYLE CUSTOMIZER -->
    <!-- BEGIN PAGE HEADER-->

    <!-- END PAGE HEADER-->
    <!-- BEGIN PAGE CONTENT-->
    <div class="row">
        <div class="col-md-12">
            <div class="tabbable tabbable-custom boxless tabbable-reversed">
                <ul class="nav nav-tabs">
                    <li class="active"></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                </ul>

                <div class="tab-pane" id="tab_2">
                    <div class="portlet box green">
                        <div class="portlet-title">
                            <div class="caption">
                                Days Compared
                            </div>
                        </div>

                        <div class="portlet-body form">
                            <!-- BEGIN FORM-->
                            <%--  <form action="#" class="form-horizontal">--%>
                            <div class="form-body">
                                <h3 class="form-section"></h3>


                                <div class="portlet-body">
                                    <div id="sample_1_wrapper" class="dataTables_wrapper no-footer">



                                        <div class="table-scrollable">
                                            <table class="table table-striped table-bordered table-hover dataTable no-footer" id="sample_1" role="grid" style="border: 1px solid #ddd" aria-describedby="sample_1_info">
                                                <thead>
                                                    <tr role="row" class="col-lg-12">
                                                        <td class="col-lg-6">
                                                            <asp:GridView ID="grdChartPlans" DataKeyNames="StoreName" ShowFooter="true" OnRowDataBound="grdChartPlans_RowDataBound" class="even" AllowPaging="true" PageSize="10" BorderColor="#dddddd" Font-Names="Open Sans, sans-serif" TabIndex="0" runat="server" AutoGenerateColumns="false" Width="100%">

                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Stores" HeaderStyle-Width="40px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblStoreName" runat="server" Text='<%#Eval("StoreName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblGrandTotal" runat="server" Text="Grand Total"></asp:Label>
                                                                            <br />
                                                                            <asp:Label ID="lblExclTotal" runat="server" Text="Excl: WND WBH"></asp:Label>

                                                                        </FooterTemplate>

                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Plan" HeaderStyle-Width="40px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPlanAmount" runat="server" Text='<%#Eval("PlanAmount") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblTotalPlan" runat="server"></asp:Label><br />
                                                                            <asp:Label ID="lblExcPlan" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Revenue" HeaderStyle-Width="40px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRevenueExlTax" Text='<%#Eval("RevenueExlTax")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblTotalRevenueExlTax" runat="server"></asp:Label><br />
                                                                            <asp:Label ID="lblExcTotalRevenueExlTax" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Difference Plan" HeaderStyle-Width="40px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDifference" runat="server" Text='<%#Eval("Difference")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblTotalDifference" runat="server"></asp:Label><br />
                                                                            <asp:Label ID="lblExclTotalDifference" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Revenue last year" HeaderStyle-Width="40px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRevenueExTaxLastYear" Text='<%#Eval("RevenueExTaxLastYear")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblTotalRevenueExTaxLastYear" runat="server"></asp:Label><br />
                                                                            <asp:Label ID="lblExclTotalRevenueExTaxLastYear" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText=" Revenue last year Difference " HeaderStyle-Width="40px" ItemStyle-ForeColor="Red">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRevenueLastYearDifference" runat="server" Text='<%#Eval("DifferneceLastYear")%>'> </asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblTotalRevenueLastYearDifference" runat="server"></asp:Label><br />
                                                                            <asp:Label ID="lblExclTotalRevenueLastYearDifference" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <FooterStyle BackColor="#404040" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                <HeaderStyle BackColor="#404040" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                            </asp:GridView>
                                                            <br />
                                                            <div class="col-lg-6">

                                                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>

                                                            </div>
                                                        </td>
                                                        <td class="col-lg-6">
                                                            <asp:GridView ID="grdChartPlans1" DataKeyNames="StoreId" ShowFooter="true" class="even" AllowPaging="true" PageSize="10" HeaderStyle-ForeColor="White" BorderColor="#dddddd" FooterStyle-ForeColor="White" Font-Names="Open Sans, sans-serif" TabIndex="0" runat="server" AutoGenerateColumns="false" Width="100%" OnRowDataBound="grdChartPlans1_RowDataBound">

                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Stores" Visible="false" HeaderStyle-Width="70px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblStores" runat="server" Visible="false" Text='<%#Eval("StoreName") %>'></asp:Label>

                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblgrndTotal" runat="server" Text="Grand Total"></asp:Label><br />
                                                                            <asp:Label ID="lblExcluTotal" runat="server" Text="Exc WBH WND"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Customer Amount" HeaderStyle-Width="40px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCustomer" runat="server" Text='<%#Eval("CustomerAmount") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblCustomerAmount" runat="server"></asp:Label><br />
                                                                            <asp:Label ID="lblExcCustomerAmount" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="40px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCustomerAmountLastYear" Text='<%#Eval("CustomerAmountLastYear")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblTotalCustomerAmountLastYear" runat="server"></asp:Label><br />
                                                                            <asp:Label ID="lblExcTotalCustomerAmountLastYear" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Comparison Last Year" HeaderStyle-Width="40px" ItemStyle-ForeColor="Red">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCustomerAmountDifference" Text='<%#Eval("CustomerAmountDifference")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblTotalCustomerAmountDifference" runat="server"></asp:Label><br />
                                                                            <asp:Label ID="lblExcTotalCustomerAmountDifference" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Revenue" HeaderStyle-Width="40px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRevenue" runat="server" Text='<%#Eval("Revenue")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblTotalRevenue" runat="server"></asp:Label><br />

                                                                            <asp:Label ID="lblExcTotalRevenue" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText=" Revenue Last Year " HeaderStyle-Width="40px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRevenueLastYear" Text='<%#Eval("CustomerRevenueLastYear")%>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblTotalRevenueLastYear" runat="server"></asp:Label><br />

                                                                            <asp:Label ID="lblExcTotalRevenueLastYear" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText=" Difference " HeaderStyle-Width="40px" ItemStyle-ForeColor="Red">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCustomerAmountLastYearDifference" runat="server" Text='<%#Eval("AmountDifference")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblTotalCustomerAmountLastYearDifference" runat="server"></asp:Label><br />
                                                                            <asp:Label ID="lblExcTotalCustomerAmountLastYearDifference" runat="server"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <FooterStyle BackColor="#404040" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                <HeaderStyle BackColor="#404040" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                            </asp:GridView>
                                                            <br />

                                                            <div class="col-lg-6">

                                                                <asp:Literal ID="Literal2" runat="server"></asp:Literal>


                                                            </div>
                                                        </td>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                </tbody>
                                            </table>


                                        </div>

                                        <%--                                        <div class="row">
                                            <div class="col-md-5 col-sm-12">
                                                <div class="dataTables_info" id="sample_1_info" role="status" aria-live="polite">Showing 1 to 10 of 43 entries</div>
                                            </div>
                                            <div class="col-md-7 col-sm-12">
                                                <div class="dataTables_paginate paging_simple_numbers" id="sample_1_paginate">
                                                    <ul class="pagination">
                                                        <li class="paginate_button previous disabled" aria-controls="sample_1" tabindex="0" id="sample_1_previous"><a href="#"><i class="fa fa-angle-left"></i></a></li>
                                                        <li class="paginate_button active" aria-controls="sample_1" tabindex="0"><a href="#">1</a></li>
                                                        <li class="paginate_button " aria-controls="sample_1" tabindex="0"><a href="#">2</a></li>
                                                        <li class="paginate_button " aria-controls="sample_1" tabindex="0"><a href="#">3</a></li>
                                                        <li class="paginate_button " aria-controls="sample_1" tabindex="0"><a href="#">4</a></li>
                                                        <li class="paginate_button " aria-controls="sample_1" tabindex="0"><a href="#">5</a></li>
                                                        <li class="paginate_button next" aria-controls="sample_1" tabindex="0" id="sample_1_next"><a href="#"><i class="fa fa-angle-right"></i></a></li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>--%>
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                </div>

            </div>

            <%--</form>--%>
            <!-- END FORM-->
        </div>
    </div>

</asp:Content>

