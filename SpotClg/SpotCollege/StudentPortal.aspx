<%@ Page Title="Student Portal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentPortal.aspx.cs" Inherits="SpotCollege.StudentPortal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row-fluid">
        <div class="msg_replay_hide">
            <div class="span3">
                <div class="searh_box">
                    <div class="pattern_box box_space">
                        <div class="h2_heading">
                            <h2>Student Search</h2>
                        </div>
                        <ul>
                            <li><span>Student From : </span>
                                <asp:DropDownList ID="dllStudentCountry" runat="server"></asp:DropDownList>
                            </li>
                            <li>
                                <span>Seeking : </span>
                                <asp:DropDownList ID="ddlSearchByLookingFor" runat="server"></asp:DropDownList>
                            </li>
                            <li>
                                <span>Going To : </span>
                                <asp:DropDownList ID="ddlSearchByCountry" runat="server"></asp:DropDownList>
                            </li>
                            <li>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" /></li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="span9">
                <div class="list_5">
                    <div class="pattern_box box_space">
                        <div runat="server" style="color: black; font-size: larger" id="errorMsgDiv"></div>
                        <ul>
                            <asp:DataList ID="dlStudentList" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" OnItemDataBound="dlStudentList_ItemDataBound">
                                <ItemTemplate>
                                    <li>
                                        <div class="name">
                                            <%#Eval("FirstName") %> <%#Eval("LastName") %>
                                            <div class="country"><%#Eval("Country")%></div>
                                        </div>

                                        <div class="img">
                                            <asp:Image ID="imgStudent" runat="server" alt="" /></a>
                                        </div>
                                        <div class="detail">
                                            <ul class="list_4">
                                                <asp:HiddenField ID="hndUsername" runat="server" Value='<%#Eval("UserName") %>' />
                                                <li>
                                                    <label>Location :</label>
                                                    <span><%#Eval("City")%>, <%#Eval("Country")%></span>
                                                </li>
                                                <li>
                                                    <label>Current Status :</label>
                                                    <%--<span><%# ((SpotCollege.Common.CurrentlyIn)(Eval("StudyingIn"))).ToString() %></span>--%>
                                                    <asp:Label ID="lblCurrentStatus" runat="server" Text='<%# (SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.CurrentlyIn)Eval("StudyingIn"))).ToString() %>'></asp:Label>
                                                </li>
                                                <li>
                                                    <label>Looking for a :</label>
                                                    <asp:Label ID="lblLookingFor" runat="server" Text='<%# ((SpotCollege.Common.ProgramLookingFor)(Eval("LookingFor"))).ToString()  +" in "+ Eval("DesiredFieldofStudy") %> '></asp:Label>
                                                </li>
                                                 <li>
                                                    <label>Desired Start Date :</label>
                                                    <asp:Label ID="lblStartDate" runat="server" Text='<%# (SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.expectedStart)Eval("StartDate"))).ToString() %>'></asp:Label>
                                                </li>
                                                <li>
                                                    <label>Desired Location :</label>
                                                    <span><%#Eval("LookingForCountry") %></span>
                                                </li>
                                                <li>
                                                    <label>Profile Created On :</label>
                                                    <asp:Label runat="server" ID="lblDate" Text='<%#Eval("CreatedDate") %>'></asp:Label>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="morebtn_">
                                            <input type='hidden' id='<%#Eval("StudentId") %>' value='<%#Eval("UserName") %>' />
                                            <a href="#" class="msgbtn" id='<%#Eval("Username") %>' onclick="javascript:OpenMsgPopup( '<%#Eval("FirstName") %> <%#Eval("LastName") %>','<%#Eval("StudentId") %>' )">Message</a>
                                            <%--<asp:LinkButton ID="lnkBtnViewMore" OnClick="lnkBtnViewMore_Click" CommandArgument='<%#Eval("UserName") %>' runat="server">Message</asp:LinkButton>--%>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:DataList>
                        </ul>
                        <div id="noUniversityMsg" style="display: none" class="left_right_space">
                            No More Students Found
                        </div>
                        <div id="waitdiv" style="display: none" class="left_right_space">
                            please wait...
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="msg_replay_show messagereplayfrom">
            <div class="pattern_box">
            <div class="span5">
                <span class="msg_replay_close">Close</span>
                <div class="trsnsparentnone_box">
                    <div class="message_right shadowbox">
                        <ul class="user_msg_box paginationul_two list_5">
                            <asp:DataList ID="dlStudentList2" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                <ItemTemplate>
                                    <li>
                                        <div class="name">
                                            <%#Eval("FirstName") %> <%#Eval("LastName") %>
                                            <div class="country"><%#Eval("Country")%></div>
                                        </div>

                                        <div class="img">
                                            <asp:Image ID="imgStudent" runat="server" ImageUrl='<%#"Images/Profile/" +Eval("Photo") %>' alt="" /></a>
                                        </div>
                                        <div class="detail">
                                            <ul class="list_4">
                                                <li>
                                                    <label>Location :</label>
                                                    <span><%#Eval("City")%>, <%#Eval("Country")%></span>
                                                </li>
                                                <li>
                                                    <label>Current Status :</label>
                                                    <%--<span><%# ((SpotCollege.Common.CurrentlyIn)(Eval("StudyingIn"))).ToString() %></span>--%>
                                                    <span><%# (SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.CurrentlyIn)Eval("StudyingIn"))).ToString() %></span>
                                                </li>
                                                <li>
                                                    <label>Looking for a :</label>
                                                    <span><%# ((SpotCollege.Common.ProgramLookingFor)(Eval("LookingFor"))).ToString() %> in <%#Eval("DesiredFieldofStudy")%></span>
                                                </li>
                                                <li>
                                                    <label>Desired Location :</label>
                                                    <span><%#Eval("LookingForCountry") %></span>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="morebtn">
                                            <a href="#" class="msg_replay_open_click" id='<%#Eval("StudentId") %>'>Message</a>
                                            <%--<asp:LinkButton ID="lnkBtnViewMore" OnClick="lnkBtnViewMore_Click" CommandArgument='<%#Eval("UserName") %>' runat="server">Message</asp:LinkButton>--%>
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
    <div id="Dialog-Send" title="Message Send" style="display: none;">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Message Send Successfully</p>
    </div>
    <div id="Dialog-Alert" title="" style="display: none;">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Please Enter Message Text</p>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#nav li").each(function () {
                $(this).removeClass('navi_active');
            });

            $("#Student").addClass('navi_active');
        });

        $(function () {
            $(".msg_replay_show").hide();
        });
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
                        url: "StudentPortal.aspx/AppendAndDisplayStudent",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d == "no")
                                $("#noUniversityMsg").show();
                            else {
                                $("#noUniversityMsg").hide();
                                flag = true;
                                $("#MainContent_dlStudentList").append(data.d);
                            }
                            $("#waitdiv").hide();
                        }
                    });
                }
                else {

                }
            }
        });


        function OpenMsgPopup(sendToUserName, userid) {
            var ee = $("#" + userid).val();

            var user = {
                ToUserName: ee,
            };
            senttoid = userid;

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
                        $("#MainContent_txtSendToUserName").val(sendToUserName);
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
            $("#divLoader").show();

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
                    sendToUserName: $("#" + senttoid).val(),
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
    </script>

</asp:Content>
