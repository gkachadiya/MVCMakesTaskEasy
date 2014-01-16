<%@ Page Title="DashBoard" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="SpotCollege.Admin.DashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row-fluid">
        <div class="span12">
            <div class="pattern_box box_space">
                <div class="span12">
                    <div class="width100per">
                        <div class="shadowbox">
                            <div class="h2_heading">
                                <h2>Welcome Admin..!</h2>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--<asp:HyperLink ID="lnkStudent" runat="server" NavigateUrl="~/Admin/StudentInfo.aspx">Student</asp:HyperLink>--%>
    <%--<asp:HyperLink ID="lnkUniversity" runat="server" NavigateUrl="~/Admin/UniversityInformation.aspx">University</asp:HyperLink>--%>
    <script type="text/javascript">
        $(function () {
            $("#nav li").each(function () {
                $(this).removeClass('navi_active');
            });
            $("#lnkHome").addClass("navi_active");
        });
    </script>
</asp:Content>
