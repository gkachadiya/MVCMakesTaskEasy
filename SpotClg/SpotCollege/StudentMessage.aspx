<%@ Page Title="Student Message" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentMessage.aspx.cs" Inherits="SpotCollege.Student_Message" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="span12 messagebox">
        <div class="row">
            <div class="span8">
                <div class="student_msg message_right shadowbox">
                    <div class="h2_heading">
                        <h2>
                            <asp:Label ID="lblmsgTitle" runat="server" />
                        </h2>
                    </div>
                    <ul class="list_2">
                        <asp:DataList ID="dlStudentlist" runat="server" OnItemDataBound="dlStudentlist_ItemDataBound" RepeatDirection="Horizontal" RepeatLayout="Flow" DataKeyField="FromUserName">
                            <ItemTemplate>
                                <li class="admin">
                                    <div class="list_2_img">
                                        <asp:HiddenField ID="HndstudentUserName1" runat="server" Value='<%#Eval("FromUserName") %>' />
                                        <asp:Image ID="ImgBtnPhoto1" runat="server" ImageUrl="" />
                                    </div>
                                    <div class="list_2_detail">
                                        <span>
                                            <b>
                                                <asp:Label ID="lblStudentname" runat="server" />
                                            </b>
                                        </span>
                                        <p>
                                            <asp:Label ID="lblMessage" runat="server" Text='<%#Eval("Description") %>' />
                                        </p>
                                    </div>
                                </li>
                                <li class="user"></li>
                            </ItemTemplate>
                        </asp:DataList>
                        <asp:Label ID="lblrec" runat="server" Text="No Record Found" Visible="false"></asp:Label>


                    </ul>
                    <ul class="list_4">
                        <li>
                            <label>Reply</label>
                            <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="field-validation-error" ErrorMessage="Enter Message" ControlToValidate="txtMessage" ValidationGroup="sendmessage"></asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label></label>
                            <asp:Button ID="btnMsgSend" runat="server" Text="Send" OnClick="btnMsgSend_Click" CssClass="large_button" ValidationGroup="sendmessage" />
                        </li>
                    </ul>
                </div>
            </div>
            <div class="span4">
                <ul class="student_right_block">
                    <li>
                        <div class="std_img">
                            <asp:Image ID="imgmsgstudentphoto" runat="server" ImageUrl="" />
                        </div>
                        <div class="std_name">
                            <asp:Label ID="lblmsgstudentname" runat="server" Text="" />
                            <asp:Label ID="lblmsgstudentcountry" runat="server" Text="" />
                        </div>
                    </li>
                    <li>
                        <div class="std_img">
                            <asp:Image ID="imgmsguniversityphoto" runat="server" ImageUrl="" />
                        </div>
                        <div class="std_name">
                            <asp:Label ID="lblmsgUniversityname" runat="server" Text="" />
                            <asp:Label ID="lblmsguniversitycountry" runat="server" Text="" />
                        </div>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnsender" runat="server" />
    <asp:HiddenField ID="hdnreceiver" runat="server" />
    <script type="text/javascript">
        $(function () {
            $("#nav li").each(function () {
                $(this).removeClass('navi_active');
            });

            //$("#Message").addClass('navi_active');
        });
    </script>
</asp:Content>
