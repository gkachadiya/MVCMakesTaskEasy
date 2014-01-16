<%@ Page Title="Login" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SpotCollege.Account.Login" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Style/style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../Style/bootstrap-responsive.css" rel="stylesheet" type="text/css" />

    <!--[if IE]>
        <link rel="stylesheet" href="../Style/ie.css" type="text/css" media="screen" />
    <![endif]-->
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../Scripts/js/jquery.min.js"></script>

    <script src="../Scripts/js/jquery.min.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="../Scripts/js/jquery-ui.js"></script>
    <link href="../Content/themes/base/jquery-ui.css" rel="stylesheet" />

    <script type="text/javascript">
        jQuery(document).ready(function ($) {

            /* prepend menu icon */
            $('#nav-wrap').prepend('<div id="menu-icon">Menu</div>');

            /* toggle nav */
            $("#menu-icon").on("click", function () {
                $("#nav").slideToggle();
                $(this).toggleClass("active");
            });

        });
    </script>

    <!-- Fade slide -->
    <script type="text/javascript">
        $(document).ready(function () {

            var imgs = $('#slider > img'); var z = 1; var previousImageId = "";

            $(imgs[0]).show(5000);

            function loop(ev) {
                imgs.delay(5000).fadeOut(1000).eq(z).fadeIn(1000, function () {
                    check = z != imgs.length - 1 ? z++ : z = 0;

                    loop();
                });
            }
            loop();
        });
    </script>

    <%--For Check Duplicate UserName--%>
    <script>
        $(function () {
            $("#txtEmail").change(function () {
                if ($("#txtEmail").val() != "") {
                    if (Page_IsValid) {
                        var User = {
                            UserName: $('#txtEmail').val(),
                        };
                        $.ajax({
                            type: "POST",
                            url: "Login.aspx/ValidateUser",
                            data: JSON.stringify(User),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                if (msg.d == 'false') {
                                    $('#btnSubmit').show();
                                    $('#duplicateUser').hide();
                                }
                                else {
                                    $('#btnSubmit').hide();
                                    $('#duplicateUser').show();
                                }
                            }
                        });
                    }
                }
            });
        });

    </script>
</head>

