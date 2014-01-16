<%@ Page Title="Dashboard" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SpotCollege.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/style.css" rel="stylesheet" type="text/css" />
    <link href="Style/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Style/bootstrap-responsive.css" rel="stylesheet" type="text/css" />

    <!--[if IE]>
        <link rel="stylesheet" href="css/ie.css" type="text/css" media="screen" />
    <![endif]-->

    <script type="text/javascript" src="Scripts/js/jquery.min.js"></script>
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

</head>
<body class="dashboardbg">
    <form runat="server" id="DefaultPageForm">
        <div class="dashboardheader">
            <div class="container">
                <div class="row">
                    <div class="span3">
                        <a href="Default.aspx" class="logo">
                            <img src="images/d_logo.png" alt="" /></a>
                    </div>
                    <div class="span9">
                        <nav id="nav-wrap">
                            <ul class="d_navigation" id="nav">
                                <li class="d_navia_active"><a href="#">Learn More</a></li>
                                <li><a href="Account/Register.aspx">Create account</a></li>
                                <li><a href="Account/Login.aspx">Login</a></li>
                            </ul>
                        </nav>
                    </div>
                </div>
            </div>
        </div>

        <div class="d_banner">
            <div class="container">
                <div class="row">
                    <div class="span3"></div>
                    <div class="span9">
                        <span class="bannertextone">There's a better way<br />
                            to find the perfect college. </span>
                    </div>
                    <div class="span7"></div>
                    <div class="span5">
                        <a class="bannerbtnlarge" href="Account/Register.aspx">Create Your Free Profile</a>
                    </div>
                </div>
            </div>
        </div>


        <div class="d_content">
            <div class="container">
                <div class="row">
                    <ul>
                        <li class="span3">
                            <div class="d_transperntbox">
                                <h1>See Which Colleges Want You</h1>
                                <img src="images/d_img1.gif" alt="" />
                                <p>Colleges on Cappex are specifically looking for students like you. So when you get a college message on Cappex you know a college is interested in you.</p>
                                <a href="#">More</a>
                            </div>
                        </li>
                        <li class="span3">
                            <div class="d_transperntbox">
                                <h1>Apply For Scholarships</h1>
                                <img src="images/d_img2.gif" alt="" />
                                <p>Once you create your profile, applying for Cappex scholarships is as easy as one click. No essays. No hassle.</p>
                                <a href="#">More</a>
                            </div>
                        </li>
                        <li class="span3">
                            <div class="d_transperntbox">
                                <h1>Find Other Students</h1>
                                <img src="images/d_img3.gif" alt="" />
                                <p>Find other students from you city and country going to the same university. Collaborate with them on matters of travel, group discounts, housing etc</p>
                                <a href="#">More</a>
                            </div>
                        </li>
                        <li class="span3">
                            <div class="d_transperntbox">
                                <h1>SpotCollege for Colleges</h1>
                                <img src="images/d_img4.gif" alt="" />
                                <p>Learn how your college can find students on SpotCollege</p>
                                <asp:LinkButton ID="lnkBtnunviercity" runat="server" OnClick="lnkUnviercity_Click">More</asp:LinkButton>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="d_footer">
            <div class="container">
                <div class="row">
                    <div class="span9">
                        <ul class="d_footer_toplink">
                           
                            <li><a href="Default.aspx">Home</a></li>
                            <li><a href="#">About Us</a></li>
                            <li><a href="#">Privacy Policy</a></li>
                            <li><a href="/TermToUse.aspx">Terms of Use</a></li>
                            <li><a href="#">Careers</a></li>
                        </ul>
                    </div>
                    <div class="span3"><span class="d_footer_righttext">© 2013, Spot College.com, LLC</span></div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
