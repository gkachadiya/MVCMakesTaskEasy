<%@ Page Title="Add New University" Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.Master" ValidateRequest="false" CodeBehind="AddNewUniversity.aspx.cs" Inherits="SpotCollege.Admin.AddNewUniversity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="pattern_box">
        <div class="row-fluid">
            <div class="span12">
                <asp:HiddenField ID="hndUniversityId" Value="0" runat="server" />
                <asp:HiddenField ID="hndCurrentTab" Value="1" runat="server" />
                <asp:HiddenField ID="hndUserName" Value="" runat="server" />


                <div class="span3 first">
                    <div class="shadowbox">
                        <div class="h2_heading">
                            <h2>Core Profile</h2>
                        </div>
                        <div class="paddingbox">
                            <asp:Panel ID="pnlHeaderLinks" runat="server">
                                <ul class="list_1" id="tabNavigation">
                                    <li class="list1_active" id="BasicInformation" runat="server">
                                        <asp:LinkButton ID="lnkBasicInformation" Text="The Basics" runat="server" OnClick="lnkBasicInformation_Click"></asp:LinkButton>
                                    </li>
                                    <li id="CostForInternationalStudents" runat="server">
                                        <asp:LinkButton ID="lnkCostForInternationalStudents" Text="Cost for International Student" OnClick="lnkCostForInternationalStudents_Click"
                                            runat="server"></asp:LinkButton>
                                    </li>
                                    <li id="EnrollmentNumbers" runat="server">
                                        <asp:LinkButton ID="lnkEnrollmentNumbers" Text="Enrollment Numbers" OnClick="lnkEnrollmentNumbers_Click" runat="server"></asp:LinkButton>
                                    </li>
                                    <li id="ProgramAndMajors" runat="server">
                                        <asp:LinkButton ID="lnkProgramAndMajors" Text="Program and Majors" OnClick="lnkProgramAndMajors_Click" runat="server"></asp:LinkButton>
                                    </li>
                                </ul>
                            </asp:Panel>
                        </div>
                    </div>
                </div>

                <%--<asp:Label ID="lblEditUniversityName" runat="server" ForeColor="Green" CssClass="span9"></asp:Label>--%>

                <asp:Panel ID="pnlBasicDetail" runat="server" Visible="true" CssClass="span9">
                    <ul class="profile_form">
                        <li class="row-fluid">
                            <div class="span3">
                                <label>Email :</label>
                            </div>
                            <div class="span4">
                                <asp:TextBox runat="server" ID="txtUserName" CssClass="address" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ControlToValidate="txtUserName" CssClass="field-validation-error" ErrorMessage="Enter Administrator Email Id " ValidationGroup="EditBasicInfoUniversity" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" runat="server" CssClass="field-validation-error" ControlToValidate="txtUserName" ValidationGroup="EditBasicInfoUniversity" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid Email"></asp:RegularExpressionValidator>
                                <span id="lblDuplicateuser" style="display: none" class="field-validation-error">UserName already exist in Record.</span>
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span3">
                                <label>Password :</label>
                            </div>
                            <div class="span4">
                                <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="address" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" CssClass="field-validation-error" ErrorMessage="Enter Administrator Password" ValidationGroup="UniversityRegistrationValidation" />
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span3">
                                <label>
                                    Confirm Password :</label>
                            </div>
                            <div class="span4">
                                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="address"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtConfirmPassword" CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The confirm password field is required." ValidationGroup="UniversityRegistrationValidation" />
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." ValidationGroup="UniversityRegistrationValidation" />
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span3">
                                <label>University Name :</label>
                            </div>
                            <div class="span4">
                                <asp:TextBox ID="txtUniversityName" runat="server" CssClass="address"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="Dynamic" runat="server" CssClass="field-validation-error" ErrorMessage="Enter University Name" ControlToValidate="txtUniversityName" ValidationGroup="EditBasicInfoUniversity"></asp:RequiredFieldValidator>
                            </div>
                        </li>

                        <li class="row-fluid">
                            <div class="span3">
                                <label>University Address :</label>

                            </div>
                            <div class="span4">
                                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="address"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" Display="Dynamic" runat="server" CssClass="field-validation-error" ErrorMessage="Enter University Address" ControlToValidate="txtAddress" ValidationGroup="EditBasicInfoUniversity"></asp:RequiredFieldValidator>
                            </div>
                        </li>



                        <li class="row-fluid">
                            <div class="span3">
                                <label>Administrator Name :</label>

                            </div>
                            <div class="span4">
                                <asp:TextBox runat="server" ID="txtAdministratorName" CssClass="address" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ControlToValidate="txtAdministratorName" CssClass="field-validation-error" ErrorMessage="Enter Administrator Name" ValidationGroup="EditBasicInfoUniversity" />
                            </div>
                        </li>

                        <li class="row-fluid">
                            <div class="span3">
                                <label>City :</label>
                            </div>
                            <div class="span4">
                                <asp:TextBox ID="txtCity" runat="server" CssClass="address"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" CssClass="field-validation-error" runat="server" ErrorMessage="Enter City Name" ControlToValidate="txtCity" ValidationGroup="EditBasicInfoUniversity"></asp:RequiredFieldValidator>
                            </div>
                        </li>

                        <li class="row-fluid">
                            <div class="span3">
                                <label>Country :</label>
                            </div>
                            <div class="span4">
                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="ddladdress"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" CssClass="field-validation-error" runat="server" ErrorMessage="Select Any Country" InitialValue="0" ControlToValidate="ddlCountry" ValidationGroup="EditBasicInfoUniversity"></asp:RequiredFieldValidator>
                            </div>
                        </li>

                        <li class="row-fluid">
                            <div class="span3">
                                <label>Established Year :</label>
                            </div>
                            <div class="span4">
                                <asp:TextBox ID="txtEstablishedYear" runat="server" MaxLength="4"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" runat="server" ValidationExpression="\d{0,10}" CssClass="field-validation-error" ErrorMessage="Invalid Year" ControlToValidate="txtEstablishedYear" ValidationGroup="EditBasicInfoUniversity"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" Display="Dynamic" CssClass="field-validation-error" runat="server" ErrorMessage="Please Enter Established Year" ControlToValidate="txtEstablishedYear" ValidationGroup="EditBasicInfoUniversity"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span3">
                                <label>Country Code :</label>
                            </div>
                            <div class="span4">
                                <asp:DropDownList ID="ddlCountryCode" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator96" InitialValue="0" Display="Dynamic" runat="server" CssClass="field-validation-error"
                                    ControlToValidate="ddlCountryCode" ErrorMessage="Country Code is Required"
                                    ValidationGroup="RegistrationValidation"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span3">
                                <label>Contact No. :</label>
                            </div>
                            <div class="span4">
                                <asp:TextBox ID="txtcontactno" runat="server" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ControlToValidate="txtcontactno" CssClass="field-validation-error" ErrorMessage="The Contact No field is required." ValidationGroup="RegistrationValidation" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" Display="Dynamic" CssClass="field-validation-error"
                                    ValidationExpression="\d{6,10}" runat="server" ControlToValidate="txtcontactno"
                                    ErrorMessage="Invalid Number" ValidationGroup="RegistrationValidation"></asp:RegularExpressionValidator>

                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span3">
                                <label>Description :</label>
                            </div>
                            <div class="span9">
                                <script type="text/javascript" src="http://js.nicedit.com/nicEdit-latest.js"></script>
                                <script type="text/javascript">
                                    //<![CDATA[
                                    bkLib.onDomLoaded(function () {
                                        nicEditors.editors.push(
            new nicEditor().panelInstance(
                document.getElementById('MainContent_txtDescription')
            )
        );
                                    });
                                    //]]>
                                </script>

                                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="address" Width="100%" Height="250px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" Display="Dynamic" CssClass="field-validation-error" runat="server" ErrorMessage="Enter Description" ControlToValidate="txtDescription" ValidationGroup="EditBasicInfoUniversity"></asp:RequiredFieldValidator>
                            </div>
                        </li>

                        <li class="row-fluid">
                            <div class="span3">
                                <label>Logo :</label>
                            </div>
                            <div class="span4">
                                <asp:FileUpload ID="fluploadImage" runat="server" CssClass="address" />
                                <asp:Label ID="lblImageValidation" Visible="false" CssClass="field-validation-error" runat="server" Text="Upload Image">Upload Image</asp:Label>
                                <img id="imgUniversityLogo" runat="server" src="" height="40" width="40" />
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span3">
                                <label>University Image :</label>
                            </div>
                            <div class="span4">
                                <asp:FileUpload ID="fluploadUniversityImage" runat="server" CssClass="address" />
                                <asp:Label ID="lblLargeImageValidation" Visible="false" CssClass="field-validation-error" runat="server" Text="Upload Image">Upload Image</asp:Label>
                                <img id="imgUniversityImage" runat="server" src="" height="40" width="40" />
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span3"></div>
                            <div class="span6">
                                <asp:Button ID="btnBasicSave" runat="server" ValidationGroup="EditBasicInfoUniversity" class="large_button" Text="Save and Continue" OnClick="btnBasicSave_Click" />
                            </div>
                        </li>
                    </ul>
                </asp:Panel>

                <%--<div class="span8">
                    <asp:Panel ID="pnlCostForInternationalStudents" runat="server" Visible="false">
                        <ul class="profile_form">
                            <li class="row">
                                <div class="span4">
                                    <label>Tution for Undergraduate Students (USD):</label>
                                </div>
                                <div class="span2">
                                    <asp:TextBox ID="txtUnderGraduateFee" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" Display="Dynamic" CssClass="field-validation-error" runat="server" ErrorMessage="Enter Under Graduate Fee" ControlToValidate="txtUnderGraduateFee" ValidationGroup="EditCostForInternationalStudent"></asp:RequiredFieldValidator>
                                </div>
                                <div class="span2">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" runat="server" ValidationExpression="\d{0,15}.\d{0,2}" CssClass="field-validation-error" ErrorMessage="Invalid Under Graduate Fee" ControlToValidate="txtUnderGraduateFee" ValidationGroup="EditCostForInternationalStudent"></asp:RegularExpressionValidator>
                                    <asp:DropDownList ID="ddlunitForUGS" runat="server">
                                        <asp:ListItem Value="Per Year">Per Year</asp:ListItem>
                                        <asp:ListItem Value="Per Unit">Per Unit</asp:ListItem>
                                        <asp:ListItem Value="Per Semester">Per Semester</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </li>

                            <li class="row">
                                <div class="span4">
                                    <label>Tution for Graduate Students (USD):</label>

                                </div>
                                <div class="span2">
                                    <asp:TextBox ID="txtGraduateFee" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" Display="Dynamic" runat="server" CssClass="field-validation-error" ErrorMessage="Enter Graduate Fee" ControlToValidate="txtGraduateFee" ValidationGroup="EditCostForInternationalStudent"></asp:RequiredFieldValidator>
                                </div>
                                <div class="span2">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Display="Dynamic" runat="server" ValidationExpression="\d{0,15}.\d{0,2}" CssClass="field-validation-error" ErrorMessage="Invalid Graduate Fee" ControlToValidate="txtGraduateFee" ValidationGroup="EditCostForInternationalStudent"></asp:RegularExpressionValidator>
                                    <asp:DropDownList ID="ddlunitForGS" runat="server">
                                        <asp:ListItem Value="Per Year">Per Year</asp:ListItem>
                                        <asp:ListItem Value="Per Unit">Per Unit</asp:ListItem>
                                        <asp:ListItem Value="Per Semester">Per Semester</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </li>

                            <li class="row">
                                <div class="span4">
                                    <label>Books and supplies (USD):</label>

                                </div>
                                <div class="span2">
                                    <asp:TextBox ID="txtBookFee" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" Display="Dynamic" runat="server" CssClass="field-validation-error" ErrorMessage="Enter Book Fee" ControlToValidate="txtBookFee" ValidationGroup="EditCostForInternationalStudent"></asp:RequiredFieldValidator>
                                </div>
                                <div class="span2">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Display="Dynamic" runat="server" ValidationExpression="\d{0,15}.\d{0,2}" CssClass="field-validation-error" ErrorMessage="Invalid Book Fee" ControlToValidate="txtBookFee" ValidationGroup="EditCostForInternationalStudent"></asp:RegularExpressionValidator>
                                    <asp:DropDownList ID="ddlunitForBook" runat="server">
                                        <asp:ListItem Value="Per Year">Per Year</asp:ListItem>
                                        <asp:ListItem Value="Per Unit">Per Unit</asp:ListItem>
                                        <asp:ListItem Value="Per Semester">Per Semester</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </li>

                            <li class="row">
                                <div class="span4">
                                    <label>Off-campus room and Board (USD):</label>
                                </div>
                                <div class="span2">
                                    <asp:TextBox ID="txtBoardFee" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" Display="Dynamic" runat="server" CssClass="field-validation-error" ErrorMessage="Enter Board Fee" ControlToValidate="txtBoardFee" ValidationGroup="EditCostForInternationalStudent"></asp:RequiredFieldValidator>
                                </div>
                                <div class="span2">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" Display="Dynamic" runat="server" ValidationExpression="\d{0,15}.\d{0,2}" CssClass="field-validation-error" ErrorMessage="Invalid Board Fee" ControlToValidate="txtBoardFee" ValidationGroup="EditCostForInternationalStudent"></asp:RegularExpressionValidator>
                                    <asp:DropDownList ID="ddlunitForBoard" runat="server">
                                        <asp:ListItem Value="Per Year">Per Year</asp:ListItem>
                                        <asp:ListItem Value="Per Unit">Per Unit</asp:ListItem>
                                        <asp:ListItem Value="Per Semester">Per Semester</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </li>
                            <div class="row">
                                <div class="span4"></div>
                                <div class="span4">
                                    <asp:Button ID="btnCostForInternationalStudentSave" runat="server" ValidationGroup="EditCostForInternationalStudent" class="large_button" Text="Save and Continue" OnClick="btnCostForInternationalStudentSave_Click" />
                                </div>
                            </div>
                        </ul>
                    </asp:Panel>
                </div>--%>

                <asp:Panel ID="pnlCostForInternationalStudents" runat="server" Visible="false" CssClass="span9">
                    <ul class="profile_form">
                        <li class="row-fluid">
                            <div class="span5">
                                <label>Tution for Undergraduate Students :</label>
                            </div>
                            <div class="span2">
                                <asp:DropDownList ID="ddlCurrencyForUGS" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="span2">
                                <asp:TextBox ID="txtUnderGraduateFee" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" runat="server" ValidationExpression="[\d]{1,8}-[\d]{1,8}" CssClass="field-validation-error" ErrorMessage="Invalid Under Graduate Fee" ControlToValidate="txtUnderGraduateFee" ValidationGroup="EditCostForInternationalStudent"></asp:RegularExpressionValidator>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator12" Display="Dynamic" CssClass="field-validation-error" runat="server" ErrorMessage="Enter Under Graduate Fee" ControlToValidate="txtUnderGraduateFee" ValidationGroup="EditCostForInternationalStudent"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="span2">
                                <asp:DropDownList ID="ddlunitForUGS" runat="server">
                                    <asp:ListItem Value="Per Year">Per Year</asp:ListItem>
                                    <asp:ListItem Value="Per Unit">Per Unit</asp:ListItem>
                                    <asp:ListItem Value="Per Semester">Per Semester</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </li>

                        <li class="row-fluid">
                            <div class="span5">
                                <label>Tution for Graduate Students :</label>
                            </div>
                            <div class="span2">
                                <asp:DropDownList ID="ddlCurrencyForGS" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="span2">
                                <asp:TextBox ID="txtGraduateFee" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Display="Dynamic" runat="server" ValidationExpression="[\d]{1,8}-[\d]{1,8}" CssClass="field-validation-error" ErrorMessage="Invalid Graduate Fee" ControlToValidate="txtGraduateFee" ValidationGroup="EditCostForInternationalStudent"></asp:RegularExpressionValidator>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator13" Display="Dynamic" runat="server" CssClass="field-validation-error" ErrorMessage="Enter Graduate Fee" ControlToValidate="txtGraduateFee" ValidationGroup="EditCostForInternationalStudent"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="span2">
                                <asp:DropDownList ID="ddlunitForGS" runat="server">
                                    <asp:ListItem Value="Per Year">Per Year</asp:ListItem>
                                    <asp:ListItem Value="Per Unit">Per Unit</asp:ListItem>
                                    <asp:ListItem Value="Per Semester">Per Semester</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </li>

                        <li class="row-fluid">
                            <div class="span5">
                                <label>Books and supplies :</label>

                            </div>
                            <div class="span2">
                                <asp:DropDownList ID="ddlCurrencyForBS" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="span2">
                                <asp:TextBox ID="txtBookFee" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Display="Dynamic" runat="server" ValidationExpression="[\d]{1,8}-[\d]{1,8}" CssClass="field-validation-error" ErrorMessage="Invalid Book Fee" ControlToValidate="txtBookFee" ValidationGroup="EditCostForInternationalStudent"></asp:RegularExpressionValidator>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator14" Display="Dynamic" runat="server" CssClass="field-validation-error" ErrorMessage="Enter Book Fee" ControlToValidate="txtBookFee" ValidationGroup="EditCostForInternationalStudent"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="span2">
                                <asp:DropDownList ID="ddlunitForBook" runat="server">
                                    <asp:ListItem Value="Per Year">Per Year</asp:ListItem>
                                    <asp:ListItem Value="Per Unit">Per Unit</asp:ListItem>
                                    <asp:ListItem Value="Per Semester">Per Semester</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </li>

                        <li class="row-fluid">
                            <div class="span5">
                                <label>Off-campus room and Board :</label>
                            </div>
                            <div class="span2">
                                <asp:DropDownList ID="ddlCurrnecyForOCR" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="span2">
                                <asp:TextBox ID="txtBoardFee" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" Display="Dynamic" runat="server" ValidationExpression="[\d]{1,8}-[\d]{1,8}" CssClass="field-validation-error" ErrorMessage="Invalid Board Fee" ControlToValidate="txtBoardFee" ValidationGroup="EditCostForInternationalStudent"></asp:RegularExpressionValidator>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator15" Display="Dynamic" runat="server" CssClass="field-validation-error" ErrorMessage="Enter Board Fee" ControlToValidate="txtBoardFee" ValidationGroup="EditCostForInternationalStudent"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="span2">
                                <asp:DropDownList ID="ddlunitForBoard" runat="server">
                                    <asp:ListItem Value="Per Year">Per Year</asp:ListItem>
                                    <asp:ListItem Value="Per Unit">Per Unit</asp:ListItem>
                                    <asp:ListItem Value="Per Semester">Per Semester</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span5"></div>
                            <div class="span6">
                                <asp:Button ID="btnCostForInternationalStudentSave" runat="server" ValidationGroup="EditCostForInternationalStudent" class="large_button" Text="Save and Continue" OnClick="btnCostForInternationalStudentSave_Click" />
                            </div>
                        </li>
                    </ul>
                </asp:Panel>

                <asp:Panel ID="pnlEnrollmentNumber" runat="server" Visible="false" CssClass="span9">
                    <ul class="profile_form">
                        <li class="row-fluid">
                            <div class="span5">
                                <label>Number of Graduate students  :</label>
                            </div>
                            <div class="span3">
                                <asp:TextBox ID="txtNoOfGraduates" runat="server"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" CssClass="field-validation-error" ErrorMessage="Enter No of Graduates" ControlToValidate="txtNoOfGraduates" ValidationGroup="EditEnrollMentNumberValidation"></asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" Display="Dynamic" runat="server" ValidationExpression="\d{0,15}" CssClass="field-validation-error" ErrorMessage="Invalid No of Graduates" ControlToValidate="txtNoOfGraduates" ValidationGroup="EditEnrollMentNumberValidation"></asp:RegularExpressionValidator>
                            </div>
                        </li>

                        <li class="row-fluid">
                            <div class="span5">
                                <label>Number of Undergraduate Students :</label>

                            </div>
                            <div class="span3">
                                <asp:TextBox ID="txtNoOfUnderGraduates" runat="server"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator17" Display="Dynamic" runat="server" CssClass="field-validation-error" ErrorMessage="Enter No of Under Graduates" ControlToValidate="txtNoOfUnderGraduates" ValidationGroup="EditEnrollMentNumberValidation"></asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" Display="Dynamic" runat="server" ValidationExpression="\d{0,15}" CssClass="field-validation-error" ErrorMessage="Invalid No of UnderGraduates" ControlToValidate="txtNoOfUnderGraduates" ValidationGroup="EditEnrollMentNumberValidation"></asp:RegularExpressionValidator>
                            </div>
                        </li>

                        <li class="row-fluid">
                            <div class="span5">
                                <label>Number of International students :</label>

                            </div>
                            <div class="span3">
                                <asp:TextBox ID="txtNoOfInterNationalGraduates" runat="server"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator18" Display="Dynamic" runat="server" CssClass="field-validation-error" ErrorMessage="Enter No of InterNational Graduates" ControlToValidate="txtNoOfInterNationalGraduates" ValidationGroup="EditEnrollMentNumberValidation"></asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" Display="Dynamic" runat="server" ValidationExpression="\d{0,15}" CssClass="field-validation-error" ErrorMessage="Invalid No of InterNational Graduates" ControlToValidate="txtNoOfInterNationalGraduates" ValidationGroup="EditEnrollMentNumberValidation"></asp:RegularExpressionValidator>
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span5"></div>
                            <div class="span6">
                                <asp:Button ID="btnSaveEnrollNumber" runat="server" ValidationGroup="EditEnrollMentNumberValidation" class="large_button" Text="Save and Continue" OnClick="btnSaveEnrollNumber_Click" />
                            </div>
                        </li>
                    </ul>
                </asp:Panel>

                <asp:Panel ID="pnlProgramsAndMajors" runat="server" Visible="false" CssClass="span9">
                    <ul class="list_5">
                        <li>
                            <h4>Degree Level Offred :</h4>
                            <asp:CheckBoxList ID="chkDegreeOffredList" CssClass="checkbox_list" runat="server" RepeatLayout="UnorderedList"></asp:CheckBoxList>
                        </li>
                        <li>
                            <h4>Courses Offred :</h4>
                            <asp:CheckBoxList ID="chkCoursesOfferedList" CssClass="checkbox_list" runat="server" RepeatLayout="UnorderedList"></asp:CheckBoxList>
                        </li>
                    </ul>
                    <div class="span4 first">
                        <asp:Button ID="btnSave" runat="server" class="large_button" Text="Save" OnClick="btnSave_Click" />
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            var currTab = $("#MainContent_hndCurrentTab").val();
            $("#tabNavigation li").each(function () {
                $(this).removeClass("list1_active");
            });
            if (currTab == "1") {
                $("#MainContent_BasicInformation").addClass("list1_active");
            }
            else if (currTab == "2") {
                $("#MainContent_CostForInternationalStudents").addClass("list1_active");
            }
            else if (currTab == "3") {
                $("#MainContent_EnrollmentNumbers").addClass("list1_active");
            }
            else if (currTab == "4") {
                $("#MainContent_ProgramAndMajors").addClass("list1_active");
            }
            else {
                $("#MainContent_BasicInformation").addClass("list1_active");
            }
            $("#MainContent_hndCurrentTab").change(function () {

            });

            $("#nav li").each(function () {
                $(this).removeClass('navi_active');
            });

            $("#Profile").addClass('navi_active');
        });

        //Check Duplicate Username
        $("#MainContent_txtUserName").change(function () {
            // alert("kk");
            if ($("#MainContent_txtUserName").val() != "") {
                // if (Page_IsValid) {
                var University = {
                    UserName: $('#MainContent_txtUserName').val(),
                };

                $.ajax({
                    type: "POST",
                    url: "AddNewUniversity.aspx/CheckDuplicateUsername",
                    data: JSON.stringify(University),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d != 'true') {
                            $('#lblDuplicateuser').hide();
                        }
                        else {
                            $('#lblDuplicateuser').show();
                        }
                    }
                });
                // }
            }
        });
    </script>
</asp:Content>
