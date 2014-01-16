﻿<%@ Page Title="Forgot Password" Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="SpotCollege.Account.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Style/style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../Style/bootstrap-responsive.css" rel="stylesheet" type="text/css" />

    <!--[if IE]>
        <link rel="stylesheet" href="../Style/ie.css" type="text/css" media="screen" />
    <![endif]-->

    <script type="text/javascript" src="../Scripts/js/jquery.min.js"></script>
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

        //$(function () {
        //    $("#txtEmail").change(function () {
        //        if ($("#txtEmail").val() != "") {
        //            if (Page_IsValid) {
        //                var University = {
        //                    UserName: $('#txtEmail').val(),
        //                };
        //                $.ajax({
        //                    type: "POST",
        //                    url: "ForgotPassword.aspx/CheckUserName",
        //                    data: JSON.stringify(University),
        //                    contentType: "application/json; charset=utf-8",
        //                    dataType: "json",
        //                    success: function (msg) {
        //                        if (msg.d == 'true') {
        //                            $('#btnforgotpassword').show();
        //                            $('#lblDuplicateuser').hide();

        //                        }
        //                        else {
        //                            $('#btnforgotpassword').hide();
        //                            $('#lblDuplicateuser').show();
        //                        }
        //                    }
        //                });
        //            }
        //        }
        //    });
        //});
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="bottombg">
            <div class="bottomgreenbg">
                <div class="header">
                    <div class="container">
                        <div class="row">
                            <div class="span3">
                                <a href="../Default.aspx" class="logo">
                                    <img src="../Images/d_logo.png" alt="" /></a>
                            </div>
                            <div class="span8">
                                <nav id="nav-wrap">
                                    <ul class="navigation" id="nav">
                                        <%--<li class="navi_active"><a href="#">Learn more</a></li>
                                    <li><a href="Register.aspx">Creat account</a></li>--%>
                                    </ul>
                                </nav>
                            </div>
                            <div class="span1">
                                <ul class="login_logoutbtn">
                                    <li><a href="Login.aspx">Login</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="content">
                    <div class="container">
                        <div class="row">
                            <div class="span3"></div>
                            <div class="span6">
                                <div class="pattern_box loginbox">
                                    <h1>Hey, welcome back!</h1>
                                    <ul class="login_list">
                                        <li>
                                            <img src="../Images/Login_thumbli.png" alt="" /></li>
                                        <li>
                                            <span class="login_sep"></span>
                                            <span class="login_email"></span>
                                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEmail" runat="server" ErrorMessage="Email is Required" CssClass="field-validation-error" ValidationGroup="LoginValidation"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" CssClass="field-validation-error" ControlToValidate="txtEmail" ValidationGroup="LoginValidation" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid Username"></asp:RegularExpressionValidator>
                                            <asp:Label ID="lblValidationMsg" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                        </li>
                                        <li>
                                            <asp:Button ID="btnforgotpassword" Text="Forgot Password" class="button fright" OnClick="btnforgot_Click" ValidationGroup="LoginValidation" runat="server" />
                                            <label>To reset your password, enter the email address you use to sign in to Spot College.</label>
                                        </li>
                                    </ul>
                                    <span class="creatac_line">It's great to have you. Please use the options below to log in. Or, <a href="Register.aspx">create an account</a> if you don't already have one.</span>
                                </div>
                            </div>
                            <div class="span3"></div>
                        </div>
                    </div>
                </div>

                <div class="d_footer">
                    <div class="container">
                        <div class="row">
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
            </div>
        </div>
    </form>
</body>
</html>
