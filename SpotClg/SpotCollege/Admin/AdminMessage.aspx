<%@ Page Title="Admin Message" Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" CodeBehind="AdminMessage.aspx.cs" Inherits="SpotCollege.Admin.AdminMessage" %>

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
                            <asp:Label ID="lblmsgTitle" runat="server" /><br />
                            <asp:Label ID="lblParentmsgDescription" runat="server"/><br />
                            <asp:LinkButton ID="lnkBtnDeleteAll" runat="server" Text="DeleteAll" OnClick="lnkBtnDeleteAll_Click" OnClientClick="return confirm('Are you sure want to delete AllMessage?');" CssClass="button fright"></asp:LinkButton>
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
                                                <a id="msgdel<%#Eval("MessageId") %>" href="javascript:MsgDelete('<%#Eval("MessageId")%>')" class="fright" style="color:red">Delete</a>
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

    <div id="dialog-confirm" title="Are You Sure?" style="display: none;">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Are you sure?</p>
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

        function MsgDelete(messageId) {
            $("#dialog-confirm").dialog({
                open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Yes": function () {
                        if (Page_IsValid) {
                            var msg = {
                                MessageId: messageId
                            };
                            $.ajax({
                                type: "POST",
                                beforeSend: function () { $("#LoadingImage").show(); },
                                complete: function () { $("#LoadingImage").hide(); },
                                url: "AdminMessage.aspx/MsgDelete",
                                data: JSON.stringify(msg),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    if (msg.d) {
                                        $("#msgdel" + messageId).parent().parent().parent().parent().parent().remove();
                                    }
                                }
                            });
                        }
                        $(this).dialog("close");
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                    }
                }
            });
        }
    </script>
</asp:Content>
