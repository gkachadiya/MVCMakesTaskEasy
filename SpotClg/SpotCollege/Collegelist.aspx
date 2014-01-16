<%@ Page Title="CollegeList" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Collegelist.aspx.cs" Inherits="SpotCollege.Collegelist" ValidateRequest="false" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="span3">
            <div class="searh_box">
                <div class="pattern_box box_space">
                    <div class="h2_heading">
                        <h2>University Search</h2>
                    </div>
                    <ul>
                        <li>
                            <asp:DropDownList ID="ddlCountryList" runat="server">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <asp:DropDownList ID="ddlLookingFor" runat="server">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <asp:DropDownList ID="ddlCourses" runat="server">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" />
                        </li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="span9">
            <div id="LoadingImage" style="display: none">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/ajaxloader.gif" />
            </div>
            <div class="alluniversitybox">
                <div class="pattern_box box_space">
                    <div runat="server" style="color: black; font-size: larger" id="errorMsgDiv"></div>
                    <ul>
                        <asp:DataList ID="ddlunivercitylist" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" OnItemDataBound="ddlunivercitylist_ItemDataBound">
                            <ItemTemplate>
                                <li>

                                    <div class="img_padding">
                                        <asp:HiddenField ID="HnduniversityUserName" runat="server" Value='<%#Eval("UserName") %>' />
                                        <asp:Image ID="Imgunvercity" runat="server" alt="" />
                                    </div>

                                    <div class="university_detail">
                                        <div class="name">
                                            <asp:Label runat="server" Text="" ID="UniversityName"><%#Eval("UniversityName")%></asp:Label>
                                        </div>
                                        <div class="establishment">In <%#Eval("City") %>, <%#Eval("Country") %>. &nbsp;&nbsp; Year of establishment <%#Eval("EstablishedYear") %> </div>
                                        <ul class="small_university_box">
                                            <li>
                                                <div class="h2_heading">
                                                    <h2>Cost for International students:</h2>
                                                </div>
                                            </li>
                                          <%-- <li>&raquo; Tution for Undergraduate students (USD) : <%#Eval("UnderGraduateFee")==null ? "Not Available" : Eval("UnderGraduateFee") %>	</li>
                                                    <li>&raquo; Tution for graduate studens (USD) : <%#Eval("GraduateFee")==null ? "Not Available" : Eval("GraduateFee") %></li>
                                                    <li>&raquo; Books and supplies (USD) : <%#Eval("BookFee")==null ? "Not Available" : Eval("BookFee") %></li>
                                                    <li>&raquo; Off-campus room and Board (USD) : <%#Eval("BoardFee")==null ? "Not Available" : Eval("BoardFee") %></li>--%>

                                             <li>&raquo; Tution for Undergraduate students :<asp:Label runat="server" ID="lblundergraduatestudFee" Text='<%#Eval("UnderGraduateFee") %>'></asp:Label> </li>
                                                    <li>&raquo; Tution for graduate studens :<asp:Label runat="server" ID="lblgraduatestudFee" Text=' <%#Eval("GraduateFee") %>'></asp:Label></li>
                                                    <li>&raquo; Books and supplies :<asp:Label runat="server" ID="lblbookstudFee" Text=' <%#Eval("BookFee") %>'></asp:Label></li>
                                                    <li>&raquo; Off-campus room and Board :<asp:Label runat="server" ID="lblboardstudFee" Text=' <%#Eval("BoardFee") %>'></asp:Label></li>
                                               
                                        </ul>
                                        <div class="width100per"></div>
                                        <div class="clg_descripition" style="display: none">
                                            <p>
                                                <b>
                                                    <asp:Label ID="lblemail" runat="server" Text=""><%#Eval("description")%></asp:Label></b>
                                                <br />
                                                <b>
                                                    <asp:Label ID="lblapproved" runat="server" /></b>
                                            </p>
                                        </div>
                                        <ul class="small_university_box">
                                            <li>
                                                <div class="h2_heading">
                                                    <h2>Enrollment Numbers</h2>
                                                </div>
                                            </li>
                                            <li>&raquo;Number of Graduate students : <%#Eval("Graduates")==null ? "Not Available" : Eval("Graduates") %> </li>
                                            <li>&raquo;Number of Undergraduate Students : <%#Eval("UnderGraduates")==null ? "Not Available" : Eval("UnderGraduates") %> </li>
                                            <li>&raquo;Number of International students : <%#Eval("InternationalGraduate")==null ? "Not Available" : Eval("InternationalGraduate") %> </li>
                                        </ul>
                                        <div class="width100per"></div>
                                        <div class="moreinformation">
                                            <a id='<%#Eval("UniversityName")%>' class="anUniversityDetail button fright">More Information</a>
                                        </div>
                                    </div>
                                    <div class="clg_apply" style="display: none">
                                        <a id="lnkMessage" href="Openpopup('<%#Eval("UserName")%>')" class="button fright">Message</a>
                                        <asp:Button ID="btndecline" runat="server" Text="Decline" OnClick="btndecline_Click" CommandArgument='<%#Eval("UserName") %>' CssClass="button fright" Visible="false" />
                                        <asp:Button ID="btnapply" runat="server" Text="Apply" OnClick="btnapply_Click" CssClass="button fright" Visible="false" />
                                        <input type="button" onclick="javascript: Openpopup('<%#Eval("UniversityName")%>    ');" class="button" value="More Detail" />
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
                    <h2>Are you interested in this College?</h2>
                </div>
                <asp:Button ID="btnintrest" runat="server" Text="Yes" CssClass="button" />
                <asp:Button ID="btnnointrest" runat="server" Text="No" CssClass="button" />
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
                        1.Tution for Undergraduate students :
                        <asp:Label ID="lblundergradutefee" runat="server"></asp:Label>
                    </p>
                    <p>
                        2.Tution for graduate studens :
                        <asp:Label ID="lblgraduatefee" runat="server"></asp:Label>
                    </p>
                    <p>
                        3.Books and supplies :
                        <asp:Label ID="lblbookfee" runat="server"></asp:Label>
                    </p>
                    <p>
                        4.Off-campus room and Board :
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

    <%--For Message PopUp--%>
    <div id="messagebox" class="span12 messagebox" style="display: none" title="Message Box">
        <div class="row">
            <div class="span12">
                <div class="message_right">
                    <ul class="profile_form">
                        <li class="row">
                            <div class="span2">
                                <label>Send to :</label>
                            </div>
                            <div class="span3">
                                <asp:TextBox runat="server" ID="txtSendToUserName" Enabled="false" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSendToUserName" CssClass="field-validation-error" ErrorMessage="Enter Administrator Email Id " ValidationGroup="sendmessage" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" CssClass="field-validation-error" ControlToValidate="txtSendToUserName" ValidationGroup="sendmessage" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid Email"></asp:RegularExpressionValidator>
                            </div>
                        </li>
                        <li class="row">
                            <div class="span2">
                                <label>Subject :</label>
                            </div>
                            <div class="span3">
                                <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="field-validation-error" ErrorMessage="Enter Subject" ControlToValidate="txtSubject" ValidationGroup="sendmessage"></asp:RequiredFieldValidator>

                            </div>
                        </li>
                        <li class="row">
                            <div class="span2">
                                <label>Message :</label>
                            </div>
                            <div class="span3">
                                <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="field-validation-error" ErrorMessage="Enter Message" ControlToValidate="txtMessage" ValidationGroup="sendmessage"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="row">
                            <div class="span2"></div>
                            <div class="span3">
                                <asp:Button ID="btnMsgSend" runat="server" Text="Send" CssClass="large_button" ValidationGroup="sendmessage" />
                                <asp:Button ID="btnMsgCancel" runat="server" class="large_button" Text="Cancel" />
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>

    <div id="Dialog-ApplicationSend" title="Application Sent" style="display: none;">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>[<label id="lbluniname"></label>] admission's office has been notified that you are interested in learning more
