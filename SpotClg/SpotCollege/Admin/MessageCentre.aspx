<%@ Page Title="Message Centre" MasterPageFile="~/Admin/Admin.Master" Language="C#" AutoEventWireup="true" CodeBehind="MessageCentre.aspx.cs" Inherits="SpotCollege.Admin.MessageCentre" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="pattern_box">
        <div class="row-fluid">
            <div class="span3">
                <div class="searh_box msg_center_box">
                    <div id="LoadingImage" style="display: none">
                        <asp:Image ID="ImgLoader" runat="server" ImageUrl="~/Images/ajaxloader.gif" />
                    </div>
                    <div class="shadowbox box_space">
                        <%--<div class="h2_heading">
                        <h2></h2>
                    </div>--%>
                        <ul>
                            <li><span>User Type : </span>
                                <asp:DropDownList ID="ddlUserType" runat="server" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem>University</asp:ListItem>
                                    <asp:ListItem>Students</asp:ListItem>
                                </asp:DropDownList>
                            </li>
                        </ul>
                        <div class="over_hidden">
                                    <div runat="server" style="color: black; font-size: larger" id="divNoUniversity"></div>
                            <div class="pre-scrollable" id="studentList">
                                <ul class="list_4">
                                    <asp:DataList ID="dlUniversitylist" runat="server" RepeatLayout="Flow" Visible="true">
                                        <ItemTemplate>
                                            <li>
                                                <div class="user_img">
                                                    <%--<asp:HiddenField ID="HndUniversityUserName" runat="server" Value='<%#Eval("UserName") %>' />--%>
                                                    <asp:Image ID="UniImage" runat="server" alt="" ImageUrl='<%#"/Images/Profile/"+Eval("Image") %>' />
                                                </div>
                                                <div class="user_name">
                                                    <%--<asp:LinkButton ID="lnkBtnUniversityName" OnClick="lnkBtnUniversityName_Click" CommandArgument='<%#Eval("UserName") %>' Text='<%#Eval("UniversityName") %>' runat="server"></asp:LinkButton>--%>
                                                    <a onclick="javascript:openUnApprovesMsgs(this,'<%#Eval("uname") %>')" style="cursor: pointer"><%#Eval("UniversityName") %></a>
                                                    <asp:Label runat="server" ID="lblNoOfUnApprovedMsg" CssClass="user_count_mg img-polaroid" Text='<%#Eval("cnt") %>'></asp:Label>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:DataList>
                                    <asp:DataList ID="dlStudentList" runat="server" RepeatLayout="Flow" Visible="false" >
                                        <ItemTemplate>
                                            <li>
                                                <div class="user_img">
                                                    <%--<asp:HiddenField ID="HndstudentUserName" runat="server" Value='<%#Eval("UserName") %>' />--%>
                                                    <asp:Image ID="UniImage" runat="server" alt="" ImageUrl='<%#"/Images/Profile/"+Eval("Photo") %>' />
                                                </div>
                                                <div class="user_name">
                                                    <a onclick="javascript:openUnApprovesMsgs(this,'<%#Eval("uname") %>')" style="cursor: pointer"><%#Eval("FirstName") %> <%#Eval("LastName") %> </a>
                                                    <%--<asp:LinkButton ID="lnkBtnUniversityName" OnClick="lnkBtnUniversityName_Click" CommandArgument='<%#Eval("UserName") %>' Text='<%#Eval("FirstName")+" "+ Eval("LastName") %>' runat="server"></asp:LinkButton>--%>
                                                    <asp:Label runat="server" ID="lblNoOfUnApprovedMsg" CssClass="user_count_mg img-polaroid" Text='<%#Eval("cnt") %>'></asp:Label>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:DataList>
                                    <asp:Label ID="lblrec" runat="server" Text="No Record Found" Visible="false"></asp:Label>
                                </ul>
                                <div id="noMsgAlert" class="left_right_space" style="display: none">
                                    No More Students Found
                                </div>
                                <div id="waitDiv" class="left_right_space" style="display: none">
                                    please wait...
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="span9">
                <div class="shadowbox box_space">
                    <asp:Panel ID="PnlMessage" runat="server">
                        <ul class="list_8" id="ulUniANDStudList">
                            <asp:DataList ID="dlMessageList" runat="server" OnItemDataBound="dlMessageList_ItemDataBound" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <ItemTemplate>
                                    <li>
                                        <div class="user_img">
                                            <asp:HiddenField ID="HndstudentUserName1" runat="server" Value='<%#Eval("ToUserName") %>' />
                                            <asp:Image ID="ImgBtnPhoto1" runat="server" alt="" CssClass="img-polaroid" />
                                        </div>
                                        <div class="user_name">
                                            <div class="width100per">
                                                <h4>
                                                    <asp:Label ID="lblStudentname" CssClass="fleft" runat="server" /></h4>
                                                <h5 class="fright">
                                                    <span>05/01/2013</span><br />
                                                    <asp:Label ID="lnkBtnTitle" runat="server" Text='<%#Eval("Title") %>' /></h5>
                                            </div>
                                            <div class="width100per">
                                                <%--<asp:LinkButton ID="lnkBtnTitle" CommandArgument='<%#Eval("MessageId") %>' Text='<%#Eval("Title") %>' runat="server"></asp:LinkButton>--%>
                                                <asp:Label ID="lblMessage" runat="server" Text='<%#Eval("Description") %>' />
                                            </div>
                                        </div>
                                        <div class="user_msg">
                                        </div>
                                        <div class="width100per">
                                            <div class="fleft">
                                                <a id='msgApproved<%#Eval("MessageId") %>' href="javascript:MsgApproving('<%#Eval("MessageId")%>')" class="button">Approved</a>
                                            </div>
                                            <div class="fright">
                                                <a id='msgRejectwithmsg<%#Eval("MessageId") %>' href="javascript:MsgDialogOpen('<%#Eval("MessageId")%>')" class="button">Reject With Message</a>
                                                <a id='msgReject<%#Eval("MessageId") %>' href="javascript:MsgDelete('<%#Eval("MessageId")%>')" class="button">Reject</a>
                                            </div>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:DataList>
                            <asp:Label ID="lblmsg" runat="server" Text="No Record Found" Visible="false"></asp:Label>
                        </ul>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    <%--For Message PopUp--%>
    <div id="messagebox" class="span12 messagebox" style="display: none" title="Message Box">
        <div class="row-fluid">
            <div class="span12">
                <div class="message_right">
                    <ul class="profile_form">
                        <asp:HiddenField ID="hndSendMsgId" runat="server" />
                        <%-- <li class="row">
                            <div class="span4">
                                <label>Send to :</label>
                            </div>
                            <div class="span5">
                                <asp:TextBox runat="server" ID="txtSendToUserName" Enabled="false" />
                            </div>
                        </li>--%>
                        <li class="row-fluid">
                            <div class="span4">
                                <label>Subject :</label>
                            </div>
                            <div class="span5">
                                <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="field-validation-error" ErrorMessage="Enter Subject" ControlToValidate="txtSubject" ValidationGroup="sendmessage"></asp:RequiredFieldValidator>

                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span4">
                                <label>Message :</label>
                            </div>
                            <div class="span6">
                                <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" CssClass="field-validation-error" ErrorMessage="Enter Message" ControlToValidate="txtMessage" ValidationGroup="sendmessage"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span4"></div>
                            <div class="span9">
                                <asp:Button ID="btnMsgSend" runat="server" Text="Send" CssClass="large_button" ValidationGroup="sendmessage" />
                                <asp:Button ID="btnMsgCancel" runat="server" class="large_button" Text="Cancel" />
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>

    <div id="dialog-confirm" title="Are You Sure?" style="display: none;">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Are you sure?</p>
    </div>
    <div id="Dialog-Alert" title="" style="display: none;">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Please Enter Message Text</p>
    </div>
    <script type="text/javascript">

        function getLastRows() {
            var childCount = document.getElementById('MainContent_dlUniversitylist').childElementCount;
            var lastChild = document.getElementById('MainContent_dlUniversitylist').children.item(childCount - 1);
            return ($(lastChild).html());
        }

        var nScrollHight = 0; // rolling distance Total length (note not the length of the scroll bar)
        var nScrollTop = 0; // Scroll to the current position
        var nDivHight = $("#studentList").height();

        var flag = true;
        $("#studentList").scroll(function () {
            nScrollHight = $(this)[0].scrollHeight;
            nScrollTop = $(this)[0].scrollTop;
            if (nScrollTop + nDivHight >= nScrollHight) {
                if (flag) {
                    $("#waitdiv").show();
                    flag = false

                    var value = $("#MainContent_ddlUserType").val();

                    if (value == "University") {

                        $.ajax({
                            type: "POST",
                            beforeSend: function () { $("#waitdiv").show(); },
                            complete: function () { $("#waitdiv").hide(); },
                            url: "MessageCentre.aspx/appendToUniversityList",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                if (data.d == "no")
                                    $("#noMsgAlert").show();
                                else {
                                    $("#noMsgAlert").hide();
                                    flag = true;
                                    $("#MainContent_dlUniversitylist").append(data.d);
                                    activeOnBind();
                                }
                                $("#waitdiv").hide();
                            },
                            fail: function (e1, e2, e3, e4) { alert("error"); }

                        });
                    }
                    else if (value == "Students") {
                        $.ajax({
                            type: "POST",
                            beforeSend: function () { $("#waitdiv").show(); },
                            complete: function () { $("#waitdiv").hide(); },
                            url: "MessageCentre.aspx/appendToStudentList",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                if (data.d == "no")
                                    $("#noMsgAlert").show();
                                else {
                                    $("#noMsgAlert").hide();
                                    flag = true;
                                    $("#MainContent_dlStudentList").append(data.d);
                                    activeOnBind();
                                }
                                $("#waitdiv").hide();
                            },
                            fail: function (e1, e2, e3, e4) { alert("error"); }

                        });
                    }
                }
                else {
                    $("#waitdiv").show();
                }
            }

        });


        function activeOnBind() {
        }

        var tempNoOfMsgSpan;
        function openUnApprovesMsgs(con, username) {

            tempNoOfMsgSpan = $(con).siblings('span');

            var user = {
                username: username,
            };
            $("#ulUniANDStudList").html("Please wait...");
            $.ajax({
                type: "POST",
                beforeSend: function () { $("#waitdiv").show(); },
                complete: function () { $("#waitdiv").hide(); },
                data: JSON.stringify(user),
                url: "MessageCentre.aspx/displayUnapprovedMessage",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.d == "")
                        $("#ulUniANDStudList").html("No Messages Found");
                    else {
                        flag = true;
                        $("#ulUniANDStudList").html(data.d);
                    }
                },
                fail: function (e1, e2, e3, e4) { alert("error"); }

            });
        }

        $(function () {
            $("#nav li").each(function () {
                $(this).removeClass('navi_active');
            });
            $("#lnkMessage").addClass("navi_active");
        });

        function MsgApproving(messageId) {

            var tmp = parseInt(tempNoOfMsgSpan.html());
            var Message = {
                MessageId: messageId,
            };

            $.ajax({
                type: "POST",
                beforeSend: function () { $("#LoadingImage").show(); },
                complete: function () { $("#LoadingImage").hide(); },
                url: "MessageCentre.aspx/SaveApproving",
                data: JSON.stringify(Message),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $("#msgApproved" + messageId).parent().parent().parent().remove();

                    tmp = tmp - 1;
                    tempNoOfMsgSpan.html(tmp);

                    $("#LoadingImage").hide();
                    $("#messagebox").dialog("close");
                }
            });
        }

        function MsgDialogOpen(messageId) {
            $("#MainContent_hndSendMsgId").val(messageId);
            $("#messagebox").dialog({ modal: true, minWidth: 650, resizable: false, closeOnEscape: true, closeText: "" });
        }

        //For Send Message
        $("#MainContent_btnMsgSend").click(function () {
            var tmp = parseInt(tempNoOfMsgSpan.html());
            if (Page_IsValid) {
                var Message = {
                    Title: $("#MainContent_txtSubject").val(),
                    Description: $("#MainContent_txtMessage").val(),
                    ParentId: "0",
                    MessageId: $("#MainContent_hndSendMsgId").val(),
                };
                if ($("#MainContent_txtSubject").val() != "" && $("#MainContent_txtMessage").val() != "") {
                    $.ajax({
                        type: "POST",
                        beforeSend: function () { $("#LoadingImage").show(); },
                        complete: function () { $("#LoadingImage").hide(); },
                        url: "MessageCentre.aspx/SendMessage",
                        data: JSON.stringify(Message),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            $("#MainContent_txtSubject").val("");
                            $("#MainContent_txtMessage").val("");
                            $("#MainContent_hndSendMsgId").val("");
                            $("#LoadingImage").hide();
                            $("#messagebox").dialog("close");
                            $("#msgRejectwithmsg" + Message.MessageId).parent().parent().parent().remove();
                            tmp = tmp - 1;
                            tempNoOfMsgSpan.html(tmp);
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

        function MsgDelete(messageId) {
            $("#dialog-confirm").dialog({
                open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Yes": function () {
                        // if (Page_IsValid) {
                        var tmp = parseInt(tempNoOfMsgSpan.html());
                        var msg = {
                            MessageId: messageId
                        };
                        $.ajax({
                            type: "POST",
                            beforeSend: function () { $("#LoadingImage").show(); },
                            complete: function () { $("#LoadingImage").hide(); },
                            url: "MessageCentre.aspx/MsgDelete",
                            data: JSON.stringify(msg),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                $("#LoadingImage").hide();
                                //if (msg.d) {
                                $("#msgReject" + messageId).parent().parent().parent().remove();
                                tmp = tmp - 1;
                                tempNoOfMsgSpan.html(tmp);
                                //}
                            }
                        });
                        //  }
                        $(this).dialog("close");
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                    }
                }
            });
        }

        // for Cancel Message
        $("#MainContent_btnMsgCancel").click(function () {
            $("#MainContent_txtSubject").val("");
            $("#MainContent_txtMessage").val("");
            $("#MainContent_hndSendMsgId").val("");
            $("#messagebox").dialog("close");
        });

        // for Message Scrolling
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
                        url: "MessageCentre.aspx/appendToMessageList",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d == "no")
                                $("#noMsgAlert").show();
                            else {
                                $("#noMsgAlert").hide();
                                flag = true;
                                $("#ulUniANDStudList").append(data.d);
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

