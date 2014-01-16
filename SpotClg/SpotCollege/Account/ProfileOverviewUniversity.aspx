<%@ Page Title="University Profile OverView" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProfileOverviewUniversity.aspx.cs" Inherits="SpotCollege.Account.ProfileOverviewUniversity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row-fluid">
        <div class="span12">
            <div class="pattern_box box_space">
                <div class="row-fluid">
                    <div class="span6">
                        <div id="BasicOverview" runat="server">
                            <div class="shadowbox">
                                <div class="h2_heading">
                                    <div class="accordion-header">
                                        <h2>Basic Profile Detail</h2>
                                    </div>
                                    <asp:LinkButton ID="lnkEditBasicInfo" runat="server" OnClick="lnkEditBasicInfo_Click" Text="Edit" class="edit fright"></asp:LinkButton>
                                </div>
                                <div class="accordion-content">
                                    <ul class="list_4">
                                        <li>
                                            <label>University Name :</label>
                                            <span>
                                                <asp:Label ID="lblUniversityName" runat="server"></asp:Label>
                                            </span>
                                        </li>
                                        <li>
                                            <label>University Address :</label>
                                            <span>
                                                <asp:Label ID="lblUniversityAddress" runat="server"></asp:Label>
                                            </span>
                                        </li>
                                        <li>
                                            <label>Administrator Email Id :</label>
                                            <span>
                                                <asp:Label ID="lblAdministratorEmail" runat="server"></asp:Label>
                                            </span>
                                        </li>
                                        <li>
                                            <label>Administrator Name :</label>
                                            <span>
                                                <asp:Label ID="lblAdminName" runat="server"></asp:Label>
                                            </span>
                                        </li>

                                        <li>
                                            <label>City :</label>
                                            <span>
                                                <asp:Label ID="lblCity" runat="server"></asp:Label>
                                            </span>
                                        </li>

                                        <li>
                                            <label>Country :</label>
                                            <span>
                                                <asp:Label ID="lblCountry" runat="server"></asp:Label>
                                            </span>
                                        </li>

                                        <li>
                                            <label>Established Year :</label>
                                            <span>
                                                <asp:Label ID="lblEstablishedYear" runat="server"></asp:Label>
                                            </span>
                                        </li>

                                        <li>
                                            <label>Description :</label>
                                            <span>
                                                <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                            </span>
                                        </li>

                                        <li>
                                            <label>Logo :</label>
                                            <span>
                                                <img id="imgProfile" runat="server" src="" />
                                            </span>
                                        </li>

                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div id="ProgramsAndMajors" runat="server">
                            <div class="shadowbox">
                                <div class="h2_heading">
                                    <div class="accordion-header">
                                        <h2>Programs and majors</h2>
                                    </div>
                                    <asp:LinkButton ID="lnkProgramAndMajorsEdit" runat="server" OnClick="lnkProgramAndMajorsEdit_Click" Text="Edit" class="edit fright"></asp:LinkButton>
                                </div>

                                <div class="accordion-content">
                                    <ul class="list_4">
                                        <li>
                                            <label>Programs Offered :</label>
                                            <asp:Label ID="lblProgramOffered" runat="server"></asp:Label>
                                        </li>
                                        <li>
                                            <label>Courses Offered :</label>
                                            <asp:Label ID="lblCoursesOffered" runat="server"></asp:Label>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="span6">
                        <div id="CostForInternationalStudent" runat="server">
                            <div class="shadowbox">
                                <div class="h2_heading">
                                    <div class="accordion-header">
                                        <h2>Cost for International students</h2>
                                    </div>
                                    <asp:LinkButton ID="lnkCostEdit" runat="server" OnClick="lnkCostEdit_Click" Text="Edit" class="edit fright"></asp:LinkButton>
                                </div>
                                <div class="accordion-content">
                                    <ul class="list_4">
                                        <li>
                                            <label>Tution for Undergraduate Students : </label>
                                            <span>
                                                <asp:Label ID="lblFeesForUnderGraduateStudents" runat="server"></asp:Label>
                                            </span>
                                        </li>
                                        <li>
                                            <label>Tution for Graduate Students :</label>
                                            <span>
                                                <asp:Label ID="lblFeesForGraduateStudents" runat="server"></asp:Label>
                                            </span>
                                        </li>
                                        <li>
                                            <label>Books and supplies :</label>
                                            <span>
                                                <asp:Label ID="lblBookFees" runat="server"></asp:Label>
                                            </span>
                                        </li>
                                        <li>
                                            <label>Off-campus room and Board :</label>
                                            <span>
                                                <asp:Label ID="lblBoardFees" runat="server"></asp:Label>
                                            </span>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div id="EnrollmentNumber" runat="server">
                            <div class="shadowbox">
                                <div class="h2_heading">
                                    <div class="accordion-header">
                                        <h2>Enrollment Number</h2>
                                    </div>
                                    <asp:LinkButton ID="lnkEnrollmentEdit" runat="server" OnClick="lnkEnrollmentEdit_Click" Text="Edit" class="edit fright"></asp:LinkButton>
                                </div>
                                <div class="accordion-content">
                                    <ul class="list_4">
                                        <li>
                                            <label>Number of Graduate students:</label>
                                            <span>
                                                <asp:Label ID="lblNoofGraduateStudent" runat="server"></asp:Label>
                                            </span>
                                        </li>
                                        <li>
                                            <label>Number of Undergraduate Students :</label>
                                            <span>
                                                <asp:Label ID="lblNoofUnderGraduateStudent" runat="server"></asp:Label>
                                            </span>
                                        </li>
                                        <li>
                                            <label>Number of International students :</label>
                                            <span>
                                                <asp:Label ID="lblNoofInternationalStudent" runat="server"></asp:Label>
                                            </span>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#nav li").each(function () {
                $(this).removeClass('navi_active');
            });

            $("#Profile").addClass('navi_active');
        });

    </script>
</asp:Content>
