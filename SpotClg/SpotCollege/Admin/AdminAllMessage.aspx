<%@ Page Title="Admin All Message" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AdminAllMessage.aspx.cs" Inherits="SpotCollege.Admin.AdminAllMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="span12 messagebox">
        <div class="row">
            <div class="span12">

                <%--For Filterring--%>
                <div class="span8">
                    <ul class="profile_form">
                        <li class="row">
                            <div class="span2">
                                <label>
                                    Search By Username :</label>
                            </div>
                            <div class="span2">
                                <asp:TextBox ID="txtFilterByUserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="field-validation-error" ControlToValidate="txtFilterByUserName" ErrorMessage="UserName is Required" ValidationGroup="SearchValidations"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" CssClass="field-validation-error" ControlToValidate="txtFilterByUserName" ValidationGroup="SearchValidations" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid UserName"></asp:RegularExpressionValidator>
                            </div>
                        </li>
                        <li class="row">
                            <div class="span2">
                                <label>
                                    From Date :</label>
                            </div>
                            <div class="span2">
                                <span class="datepickericon"></span>
                                <asp:TextBox ID="txtFilterByFromDate" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="field-validation-error" ControlToValidate="txtFilterByFromDate" ErrorMessage="From Date is Required" ValidationGroup="SearchValidations"></asp:RequiredFieldValidator>
                            </div>
                            <div class="span2">
                                <label>
                                    To Date :</label>
                            </div>
                            <div class="span2">
                                <span class="datepickericon"></span>
                                <asp:TextBox ID="txtFilterByToDate" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="field-validation-error" ControlToValidate="txtFilterByToDate" ErrorMessage="To Date is Required" ValidationGroup="SearchValidations"></asp:RequiredFieldValidator>
                            </div>
                            <div class="row">
                                <div class="span2" />
                                <div class="span2">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" ValidationGroup="SearchValidations" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </li>

                    </ul>
                </div>

                <div class="message_right">
                    <ul class="user_msg_box" id="paginationul">
                        <asp:DataList ID="dlStudentlist" runat="server" OnItemDataBound="dlStudentlist_ItemDataBound" RepeatDirection="Horizontal" RepeatLayout="Flow" DataKeyField="FromUserName">
                            <ItemTemplate>
                                <li>
                                    <div class="user_img">
                                        <asp:HiddenField ID="HndstudentUserName1" runat="server" Value='<%#Eval("ToUserName") %>' />
                                        <asp:Image ID="ImgBtnPhoto1" runat="server" alt="" />
                                    </div>
                                    <div class="user_name">
                                        <asp:Label ID="lblStudentname" CssClass="fleft" runat="server" />
                                    </div>
                                    <div class="user_msg">
                                        <asp:LinkButton ID="lnkBtnTitle" OnClick="lnkBtnTitle_Click" CommandArgument='<%#Eval("MessageId") %>' Text='<%#Eval("Title") %>' runat="server"></asp:LinkButton><br />
                                        <asp:Label ID="lblMessage" runat="server" Text='<%#Eval("Description") %>' />
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:DataList>
                        <asp:Label ID="lblrec" runat="server" Text="No Record Found" Visible="false"></asp:Label>
                    </ul>
                    <div id="noUniversityMsg" style="display: none">
                        <br />
                        <br />
                        No More Messages
                    </div>
                    <div id="waitdiv" style="display: none">
                        please wait...
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
            $("#lnkMessage").addClass("navi_active");

            $("#MainContent_txtFilterByFromDate").datepicker();
            $("#MainContent_txtFilterByToDate").datepicker();
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
                        url: "AdminAllMessage.aspx/AppendAndDisplayMessage",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d == "no")
                                $("#noUniversityMsg").show();
                            else {
                                $("#noUniversityMsg").hide();
                                flag = true;
                                $("#MainContent_dlStudentlist").append(data.d);
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
