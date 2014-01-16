<%@ Page Title="DashBoard College" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashboardCollege.aspx.cs" Inherits="SpotCollege.DashboardCollege" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="pattern_box">
        <div class="row-fluid">
            <div class="msg_replay_hide">
                <div class="span12">
                    <div id="LoadingImage" style="display: none">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/ajaxloader.gif" />
                    </div>
                    <div id="Dialog-Send" title="Message Send" style="display: none;">
                        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Message Send Successfully</p>
                    </div>
                    <div class="span8 first">
                        <div class="row-fluid">
                            <div class="span6">
                                <div class="shadowbox">
                                    <div class="h2_heading">
                                        <h2>Student Who Have Shown<br />
                                            Interest</h2>
                                        <asp:LinkButton ID="lnkBtnViewAllshownInt" CssClass="more fright" OnClick="lnkBtnViewAllshownInt_Click" runat="server">View All</asp:LinkButton>
                                    </div>
                                    <ul class="list_2">
                                        <asp:DataList ID="dlStudentShownInterestList" runat="server" OnItemDataBound="dlStudentShownInterestList_ItemDataBound" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <ItemTemplate>
                                                <li>
                                                    <div class="list_2_img">
                                                        <asp:HiddenField ID="HndstudentUserName" runat="server" Value='<%#Eval("User.UserName") %>' />
                                                        <asp:ImageButton ID="ImgBtnPhoto" runat="server" CommandArgument='<%#Eval("User.UserName") %>' ImageUrl="" />

                                                    </div>

                                                    <div class="list_2_detail">
                                                        <p>
                                                            <b>
                                                                <asp:Label ID="lblstdname" runat="server" />
                                                                <%--<asp:LinkButton ID="lnkBtnViewProfile" OnClick="lnkBtnViewProfile_Click" CommandArgument='<%#Eval("User.UserName") %>' runat="server"></asp:LinkButton>--%>
                                                            </b>
                                                            from
                                                                <asp:Label ID="lblCityCountry" runat="server" />

                                                        </p>
                                                        <asp:LinkButton ID="lnkBtnViewProfile" OnClick="lnkBtnViewProfile_Click" CommandArgument='<%#Eval("User.UserName") %>' CssClass="button fright" runat="server">Profile</asp:LinkButton>
                                                        <%--<asp:LinkButton ID="lnkMsg" CssClass="msgbtn" runat="server">Message</asp:LinkButton>--%>
                                                        <%--<a id="lnkMessage" href="javascript:OpenMsgPopup('<%#Eval("User.UserName")%>')" class="msgbtn">Message</a>--%>
                                                    </div>
                                                </li>
                                            </ItemTemplate>
                                        </asp:DataList>
                                        <asp:Label ID="lblrec" runat="server" Text="No Record Found" Visible="false"></asp:Label>
                                    </ul>
                                </div>
                            </div>

                            <div class="span6">
                                <div class="shadowbox">
                                    <div class="h2_heading">
                                        <h2>Recently Joined Student </h2>
                                        <asp:LinkButton ID="lnkBtnViewAllJoined" CssClass="more fright" OnClick="lnkBtnViewAllJoined_Click" runat="server">View All</asp:LinkButton>

                                    </div>
                                    <ul class="list_2">
                                        <asp:DataList ID="dlStudentJoinedList" runat="server" OnItemDataBound="dlStudentJoinedList_ItemDataBound" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <ItemTemplate>
                                                <li>
                                                    <div class="list_2_img">
                                                        <asp:HiddenField ID="HndstudentUserName1" runat="server" Value='<%#Eval("User.UserName") %>' />
                                                        <asp:ImageButton ID="ImgBtnPhoto1" runat="server" CommandArgument='<%#Eval("User.UserName") %>' ImageUrl="" />
                                                    </div>
                                                    <div class="list_2_detail">
                                                        <p style="text-align: justify">
                                                            <b>
                                                                <asp:Label ID="lblstdname1" runat="server" />
                                                                <%--<asp:LinkButton ID="lnkBtnViewProfile1" OnClick="lnkBtnViewProfile1_Click" CommandArgument='<%#Eval("User.UserName") %>' runat="server"></asp:LinkButton>--%>
                                                            </b>from 
                                                            <asp:Label ID="lblCityCountry1" runat="server" />
                                                        </p>
                                                        <asp:LinkButton ID="lnkBtnViewProfile1" OnClick="lnkBtnViewProfile1_Click" CommandArgument='<%#Eval("User.UserName") %>' CssClass="button fright" runat="server">Profile</asp:LinkButton>
                                                        <%--<asp:LinkButton ID="lnkMsg" CssClass="msgbtn" runat="server">Message</asp:LinkButton>--%>
                                                        <%--<a href="javascript:Openpopup('<%#Eval("User.UserName")%>')" class="msgbtn">Message</a>--%>
                                                    </div>
                                                </li>
                                            </ItemTemplate>
                                        </asp:DataList>
                                        <asp:Label ID="lblrec1" runat="server" Text="No Record Found" Visible="false"></asp:Label>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="span4">
                        <div class="shadowbox">
                            <div class="h2_heading">
                                <h2>Total Student Profile Viewed</h2>
                                <%--<a href="#" class="more fright">More</a>--%>
                            </div>
                            <ul class="list_3">
                                <li>
                                    <%-- <div class="list_3_date">
                                    <span></span>
                                    <span></span>
                                </div>--%>
                                    <div class="list_3_detail">
                                        <p>No Viewed</p>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>

            <div class="msg_replay_show messagereplayfrom">
                <div class="span5">
                    <span class="msg_replay_close">Close</span>
                    <div class="trsnsparentnone_box">
                        <div class="message_right shadowbox">
                            <ul class="list_2">
                                <asp:DataList ID="dlStudentShownInterestList2" runat="server" OnItemDataBound="dlStudentShownInterestList_ItemDataBound" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <ItemTemplate>
                                        <li>
                                            <div class="list_2_img">
                                                <asp:HiddenField ID="HndstudentUserName" runat="server" Value='<%#Eval("User.UserName") %>' />
                                                <asp:ImageButton ID="ImgBtnPhoto" runat="server" CommandArgument='<%#Eval("User.UserName") %>' ImageUrl="" />
                                            </div>
                                            <div class="list_2_detail">
                                                <p>
                                                    <b>
                                                        <asp:Label ID="lblstdname" runat="server" />
                                                    </b>
                                                    <br />
                                                    <asp:Label ID="lblCityCountry" runat="server" />
                                                </p>
                                                <p>
                                                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                                </p>
                                                <asp:LinkButton ID="lnkBtnViewProfile" OnClick="lnkBtnViewProfile_Click" CommandArgument='<%#Eval("User.UserName") %>' runat="server">Profile</asp:LinkButton>

                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:DataList>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="span7">
                    <div class="row-fluid">
                        <div class="span8">
                            <div id="messageList" class="student_msg message_right shadowbox">
                            </div>
                            <%--Message Box--%>
                            <div id="messagebox" class="student_msg message_right shadowbox" style="display: none" title="Message Box">
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="message_right">
                                            <ul class="profile_form">
                                                <li class="row-fluid">
                                                    <div class="span4">
                                                        <label>Send to :</label>
                                                    </div>
                                                    <div class="span4">
                                                        <asp:TextBox runat="server" ID="txtSendToUserName" Enabled="false" />
                                                    </div>
                                                </li>
                                                <li class="row-fluid">
                                                    <div class="span4">
                                                        <label>Subject :</label>
                                                    </div>
                                                    <div class="span4">
                                                        <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="field-validation-error" ErrorMessage="Enter Subject" ControlToValidate="txtSubject" ValidationGroup="sendmessage"></asp:RequiredFieldValidator>
                                                    </div>
                                                </li>
                                                <li class="row-fluid">
                                                    <div class="span4">
                                                        <label>Message :</label>
                                                    </div>
                                                    <div class="span6">
                                                        <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="field-validation-error" ErrorMessage="Enter Message" ControlToValidate="txtMessage" ValidationGroup="sendmessage"></asp:RequiredFieldValidator>
                                                    </div>
                                                </li>
                                                <li class="row-fluid">
                                                    <div class="span4"></div>
                                                    <div class="span6">
                                                        <asp:Button ID="btnMsgSend" runat="server" Text="Send" CssClass="large_button" OnClientClick="return false" ValidationGroup="sendmessage" />
                                                        <asp:Button ID="btnMsgCancel" runat="server" class="large_button" Text="Cancel" OnClientClick="return false" />
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--End--%>

                            <div id="divLoader" class="student_msg message_right shadowbox">
                                please wait...<br />
                                <img src="Images/ajaxloader.gif" />
                            </div>
                        </div>
                        <div class="span4">
                            <ul class="student_right_block" id="ulMessageBetween">
                                <li></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="Dialog-Alert" title="" style="display: none;">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Please Enter Message Text</p>
    </div>
    <script type="text/javascript">


        $(function () {
            $(".msg_replay_show").hide();
        });
        $(".msg_replay_close").click(function () {
            flag_showpopup = false;
            $(".msg_replay_show").hide(1000);
            $(".msg_replay_hide").show(1000);
        });

        var senttoid;
        function OpenMsgPopup(sendToUserName, username) {
            $(".msg_replay_hide").hide(1000);
            $(".msg_replay_show").show(1000);
            $("#divLoader").show();
            $("#messagebox").hide();
            $("#messageList").hide();
            $("#ulMessageBetween").html("");

            var user = {
                ToUserName: username,
            };
            senttoid = username;
            $.ajax({
                type: "POST",
                beforeSend: function () { $("#LoadingImage").show(); },
                complete: function () { $("#LoadingImage").hide(); },
                url: "DashboardStudent.aspx/Getstatus",
                data: JSON.stringify(user),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;
                    if (data != "") {
                        retriveConversation(data);
                    }
                    else {
                        $("#MainContent_txtSendToUserName").val(sendToUserName);
                        $("#messagebox").show();
                        $("#messageList").hide();
                        $("#divLoader").hide();
                    }
                }
            });
        }

        function retriveConversation(parentId) {
            var msgId = {
                messageId: parentId
            };

            $.ajax({
                type: "POST",
                url: "AllMessage.aspx/GetAllConversation",
                data: JSON.stringify(msgId),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;
                    $("#messageList").html(data[0]);
                    $("#ulMessageBetween").html(data[1]);
                    $("#messagebox").hide();
                    $("#messageList").show();
                    $("#divLoader").hide();
                    bindToDisplayRemainMessage();
                }
            });

        }

        function bindToDisplayRemainMessage() {
            var str = $("#openAll").text();
            var status = true;
            $("#openAll").click(function () {
                $(".remainMsg").toggle(1000);

                if (status) {
                    $(this).text("hide Messages");
                    status = false;
                }
                else {
                    $(this).text(str);
                    status = true;
                }
            });
        }

        function SaveMessageReply(parentMsgId, title, fromuser, touser) {
            var msgdata = {
                parentMsgId: parentMsgId,
                msgDesc: $('#txtReplyDesc').val(),
                msgTitle: title,
                fromusername: fromuser,
                tousername: touser
            };
            if ($('#txtReplyDesc').val() != "") {
                $.ajax({
                    type: "POST",
                    url: "AllMessage.aspx/SaveMessageReply",
                    data: JSON.stringify(msgdata),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $("#txtReplyDesc").val("");
                        $("#ulMessage").html(msg.d + $("#ulMessage").html());
                    }
                });
            }
            else {
                $("#Dialog-Alert").dialog({
                    open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                    resizable: false,
                    height: 140,
                    modal: true,
                    buttons: {
                        "OK": function () {
                            $(this).dialog("close");
                        }

                    },
                });
            }
        }

        //For Send Message
        $("#MainContent_btnMsgSend").click(function () {
            if (Page_IsValid) {
                var Message = {
                    Title: $("#MainContent_txtSubject").val(),
                    Description: $("#MainContent_txtMessage").val(),
                    sendToUserName: senttoid,
                    ParentId: "0",
                };
                if ($("#MainContent_txtSubject").val() != "" && $("#MainContent_txtMessage").val() != "") {
                    $.ajax({
                        type: "POST",
                        beforeSend: function () { $("#LoadingImage").show(); },
                        complete: function () { $("#LoadingImage").hide(); },
                        url: "DashboardStudent.aspx/SendMessage",
                        data: JSON.stringify(Message),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {

                            $("#divLoader").hide();
                            $("#MainContent_txtSubject").val("");
                            $("#MainContent_txtMessage").val("");
                            $("#messagebox").hide();
                            $("#divLoader").show();
                            retriveConversation(msg.d);
                        }
                    });
                }
                else {
                    $("#Dialog-Alert").dialog({
                        open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                        resizable: false,
                        height: 140,
                        modal: true,
                        buttons: {
                            "OK": function () {
                                $(this).dialog("close");
                            }

                        },
                    });
                }
            }
        });

        // for Cancel Message
        $("#MainContent_btnMsgCancel").click(function () {
            $("#MainContent_txtSubject").val("");
            $("#MainContent_txtMessage").val("");
            $("#messagebox").dialog("close");
        });

        $(function () {
            $("#nav li").each(function () {
                $(this).removeClass('navi_active');
            });

            $("#Home").addClass('navi_active');
        });
    </script>
</asp:Content>