<body class="dashboardbg">
    <form id="requestform" runat="server">
        
        <div class="dashboardheader">
            <div class="container">
                <div class="row-fluid">
                    <div class="span7">
                        <a href="../Account/Login.aspx" class="logo">
                            <img src="../Images/d_logo.png" alt="" /></a>
                    </div>
                    <div class="span5 pull-right">
                        <ul class="d_navigation">
                            <li>
                                <asp:TextBox ID="txtUserName" runat="server" placeholder="Username"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtUserName" runat="server" ErrorMessage="Username is Required" CssClass="field-validation-error" ValidationGroup="LoginValidation"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" runat="server" CssClass="field-validation-error" ControlToValidate="txtUserName" ValidationGroup="LoginValidation" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid Username"></asp:RegularExpressionValidator>
                            </li>
                            <li>
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="password"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="txtPassword" runat="server" ErrorMessage="Password is Required" CssClass="field-validation-error" ValidationGroup="LoginValidation"></asp:RequiredFieldValidator>
                                <a href="ForgotPassword.aspx">Forgot your password?</a>
                            </li>
                            <li class="d_navigation_last">
                                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" ValidationGroup="LoginValidation" />
                            </li>
                        </ul>
                        <asp:Label ID="lblErrorMsg" runat="server" Visible="false" CssClass="field-validation-error username_password_error"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div id="dialog-Alert" title="Aggrement" style="display: none;">
            <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Must accept our Privacy Policy and T&C to join</p>
        </div>
        <div class="d_banner">
            <div class="container">
                <div class="row-fluid">
                    <div class="span8">
                        <div class="row-fluid">
                            <div class="span6">
                                <div class="country_block">
                                    <h3>Contact International Universities
                                    <br />
                                        from more than a dozen Countries.</h3>
                                    <img src="../Images/country_img.png" alt="" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="span4">
                        <div class="d_register_block">
                            <h1>Students: Sign up here</h1>
                            <ul class="login_list">
                                <li>
                                    <label>Enter Username</label>
                                    <div>
                                        <span class="login_sep"></span><span class="login_email"></span>
                                        <asp:TextBox ID="txtEmail" runat="server" placeholder="Enter Email"></asp:TextBox>
                                        <span id="reqEmail" style="display: none; color: red">The Email field is required.</span>
                                        <span id="isValidEmail" style="display: none; color: red">Invalid Email</span>
                                        <%--<asp:RequiredFieldValidator ID="reqEmail" Display="Dynamic" runat="server" ControlToValidate="txtEmail" CssClass="field-validation-error" ErrorMessage="The Email field is required." ValidationGroup="RegistrationValidation" />--%>
                                        <%--<asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator2" runat="server" CssClass="field-validation-error" ControlToValidate="txtEmail" ValidationGroup="RegistrationValidation" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid Email"></asp:RegularExpressionValidator>--%>
                                        <asp:Label ID="duplicateUser" runat="server" CssClass="field-validation-error" Style="display: none">Email already exist in Record. Please Choose Any other..!</asp:Label>
                                    </div>
                                </li>
                                <li>
                                    <label>Enter Password</label>
                                    <div>
                                        <span class="login_sep"></span><span class="login_password"></span>
                                        <asp:TextBox ID="txtRegPassword" runat="server" TextMode="Password" placeholder="Enter Password"></asp:TextBox>
                                        <span id="reqPassword" style="display: none; color: red">The Password field is required.</span>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ControlToValidate="txtRegPassword" CssClass="field-validation-error" ErrorMessage="The Password field is required." ValidationGroup="RegistrationValidation" />--%>
                                    </div>
                                </li>
                                <li>
                                    <label>Confirm Password</label>
                                    <div>
                                        <span class="login_sep"></span><span class="login_password"></span>
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" placeholder="Confirm Password"></asp:TextBox>
                                        <span id="reqConfirmPassword" style="display: none; color: red">The Confirm Password field is required.</span>
                                        <span id="isValidConfirmPassword" style="display: none; color: red">The password and confirmation password do not match.</span>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ControlToValidate="txtConfirmPassword" CssClass="field-validation-error" ErrorMessage="The confirm password field is required." ValidationGroup="RegistrationValidation" />--%>
                                        <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtRegPassword" ControlToValidate="txtConfirmPassword" CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." ValidationGroup="RegistrationValidation" />--%>
                                    </div>
                                </li>
                                <li>
                                    <asp:CheckBox ID="chkPolicy" runat="server" /><span>I agree to <a href="../PrivacyAndPolicy.aspx">Privacy Policy</a> and <a href="../TermToUse.aspx">T&C</a></span>
                                </li>
                                <li>
                                    <asp:Button ID="btnSubmit" runat="server" Text="Register" OnClientClick="return false;" ValidationGroup="RegistrationValidation" CssClass="button" />
                                </li>
                                <asp:Label ID="lblpolicy" runat="server" ForeColor="Red"></asp:Label>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div id="slider">
                <img src="../Images/slide1.jpg" alt="" />
                <img src="../Images/slide2.jpg" alt="" />
                <img src="../Images/slide3.jpg" alt="" />
                <img src="../Images/slide4.jpg" alt="" />
            </div>
        </div>


        <div class="d_content">
            <div class="container">
                <div class="row-fluid">
                    <ul>
                        <li class="span6">
                            <div class="d_transperntbox">
                                <div class="heading">
                                    <h1>See Which Colleges Want You</h1>
                                </div>
                                <ul>
                                    <li>
                                        <img src="../Images/d_img11.gif" alt="" />
                                        <span>Search for the perfect international university that fits your budget and program</span>
                                    </li>
                                    <li>
                                        <img src="../Images/d_img22.gif" alt="" />
                                        <span>Contact admissions office for scholarships and other queries</span>
                                    </li>
                                    <li>
                                        <img src="../Images/d_img33.gif" alt="" />
                                        <span>Locate other students from your city applying to same university and collaborate with them on matters of housing, travel, flights and more</span>
                                    </li>
                                    <%-- <li>
                                    <img src="../Images/d_img44.gif" alt="" />
                                    <span>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor.</span>
                                </li>--%>
                                </ul>
                            </div>
                        </li>
                        <li class="span6">
                            <div class="d_transperntbox">
                                <div class="heading">
                                    <h1>Universities: Contact students around the world</h1>
                                </div>
                                <ul>
                                    <li>
                                        <img src="../Images/d_img5.gif" alt="" />
                                        <span>Find the perfect students matching your admissions criteria.</span>
                                    </li>
                                    <li>
                                        <img src="../Images/d_img6.gif" alt="" />
                                        <span>View complete profiles including marksheets, test scores, certificates</span>
                                    </li>
                                    <li>
                                        <img src="../Images/d_img7.gif" alt="" />
                                        <span>Make spot offers to the right candidate</span>
                                    </li>
                                    <%-- <li>
                                    <img src="../Images/d_img8.gif" alt="" />
                                    <span>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor.</span>
                                </li>--%>
                                    <a href="RegisterUniversityAccount.aspx" id="signUni" runat="server" class="button fright">Sign Up here</a>
                                </ul>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="d_footer">
            <div class="container">
                <div class="row-fluid">
                    <div class="span9">
                        <ul class="d_footer_toplink">
                            <li><a href="Login.aspx">Home</a></li>
                            <li><a href="../AboutUs.aspx">About Us</a></li>
                            <li><a href="../PrivacyAndPolicy.aspx">Privacy Policy</a></li>
                            <li><a href="../TermToUse.aspx">Terms of Use</a></li>
                            <li><a href="../Careers.aspx">Careers</a></li>
                        </ul>
                    </div>
                    <div class="span3"><span class="d_footer_righttext">© 2013, Spot College.com, LLC</span></div>
                </div>
            </div>
        </div>
    </form>
