<%@ Page Title="Student Profile OverView" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProfileOverView.aspx.cs" Inherits="SpotCollege.Account.ProfileOverView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="Scripts/js/jquery.flexslider-min.js"></script>
    <script src="../Scripts/WebForms/WebUIValidation.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hdnQueryUID" runat="server" />
    <div class="row-fluid">
        <div class="span12">
            <div id="dialog-confirm" title="Are You Sure?" style="display: none;">
                <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Are you sure?</p>
            </div>
            <div class="msg_replay_hide">
                <div class="pattern_box box_space">
                    <div class="width100per">
                        <div class="fright">
                            <asp:LinkButton ID="lnkBtnDecline" runat="server" OnClick="lnkBtnDecline_Click" Visible="false" class="button fright">Ignore </asp:LinkButton>
                            <div id="lnkBtnApprove" runat="server" visible="false" style="float: right; margin-right: 5px; display: inline-block;">
                                <a id="lnkMessage" onclick="javascript:OpenMsgPopup('<%=hdnQueryUID.Value%>')" class="button">Send Message</a>
                            </div>
                            <%--<asp:LinkButton ID="lnkBtnApprove" runat="server" OnClick="lnkBtnApprove_Click" Visible="false">Send Message</asp:LinkButton>--%>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span6">
                            <div id="BasicOverview" runat="server">
                                <div class="shadowbox">
                                    <div class="h2_heading">
                                        <div class="accordion-header">
                                            <h2>Basics Information</h2>
                                        </div>
                                        <asp:LinkButton ID="lnkEditBasicInfo" runat="server" OnClick="lnkEditBasicInfo_Click" Text="Edit" CssClass="edit fright"></asp:LinkButton>
                                    </div>

                                    <div class="accordion-content">
                                        <ul class="list_4">
                                            <li>
                                                <label>
                                                    First Name :</label>
                                                <asp:Label ID="lblFirstName" runat="server"></asp:Label>

                                            </li>
                                            <li>
                                                <label>
                                                    Middle Name :</label>
                                                <asp:Label ID="lblMiddleName" runat="server"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    Last Name :</label>
                                                <asp:Label ID="lblLastName" runat="server"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    Country of Citizenship :</label>
                                                <asp:Label ID="lblCountryOfCitizenship" runat="server"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    Address 1 :</label>
                                                <asp:Label ID="lblAddress1" runat="server"></asp:Label>

                                            </li>
                                            <li>
                                                <label>
                                                    Address 2 :</label>
                                                <asp:Label ID="lblAddress2" runat="server"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    Zip Code :</label>
                                                <asp:Label ID="lblzipcode" runat="server"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    Primary Phone :</label>
                                                <asp:Label ID="lblPrimaryNumber" runat="server"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    Secondary Phone :</label>
                                                <asp:Label ID="lblSecondaryNumber" runat="server"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    Birth Date :</label>
                                                <asp:Label ID="lblBirthDate" runat="server"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    City :</label>
                                                <asp:Label ID="lblCity" runat="server"></asp:Label>

                                            </li>
                                            <li>
                                                <label>
                                                    Country :</label>
                                                <asp:Label ID="lblCountry" runat="server"></asp:Label>

                                            </li>
                                            <li>
                                                <label>
                                                    Primary Email :</label>
                                                <asp:Label ID="lblPrimaryEmail" runat="server"></asp:Label>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div id="EducationPreferences" runat="server">
                                <div class="shadowbox">
                                    <div class="h2_heading">
                                        <div class="accordion-header">
                                            <h2>Education Preferences</h2>
                                        </div>
                                        <asp:LinkButton ID="lnkEducationPreferences" runat="server" OnClick="lnkEducationPreferences_Click" Text="Edit" CssClass="edit fright"></asp:LinkButton>
                                    </div>

                                    <div class="accordion-content">
                                        <ul class="list_4">
                                            <li>
                                                <label>
                                                    I am currently in :</label>
                                                <asp:Label ID="lblCurrentlyIn" runat="server"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    program looking for :</label>
                                                <asp:Label ID="lblLookingFor" runat="server"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    I prefer to study in :</label>

                                                <asp:Label ID="lblPreferStudyIn" runat="server"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    Expected Start Date :</label>
                                                <asp:Label ID="lblExpectedStartDate" runat="server"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    I can afford to pay :</label>
                                                <asp:Label ID="lblPayout" runat="server"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    Desired Field of Study:</label>
                                                <asp:Label ID="lbldesiredfieldstudy" runat="server"></asp:Label>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="shadowbox right_space">
                                <div class="h2_heading">
                                    <div class="accordion-header">
                                        <h2>Photo :</h2>
                                    </div>
                                    <asp:LinkButton ID="lnkPhotoEdit" runat="server" OnClick="lnkPhotoEdit_Click" Text="Change" CssClass="edit fright"></asp:LinkButton>
                                </div>
                                <div class="accordion-content">
                                    <ul class="list_4">

                                        <li>
                                            <label>
                                                profile Image :</label>
                                            <span>
                                                <a href="" runat="server" id="hoverimg" class="preview">
                                                    <img id="imgProfileImage" class="large_images" runat="server" visible="false" src="" /></a></span> </li>

                                    </ul>
                                </div>
                            </div>
                        </div>

                        <div class="span6">
                            <div id="CurrentAcademics" runat="server">
                                <div class="shadowbox right_space">
                                    <div class="h2_heading">
                                        <div class="accordion-header">
                                            <h2>Current Academics</h2>
                                        </div>
                                        <asp:LinkButton ID="lnkEditCurrentAcademics" runat="server" OnClick="lnkEditCurrentAcademics_Click" Text="Edit" CssClass="edit fright"></asp:LinkButton>
                                    </div>

                                    <div class="accordion-content">
                                        <asp:GridView ID="grvAcademicDetails" runat="server" CssClass="box simple_table" AutoGenerateColumns="false"
                                            DataKeyNames="StudentAcademicsId" EmptyDataText="No Academic Record Found...!">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SchoolName" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lnkSchoolName" runat="server" Text='<%# Eval("SchoolName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Graduate?" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGraduateStatus" runat="server" Text='<%# ((SpotCollege.Common.GraduateStatus)(Eval("Graduate"))).ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Rank" HeaderText="Rank" HeaderStyle-Width="8%" ItemStyle-Width="8%"></asp:BoundField>
                                                <asp:TemplateField HeaderText="Certificate" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <a href='<%# "..\\Images\\Certificate\\"+Eval("CertificatePath") %>' runat="server" id="hovercertificate" class="preview">
                                                            <img id="imgCertificate" class="large_images" src='<%# "..\\Images\\Certificate\\"+Eval("CertificatePath") %>' height="30" width="30" /></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div id="internationalTest" runat="server">
                                <div class="shadowbox right_space">
                                    <div class="h2_heading">
                                        <div class="accordion-header">
                                            <h2>International Test</h2>
                                        </div>
                                        <asp:LinkButton ID="lnkInternationalTest" runat="server" OnClick="lnkInternationalTest_Click" Text="Edit" CssClass="edit fright"></asp:LinkButton>
                                    </div>
                                    <div class="accordion-content">
                                        <asp:GridView ID="GrdInternationalTest" runat="server" CssClass="simple_table box" AutoGenerateColumns="false" DataKeyNames="StudentTestId"
                                            EmptyDataText="No Test Record Found..!">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Test Name" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hndStudentTestId" runat="server" Value='<%#Eval("StudentTestId") %>' />
                                                        <asp:Label ID="lblTestname" runat="server" Text=' <%# (SpotCollege.Common.EnumHelper.GetDescriptionFromEnumValue((SpotCollege.Common.InternationalTestRecord)Eval("TestId"))).ToString()+"-"+Eval("Date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="txt_align_right" ItemStyle-CssClass="txt_align_right" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <a id='deleteTest<%#Eval("StudentTestId")%>' href="javascript:TestDelete('<%#Eval("StudentTestId")%>')" class="delete">Delete</a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div id="WorkHistory" runat="server">
                                <div class="shadowbox right_space">
                                    <div class="h2_heading">
                                        <div class="accordion-header">
                                            <h2>Work history</h2>
                                        </div>
                                        <asp:LinkButton ID="lnkWorkhistory" runat="server" OnClick="lnkWorkhistory_Click" Text="Edit" CssClass="edit fright"></asp:LinkButton>
                                    </div>
                                    <div class="accordion-content">
                                        <asp:GridView ID="grvWorkHistory" runat="server" CssClass="simple_table box" AutoGenerateColumns="false" DataKeyNames="StudentWorkHistoryId"
                                            EmptyDataText="No History Record Found..!">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Company Name" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lnkCompanyName" runat="server" Text='<%# Eval("CompanyName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:BoundField DataField="Position" HeaderText="Position" HeaderStyle-Width="8%" ItemStyle-Width="8%"></asp:BoundField>--%>
                                                <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-Width="8%" ItemStyle-Width="8%"></asp:BoundField>
                                                <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-Width="8%" ItemStyle-Width="8%"></asp:BoundField>
                                                <%-- <asp:BoundField DataField="Responsibilities" HeaderText="Responsibilities" HeaderStyle-Width="8%" ItemStyle-Width="8%"></asp:BoundField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div id="CurricularActivities" runat="server">
                                <div class="shadowbox right_space">
                                    <div class="h2_heading">
                                        <div class="accordion-header">
                                            <h2>Extra Curricular Activies</h2>
                                        </div>
                                        <asp:LinkButton ID="lnkExtraCurricularActivies" runat="server" OnClick="lnkExtraCurricularActivies_Click" Text="Edit" CssClass="edit fright"></asp:LinkButton>
                                    </div>

                                    <div class="accordion-content">
                                        <ul class="list_4">
                                            <li>
                                                <label>Sports :</label>
                                                <asp:Label ID="lblSports" runat="server"></asp:Label>
                                            </li>
                                            <li>
                                                <label>Leadership :</label>
                                                <asp:Label ID="lblLeadership" runat="server"></asp:Label>
                                            </li>
                                            <li>
                                                <label>Activites :</label>
                                                <asp:Label ID="lblOtherActivities" runat="server"></asp:Label>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
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
                            <div id="Div1" runat="server">
                                <div class="shadowbox">
                                    <div class="h2_heading">
                                        <div class="accordion-header">
                                            <h2>Basics Information</h2>
                                        </div>
                                    </div>

                                    <div class="accordion-content">
                                        <ul class="list_4">
                                            <li>
                                                <label>
                                                    First Name :</label>
                                                <asp:Label ID="Label1" runat="server"></asp:Label>

                                            </li>
                                            <li>
                                                <label>
                                                    Middle Name :</label>
                                                <asp:Label ID="Label2" runat="server"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    Last Name :</label>
                                                <asp:Label ID="Label3" runat="server"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    Country of Citizenship :</label>
                                                <asp:Label ID="Label4" runat="server"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    Address 1 :</label>
                                                <asp:Label ID="Label5" runat="server"></asp:Label>

                                            </li>
                                            <li>
                                                <label>
                                                    Address 2 :</label>
                                                <asp:Label ID="Label6" runat="server"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    Zip Code :</label>
                                                <asp:Label ID="Label7" runat="server"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    Primary Phone :</label>
                                                <asp:Label ID="Label8" runat="server"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    Secondary Phone :</label>
                                                <asp:Label ID="Label9" runat="server"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    Birth Date :</label>
                                                <asp:Label ID="Label10" runat="server"></asp:Label>
                                            </li>
                                            <li>
                                                <label>
                                                    City :</label>
                                                <asp:Label ID="Label11" runat="server"></asp:Label>

                                            </li>
                                            <li>
                                                <label>
                                                    Country :</label>
                                                <asp:Label ID="Label12" runat="server"></asp:Label>

                                            </li>
                                            <li>
                                                <label>
                                                    Primary Email :</label>
                                                <asp:Label ID="Label13" runat="server"></asp:Label>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
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
        </div>`
    </div>
    <script type="text/javascript">
        $(function () {
            $("#nav li").each(function () {
                $(this).removeClass('navi_active');
            });

            $("#Profile").addClass('navi_active');

        });

        $(function () {
            $(".msg_replay_show").hide();
        });
        $(".msg_replay_close").click(function () {
            flag_showpopup = false;
            $(".msg_replay_show").hide(1000);
            $(".msg_replay_hide").show(1000);
        });
        var senttoid;
        function OpenMsgPopup(username) {
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
                url: "/Account/ProfileOverView.aspx/Getstatus",
                data: JSON.stringify(user),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;
                    if (data != "") {
                        retriveConversation(data);
                    }
                    else {
                        $("#MainContent_txtSendToUserName").val(username);
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
                url: "/Account/ProfileOverView.aspx/GetAllConversation",
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
                    url: "/Account/ProfileOverView.aspx/SaveMessageReply",
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
                        url: "/Account/ProfileOverView.aspx/SendMessage",
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

        //for delete International test record
        function TestDelete(studentTestId) {
            $("#dialog-confirm").dialog({
                open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Yes": function () {
                        if (Page_IsValid) {
                            var test = {
                                StudentTestId: studentTestId
                            };
                            $.ajax({
                                type: "POST",
                                url: "ProfileOverView.aspx/TestDelete",
                                data: JSON.stringify(test),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    $("#deleteTest" + studentTestId).parent().parent().remove();

                                    if (msg.d) {
                                        //location.reload();
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