about their institution. If your profile is a match, their representative will get in touch with you soon.</p>
    </div>
    <div id="Dialog-Send" title="Message Send" style="display: none;">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Message Send Successfully</p>
    </div>
    <div id="Dialog-Alert" title="" style="display: none;">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Please Enter Message Text</p>
    </div>

    <script type="text/ecmascript">
        $("#nav li").each(function () {
            $(this).removeClass('navi_active');
        });

        $("#University").addClass('navi_active');

        $(".anUniversityDetail").click(function(){
            Openpopup(this.id);
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
                            document.getElementById("MainContent_lblgradustudent").innerHTML =  "Not Available";
                        if (data[4] != null)
                            document.getElementById("MainContent_lblundergradute").innerHTML = data[4];
                        else
                            document.getElementById("MainContent_lblundergradute").innerHTML =  "Not Available";
                        if (data[5] != null)
                        {
                            document.getElementById("MainContent_lblinternational").innerHTML = data[5]; MainContent_imgprofile;
                        }
                        else
                            document.getElementById("MainContent_lblinternational").innerHTML =  "Not Available";

                        if (data[6] == null)
                            document.getElementById("MainContent_imgprofile").src = "Images/no_image.jpg";
                        else
                            document.getElementById("MainContent_imgprofile").src = "Images/Profile/" + data[6];

                        document.getElementById("MainContent_lblDescription").innerHTML =  data[7];

                        if (data[8] != null)
                            document.getElementById("MainContent_lblundergradutefee").innerHTML =  data[8];
                        else
                            document.getElementById("MainContent_lblundergradutefee").innerHTML =  "Not Available";

                        if (data[9] != null)
                            document.getElementById("MainContent_lblgraduatefee").innerHTML =  data[9];
                        else
                            document.getElementById("MainContent_lblgraduatefee").innerHTML =  "Not Available";

                        if (data[10] != null)
                            document.getElementById("MainContent_lblbookfee").innerHTML =  data[10];
                        else
                            document.getElementById("MainContent_lblbookfee").innerHTML =  "Not Available";

                        if (data[11] != null)
                            document.getElementById("MainContent_lblroom").innerHTML =  data[11];
                        else
                            document.getElementById("MainContent_lblroom").innerHTML =  "Not Available";

                        if (data[12] != "" &&data[12] !=null )
                            document.getElementById("MainContent_lbldegreelvloffred").innerHTML =  data[12];
                        else
                            document.getElementById("MainContent_lbldegreelvloffred").innerHTML =  "Not Available";

                        if (data[13] != "" &&data[13] !=null )
                            document.getElementById("MainContent_lblcourceoffered").innerHTML =  data[13];
                        else
                            document.getElementById("MainContent_lblcourceoffered").innerHTML =  "Not Available";

                        if(data[14]=="hideStdInt")
                            $("#MainContent_divIntersted").hide();
                        else
                            $("#MainContent_divIntersted").show();
                        
                        $("#myModal").dialog({ modal: true, minWidth: 700, resizable: false, minHeight: 300, closeOnEscape: true, closeText: "" });
                    }
                },
                error: function(xhr, textStatus, errorThrown){
                    alert('request failed'  + JSON.stringify(xhr)+ " "+  JSON.stringify(textStatus)+ " "+JSON.stringify(errorThrown));
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
                    $("#myModal").dialog('close');
                }

            });
        });
        $("#MainContent_btnnointrest").click(function () {
            $("#myModal").dialog('close');
        });
        //For Open Massegebox
        function OpenMsgPopup(sendToUserName) {
            var user = {
                ToUserName: sendToUserName,
            };
            $.ajax({
                type: "POST",
                beforeSend: function () { $("#LoadingImage").show(); },
                complete: function () { $("#LoadingImage").hide(); },
                url: "Collegelist.aspx/Getstatus",
                data: JSON.stringify(user),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;
                    if (data != "") {
                        window.location.href = "StudentMessage.aspx?id=" + data;
                    }
                    else {
                        $("#MainContent_txtSendToUserName").val(sendToUserName);
                        $("#messagebox").dialog({ modal: true, minWidth: 600, resizable: false, minHeight: 364, closeOnEscape: true, closeText: "" });
                    }
                }
            });

        }

        //For Send Message
        $("#MainContent_btnMsgSend").click(function () {
            if (Page_IsValid) {
                var Message = {
                    Title: $("#MainContent_txtSubject").val(),
                    Description: $("#MainContent_txtMessage").val(),
                    sendToUserName: $("#MainContent_txtSendToUserName").val(),
                    ParentId: "0",
                };
                if ($("#MainContent_txtSubject").val() != "" && $("#MainContent_txtMessage").val() != "") {
                    $.ajax({
                        type: "POST",
                        beforeSend: function () { $("#LoadingImage").show(); },
                        complete: function () { $("#LoadingImage").hide(); },
                        url: "Collegelist.aspx/SendMessage",
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
                            $("#MainContent_txtSubject").val("");
                            $("#MainContent_txtMessage").val("");
                            $("#messagebox").dialog("close");
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



        var flag=true;
        $(window).scroll(function(){
            if($(window).scrollTop() + $(window).height() == $(document).height()) {
                if(flag)
                {
                    $("#waitdiv").show();
                    flag=false
                    $.ajax({
                        type: "POST",
                        beforeSend: function(){ $("#waitdiv").show(); },
                        complete: function(){ $("#waitdiv").hide(); },
                        url: "Collegelist.aspx/AppendAndDisplayUniversity",
                        contentType: "application/json; charset=utf-8",
                        success: function(data)
                        {
                            if(data.d=="no")
                                $("#noUniversityMsg").show();
                            else{
                                $("#noUniversityMsg").hide();
                                flag=true;
                                $("#MainContent_ddlunivercitylist").append(data.d);
                            } 
                            $("#waitdiv").hide(); 
                        }
                    });
                }
                else
                {
                    
                }
            }
        });

    </script>

</asp:Content>