</body>
<script type="text/javascript">
    //$("#btnSubmit").click(function () {
    //    if (!$("#chkPolicy").is(":checked")) {
    //        alert("Must accept our Privacy Policy and T&C to join");
    //        return false;
    //    }
    //});

    $("#btnSubmit").click(function () {
        if (Page_IsValid) {
            var userinfo = {
                UserName: $("#txtEmail").val(),
                Password: $("#txtRegPassword").val(),
            };

            var flag = "";

            if ($("#txtEmail").val() == "") {
                $("#reqEmail").show();
                flag = false;
            }
            else {
                $("#reqEmail").hide();
                var x = $("#txtEmail").val();
                var atpos = x.indexOf("@");
                var dotpos = x.lastIndexOf(".");
                if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {
                    $("#isValidEmail").show();
                    flag = false;
                }
            }

            if ($("#txtRegPassword").val() == "") {
                $("#reqPassword").show();
                flag = false;
            }
            else if ($("#txtConfirmPassword").val() == "") {
                $("#reqPassword").hide();
                $("#reqConfirmPassword").show();
                flag = false;
            }
            else {
                $("#reqEmail").hide();
                $("#isValidEmail").hide();
                $("#reqPassword").hide();
                $("#reqConfirmPassword").hide();
                $("#reqConfirmPassword").hide();
                if ($("#txtRegPassword").val() != $("#txtConfirmPassword").val()) {
                    $("#isValidConfirmPassword").show();
                    flag = false;
                }
                else {
                    $("#isValidConfirmPassword").hide();
                    flag = true;
                }
                flag = true;
            }

            if (flag == true) {
                if ($("#chkPolicy").is(":checked")) {
                    $.ajax({
                        type: "POST",
                        beforeSend: function () { $("#LoadingImage").show(); },
                        complete: function () { $("#LoadingImage").hide(); },
                        url: "Login.aspx/RegisterUser",
                        data: JSON.stringify(userinfo),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            $("#txtEmail").val("");
                            $("#txtRegPassword").val("");
                            $("#txtConfirmPassword").val("");
                            $("#LoadingImage").hide();
                            window.location.href = "EditProfile.aspx";
                        }
                    }); 
                }
                else {
                    $("#dialog-Alert").dialog({
                        open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                        resizable: false,
                        height: 170,
                        modal: true,
                        buttons: {
                            "OK": function () {
                                $(this).dialog("close");
                            }
                        },
                    });
                }
            }
        }
    });
</script>
</html>
