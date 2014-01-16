<%@ Page Title="View All Student List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewAllList.aspx.cs" Inherits="SpotCollege.ViewAllList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="pattern_box">
        <div class="row-fluid">
            <div class="msg_replay_hide">
                <div class="span12 shadowbox">
                    <div class="row-fluid">
                        <div class="span12">
                            <div id="LoadingImage" style="display: none">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/ajaxloader.gif" />
                            </div>
                            <div class="h2_heading">
                                <h2>Student List</h2>
                            </div>
                            <ul id="paginationul" class="clg_listing">
                                <asp:DataList ID="dlStudentShownInterestList" runat="server" OnItemDataBound="dlStudentShownInterestList_ItemDataBound" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <ItemTemplate>
                                        <li>
                                            <div class="clg_img">
                                                <asp:HiddenField ID="HndstudentUserName" runat="server" Value='<%#Eval("User.UserName") %>' />
                                                <%--<asp:ImageButton ID="ImgBtnPhoto" Style="height: 60px; width: 60px" runat="server" OnClick="ImgBtnPhoto_Click" CommandArgument='<%#Eval("User.UserName") %>' ImageUrl="" />--%>
                                                <asp:Image ID="ImgBtnPhoto" runat="server" alt="" />
                                            </div>
                                            <div class="clg_descripition">
                                                <p>
                                                    <input type="hidden" id="hndId" value="<%#Eval("User.UserName") %>" />
                                                    <%--<asp:HiddenField ID="hndId" Value='<%#Eval("User.UserName") %>'  runat="server" />--%>
                                                    <b>
                                                        <asp:LinkButton ID="lnkBtnViewProfile" OnClick="lnkBtnViewProfile_Click" CommandArgument='<%#Eval("User.UserName") %>' runat="server"></asp:LinkButton>
                                                    </b>
                                                    <br />
                                                    Applying from                                                 
                                                <asp:Label ID="lblAddressCityCountry" runat="server" />
                                                    for 
                                                <asp:Label ID="lblLookingFor" runat="server"></asp:Label>
                                                    <%-- in
                                                <%# Eval("DesiredFieldOfStudy") %>
                                                in
                                                <%# Eval("LookingForCountry") %>--%>
                                                    <br />
                                                    Desired joining in date 
                                                <asp:Label ID="lblExpectedJoining" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="clg_apply">
                                                <%--<a href="#" class="button fright">Message</a>--%>
                                                <asp:HyperLink ID="lnkMessage" runat="server" CssClass="msgbtn fright">Message</asp:HyperLink>
                                                <%--<a id="lnkMessage" href="javascript:OpenMsgPopup('<%#Eval("StudentId")%>','<%#Eval("FirstName") %> <%#Eval("LastName") %>')" class="msgbtn fright" >Message</a>--%>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:DataList>
                                <asp:Label ID="lblrec" runat="server" Text="No Record Found" Visible="false"></asp:Label>
                            </ul>
                            <div id="noUniversityMsg" style="display: none" class="left_right_space">
                                No More Students
                            </div>
                            <div id="waitdiv" style="display: none" class="left_right_space">
                                please wait...
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="Dialog-Send" title="Message Send" style="display: none;">
                <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Message Send Successfully</p>
            </div>
            <div class="msg_replay_show messagereplayfrom">
                <div class="span5">
                    <span class="msg_replay_close">Close</span>
                    <div class="trsnsparentnone_box">
                        <div class="message_right shadowbox">
                            <ul class="user_msg_box paginationul_two list_5">
                                <asp:DataList ID="dlStudentShownInterestList2" runat="server" OnItemDataBound="dlStudentShownInterestList_ItemDataBound2" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <ItemTemplate>
                                        <li>
                                            <div class="clg_img" style="width: 8%; border: 0px">
                                                <asp:HiddenField ID="HndstudentUserName" runat="server" Value='<%#Eval("User.UserName") %>' />
                                                <%--<asp:ImageButton ID="ImgBtnPhoto" Style="height: 60px; width: 60px" runat="server" OnClick="ImgBtnPhoto_Click" CommandArgument='<%#Eval("User.UserName") %>' ImageUrl="" />--%>
                                                <asp:Image ID="ImgBtnPhoto" runat="server" alt="" />
                                            </div>
                                            <div class="clg_descripition">
                                                <p>
                                                    <input type="hidden" id="hndId" value="<%#Eval("User.UserName") %>" />
                                                    <%--<asp:HiddenField ID="hndId" Value='<%#Eval("User.UserName") %>'  runat="server" />--%>
                                                    <b>
                                                        <asp:LinkButton ID="lnkBtnViewProfile" OnClick="lnkBtnViewProfile_Click" CommandArgument='<%#Eval("User.UserName") %>' runat="server"></asp:LinkButton>
                                                    </b>
                                                    <br />
                                                    Applying from                                                 
                                                <asp:Label ID="lblAddressCityCountry" runat="server" />
                                                    for 
                                                <asp:Label ID="lblLookingFor" runat="server"></asp:Label>
                                                    <%-- in
                                                <%# Eval("DesiredFieldOfStudy") %>
                                                in
                                                <%# Eval("LookingForCountry") %>--%>
                                                    <br />
                                                    Desired joining in date 
                                                <asp:Label ID="lblExpectedJoining" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="clg_apply">
                                                <%--<a href="#" class="button fright">Message</a>--%>
                                                <asp:HyperLink ID="lnkMessage" runat="server" CssClass="msgbtn fright">Message</asp:HyperLink>
                                                <%--<a id="lnkMessage" href="javascript:OpenMsgPopup('<%#Eval("StudentId")%>','<%#Eval("FirstName") %> <%#Eval("LastName") %>')" class="msgbtn fright" >Message</a>--%>
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
                            <%--For Message PopUp--%>
                            <div id="messagebox" class="span12 messagebox" style="display: none" title="Message Box">
                                <div class="row">
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
                                                        <asp:Button ID="btnMsgSend" runat="server" Text="Send" CssClass="large_button" ValidationGroup="sendmessage" OnClientClick="return false;" />
                                                        <asp:Button ID="btnMsgCancel" runat="server" class="large_button" Text="Cancel" />
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
                                <li>
                                    <%-- <div class="std_img">
                                    <img src="images/std_img.gif" alt="" />
                                </div>
                                <div class="std_name">
                                    <label>Student Name</label>
                                </div>
                                <div class="std_time_country"><span>Its 3:06 AM in United States</span></div>--%>
                                </li>
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


    <script type="text/ecmascript">

        $(function () {
            $(".msg_replay_show").hide();
        });
        // var hdnID;
        function OpenMsgPopup(sendToUserName, sendToName) {
            //hdnID = sendToUserName;
            //var em = $("#hndId").val();
            var user = {
                ToUserName: sendToUserName,
            };
            senttoid = sendToUserName;

            $(".msg_replay_hide").hide(1000);
            $(".msg_replay_show").show(1000);
            $("#divLoader").show();
            $("#messagebox").hide();
            $("#messageList").hide();
            $("#ulMessageBetween").html("");

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
                        $("#MainContent_txtSendToUserName").val(sendToName);
                        $("#messagebox").show();
                        $("#messageList").hide();
                        $("#divLoader").hide();
                        //$("#messagebox").dialog({ modal: true, minWidth: 650, resizable: false, minHeight: 600, closeOnEscape: true, closeText: "" });
                    }
                },
                fail: function (ex) { alert("Error : " + ex); }
            });


        }

        function retriveConversation(parentId) {
            var msgId = {
                messageId: parentId
            };
            $("#messageList").html("please wait...");
            $("#ulMessageBetween").html("");

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

        //For Send Message
        $("#MainContent_btnMsgSend").click(function () {

            if (Page_IsValid) {

                var Message = {
                    Title: $("#MainContent_txtSubject").val(),
                    Description: $("#MainContent_txtMessage").val(),
                    sendToUserName: senttoid, //$("#hndId").val(),// $("#" + senttoid).val(),
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
                            $("#Dialog-Send").dialog({
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
                            $("#divLoader").hide();
                            $("#MainContent_txtSubject").val("");
                            $("#MainContent_txtMessage").val("");
                            $("#messagebox").hide();
                            retriveConversation(msg.d);
                        },
                        fail: function (a, b, c, d) {
                            alert(JSON.stringify(d));
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
            $(".msg_replay_hide").show(1000);
            $(".msg_replay_show").hide(1000);
        });

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

        $(".msg_replay_open_click").click(function () {
            var msgId = {
                messageId: this.id
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
                }
            });
            $(".msg_replay_hide").hide(1000);
            $(".msg_replay_show").show(1000);
        });

        $(".msg_replay_close").click(function () {
            $(".msg_replay_show").hide(1000);
            $(".msg_replay_hide").show(1000);
        });


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


        // For Open Massegebox
        //var hdnID;
        //function OpenMsgPopup(sendToUserName, sendToName) {
        //    hdnID = sendToUserName;
        //    var em = $("#hndId").val();
        //    var user = {
        //        ToUserName: em,
        //    };
        //    $.ajax({
        //        type: "POST",
        //        beforeSend: function () { $("#LoadingImage").show(); },
        //        complete: function () { $("#LoadingImage").hide(); },
        //        url: "ViewAllList.aspx/Getstatus",
        //        data: JSON.stringify(user),
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (msg) {
        //            var data = msg.d;
        //            if (data != "") {
        //                window.location.href = "StudentMessage.aspx?id=" + data;
        //            }
        //            else {
        //                $("#MainContent_txtSendToUserName").val(sendToName);
        //                $("#messagebox").dialog({ modal: true, minWidth: 600, resizable: false, minHeight: 364, closeOnEscape: true, closeText: "" });
        //            }
        //        }
        //    });

        //}

        ////For Send Message
        //$("#MainContent_btnMsgSend").click(function () {
        //    if (Page_IsValid) {
        //        var Message = {
        //            Title: $("#MainContent_txtSubject").val(),
        //            Description: $("#MainContent_txtMessage").val(),
        //            sendToUserName: $("#hndId").val(),
        //            ParentId: "0",
        //        };
        //        $.ajax({
        //            type: "POST",
        //            beforeSend: function () { $("#LoadingImage").show(); },
        //            complete: function () { $("#LoadingImage").hide(); },
        //            url: "ViewAllList.aspx/SendMessage",
        //            data: JSON.stringify(Message),
        //            contentType: "application/json; charset=utf-8",
        //            dataType: "json",
        //            success: function (msg) {
        //                $("#MainContent_txtSubject").val("");
        //                $("#MainContent_txtMessage").val("");
        //                $("#messagebox").dialog("close");
        //            }
        //        });
        //    }
        //});

        //// for Cancel Message
        //$("#MainContent_btnMsgCancel").click(function () {
        //    $("#MainContent_txtSubject").val("");
        //    $("#MainContent_txtMessage").val("");
        //    $("#messagebox").dialog("close");
        //});

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
                        url: "ViewAllList.aspx/AppendAndDisplayStudents",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d == "no")
                                $("#noUniversityMsg").show();
                            else {
                                $("#noUniversityMsg").hide();
                                flag = true;
                                $("#MainContent_dlStudentShownInterestList").append(data.d);
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
