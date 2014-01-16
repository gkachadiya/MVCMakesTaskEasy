<%@ Page Title="AllMessage" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllMessage.aspx.cs" Inherits="SpotCollege.AllMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="pattern_box allmessage">
        <div class="row-fluid">
            <div class="msg_replay_hide">
                <div class="msg_searchbar" style="margin: 0">
                    <span class="search_btn"></span>
                    <asp:TextBox ID="txtSearch" placeholder="Search" OnTextChanged="txtSearch_TextChanged" runat="server"></asp:TextBox>
                </div>
                <br />
                <br />
                <div>
<%--                    <asp:LinkButton ID="lnkFromStudents" runat="server" OnClick="lnkFromStudents_Click" CssClass="button ">Messages From Students</asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkFromUniversity" runat="server" OnClick="lnkFromUniversity_Click" CssClass="button">Messages From University</asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkFromAdmin" runat="server" OnClick="lnkFromAdmin_Click" CssClass="button">Messages From Admin</asp:LinkButton>
                    <asp:HiddenField ID="hndMsgBtnActive" runat="server" />--%>
                    <ul class="nav nav-tabs">
                        <li>
                            <asp:LinkButton ID="lnkFromStudents" runat="server" OnClick="lnkFromStudents_Click" >Messages From Students</asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="lnkFromUniversity" runat="server" OnClick="lnkFromUniversity_Click">Messages From University</asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="lnkFromAdmin" runat="server" OnClick="lnkFromAdmin_Click">Messages From Admin</asp:LinkButton>
                        </li>
                        <asp:HiddenField ID="hndMsgBtnActive" runat="server" />
                    </ul>
                </div>
                    <asp:Label ID="lblNewMessageFromStudent" runat="server" ForeColor="Blue"></asp:Label><br />
                    <asp:Label ID="lblNewMessageFromUniversity" runat="server" ForeColor="Green"></asp:Label>
                    <div class="message_right">
                        <ul class="user_msg_box" id="paginationul">
                            <asp:DataList ID="dlStudentlist" runat="server" OnItemDataBound="dlStudentlist_ItemDataBound" RepeatDirection="Horizontal" RepeatLayout="Flow" DataKeyField="FromUserName">
                                <ItemTemplate>
                                    <li>
                                        <div class="user_img">
                                            <asp:HiddenField ID="HndstudentUserName1" runat="server" Value='<%#Eval("ToUserName") %>' />
                                            <asp:Image ID="ImgBtnPhoto1" runat="server" ImageUrl="" />
                                        </div>
                                        <div class="user_name">
                                            <label>
                                                <a href="#">
                                                    <asp:Label ID="lblChildMsgCounting" runat="server" Text='<%#Eval("MessageId") %>'></asp:Label>
                                                    <asp:Label ID="lblStudentname" runat="server"></asp:Label>
                                                </a>
                                            </label>
                                            <br />
                                            <i><%#Eval("CreatedDate", "{0:d}")%></i>
                                        </div>
                                        <div class="user_msg">
                                            <%-- <asp:LinkButton ID="lnkBtnTitle" OnClick="lnkBtnTitle_Click" CommandArgument='<%#Eval("MessageId") %>' Text='<%#Eval("Title") %>' runat="server"></asp:LinkButton><br />--%>
                                            <label><%#Eval("Title") %></label>
                                            <p><%#Eval("Description") %> 
                                                <a href="#" class="msg_replay_open_click button" style="float:right" id='<%#Eval("MessageId") %>'>View message and Respond</a>
                                            </p>
                                        </div>
                                    </li>

                                </ItemTemplate>
                            </asp:DataList>
                            <asp:Label ID="lblrec" runat="server" Text="No Record Found" Visible="false"></asp:Label>
                        </ul>
                        <div id="noMsgAlert" class="left_right_space" style="display: none">
                            No More Messages Found
                        </div>
                        <div id="waitDiv" class="left_right_space" style="display: none">
                            please wait...
                        </div>
                    </div>
            </div>

            <div class="msg_replay_show messagereplayfrom">
                <div class="span5">
                    <span class="msg_replay_close">Close</span>
                    <div class="trsnsparentnone_box">
                        <div class="message_right shadowbox">
                            <ul class="user_msg_box paginationul_two">
                                <asp:DataList ID="dlStudentlist1" runat="server" OnItemDataBound="dlStudentlist_ItemDataBound1" RepeatDirection="Horizontal" RepeatLayout="Flow" DataKeyField="FromUserName">
                                    <ItemTemplate>
                                        <li>
                                            <div class="user_img">
                                                <asp:HiddenField ID="HndstudentUserName11" runat="server" Value='<%#Eval("ToUserName") %>' />
                                                <asp:Image ID="ImgBtnPhoto11" runat="server" Height="65" Style="border-radius: 30px" ImageUrl="" />
                                            </div>
                                            <div class="user_name">
                                                <label>
                                                    <a href="#" class="msg_replay_open_click">
                                                        <asp:Label ID="lblStudentname1" runat="server"></asp:Label></a></label>
                                                <span></span>

                                            </div>
                                            <div class="user_msg">
                                                <%-- <asp:LinkButton ID="lnkBtnTitle" OnClick="lnkBtnTitle_Click" CommandArgument='<%#Eval("MessageId") %>' Text='<%#Eval("Title") %>' runat="server"></asp:LinkButton><br />--%>
                                                <label><%#Eval("Title") %></label>
                                                <p><%#Eval("Description") %> </p>
                                            </div>
                                        </li>

                                    </ItemTemplate>
                                </asp:DataList>
                            </ul>
                            <div id="noUniversityMsg" class="left_right_space" style="display: none">
                                No More Universities
                            </div>
                            <div id="waitdiv" class="left_right_space" style="display: none">
                                please wait...
                            </div>
                        </div>
                    </div>
                </div>
                <div class="span7">
                    <div class="row-fluid">
                        <div class="span8">
                            <div id="messageList" class="student_msg message_right shadowbox">
                                <div id="divLoader" class="student_msg message_right shadowbox">
                                    please wait...<div id="LoadingImage" style="display: none">
                                        <img src="Images/ajaxloader.gif" />
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="span4">
                            <ul class="student_right_block" id="ulMessageBetween">
                                <li>
                                    <%--<div class="std_img">
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

            $("#Message").addClass('navi_active');

            var msgval = $("#MainContent_hndMsgBtnActive").val();
            if (msgval == "Student") {
                $("#MainContent_lnkFromStudents").addClass('active');
                $("#MainContent_lnkFromUniversity").removeClass('active');
                $("#MainContent_lnkFromAdmin").removeClass('active');
            }
            else if (msgval == "University") {
                $("#MainContent_lnkFromStudents").removeClass('active');
                $("#MainContent_lnkFromUniversity").addClass('active');
                $("#MainContent_lnkFromAdmin").removeClass('active');
            }
            else if (msgval == "Admin") {
                $("#MainContent_lnkFromStudents").removeClass('active');
                $("#MainContent_lnkFromUniversity").removeClass('active');
                $("#MainContent_lnkFromAdmin").addClass('active');
            }
            else {
                $("#MainContent_lnkFromStudents").addClass('active');
                $("#MainContent_lnkFromUniversity").removeClass('active');
                $("#MainContent_lnkFromAdmin").removeClass('active');
            }


        });

        $("#MainContent_lnkFromStudents").click(function () {
            $("#MainContent_hndMsgBtnActive").val("Student");
        });
        $("#MainContent_lnkFromUniversity").click(function () {
            $("#MainContent_hndMsgBtnActive").val("University");
        });
        $("#MainContent_lnkFromAdmin").click(function () {
            $("#MainContent_hndMsgBtnActive").val("Admin");
        });

        $(function () {
            $(".msg_replay_show").hide();
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

        var flag_showpopup = true;

        $(".msg_replay_open_click").click(function () {
           // var id = $(this).parent().parent().prev().children().children().children().next();
            toBindRemainMessages(this);
        });

        function toBindRemainMessages(msgcontrol) {
            var msgId = msgcontrol.id;
            flag_showpopup = true;
            $("#messageList").html("please wait..");
            $("#ulMessageBetween").html("please wait...");
            var msgId = {
                messageId: msgId
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
                    bindToDisplayRemainMessage();
                    try {
                        //this is used to remove hightlight when message is opened 
                        $(msgcontrol).parent().parent().prev().children().children().children().next().css('color', '#404040');
                    } catch (ex) { }
                }
            });
            $(".msg_replay_hide").hide(1000);
            $(".msg_replay_show").show(1000);
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

        $(".msg_replay_close").click(function () {
            flag_showpopup = false;
            $(".msg_replay_show").hide(1000);
            $(".msg_replay_hide").show(1000);
        });

        // for Scrolling
        var flag = true;
        $(window).scroll(function () {
            //alert("kk");
            if (!flag_showpopup) {
                if ($(window).scrollTop() + $(window).height() == $(document).height()) {
                    if (flag) {
                        $("#waitdiv").show();
                        flag = false
                        $.ajax({
                            type: "POST",
                            beforeSend: function () { $("#waitdiv").show(); },
                            complete: function () { $("#waitdiv").hide(); },
                            url: "AllMessage.aspx/AppendAndDisplayMessage",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                if (data.d == "no")
                                    $("#noMsgAlert").show();
                                else {
                                    $("#noMsgAlert").hide();
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
            }
        });


    </script>
</asp:Content>
