<%@ Page Title="View All Alerts" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewAllAlert.aspx.cs" Inherits="SpotCollege.ViewAllAlert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%--<script>
        jQuery(window).load(function () {

            AlertBox();
            setInterval(function () { AlertBox() }, 5000);
        });

        function AlertBox() {
            $.ajax({
                url: "Handler/AlertMsg.ashx?id=All",
                contentType: "text/plain",
                data: {},
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data != "") {
                        $("#AlertListAll").html(data);
                    }
                },
                error: function () {
                }
            });
        }

    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="span12 pattern_box box_space">
                <div class="row">
                    <div class="span12">
                        <div class="h2_heading">
                            <h2>Alert List</h2>
                        </div>
                        <ul class="list_3" id="AlertListAll" runat="server">
                        </ul>
                        <div id="noUniversityMsg" style="display: none">
                            <br />
                            <br />
                            No More Alerts
                        </div>
                        <div id="waitdiv" style="display: none">
                            please wait...
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        $(function () {
            $("#nav li").each(function () {
                $(this).removeClass('navi_active');
            });
        });

        // for Scrolling
        var flag = true;
        $(window).scroll(function () {
            if ($(window).scrollTop() + $(window).height() == $(document).height()) {
                if (flag) {
                    $("#waitdiv").show();
                    flag = false
                    $.ajax({
                        type: "POST",
                        beforeSend: function () { $("#waitdiv").show(); },
                        complete: function () { $("#waitdiv").hide(); },
                        url: "ViewAllAlert.aspx/AppendAndDisplayAlert",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d == "no")
                                $("#noUniversityMsg").show();
                            else {
                                $("#noUniversityMsg").hide();
                                flag = true;
                                $("#AlertListAll").append(data.d);
                            }
                            $("#waitdiv").hide();
                        }
                    });
                }
                else {

                }
            }
        });
    </script>
</asp:Content>
