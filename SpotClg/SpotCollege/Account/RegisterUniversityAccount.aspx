<%@ Page Title="Register University Account" Language="C#" AutoEventWireup="true" CodeBehind="RegisterUniversityAccount.aspx.cs" Inherits="SpotCollege.Account.RegisterUniversityAccount" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

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
        //    $("#ddlStudentType").change(function () {
        //        var selectedType = $(this).val();
        //        if (selectedType == "5" || selectedType == "6") {
        //            window.location.href = "RegisterUniversity.aspx";
        //        }
        //    });
        //});
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <div class="bottombg">
            <div class="bottomgreenbg">
                <div class="header">
                    <div class="container">
                        <div class="row">
                            <div class="span3">
                                <a href="../Account/Login.aspx" class="logo">
                                    <img src="../Images/d_logo.png" alt="" /></a>
                            </div>
                            <div class="span8">
                                <nav id="nav-wrap">
                                    <ul class="navigation" id="nav">
                                        <%--<li><a href="#">Learn more</a></li>--%>
                                        <li class="navi_active"><a href="Register.aspx">Create University account</a></li>
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
                                <div class="pattern_box university_accountbox box_space">
                                    <h1><%--Hey, Welcome to Join Us!--%>To join our portal, kindly fill up this form and our representative will get in touch with you soon</h1>
                                    <ul class="university_account_list">
                                        <%-- <asp:DropDownList ID="ddlStudentType" runat="server"></asp:DropDownList>--%>
                                        <li>
                                            <label>University Name</label>
                                            <asp:TextBox ID="txtunivercitname" runat="server" placeholder="Enter University Name"></asp:TextBox>
                                            <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" TargetControlID="txtunivercitname" WatermarkText="Enter University Name" runat="server"></asp:TextBoxWatermarkExtender>--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ControlToValidate="txtunivercitname" CssClass="field-validation-error" ErrorMessage="The University Name field is required." ValidationGroup="RegistrationValidation" />
                                        </li>
                                        <li>
                                            <label>University Address</label>
                                            <asp:TextBox ID="txtaddress" runat="server" placeholder="Enter University Address"></asp:TextBox>
                                            <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" TargetControlID="txtaddress" WatermarkText="Enter University Address" runat="server"></asp:TextBoxWatermarkExtender>--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtaddress" CssClass="field-validation-error" ErrorMessage="The Address field is required." ValidationGroup="RegistrationValidation" />
                                        </li>
                                        <li>
                                            <label>University City</label>
                                            <asp:TextBox ID="txtunviersitycity" runat="server" placeholder="Enter University City"></asp:TextBox>
                                            <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" TargetControlID="txtunviersitycity" WatermarkText="Enter University City" runat="server"></asp:TextBoxWatermarkExtender>--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ControlToValidate="txtunviersitycity" CssClass="field-validation-error" ErrorMessage="The City field is required." ValidationGroup="RegistrationValidation" />
                                        </li>

                                        <li>
                                            <label>Country</label>
                                            <asp:DropDownList ID="ddlCountry" runat="server">
                                            </asp:DropDownList>
                                            <%--   <asp:Label ID="lblCountryValidation" runat="server" Text="Please Select Any" Visible="false"
                                        CssClass="field-validation-error"></asp:Label>--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Display="Dynamic" ControlToValidate="ddlCountry"
                                                InitialValue="0" CssClass="field-validation-error" ErrorMessage="Plsase Select Country"
                                                ValidationGroup="RegistrationValidation"></asp:RequiredFieldValidator>
                                        </li>

                                        <li>
                                            <label>Authorized</label>
                                            <asp:TextBox ID="txtAuthRepr" runat="server" placeholder="Name of Authorized representative"></asp:TextBox>
                                            <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" TargetControlID="txtAuthRepr" WatermarkText="Name of Authorized representative" runat="server"></asp:TextBoxWatermarkExtender>--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ControlToValidate="txtAuthRepr" CssClass="field-validation-error" ErrorMessage="The Contact No field is required." ValidationGroup="RegistrationValidation" />

                                        </li>
                                        <li>
                                            <label>Country Code</label>

                                            <asp:DropDownList ID="ddlCountryCode" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator96" InitialValue="0" Display="Dynamic" runat="server" CssClass="field-validation-error"
                                                ControlToValidate="ddlCountryCode" ErrorMessage="Country Code is Required"
                                                ValidationGroup="RegistrationValidation"></asp:RequiredFieldValidator>
                                        </li>
                                        <li>
                                            <label>University Contact no.</label>
                                            <asp:TextBox ID="txtcontactno" runat="server" MaxLength="10" placeholder="Enter University Contact No"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ControlToValidate="txtcontactno" CssClass="field-validation-error" ErrorMessage="The Contact No field is required." ValidationGroup="RegistrationValidation" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" CssClass="field-validation-error"
                                                ValidationExpression="\d{6,10}" runat="server" ControlToValidate="txtcontactno"
                                                ErrorMessage="Invalid Number" ValidationGroup="RegistrationValidation"></asp:RegularExpressionValidator>
                                        </li>
                                        <li>
                                            <label>Email</label>
                                            <asp:TextBox ID="txtuniversityEmail" runat="server" placeholder="Enter Email"></asp:TextBox>
                                            <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" TargetControlID="txtuniversityEmail" WatermarkText="Enter Email" runat="server"></asp:TextBoxWatermarkExtender>--%>
                                            <asp:RequiredFieldValidator ID="reqEmail" runat="server" Display="Dynamic" ControlToValidate="txtuniversityEmail" CssClass="field-validation-error" ErrorMessage="The Email field is required." ValidationGroup="RegistrationValidation" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" runat="server" CssClass="field-validation-error" ControlToValidate="txtuniversityEmail" ValidationGroup="RegistrationValidation" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid Email"></asp:RegularExpressionValidator>
                                            <asp:Label ID="duplicateUser" runat="server" CssClass="field-validation-error" Style="display: none">Email already exist in Record. Please Choose Any other..!</asp:Label>
                                        </li>
                                        <li>
                                            <label></label>
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit Application" OnClick="btnSubmit_Click" ValidationGroup="RegistrationValidation" CssClass="button" />
                                        </li>
                                        <li>
                                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                        </li>
                                    </ul>
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
                                    <li><a href="../Account/Login.aspx">Home</a></li>
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
<script>
    $(function () {
        $("#txtuniversityEmail").change(function () {
            if ($("#txtuniversityEmail").val() != "") {
                if (Page_IsValid) {
                    var User = {
                        UserName: $('#txtuniversityEmail').val(),
                    };
                    $.ajax({
                        type: "POST",
                        url: "RegisterUniversityAccount.aspx/CheckUserName",
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
