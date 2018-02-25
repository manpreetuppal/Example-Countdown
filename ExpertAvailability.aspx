<%@ Page Title="Mena Council" Language="C#" MasterPageFile="~/MenaCouncil.master" AutoEventWireup="true" CodeFile="ExpertAvailability.aspx.cs" Inherits="ExpertAvailability" %>

<%@ Register Src="~/Controls/SideMenu.ascx" TagPrefix="uc1" TagName="SideMenu" %>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function pageLoad() {
            $(".column-selected").click(function () {
                var Availability = "";
                var selectedDay = $(this).parent().find('.normalWeight').text();
                var selectedTime = $('.selected-time').find('th').eq($(this).index()).find('.time').text();
                var Time = selectedDay + '_' + selectedTime;
                var Time1 = ',' + selectedDay + '_' + selectedTime;
                var Time2 = selectedDay + '_' + selectedTime + ',';
                if ($(this).hasClass("active-bg")) {
                    $(this).removeClass("active-bg");

                    if ($('#<%=hdnDay.ClientID%>').val().indexOf(Time1) > -1) {
                        Availability = $('#<%=hdnDay.ClientID%>').val().replace(Time1, '');
                    }
                    if ($('#<%=hdnDay.ClientID%>').val().indexOf(Time2) > -1) {
                        Availability = $('#<%=hdnDay.ClientID%>').val().replace(Time2, '');
                    }
                }
                else {
                    $(this).addClass("active-bg");
                    if ($('#<%=hdnDay.ClientID%>').val() == "") {
                        Availability = Time;
                    }
                    else {
                        Availability = $('#<%=hdnDay.ClientID%>').val() + "," + Time
                    }
                }
                $('#<%=hdnDay.ClientID%>').val(Availability);

              
            });

            if ($('#<%=hdnDay.ClientID%>').val() != "") {
                var Times = $('#<%=hdnDay.ClientID%>').val();
                var arr = Times.split(',');
                var Day = "";
                var Time = "";
                $.each(arr, function (index, value) {
                    var a = value.split('_');
                    Day = a[0];
                    Time = a[1]
                    $(".normalWeight:contains(" + Day.substring(0,3) + ")").parent().find('td').eq(Time).addClass('active-bg');
                });
            }
        }
        function showmsg(msg) {
            $('#spnSmallMessages').text(msg);
            jQuery('#smallpopup').css('display', 'block');
            $('#smallpopup').fadeTo(4000, 500).slideUp(500, function () {
                $('#smallpopup').alert('close');
               
            });
        }
    </script>
    <style>
        .time {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="upMenaCouncil" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:UpdateProgress runat="server" ID="PageUpdateProgress" AssociatedUpdatePanelID="upMenaCouncil"
                DisplayAfter="1">

                <ProgressTemplate>

                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.1;">
                        <div style="z-index: -9999;">
                            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Images/loader.gif" Width="100px" Height="100px" Style="padding: 10px; position: fixed; top: 35%; left: 55%" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:HiddenField ID="hdnCountry" runat="server" />
            <asp:HiddenField ID="hdnSubject" runat="server" />
            <asp:HiddenField ID="hdnStatus" runat="server" />
            <asp:HiddenField runat="server" ID="hdnDay" />
            <%--            <asp:HiddenField runat="server" ID="hdnDbDay"  />--%>
            <section class="main-cont">
                <div class="container">
                    <div class="edit-profile">

                        <!-- Steps Container Start -->
                        <div class="row">
                            <div class="col-md-3 col-sm-4">
                                <div class="edit-profile-left">
                                    <div class="side-menu-container">
                                        <uc1:SideMenu runat="server" ID="SideMenu1" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-9 col-sm-8 bdr-lft">
                                <div class="alert-custom" id="smallpopup" style="display: none;margin-top:10px ">
                                    <div class="alert alert-success">
                                       <span id="spnSmallMessages"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-9 col-sm-8 bdr-lft">
                                <div class="right-portion-edit-profile">
                                    <div class="title-hd">
                                        <h3>Set Availability</h3>
                                        <p style="font-size:16px">Clients will suggest three different times for their call based on the times that you indicate here. Click at your desired time and set availability.</p>

                                    </div>

                                    <div class="availibility-table">
                                        <div class="table-outer-avail">
                                            <div class="ddd">
                                                <div class="table-wrapper ui-corner-all">
                                                    <div class="table-responsive tbl-overflx">
                                                        <table cellspacing="0" cellpadding="0" border="0" class="tbl_hours table" style="text-align: center;">
                                                            <tbody>
                                                                <tr style="background: #ddd;">
                                                                    <th></th>
                                                                    <th class="hrd_am" colspan="12">AM</th>
                                                                    <th class="hrd_pm">&nbsp;</th>
                                                                    <th class="hrd_pm" colspan="11">PM</th>
                                                                </tr>
                                                                <tr style="background: #f1f1f1;" class="selected-time">
                                                                    <th class="iocn-table">
                                                                        <img src="images/calander-blue.png" class="cal-img-style">
                                                                    </th>
                                                                    <th class="verticaltext bg-dark">12<span class="time">0</span><br>
                                                                        <span class="daytime">AM</span></th>
                                                                    <th class="verticaltext">1<span class="time">1</span> </th>
                                                                    <th class="verticaltext">2<span class="time">2</span> </th>
                                                                    <th class="verticaltext">3<span class="time">3</span> </th>
                                                                    <th class="verticaltext">4<span class="time">4</span> </th>
                                                                    <th class="verticaltext">5<span class="time">5</span> </th>
                                                                    <th class="verticaltext">6<span class="time">6</span> </th>
                                                                    <th class="verticaltext">7<span class="time">7</span> </th>
                                                                    <th class="verticaltext">8<span class="time">8</span> </th>
                                                                    <th class="verticaltext">9<span class="time">9</span> </th>
                                                                    <th class="verticaltext">10<span class="time">10</span> </th>
                                                                    <th class="verticaltext">11<span class="time">11</span> </th>
                                                                    <th class="verticaltext bg-dark">12<span class="time">12</span>
                                                                        <br>
                                                                        <span class="daytime">PM</span></th>
                                                                    <th class="verticaltext">1<span class="time">13</span></th>
                                                                    <th class="verticaltext">2<span class="time">14</span></th>
                                                                    <th class="verticaltext">3<span class="time">15</span></th>
                                                                    <th class="verticaltext">4<span class="time">16</span></th>
                                                                    <th class="verticaltext">5<span class="time">17</span></th>
                                                                    <th class="verticaltext">6<span class="time">18</span></th>
                                                                    <th class="verticaltext">7<span class="time">19</span></th>
                                                                    <th class="verticaltext">8<span class="time">20</span></th>
                                                                    <th class="verticaltext">9<span class="time">21</span></th>
                                                                    <th class="verticaltext">10<span class="time">22</span></th>
                                                                    <th class="verticaltext">11<span class="time">23</span></th>
                                                                </tr>
                                                                <tr id="day_0">
                                                                    <th class="normalWeight bg-white" align="left" style="padding-right: 10px;">MON</th>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv"></div>
                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>
                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>
                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>
                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>
                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                </tr>
                                                                <tr id="day_1">
                                                                    <th class="normalWeight bg-gray" align="left" style="padding-right: 10px;">TUE</th>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>
                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>
                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                </tr>
                                                                <tr id="day_2">
                                                                    <th class="normalWeight bg-white" align="left" style="padding-right: 10px;">WED</th>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>
                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>
                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                </tr>
                                                                <tr id="day_3">
                                                                    <th class="normalWeight bg-gray" align="left" style="padding-right: 10px;">THU</th>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>
                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>
                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                </tr>
                                                                <tr id="day_4">
                                                                    <th class="normalWeight bg-white" align="left" style="padding-right: 10px;">FRI</th>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>
                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>
                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                </tr>
                                                                <tr id="day_5">
                                                                    <th class="normalWeight bg-gray" align="left" style="padding-right: 10px;">SAT</th>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                </tr>
                                                                <tr id="day_6">
                                                                    <th class="normalWeight bg-white" align="left" style="padding-right: 10px;">SUN</th>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv ">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv ">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv ">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv ">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv ">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv ">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv ">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv ">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv ">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv ">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv ">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv ">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv ">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv ">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv ">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv ">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                    <td class="column-selected">
                                                                        <div class="leftDiv">&nbsp;</div>

                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                                <div class="out-div">

                                    <div class="btn-back-frd"  align="center">
                                  <%--      <div class="col-md-6"></div>--%>
                                        <div class="col-md-12">
                                            <asp:LinkButton runat="server" ID="Button1" class="btn btn-info btn-send-edit-page" Text="Save" OnClick="btnSave_Click" style="margin-bottom:10px" ></asp:LinkButton>
                                               </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- Steps Container End -->

                    </div>
                </div>
            </section>

                       <div class="my-mod">
                <div class="modal fade" id="LoginMessage" tabindex="-1" role="dialog" aria-labelledby="LoginMessageLabel">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header" style="background: #37a0c9!important">
                                <div class="check-bg" style="background: #37a0c9!important; color: #fff!important">
                                    <h4>To activate your profile and start receiving Call Requests please complete the following sections:</h4>
                                </div>
                            </div>
                            <div class="modal-body">
                                <ul class="login-msg-ul">
                                    <%--       <li runat="server" id="details">General Details</li>--%>
                                    <%--  <li runat="server" id="photo" >Profile Photo</li>--%>
                               <%--     <li runat="server" id="history">Work History</li>--%>
                                 <%--   <li>At least one Area of Expertise</li>--%>
                                    <li>Availibility</li>
                                    <%--       <li>Social Verifications</li>
                                    <li>Change Password</li>--%>
                                </ul>
                                <p class="pMessage"></p>
                                <button type="button" class="btn btn-default pop-close-btn" data-dismiss="modal">OK</button>
                            </div>
                            <div class="modal-footer"></div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

