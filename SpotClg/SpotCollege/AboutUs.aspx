<%@ Page Title="AboutUs" Language="C#" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="SpotCollege.AboutUs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Careers</title>
    <link href="../Style/style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../Style/bootstrap-responsive.css" rel="stylesheet" type="text/css" />

    <!--[if IE]>
        <link rel="stylesheet" href="../Style/ie.css" type="text/css" media="screen" />
    <![endif]-->

    <script type="text/javascript" src="../Scripts/js/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
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
                                        <%--<li><a href="Account/Login.aspx" class="home">Home</a></li>--%>
                                        <li class="navi_active"><a href="#">AboutUs</a></li>
                                    </ul>
                                </nav>
                            </div>
                            <div class="span1">
                                <ul class="login_logoutbtn">
                                    <li><a href="Account/Login.aspx">Login</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="content">
                    <div class="container">
                        <div class="row">
                            <div class="span12">
                                <div class="simplepage_design">
                                    <div class="pattern_box box_space">
                                        <h1>SpotCollege.com About Us</h1>
                                    </div>
                                </div>
                            </div>
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

