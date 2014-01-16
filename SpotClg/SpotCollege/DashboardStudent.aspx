<%@ Page Title="DashBoard Student" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashboardStudent.aspx.cs" Inherits="SpotCollege.DashboardStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        jQuery(window).load(function () {
            jQuery('#client_slide').flexslider({
                animation: "slide",
                animationLoop: true,
                controlNav: false,
                itemWidth: 125,
                itemMargin: 20,
                slideshowSpeed: 5000,
                pauseOnHover: false,
            });
            AlertBox();
            setInterval(function () { AlertBox() }, 5000);
        });

        function AlertBox() {
            $.ajax({
                url: "Handler/AlertMsg.ashx",
                contentType: "text/plain",
                data: {},
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data != "") {
                        $("#AlertList").html(data);
                    }
                },
                error: function () {
                }
            });
        }

    </script>
    <%--<style type="text/css">
        .ui-widget-content
        {
            background: none !important;
            color: none !important;
            border: none !important;
        }

        .popup_block
        {
            background: url("images/ui-bg_flat_75_ffffff_40x100.png") repeat-x scroll 50% 50% #FFFFFF !important;
        }

        .ui-widget-overlay
        {
            position: fixed!important;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Client logo slide -->
    <div class="row-fluid">
        <div class="msg_replay_hide">
            <div class="span12">
                <div id="Dialog-Save" title="Data Saved Sucessfully" style="display: none;">
                    <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Data Saved Sucessfully</p>
                </div>
                <div id="Dialog-Send" title="Message Send" style="display: none;">
                    <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Message Send Successfully</p>
                </div>

                <div class="pattern_box box_space">

                    <div class="span8">
                        <div class="width100per">
                            <div class="shadowbox">
                                <div class="h2_heading">
                                    <h2 id="headingcollege" runat="server">College that offer cs in</h2>
                                    <a href="Collegelist.aspx" class="more fright">View All</a>
                                </div>
                                <div runat="server" style="color: black; font-size: larger" id="divNoUniversity"></div>
                                <div id="client_slide">

                                    <ul class="slides">
                                        <asp:DataList ID="dlUniversityList" runat="server" OnItemDataBound="dlUniversityList_ItemDataBound" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <ItemTemplate>
                                                <li style="width: 125px; float: left; display: block;"><%--Width="125px" Height="125px"--%>
                                                    <a href="javascript:Openpopup('<%#Eval("UniversityName")%>');"><%--'<%#"Images/Profile/" +Eval("Image") %>'--%>
                                                        <asp:Image Width="125" Height="100" ID="ImgUniversity" runat="server" ImageUrl="" /></a>
                                                    <a href="javascript:Openpopup('<%#Eval("UniversityName")%>');" class="view_morebtn"><%#Eval("UniversityName")%></a>
                                                </li>
                                            </ItemTemplate>

                                        </asp:DataList>
                                    </ul>

                                    <ul class="slides" style="display: none">
                                        <li>
                                            <a href="#">
                                                <img src="images/Client_logo1.gif" alt="" /></a>
                                            <a class="view_morebtn" href="#">View more</a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <img src="images/Client_logo2.gif" alt="" /></a>
                                            <a class="view_morebtn" href="#">View more</a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <img src="images/Client_logo3.gif" alt="" /></a>
                                            <a class="view_morebtn" href="#">View more</a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <img src="images/Client_logo1.gif" alt="" /></a>
                                            <a class="view_morebtn" href="#">View more</a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <img src="images/Client_logo2.gif" alt="" /></a>
                                            <a class="view_morebtn" href="#">View more</a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <img src="images/Client_logo3.gif" alt="" /></a>
                                            <a class="view_morebtn" href="#">View more</a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <img src="images/Client_logo1.gif" alt="" /></a>
                                            <a class="view_morebtn" href="#">View more</a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <img src="images/Client_logo2.gif" alt="" /></a>
                                            <a class="view_morebtn" href="#">View more</a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <img src="images/Client_logo3.gif" alt="" /></a>
                                            <a class="view_morebtn" href="#">View more</a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <img src="images/Client_logo1.gif" alt="" /></a>
                                            <a class="view_morebtn" href="#">View more</a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <img src="images/Client_logo2.gif" alt="" /></a>
                                            <a class="view_morebtn" href="#">View more</a>
                                        </li>
                                        <li>
                                            <a href="#">
                                                <img src="images/Client_logo3.gif" alt="" /></a>
                                            <a class="view_morebtn" href="#">View more</a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div id="LoadingImage" style="display: none">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/ajaxloader.gif" />
                        </div>
                        <div class="row-fluid">
                            <div class="span6">
                                <div class="shadowbox">
                                    <div class="h2_heading">
                                        <h2 id="headingcountry" runat="server"></h2>
                                        <a href="ViewAllList.aspx?status=Country" class="more fright">View All</a>
                                    </div>
                                    <div runat="server" style="color: black; font-size: larger" id="errorMsgDiv"></div>

                                    <ul class="list_2" id="ddlapplycountry" runat="server">
                                        <%--  <asp:DataList ID="ddlaplycountry" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                            <li>
                                                <div class="list_2_img">
                                                    <a href="#">
                                                        <asp:Image ID="Imgcountry" runat="server" ImageUrl='<%#"Images/Profile/" +Eval("Photo") %>' alt="" /></a>
                                                </div>
                                                <div class="list_2_detail">
                                                    <p>
                                                        <b>
                                                            <asp:Label runat="server" Text="" ID="fullname"><%#Eval("FirstName")%></asp:Label></b>  from 
                                                            <asp:Label ID="lbladdress1" runat="server" Text=""><%#Eval("City")%>&nbsp; applying for <%#Eval("LookingFor")%> in <%#Eval("DesiredFieldOfStudy")%> to <%#Eval("Country")%></asp:Label>
                                                        <br />
                                                       <asp:Label ID="lblemail" runat="server" Text=""><%#Eval("UserName")%></asp:Label>
                                                    </p>
                                                    <a id="lnkMessage" href="javascript:OpenMsgPopup('<%#Eval("UserName")%>')" class="msgbtn">Message</a>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:DataList>--%>
                                    </ul>
                                </div>
                            </div>
                            <div class="span6">
                                <div class="shadowbox">
                                    <div class="h2_heading">
                                        <h2 id="headercuurentlylooking" runat="server"></h2>
                                        <a href="ViewAllList.aspx?status=Course" class="more fright">View All</a>
                                    </div>
                                    <div runat="server" style="color: black; font-size: larger" id="divNostudent"></div>
                                    <ul class="list_2" id="ddlapplystudent" runat="server">
                                        <%--<asp:DataList ID="ddlapplystudent" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                        <ItemTemplate>
                                            <li>
                                                <div class="list_2_img">
                                                    <a href="#">
                                                        <asp:Image ID="Imgcountry" runat="server" ImageUrl='<%#"Images/Profile/" +Eval("Photo") %>' alt="" /></a>
                                                </div>
                                                <div class="list_2_detail">
                                                  <%--  <p>
                                                        <b>
                                                            <asp:Label runat="server" Text="" ID="fullname"><%#Eval("FirstName")%>&nbsp;&nbsp;<%#Eval("MiddleName")%>&nbsp;&nbsp;<%#Eval("LastName")%> </asp:Label></b><br />

                                                        <asp:Label ID="Label1" runat="server" Text=""><%#Eval("City")%>&nbsp;&nbsp;<%#Eval("Country")%></asp:Label>

                                                        <asp:Label ID="lbladdress" runat="server" Text=""><%#Eval("City")%>&nbsp;&nbsp;<%#Eval("Country")%></asp:Label>
                                                        <br />

                                                        <asp:Label ID="lblemail" runat="server" Text=""><%#Eval("UserName")%></asp:Label>
                                                    </p>--%>
                                        <%-- <p>
                                                        <b>
                                                            <asp:Label runat="server" Text="" ID="fullname"><%#Eval("FirstName")%></asp:Label></b>  from 
                                                            <asp:Label ID="lbladdress" runat="server" Text=""><%#Eval("City")%>&nbsp; applying for <%#Eval("LookingFor")%> in <%#Eval("DesiredFieldOfStudy")%> to <%#Eval("Country")%></asp:Label>
                                                        <br />
                                                       <asp:Label ID="lblemail" runat="server" Text=""><%#Eval("UserName")%></asp:Label>
                                                    </p>
                                                    <a id="lnkMessage" href="javascript:OpenMsgPopup('<%#Eval("UserName")%>')" class="msgbtn">Message</a>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:DataList>--%>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="span4">
                        <div class="shadowbox">
                            <div class="h2_heading">
                                <h2>Alert box</h2>
                                <a href="ViewAllAlert.aspx" class="more fright">View All</a>
                            </div>
                            <ul class="list_3" id="AlertList">
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="msg_replay_show messagereplayfrom">
            <div class="span5">
                <span class="msg_replay_close">Close</span>
                <div class="trsnsparentnone_box">
                    <div class="message_right shadowbox">
                        <ul class="user_msg_box paginationul_two list_2" id="ddlapplycountry2" runat="server">
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

    <%-- Popup --%>
    <div id="myModal" title="University Information" class="popup_block pattern_box" style="width: 100% !important; display: none;">
        <ul class="list_4">
            <li>
                <label>University Name :</label>
                <asp:Label ID="lblusername" runat="server"></asp:Label>
            </li>
            <li>
                <label>Address :</label>
                <asp:Label ID="lbladdress" runat="server"></asp:Label>
            </li>
        </ul>
        <div class="yesno_block">
            <div class="shadowbox" style="margin-top: 0px" id="divIntersted" runat="server">
                <div class="h2_heading">
                    <h2>Are you interested in this collage?</h2>
                </div>
                <asp:Button ID="btnintrest" runat="server" Text="Yes" CssClass="yesbtn bannerbtnlarge" />
                <asp:Button ID="btnnointrest" runat="server" Text="No" CssClass="yesbtn bannerbtnlarge" />
                <%--  <a class="yesbtn bannerbtnlarge" href="#">Yes</a>
                <a class="nobtn bannerbtnlarge" href="#">No</a>--%>
            </div>
        </div>
        <div class="simple_block">
            <img src="" id="imgprofile" height="100" width="100" runat="server" alt="" />
            <p>
                <asp:Label ID="lblDescription" runat="server"></asp:Label>
            </p>
        </div>
        <div class="accordion_wrapper">
            <h1>About This College</h1>
            <div class="shadowbox">
                <div class="accordion-header">
                    <div class="h2_heading">
                        <h2>Cost for International students: </h2>
                    </div>
                </div>
                <div class="accordion-content">
                    <p>
                        1.Tution for Undergraduate students (USD):
                        <asp:Label ID="lblundergradutefee" runat="server"></asp:Label>
                    </p>
                    <p>
                        2.Tution for graduate studens (USD):
                        <asp:Label ID="lblgraduatefee" runat="server"></asp:Label>
                    </p>
                    <p>
                        3.Books and supplies (USD):
                        <asp:Label ID="lblbookfee" runat="server"></asp:Label>
                    </p>
                    <p>
                        4.Off-campus room and Board (USD):
                        <asp:Label ID="lblroom" runat="server"></asp:Label>
                    </p>
                </div>
            </div>
            <div class="shadowbox">
                <div class="accordion-header">
                    <div class="h2_heading">
                        <h2>Enrollment Number </h2>
                    </div>
                </div>
                <div class="accordion-content">
                    <p>
                        Number of Graduate students :
                        <asp:Label ID="lblgradustudent" runat="server"></asp:Label>
                    </p>
                    <p>
                        Number of Undergraduate Students :
                        <asp:Label ID="lblundergradute" runat="server"></asp:Label>
                    </p>
                    <p>
                        Number of International students :
                        <asp:Label ID="lblinternational" runat="server"></asp:Label>
                    </p>

                </div>
            </div>
            <div class="shadowbox">
                <div class="accordion-header">
                    <div class="h2_heading">
                        <h2>Programs and majors</h2>
                    </div>
                </div>
                <div class="accordion-content">
                    <p>
                        1.Degree Level Offred :
                        <asp:Label ID="lbldegreelvloffred" runat="server"></asp:Label>
                    </p>

                    <p>
                        2.Courses Offred :
                        <asp:Label ID="lblcourceoffered" runat="server"></asp:Label>
                    </p>
                </div>

                <div class="accordion-content">
                </div>
            </div>
        </div>
    </div>

    <div id="Dialog-Alert" title="" style="display: none;">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Please Enter Message Text</p>
    </div>
    <div id="Dialog-ApplicationSend" title="Application Sent" style="display: none;">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>[<label id="lbluniname"></label>] admission's office has been notified that you are interested in learning more
about their institution. If your profile is a match, their representative will get in touch with you soon.
        </p>
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
        $(function () {
            $("#nav li").each(function () {
                $(this).removeClass('navi_active');
            });

            $("#Home").addClass('navi_active');
        });
        function Openpopup(universityName) {
            document.getElementById("MainContent_lblusername").innerHTML = universityName;
            var University = {
                UniversityName: universityName,
            };
            // $("#MainContent_lblusername").val(username);
            //  alert(username);
            $.ajax({
                type: "POST",
                beforeSend: function () { $("#LoadingImage").show(); },
                complete: function () { $("#LoadingImage").hide(); },
                url: "Collegelist.aspx/GetUnivercityData",
                data: JSON.stringify(University),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;
                    if (data == null) {
                        alert("Please input atleast one test scores in order to contact universities");
                    }
                    else {
                        document.getElementById("MainContent_lbladdress").innerHTML = data[0] + " " + data[1] + " " + data[2];
                        if (data[3] != null)
                            document.getElementById("MainContent_lblgradustudent").innerHTML = data[3];
                        else
                            document.getElementById("MainContent_lblgradustudent").innerHTML = "Not Available";
                        if (data[4] != null)
                            document.getElementById("MainContent_lblundergradute").innerHTML = data[4];
                        else
                            document.getElementById("MainContent_lblundergradute").innerHTML = "Not Available";
                        if (data[5] != null) {
                            document.getElementById("MainContent_lblinternational").innerHTML = data[5]; MainContent_imgprofile;
                        }
                        else
                            document.getElementById("MainContent_lblinternational").innerHTML = "Not Available";

                        if (data[6] == null)
                            document.getElementById("MainContent_imgprofile").src = "Images/no_image.jpg";
                        else
                            document.getElementById("MainContent_imgprofile").src = "Images/Profile/" + data[6];

                        document.getElementById("MainContent_lblDescription").innerHTML = data[7];

                        if (data[8] != null)
                            document.getElementById("MainContent_lblundergradutefee").innerHTML = data[8];
                        else
                            document.getElementById("MainContent_lblundergradutefee").innerHTML = "Not Available";

                        if (data[9] != null)
                            document.getElementById("MainContent_lblgraduatefee").innerHTML = data[9];
                        else
                            document.getElementById("MainContent_lblgraduatefee").innerHTML = "Not Available";

                        if (data[10] != null)
                            document.getElementById("MainContent_lblbookfee").innerHTML = data[10];
                        else
                            document.getElementById("MainContent_lblbookfee").innerHTML = "Not Available";

                        if (data[11] != null)
                            document.getElementById("MainContent_lblroom").innerHTML = data[11];
                        else
                            document.getElementById("MainContent_lblroom").innerHTML = "Not Available";

                        if (data[12] != "")
                            document.getElementById("MainContent_lbldegreelvloffred").innerHTML = data[12];
                        else
                            document.getElementById("MainContent_lbldegreelvloffred").innerHTML = "Not Available";

                        if (data[13] != "")
                            document.getElementById("MainContent_lblcourceoffered").innerHTML = data[13];
                        else
                            document.getElementById("MainContent_lblcourceoffered").innerHTML = "Not Available";

                        $("#myModal").dialog({ modal: true, minWidth: 700, resizable: false, minHeight: 300, closeOnEscape: true, closeText: "" });
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    alert('request failed' + JSON.stringify(xhr) + " " + JSON.stringify(textStatus) + " " + JSON.stringify(errorThrown));
                }
            });
        }

        $("#MainContent_btnintrest").click(function () {
            var universityName = document.getElementById("MainContent_lblusername").innerHTML;
            var University = {
                UniversityName: universityName,
            };
            $("#lbluniname").html(universityName);
            $.ajax({
                type: "POST",
                beforeSend: function () { $("#LoadingImage").show(); },
                complete: function () { $("#LoadingImage").hide(); },
                url: "DashboardStudent.aspx/SaveStudentIntrest",
                data: JSON.stringify(University),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d != "true") {
                        //alert(msg.d);
                    }
                    else {
                        $("#Dialog-ApplicationSend").dialog({
                            open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                            resizable: false,
                            height: 270,
                            modal: true,
                            buttons: {
                                "OK": function () {
                                    $(this).dialog("close");
                                }

                            },
                        });
                    }
                    $("#myModal").dialog('close');
                }

            });
        });
        $("#MainContent_btnnointrest").click(function () {
            $("#myModal").dialog('close');
        });

        //For Open Massegebox
        //This userid is used to get value of hidden field.
        function OpenMsgPopup(sendToUserName, userid) {
            $(".msg_replay_hide").hide(1000);
            $(".msg_replay_show").show(1000);
            $("#divLoader").show();
            $("#messagebox").hide();
            $("#messageList").hide();
            $("#ulMessageBetween").html("");

            var ee = $("#" + userid).val();

            var user = {
                ToUserName: ee,
            };
            senttoid = userid;
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
                        //var msgId = {
                        //  MessageId: data,
                        //};

                        //$.ajax({
                        //    type: "POST",
                        //    url: "DashboardStudent.aspx/SaveIsRead",
                        //    data: JSON.stringify(msgId),
                        //    contentType: "application/json; charset=utf-8",
                        //    dataType: "json",
                        //    success: function (msg) {




                        //    }
                        //});
                        //window.location.href = "StudentMessage.aspx?id=" + data.MessageId;
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
    </script>
</asp:Content>
