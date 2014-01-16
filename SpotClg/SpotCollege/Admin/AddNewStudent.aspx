<%@ Page Title="Add New Student" MasterPageFile="~/Admin/Admin.Master" Language="C#" AutoEventWireup="true" CodeBehind="AddNewStudent.aspx.cs" Inherits="SpotCollege.Admin.AddNewStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="pattern_box">
        <div class="row-fluid">
            <div class="span12">
                <asp:Label ID="lbltitle" runat="server" Style="display: none"></asp:Label>
                <div id="LoadingImage" style="display: none">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/ajaxloader.gif" />
                </div>
                <div id="dialog-confirm" title="Are You Sure?" style="display: none;">
                    <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Are you sure?</p>
                </div>
                <div id="Dialog-Save" title="" style="display: none;">
                    <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Data Saved Sucessfully</p>
                </div>
                <script type="text/javascript">
                    $(document).ready(function () {
                        $("#Dialog-Save").attr('title', '');
                    });
                </script>

                <asp:HiddenField ID="hndStudentId" Value="0" runat="server" />
                <asp:HiddenField ID="hndCurrentTab" Value="1" runat="server" />
                <asp:HiddenField ID="hndUserName" Value="" runat="server" />
                <asp:HiddenField ID="hndNewStudentId" Value="0" runat="server" />



                <%--For Tab--%>
                <div class="span3 first">
                    <div class="shadowbox">
                        <div class="h2_heading">
                            <h2>Core Profile</h2>
                        </div>
                        <div class="paddingbox">
                            <asp:Panel ID="pnlHeaderLinks" runat="server">
                                <ul class="list_1" id="tabNavigation">
                                    <li class="list1_active" id="BasicInformation" runat="server">
                                        <asp:LinkButton ID="lnkBasicInformation" Text="The Basics" OnClick="lnkBasicInformation_Click"
                                            runat="server"></asp:LinkButton>
                                    </li>
                                    <li runat="server" id="lblcurrentacademic">
                                        <asp:LinkButton ID="lnkCurrentAcademics" Text="Current Academics" OnClick="lnkCurrentAcademics_Click"
                                            runat="server"></asp:LinkButton>
                                    </li>
                                    <li runat="server" id="lblInternationalTest">
                                        <asp:LinkButton ID="lnkInternationalTest" Text="International Test" OnClick="lnkInternationalTest_Click"
                                            runat="server"></asp:LinkButton>
                                    </li>
                                    <li runat="server" id="EducationalPreferences">
                                        <asp:LinkButton ID="lnkEducationalPreferences" Text="Education Preferences" OnClick="lnkEducationalPreferences_Click"
                                            runat="server"></asp:LinkButton>
                                    </li>
                                    <li runat="server" id="Photo">
                                        <asp:LinkButton ID="lnkPhoto" Text="Photo" OnClick="lnkPhoto_Click" runat="server"></asp:LinkButton>
                                    </li>
                                    <li id="lblworkhistory" runat="server">
                                        <asp:LinkButton ID="lnkWorkhistory" Text="Work history" OnClick="lnkWorkhistory_Click"
                                            runat="server"></asp:LinkButton>
                                    </li>
                                    <li runat="server" id="ExtraCurricularActivities">
                                        <asp:LinkButton ID="lnkExtraCurricularActivities" Text="Extra Curricular Activies"
                                            OnClick="lnkExtraCurricularActivities_Click" runat="server"></asp:LinkButton>
                                    </li>
                                </ul>
                            </asp:Panel>
                        </div>
                    </div>
                </div>

                <%--<div class="span9">
                    <asp:Label ID="lblEditStudentName" runat="server" ForeColor="Green"></asp:Label>
                </div>--%>

                <%--For Basic Detail--%>
                <asp:Panel ID="pnlBasicDetail" runat="server" Visible="false" CssClass="span9">
                    <ul class="profile_form">
                        <li class="row-fluid">
                            <div class="span3">
                                <label>
                                    Email :</label>
                            </div>
                            <div class="span3">
                                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail"
                                    CssClass="field-validation-error" ErrorMessage="The Email field is required." ValidationGroup="EditProfileValidation" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator51" runat="server" CssClass="field-validation-error" ControlToValidate="txtEmail" ValidationGroup="EditProfileValidation" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid Email"></asp:RegularExpressionValidator>
                                <span id="lblDuplicateuser" style="display: none" class="field-validation-error">UserName already exist in Record.</span>

                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span3">
                                <label>
                                    Password :</label>
                            </div>
                            <div class="span3">
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator96" runat="server" ControlToValidate="txtPassword"
                                    CssClass="field-validation-error" ErrorMessage="The Password field is required." ValidationGroup="EditProfileValidation" />
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span3">
                                <label>
                                    Confirm Password :</label>
                            </div>
                            <div class="span3">
                                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator97" runat="server" ControlToValidate="txtConfirmPassword" CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The confirm password field is required." ValidationGroup="EditProfileValidation" />
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." ValidationGroup="EditProfileValidation" />
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span3">
                                <label>Name :</label>
                            </div>
                            <div class="span3">
                                <asp:TextBox ID="txtFirstName" runat="server" placeholder="First Name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" CssClass="field-validation-error"
                                    ControlToValidate="txtFirstName" ErrorMessage="First Name is Required" ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                            </div>
                            <div class="span3">
                                <asp:TextBox ID="txtMiddleName" runat="server" placeholder="Middle Name"></asp:TextBox>
                            </div>
                            <div class="span3">
                                <asp:TextBox ID="txtLastName" runat="server" placeholder="Last Name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" CssClass="field-validation-error"
                                    ControlToValidate="txtLastName" Display="Dynamic" ErrorMessage="Last Name is Required" ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span3">
                                <label>Address 1 :</label>
                            </div>
                            <div class="span6">
                                <asp:TextBox ID="txtAddress1" runat="server" CssClass="address"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" CssClass="field-validation-error"
                                    ControlToValidate="txtAddress1" ErrorMessage="Address1 is Required" ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                            </div>
                        </li>

                        <li class="row-fluid">
                            <div class="span3">
                                <label>Address 2 :</label>
                            </div>
                            <div class="span6">
                                <asp:TextBox ID="txtAddress2" runat="server" CssClass="address"></asp:TextBox>
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span3">
                                <label>Zip Code :</label>
                            </div>
                            <div class="span3">
                                <asp:TextBox ID="txtzipcode" runat="server" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" Display="Dynamic" runat="server" CssClass="field-validation-error"
                                    ControlToValidate="txtzipcode" ErrorMessage="Zip Code is Required"
                                    ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" CssClass="field-validation-error"
                                    ValidationExpression="^[a-zA-Z0-9]*$" runat="server" ControlToValidate="txtzipcode"
                                    ErrorMessage="Invalid Number" ValidationGroup="EditProfileValidation"></asp:RegularExpressionValidator>
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span3">
                                <label>
                                    Country :</label>
                            </div>
                            <div class="span3">
                                <asp:DropDownList ID="ddlCountry" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlCountry"
                                    InitialValue="0" CssClass="field-validation-error" ErrorMessage="Plsase Select Country"
                                    ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                            </div>

                        </li>
                        <li class="row-fluid">
                            <div class="span3">
                                <label>City:</label>
                            </div>
                            <div class="span3">
                                <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" CssClass="field-validation-error"
                                    ControlToValidate="txtCity" ErrorMessage="City is Required" ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span3">
                                <label>
                                    Primary Phone :</label>
                            </div>
                            <div class="span2">
                                <asp:DropDownList ID="ddlPhoneType" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" Display="Dynamic" runat="server" ControlToValidate="ddlPhoneType"
                                    InitialValue="0" CssClass="field-validation-error" ErrorMessage="Plsase Select Phone Type"
                                    ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>

                            </div>
                            <div class="span3">
                                <asp:DropDownList ID="ddlCountryCode" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator99" InitialValue="0" Display="Dynamic" runat="server" CssClass="field-validation-error"
                                    ControlToValidate="ddlCountryCode" ErrorMessage="Country Code is Required"
                                    ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                            </div>

                            <div class="span2">
                                <asp:TextBox ID="txtAreaCode" runat="server" placeholder="AreaCode"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator98" Display="Dynamic" runat="server" CssClass="field-validation-error"
                                    ControlToValidate="txtAreaCode" ErrorMessage="Area Code is Required"
                                    ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator52" CssClass="field-validation-error"
                                    ValidationExpression="\d{0,10}" runat="server" ControlToValidate="txtAreaCode"
                                    ErrorMessage="Invalid Number" ValidationGroup="EditProfileValidation"></asp:RegularExpressionValidator>
                            </div>

                            <div class="span2">
                                <asp:TextBox ID="txtPrimaryNumber" runat="server" placeholder="Mobile Number"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" runat="server" CssClass="field-validation-error"
                                    ControlToValidate="txtPrimaryNumber" ErrorMessage="Primary Number is Required"
                                    ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="field-validation-error"
                                    ValidationExpression="\d{6,10}" runat="server" ControlToValidate="txtPrimaryNumber"
                                    ErrorMessage="Invalid Number" ValidationGroup="EditProfileValidation"></asp:RegularExpressionValidator>
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span3">
                                <label>
                                    Secondry Phone :</label>
                            </div>
                            <div class="span2">
                                <asp:DropDownList ID="ddlSecondaryPhoneType" runat="server">
                                </asp:DropDownList>
                            </div>

                            <div class="span3">
                                <asp:DropDownList ID="ddlCountryCode2" runat="server">
                                </asp:DropDownList>
                            </div>

                            <div class="span2">
                                <asp:TextBox ID="txtAreaCode2" runat="server" placeholder="AreaCode"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator53" Display="Dynamic" CssClass="field-validation-error"
                                    ValidationExpression="\d{0,10}" runat="server" ControlToValidate="txtAreaCode2"
                                    ErrorMessage="Invalid Number" ValidationGroup="EditProfileValidation"></asp:RegularExpressionValidator>
                            </div>

                            <div class="span2">
                                <asp:TextBox ID="txtSecondaryNumber" runat="server" placeholder="Mobile Number"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" CssClass="field-validation-error"
                                    ValidationExpression="\d{6,10}" runat="server" ControlToValidate="txtSecondaryNumber"
                                    ErrorMessage="Invalid Number" ValidationGroup="EditProfileValidation"></asp:RegularExpressionValidator>
                            </div>



                        </li>
                        <li class="row-fluid">
                            <div class="span3">
                                <label>
                                    Email address :</label>
                            </div>
                            <div class="span3">
                                <asp:TextBox ID="txtPrimaryEmail" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" CssClass="field-validation-error"
                                    ControlToValidate="txtPrimaryEmail" ErrorMessage="Primary Email is Required"
                                    ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Display="Dynamic" runat="server" CssClass="field-validation-error"
                                    ControlToValidate="txtPrimaryEmail" ValidationGroup="EditProfileValidation" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ErrorMessage="Invalid Email"></asp:RegularExpressionValidator>
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span3">
                                <label>
                                    Birth date :</label>
                            </div>
                            <div class="span2">
                                <asp:TextBox ID="txtBirthDate" runat="server" Visible="false"></asp:TextBox>
                                <asp:DropDownList ID="ddlday" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" Display="Dynamic" runat="server" ControlToValidate="ddlday"
                                    InitialValue="0" CssClass="field-validation-error" ErrorMessage="Please Select Day"
                                    ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                            </div>
                            <div class="span2">
                                <asp:DropDownList ID="ddlmonth" runat="server">
                                    <asp:ListItem Text="Select Month" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" Display="Dynamic" runat="server" ControlToValidate="ddlmonth"
                                    InitialValue="0" CssClass="field-validation-error" ErrorMessage="Please Select Month"
                                    ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                            </div>
                            <div class="span2">
                                <asp:DropDownList ID="ddlyear" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" Display="Dynamic" runat="server" ControlToValidate="ddlyear"
                                    InitialValue="0" CssClass="field-validation-error" ErrorMessage="Please Select Year"
                                    ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                            </div>

                        </li>
                        <li class="row-fluid">
                            <div class="span3">
                                <label>
                                    Citizenship Of :</label>
                            </div>
                            <div class="span3">
                                <asp:DropDownList ID="ddlCountryOfCitizenship" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="CountryRequired" runat="server" Display="Dynamic" ControlToValidate="ddlCountryOfCitizenship"
                                    InitialValue="0" CssClass="field-validation-error" ErrorMessage="Plsase Select CitizenShip"
                                    ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <div class="row-fluid">
                            <div class="span3">
                            </div>
                            <div class="span3">
                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save & Continue"
                                    ValidationGroup="EditProfileValidation" CssClass="large_button" />
                            </div>
                        </div>
                    </ul>
                </asp:Panel>

                <%--For Academic Detail--%>
                <asp:Panel ID="pnlCurrentAcademics" Visible="false" runat="server" CssClass="span9">
                    <asp:HiddenField ID="hndStudentAcademicsId" Value="0" runat="server" />
                    <asp:Panel ID="pnlAcademicInfo" Visible="true" runat="server">
                        <asp:Label ID="lblstudentacademic" runat="server" ForeColor="Blue">To get best offers from Universities, please enter information for all schools attended starting with the most recent</asp:Label>
                        <br />
                        <asp:Label ID="lblAcademicMsg" runat="server" ForeColor="Red"></asp:Label>
                        <ul class="profile_form">
                            <li class="row-fluid">
                                <div class="span3">
                                    <label>
                                        School name :</label>
                                </div>
                                <div class="span3">
                                    <asp:TextBox ID="txtSchoolName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" Display="Dynamic" runat="server" ErrorMessage="School Name is Required"
                                        CssClass="field-validation-error" ControlToValidate="txtSchoolName" ValidationGroup="AcademicsValidation"></asp:RequiredFieldValidator>
                                </div>
                            </li>
                            <li class="row-fluid">
                                <div class="span3">
                                    <label>
                                        School City :</label>
                                </div>
                                <div class="span3">
                                    <asp:TextBox ID="txtSchoolCity" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" Display="Dynamic" runat="server" ErrorMessage="School City is Required"
                                        CssClass="field-validation-error" ControlToValidate="txtSchoolCity" ValidationGroup="AcademicsValidation"></asp:RequiredFieldValidator>
                                </div>
                            </li>
                            <li class="row-fluid">
                                <div class="span3">
                                    <label>
                                        School Country :</label>
                                </div>
                                <div class="span3">
                                    <asp:DropDownList ID="ddlSchoolCountry" runat="server">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblSchoolCountryValidation" runat="server" Text="Please Select Any"
                                        Visible="false" CssClass="field-validation-error"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" Display="Dynamic" runat="server" ControlToValidate="ddlSchoolCountry"
                                        InitialValue="0" CssClass="field-validation-error" ErrorMessage="Please Select Any"
                                        ValidationGroup="AcademicsValidation"></asp:RequiredFieldValidator>
                                </div>
                            </li>
                            <li class="row-fluid">
                                <div class="span3">
                                    <label>
                                        Did you graduate :</label>
                                </div>
                                <div class="span3">
                                    <asp:RadioButtonList ID="rdoDidYouGraduate" CssClass="list_6" runat="server" RepeatLayout="UnorderedList">
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" Display="Dynamic" runat="server" ControlToValidate="rdoDidYouGraduate"
                                        CssClass="field-validation-error" ErrorMessage="Please Select Any"
                                        ValidationGroup="AcademicsValidation"></asp:RequiredFieldValidator>
                                    <%--  <asp:Label ID="lblGraduateStatusValidation" runat="server" Text="Please Select Any"
                                            Visible="false" CssClass="field-validation-error"></asp:Label>--%>
                                </div>
                            </li>
                            <asp:Panel ID="pnlGraduateDetail" runat="server" CssClass="pnlGraduate" Style="display: none;">
                                <ul class="profile_form">
                                    <li class="row-fluid">
                                        <div class="span3">
                                            <label>
                                                Year of graudation :</label>
                                        </div>
                                        <div class="span3">
                                            <asp:DropDownList ID="ddlYearOfGraduation" runat="server">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblyeargradution" runat="server" Text="Please Select Any"
                                                Style="display: none;" CssClass="field-validation-error"></asp:Label>
                                        </div>
                                    </li>
                                    <li class="row-fluid">
                                        <div class="span3">
                                            <label>
                                                Level completed :</label>
                                        </div>
                                        <div class="span3">
                                            <asp:DropDownList ID="ddlLevelCompleted" runat="server">
                                            </asp:DropDownList>
                                            <asp:Label ID="lbllevelcompleted" runat="server" Text="Please Select Any"
                                                Style="display: none;" CssClass="field-validation-error"></asp:Label>

                                        </div>
                                    </li>
                                </ul>
                            </asp:Panel>

                            <asp:Panel ID="pnlongoing" runat="server" CssClass="pnlGraduate" Style="display: none;">
                                <ul class="profile_form">
                                    <li class="row-fluid">
                                        <div class="span3">
                                            <label>
                                                Degree being pursued :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:DropDownList ID="ddldegreepusued" runat="server">
                                            </asp:DropDownList>
                                            <asp:Label ID="lbldegreepusued" runat="server" Text="Please Select Any"
                                                Style="display: none;" CssClass="field-validation-error"></asp:Label>
                                        </div>
                                    </li>
                                    <li class="row-fluid">
                                        <div class="span3">
                                            <label>
                                                Expected year of graduation :</label>
                                        </div>
                                        <div class="span3">
                                            <asp:DropDownList ID="ddlexpectedgraduation" runat="server">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblexpectedgraduation" runat="server" Text="Please Select Any"
                                                Style="display: none;" CssClass="field-validation-error"></asp:Label>

                                        </div>
                                    </li>
                                    <li class="row-fluid">
                                        <div class="span3">
                                            <label>
                                                Field of Study :</label>
                                        </div>
                                        <div class="span3">
                                            <asp:TextBox ID="txtfieldstudy" runat="server"></asp:TextBox>
                                            <asp:Label ID="lblfieldofstudy" runat="server" Text="Please Enter Study"
                                                Style="display: none;" CssClass="field-validation-error"></asp:Label>
                                        </div>
                                    </li>
                                </ul>
                            </asp:Panel>
                            <li class="row-fluid">
                                <div class="span3">
                                    <label>
                                        GPA or Ranking in class :</label>
                                </div>
                                <div class="span3">
                                    <asp:TextBox ID="txtRankingInClass" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Display="Dynamic" CssClass="field-validation-error"
                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtRankingInClass"
                                        ErrorMessage="Invalid Ranking" ValidationGroup="AcademicsValidation"></asp:RegularExpressionValidator>
                                </div>
                            </li>
                            <li class="row-fluid">
                                <div class="span3">
                                    <label>
                                        Certificate :</label>
                                </div>
                                <div class="span3">
                                    <asp:FileUpload ID="fuCertificate" runat="server" />
                                    <%--  <asp:Label ID="lblImageValidation" runat="server" Text="Please Uploda Image" Visible="false"
                                            CssClass="field-validation-error"></asp:Label>--%>
                                    <%--<asp:RequiredFieldValidator ID="rqdcertificate" ControlToValidate="fuCertificate" runat="server" ErrorMessage="Upload Certificate" CssClass="field-validation-error" ValidationGroup="AcademicsValidation"></asp:RequiredFieldValidator>--%>
                                    <a href="" runat="server" id="hovercertificate" class="preview">
                                        <img id="imgCertificate" class="large_images" runat="server" visible="false" height="50" width="50" />
                                    </a>
                                </div>
                            </li>
                            <div class="row-fluid">
                                <div class="span3">
                                </div>
                                <div class="span5">
                                    <asp:Button ID="btnSaveAcademics" runat="server" Text="Add to List" OnClick="btnSaveAcademics_Click"
                                        ValidationGroup="AcademicsValidation" CssClass="large_button" />
                                    <asp:Button ID="btnSubmitAcademics" runat="server" Text="Next section >" OnClick="btnSubmitAcademics_Click" CssClass="large_button" />
                                </div>
                            </div>
                        </ul>

                    </asp:Panel>
                    <asp:Panel ID="pnlAcademicDetails" runat="server" Visible="true">
                        <asp:GridView ID="grvAcademicDetails" runat="server" CssClass="simple_table box" AutoGenerateColumns="false"
                            DataKeyNames="StudentAcademicsId" EmptyDataText="No Academic Record Found...!">
                            <Columns>
                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDeleteAcademic" runat="server" Text="Delete" OnClientClick="return confirmDelete();" OnClick="lnkDeleteAcademic_Click"
                                            CommandArgument='<%# Eval("StudentAcademicsId") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="School Name" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSchoolName" runat="server" Text='<%# Eval("SchoolName")%>'
                                            OnClick="lnkSchoolName_Click" CommandArgument='<%# Eval("StudentAcademicsId") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SchoolCity" HeaderText="School City" HeaderStyle-Width="10%" ItemStyle-Width="10%"></asp:BoundField>
                                <asp:BoundField DataField="SchoolCountry" HeaderText="School Country" HeaderStyle-Width="10%" ItemStyle-Width="10%"></asp:BoundField>
                                <asp:TemplateField HeaderText="Graduate" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGraduateStatus" runat="server" Text='<%# ((SpotCollege.Common.GraduateStatus)(Eval("Graduate"))).ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="GraduationYear" HeaderText="Graduation Year" HeaderStyle-Width="10%" ItemStyle-Width="10%"></asp:BoundField>
                                <asp:TemplateField HeaderText="GraduationLevel" HeaderStyle-Width="17%" ItemStyle-Width="17%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGraduateLevel" runat="server" Text='<%# Eval("GraduationLevel")==null?"":((SpotCollege.Common.LevelCompleted)(Eval("GraduationLevel"))).ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Rank" HeaderText="Rank" HeaderStyle-Width="5%" ItemStyle-Width="5%"></asp:BoundField>
                                <asp:TemplateField HeaderText="Certificate" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <a href='<%# "..\\Images\\Certificate\\"+Eval("CertificatePath") %>' runat="server" id="hovercertificate" class="preview">
                                            <img id="imgCertificate" class="large_images" src='<%# "..\\Images\\Certificate\\"+Eval("CertificatePath") %>'
                                                height="30" width="30" />
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </asp:Panel>


                <%--For Education Preference--%>
                <asp:Panel ID="pnlEducationPreferences" runat="server" Visible="false" CssClass="span9">
                    <ul class="profile_form">
                        <li class="row-fluid">
                            <div class="span4">
                                <label>
                                    I am currently :</label>
                            </div>
                            <div class="span4">
                                <asp:DropDownList ID="ddlCurrentlyIn" runat="server">
                                </asp:DropDownList>
                                <%--   <asp:Label ID="lblCurrentlyInValidation" runat="server" Text="Please Select Any"
                                        Visible="false" CssClass="field-validation-error"></asp:Label>--%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" Display="Dynamic" runat="server" ControlToValidate="ddlCurrentlyIn"
                                    InitialValue="0" CssClass="field-validation-error" ErrorMessage="Please Select Any"
                                    ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>

                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span4">
                                <label>
                                    Seeking a degree in :</label>
                            </div>
                            <div class="span4">
                                <asp:DropDownList ID="ddlLookingFor" runat="server">
                                </asp:DropDownList>
                                <%--<asp:Label ID="lblLookingForValidation" runat="server" Text="Please Select Any" Visible="false"
                                        CssClass="field-validation-error"></asp:Label>--%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" Display="Dynamic" runat="server" ControlToValidate="ddlLookingFor"
                                    InitialValue="0" CssClass="field-validation-error" ErrorMessage="Please Select Any"
                                    ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span4">
                                <label>
                                    I prefer to study in :</label>
                            </div>
                            <div class="span4">
                                <asp:DropDownList ID="ddlLookingForCountry" runat="server">
                                </asp:DropDownList>
                                <%--  <asp:Label ID="lblLookingForCountryValidation" runat="server" Text="Please Select Any"
                                        Visible="false" CssClass="field-validation-error"></asp:Label>--%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" Display="Dynamic" runat="server" ControlToValidate="ddlLookingForCountry"
                                    InitialValue="0" CssClass="field-validation-error" ErrorMessage="Please Select Any"
                                    ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <asp:Panel ID="pnlotherstudy" runat="server" CssClass="pnlGraduate" Style="display: none;">
                            <ul class="">
                                <li class="row-fluid">
                                    <div class="span4">
                                        <label>
                                            Destination :</label>
                                    </div>
                                    <div class="span4">
                                        <asp:TextBox ID="txtotherstudy" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblotherstudy" runat="server" Text="Please Enter Destination"
                                            Style="display: none;" CssClass="field-validation-error"></asp:Label>
                                    </div>
                                </li>
                            </ul>
                        </asp:Panel>
                        <li class="row-fluid">
                            <div class="span4">
                                <label>
                                    Preferred Start Date :</label>
                            </div>
                            <div class="span4">
                                <asp:DropDownList ID="ddlStartDate" runat="server">
                                </asp:DropDownList>
                                <%-- <asp:Label ID="lblStartDateValidation" runat="server" Text="Please Select Any" Visible="false"
                                        CssClass="field-validation-error"></asp:Label>--%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" Display="Dynamic" runat="server" ControlToValidate="ddlStartDate"
                                    InitialValue="0" CssClass="field-validation-error" ErrorMessage="Please Select Any"
                                    ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span4">
                                <label>
                                    I can afford to pay (per year in USD):</label>
                            </div>
                            <div class="span4">
                                <asp:DropDownList ID="ddlPayout" runat="server">
                                </asp:DropDownList>
                                <asp:Label ID="lblPayoutValidation" runat="server" Text="Please Select Any" Visible="false"
                                    CssClass="field-validation-error"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" Display="Dynamic" runat="server" ControlToValidate="ddlPayout"
                                    InitialValue="0" CssClass="field-validation-error" ErrorMessage="Please Select Any"
                                    ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span4">
                                <label>
                                    Desired Field of Study :</label>
                            </div>
                            <div class="span4">
                                 <asp:DropDownList ID="ddldesirefieldofstudy" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" Display="Dynamic" runat="server" ErrorMessage="Please Select Any"
                                            CssClass="field-validation-error" InitialValue="0" ControlToValidate="ddldesirefieldofstudy" ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                                   </div>
                        </li>

                        <div class="row-fluid">
                            <div class="span4">
                            </div>
                            <div class="span5">
                                <asp:Button ID="btnSaveEduPreference" OnClick="btnSaveEduPreference_Click" runat="server"
                                    Text="Save & Continue" ValidationGroup="EditProfileValidation" CssClass="large_button" />
                            </div>
                        </div>
                    </ul>
                </asp:Panel>


                <%--For Upload Image--%>
                <asp:Panel ID="pnlProfileImage" Visible="false" runat="server" CssClass="span9">
                    <ul class="profile_form">
                        <li class="row-fluid">
                            <div class="span4">
                                <label>
                                    Please upload your picture :</label>
                            </div>
                            <div class="span4">
                                <asp:FileUpload ID="fuProfileImage" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" Display="Dynamic" ControlToValidate="fuProfileImage"
                                    runat="server" ErrorMessage="Please upload your picture" CssClass="field-validation-error"
                                    ValidationGroup="ProfileImageValidation"></asp:RequiredFieldValidator>
                                <a href="" id="hoverimage" runat="server" visible="true" class="preview">
                                    <img id="imgProfileImage" runat="server" visible="false" height="40" width="40" src="" />
                                </a>
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span4">
                            </div>
                            <div class="span6">
                                <asp:Button ID="btnSubmitImage" runat="server" Text="Upload & Continue" ValidationGroup="ProfileImageValidation"
                                    OnClick="btnSubmitImage_Click" CssClass="large_button" />
                                <asp:Button ID="btnContinue" runat="server" Text="Continue" OnClick="btnContinue_Click"
                                    CssClass="large_button" />
                            </div>
                        </li>
                    </ul>
                </asp:Panel>


                <%--For Work History--%>
                <asp:Panel ID="pnlWorkhistory" runat="server" Visible="false" CssClass="span9">
                    <asp:HiddenField ID="hndStudentWorkHistoryId" Value="0" runat="server" />
                    <asp:Panel ID="pnlWorkHistoryInfo" runat="server">
                        <ul class="profile_form">
                            <li class="row-fluid">
                                <div class="span3">
                                    <label>
                                        Company Name :</label>
                                </div>
                                <div class="span3">
                                    <asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" Display="Dynamic" runat="server" ErrorMessage="Company Name is Required"
                                        CssClass="field-validation-error" ControlToValidate="txtCompanyName" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                </div>
                            </li>
                            <li class="row-fluid">
                                <div class="span3">
                                    <label>
                                        Position :</label>
                                </div>
                                <div class="span3">
                                    <asp:TextBox ID="txtWorkPosition" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Position Name is Required"
                                        CssClass="field-validation-error" ControlToValidate="txtWorkPosition" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                </div>
                            </li>
                            <li class="row-fluid">
                                <div class="span3">
                                    <label>
                                        Start Date :</label>
                                </div>
                                <%--<div class="span2">
                                        <span class="datepickericon"></span>
                                        
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Start Date is Required"
                                            CssClass="field-validation-error" ControlToValidate="txtStartDate" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                    </div>--%>

                                <div class="span2">
                                    <asp:TextBox ID="txtStartDate" runat="server" Visible="false"></asp:TextBox>
                                    <asp:DropDownList ID="ddlstartday" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="Dynamic" runat="server" ControlToValidate="ddlstartday"
                                        InitialValue="0" CssClass="field-validation-error" ErrorMessage="Please Select Day"
                                        ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                </div>
                                <div class="span2">
                                    <asp:DropDownList ID="ddlstartmonth" runat="server">
                                        <asp:ListItem Text="Select Month" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator31" Display="Dynamic" runat="server" ControlToValidate="ddlstartmonth"
                                        InitialValue="0" CssClass="field-validation-error" ErrorMessage="Please Select Month"
                                        ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                </div>
                                <div class="span2">
                                    <asp:DropDownList ID="ddlstartyear" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" Display="Dynamic" runat="server" ControlToValidate="ddlstartyear"
                                        InitialValue="0" CssClass="field-validation-error" ErrorMessage="Please Select Year"
                                        ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                </div>
                            </li>
                            <li class="row-fluid">
                                <div class="span3">
                                    <label>
                                        End Date :</label>
                                </div>
                                <%--  <div class="span2">
                                        <span class="datepickericon"></span>
                                       
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="End Date is Required"
                                            CssClass="field-validation-error" ControlToValidate="txtEndDate" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                    </div>--%>

                                <div class="span2">
                                    <asp:TextBox ID="txtEndDate" runat="server" Visible="false"></asp:TextBox>
                                    <asp:DropDownList ID="ddlendday" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" Display="Dynamic" runat="server" ControlToValidate="ddlendday"
                                        InitialValue="0" CssClass="field-validation-error" ErrorMessage="Please Select Day"
                                        ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                </div>
                                <div class="span2">
                                    <asp:DropDownList ID="ddlendmonth" runat="server">
                                        <asp:ListItem Text="Select Month" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator33" Display="Dynamic" runat="server" ControlToValidate="ddlendmonth"
                                        InitialValue="0" CssClass="field-validation-error" ErrorMessage="Please Select Month"
                                        ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                </div>
                                <div class="span2">
                                    <asp:DropDownList ID="ddlendyear" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator34" Display="Dynamic" runat="server" ControlToValidate="ddlendyear"
                                        InitialValue="0" CssClass="field-validation-error" ErrorMessage="Please Select Year"
                                        ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                </div>

                            </li>
                            <li class="row-fluid">
                                <div class="span3">
                                    <label>
                                        Resposibilities :</label>
                                </div>
                                <div class="span3">
                                    <asp:TextBox ID="txtResposibilities" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" Display="Dynamic" runat="server" ErrorMessage="Resposibilities is Required"
                                        CssClass="field-validation-error" ControlToValidate="txtResposibilities" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                </div>
                            </li>
                            <li class="row-fluid">
                                <div class="span3">
                                </div>
                                <div class="span6">
                                    <asp:Button ID="btnSaveWorkHistory" runat="server" Text="Add to List" OnClick="btnSaveWorkHistory_Click"
                                        ValidationGroup="WorkHistoryValidation" CssClass="large_button" />
                                    <asp:Button ID="btnSaveAndContinueWorkHistory" runat="server" Text="Next section >"
                                        OnClick="btnSaveAndContinueWorkHistory_Click" CssClass="large_button" />
                                </div>
                            </li>
                        </ul>
                    </asp:Panel>
                    <asp:Panel ID="pnlWorkHistoryDetail" runat="server">
                        <asp:GridView ID="grvWorkHistory" runat="server" CssClass="simple_table box" AutoGenerateColumns="false" DataKeyNames="StudentWorkHistoryId"
                            EmptyDataText="No History Record Found..!">
                            <Columns>
                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDeleteWorkHistory" runat="server" Text="Delete" OnClick="lnkDeleteWorkHistory_Click" OnClientClick="return confirmDelete();"
                                            CommandArgument='<%# Eval("StudentWorkHistoryId") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company Name" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkCompanyName" runat="server" Text='<%# Eval("CompanyName")%>'
                                            OnClick="lnkCompanyName_Click" CommandArgument='<%# Eval("StudentWorkHistoryId") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Position" HeaderText="Position" HeaderStyle-Width="10%" ItemStyle-Width="10%"></asp:BoundField>
                                <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-Width="10%" ItemStyle-Width="10%"></asp:BoundField>
                                <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-Width="10%" ItemStyle-Width="10%"></asp:BoundField>
                                <asp:BoundField DataField="Responsibilities" HeaderText="Responsibilities" HeaderStyle-Width="10%" ItemStyle-Width="10%"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </asp:Panel>


                <%--For CurricularActivies--%>
                <asp:Panel ID="pnlCurricularActivies" runat="server" Visible="false" CssClass="span9">
                    <ul class="profile_form">
                        <li class="row-fluid">
                            <div class="span3">
                                <label>
                                    Sports :</label>
                            </div>
                            <div class="span5">
                                <asp:TextBox ID="txtSports" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span3">
                                <label>
                                    Leadership :</label>
                            </div>
                            <div class="span5">
                                <asp:TextBox ID="txtLeadership" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span3">
                                <label>
                                    Other Activities :</label>
                            </div>
                            <div class="span5">
                                <asp:TextBox ID="txtOtherActivities" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span3">
                            </div>
                            <div class="span6">
                                <asp:Button ID="btnSubmitActivities" runat="server" Text="Save" OnClick="btnSubmitActivities_Click" CssClass="large_button" />
                            </div>
                        </li>
                    </ul>
                </asp:Panel>


                <%--For International Test--%>
                <div class="span9" id="divInternationalTest">
                    <asp:Panel ID="pnlInternationalTest" CssClass="ui_form" runat="server" Visible="false">

                        <div class="width100per">
                            <div class="shadowbox">
                                <div class="h2_heading">
                                    <h2>ACT</h2>
                                    <a href="javascript:OpenACT()" class="add_btn fright">Add a score</a>
                                </div>
                                <ul id="ulACT" class="list_4">
                                    <asp:DataList ID="dlACT" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                        <ItemTemplate>
                                            <li>
                                                <a id='editact<%#Eval("StudentTestId")%>' href="javascript:OpenACTEdit('<%#Eval("StudentTestId")%>')">ACT-<%#Eval("Date") %></a>
                                                <a id='deleteact<%#Eval("StudentTestId")%>' href="javascript:ACTDelete('<%#Eval("StudentTestId")%>')" class="delete">Delete</a>
                                            </li>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ul>
                                <%--For ACT--%>
                                <div title="ACT" id="divACT" style="display: none">
                                    <asp:Panel ID="pnlACT" runat="server" CssClass="ui_form">
                                        <asp:HiddenField ID="hndStudentTestId" Value="0" runat="server" />
                                        <asp:HiddenField ID="hndACTID" Value="0" runat="server" />
                                        <ul class="profile_form left_right_space">
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>Composite :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtComposite" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ErrorMessage="Composite is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtComposite" ValidationGroup="ACTValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtComposite"
                                                        ErrorMessage="Invalid Value" ValidationGroup="ACTValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        English :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtEnglish" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ErrorMessage="English is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtEnglish" ValidationGroup="ACTValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtEnglish"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="ACTValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Math :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtMath" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ErrorMessage="Math is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtMath" ValidationGroup="ACTValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtMath"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="ACTValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Reading :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtReading" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ErrorMessage="Reading is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtReading" ValidationGroup="ACTValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtReading"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="ACTValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Science :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtScience" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ErrorMessage="Science is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtScience" ValidationGroup="ACTValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtScience"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="ACTValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Writing :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtWriting" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ErrorMessage="Writing is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtWriting" ValidationGroup="ACTValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtWriting"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="ACTValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>Date :</label>
                                                </div>
                                                <div class="span3">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtACTDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" CssClass="field-validation-error" ControlToValidate="txtACTDate" ErrorMessage="Date is Required" ValidationGroup="ACTValidation"></asp:RequiredFieldValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label></label>
                                                </div>
                                                <div class="span5">
                                                    <asp:Button ID="btnACTSave" runat="server" Text="Save" ValidationGroup="ACTValidation" CssClass="small_button" />
                                                    <a href="javascript:CancelACT()" class="small_button">Cancel</a>

                                                </div>
                                            </li>
                                        </ul>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>

                        <div class="width100per">
                            <div class="shadowbox">
                                <div class="h2_heading">
                                    <h2>SAT</h2>
                                    <a href="javascript:OpenSAT()" class="add_btn fright">Add a score</a>
                                </div>
                                <ul id="ulSAT" class="list_4">
                                    <asp:DataList ID="dlSAT" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                        <ItemTemplate>
                                            <li>
                                                <a id='editsat<%#Eval("StudentTestId")%>' href="javascript:OpenSATEdit('<%#Eval("StudentTestId")%>')">SAT-<%#Eval("Date") %></a>
                                                <a id='deletesat<%#Eval("StudentTestId")%>' href="javascript:SATDelete('<%#Eval("StudentTestId")%>')" class="delete">Delete</a>
                                            </li>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ul>
                                <%--For SAT--%>
                                <div title="SAT" id="divSAT" style="display: none">
                                    <asp:Panel ID="pnlSAT" value="0" runat="server" CssClass="ui_form">
                                        <asp:HiddenField ID="hndSATID" Value="0" runat="server" />
                                        <ul class="profile_form left_right_space">
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Reading :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtSATReading" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator45" runat="server" ErrorMessage="Reading is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtSATReading" ValidationGroup="SATValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtSATReading"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="SATValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>

                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Math :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtSATMath" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator46" runat="server" ErrorMessage="Math is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtSATMath" ValidationGroup="SATValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtSATMath"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="SATValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Writing :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtSATWriting" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator47" runat="server" ErrorMessage="Writing is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtSATWriting" ValidationGroup="SATValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtSATWriting"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="SATValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Composite :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtSATComposite" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator48" runat="server" ErrorMessage="Composite is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtSATComposite" ValidationGroup="SATValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator15" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtSATComposite"
                                                        ErrorMessage="Invalid Value" ValidationGroup="SATValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span3">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtSATDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator49" runat="server" CssClass="field-validation-error" ControlToValidate="txtSATDate" ErrorMessage="Date is Required" ValidationGroup="SATValidation"></asp:RequiredFieldValidator>

                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label></label>
                                                </div>
                                                <div class="span6">
                                                    <asp:Button ID="btnSATSave" runat="server" Text="Save"
                                                        ValidationGroup="SATValidation" CssClass="small_button" />
                                                    <a href="javascript:CancelSAT()" class="small_button">Cancel</a>

                                                </div>
                                            </li>
                                        </ul>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>

                        <div class="width100per">
                            <div class="shadowbox">
                                <div class="h2_heading">
                                    <h2>AP</h2>
                                    <a href="javascript:OpenAP()" class="add_btn fright">Add a score</a>
                                </div>
                                <ul id="ulAP" class="list_4">
                                    <asp:DataList ID="dlAP" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                        <ItemTemplate>
                                            <li>
                                                <a id='editap<%#Eval("StudentTestId")%>' href="javascript:OpenAPEdit('<%#Eval("StudentTestId")%>')">AP-<%#Eval("Date") %></a>
                                                <a id='deleteap<%#Eval("StudentTestId")%>' href="javascript:APDelete('<%#Eval("StudentTestId")%>')" class="delete">Delete</a>
                                            </li>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ul>
                                <%--For AP--%>
                                <div title="AP" id="divAP" style="display: none">
                                    <asp:Panel ID="pnlAP" runat="server" CssClass="ui_form">
                                        <asp:HiddenField ID="hndAPID" Value="0" runat="server" />
                                        <ul class="profile_form left_right_space">
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Subject :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtAPSubject" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator50" runat="server" ErrorMessage="Subject is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtAPSubject" ValidationGroup="APValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator16" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtAPSubject"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="APValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>

                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Score :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtAPScore" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator51" runat="server" ErrorMessage="Score is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtAPScore" ValidationGroup="APValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator17" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtAPScore"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="APValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>

                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span3">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtAPDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator52" runat="server" CssClass="field-validation-error" ControlToValidate="txtAPDate" ErrorMessage="Date is Required" ValidationGroup="APValidation"></asp:RequiredFieldValidator>

                                                </div>
                                            </li>

                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label></label>
                                                </div>
                                                <div class="span6">
                                                    <asp:Button ID="btnAPSave" runat="server" Text="Save"
                                                        ValidationGroup="APValidation" CssClass="small_button" />
                                                    <a href="javascript:CancelAP()" class="small_button">Cancel</a>
                                                </div>
                                            </li>
                                        </ul>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>

                        <div class="width100per">
                            <div class="shadowbox">
                                <div class="h2_heading">
                                    <h2>GRE</h2>
                                    <a href="javascript:OpenGRE()" class="add_btn fright">Add a score</a>
                                </div>
                                <ul id="ulGRE" class="list_4">
                                    <asp:DataList ID="dlGRE" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                        <ItemTemplate>
                                            <li>
                                                <a id='editgre<%#Eval("StudentTestId")%>' href="javascript:OpenGREEdit('<%#Eval("StudentTestId")%>')">GRE-<%#Eval("Date") %></a>
                                                <a id='deletegre<%#Eval("StudentTestId")%>' href="javascript:GREDelete('<%#Eval("StudentTestId")%>')" class="delete">Delete</a>
                                            </li>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ul>
                                <%--For GRE--%>
                                <div title="GRE" id="divGRE" style="display: none">
                                    <asp:Panel ID="pnlGRE" runat="server" CssClass="ui_form">
                                        <asp:HiddenField ID="hndGREID" Value="0" runat="server" />
                                        <ul class="profile_form left_right_space">
                                            <li class="row-fluid">
                                                <div class="span4">
                                                    <label>
                                                        Verbal Reasoning  :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtGREVerbalReasoning" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator53" runat="server" ErrorMessage="Verbal Reasoning is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtGREVerbalReasoning" ValidationGroup="GREValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator18" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtGREVerbalReasoning"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="GREValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span4">
                                                    <label>
                                                        Quantitative Reasoning  :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtGREQuantitativeReasoning" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator54" runat="server" ErrorMessage="Quantitative Reasoning is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtGREQuantitativeReasoning" ValidationGroup="GREValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator19" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtGREQuantitativeReasoning"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="GREValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span4">
                                                    <label>
                                                        Analytical Writing :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtGREAnalyticalWriting" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" ErrorMessage="Analytical Writing is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtGREAnalyticalWriting" ValidationGroup="GREValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator20" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtGREAnalyticalWriting"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="GREValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span4">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span3">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtGREDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator56" runat="server" CssClass="field-validation-error" ControlToValidate="txtGREDate" ErrorMessage="Date is Required" ValidationGroup="GREValidation"></asp:RequiredFieldValidator>

                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span4">
                                                    <label></label>
                                                </div>
                                                <div class="span6">
                                                    <asp:Button ID="btnGRESave" runat="server" Text="Save"
                                                        ValidationGroup="GREValidation" CssClass="small_button" />
                                                    <a href="javascript:CancelGRE()" class="small_button">Cancel</a>
                                                </div>
                                            </li>
                                        </ul>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>

                        <div class="width100per">
                            <div class="shadowbox">
                                <div class="h2_heading">
                                    <h2>GMAT</h2>
                                    <a href="javascript:OpenGMAT()" class="add_btn fright">Add a score</a>
                                </div>
                                <ul id="ulGMAT" class="list_4">
                                    <asp:DataList ID="dlGMAT" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                        <ItemTemplate>
                                            <li>
                                                <a id='editgmat<%#Eval("StudentTestId")%>' href="javascript:OpenGMATEdit('<%#Eval("StudentTestId")%>')">GMAT-<%#Eval("Date") %></a>
                                                <a id='deletegmat<%#Eval("StudentTestId")%>' href="javascript:GMATDelete('<%#Eval("StudentTestId")%>')" class="delete">Delete</a>
                                            </li>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ul>
                                <%--For GMAT--%>
                                <div title="GMAT" id="divGMAT" style="display: none">
                                    <asp:Panel ID="pnlGMAT" runat="server" CssClass="ui_form">
                                        <asp:HiddenField ID="hndGMATID" Value="0" runat="server" />
                                        <ul class="profile_form left_right_space">
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Verbal :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtGMATVerbal" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" ErrorMessage="Verbal is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtGMATVerbal" ValidationGroup="GMATValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator21" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtGMATVerbal"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="GMATValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Quantitative :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtGMATQuantitative" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator58" runat="server" ErrorMessage="Quantitative is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtGMATQuantitative" ValidationGroup="GMATValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator22" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtGMATQuantitative"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="GMATValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Total :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtGMATTotal" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator59" runat="server" ErrorMessage="Total is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtGMATTotal" ValidationGroup="GMATValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator23" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtGMATTotal"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="GMATValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Analytical Writing :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtGMATAnalyticalWriting" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator60" runat="server" ErrorMessage="Analytical Writing is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtGMATAnalyticalWriting" ValidationGroup="GMATValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator24" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtGMATAnalyticalWriting"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="GMATValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span3">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtGMATDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator61" runat="server" CssClass="field-validation-error" ControlToValidate="txtGMATDate" ErrorMessage="Date is Required" ValidationGroup="GMATValidation"></asp:RequiredFieldValidator>

                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label></label>
                                                </div>
                                                <div class="span6">
                                                    <asp:Button ID="btnGMATSave" runat="server" Text="Save"
                                                        ValidationGroup="GMATValidation" CssClass="small_button" />
                                                    <a href="javascript:CancelGMAT()" class="small_button">Cancel</a>
                                                </div>
                                            </li>
                                        </ul>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>

                        <div class="width100per">
                            <div class="shadowbox">
                                <div class="h2_heading">
                                    <h2>IB</h2>
                                    <a href="javascript:OpenIB()" class="add_btn fright">Add a score</a>
                                </div>
                                <ul id="ulIB" class="list_4">
                                    <asp:DataList ID="dlIB" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                        <ItemTemplate>
                                            <li>
                                                <a id='editib<%#Eval("StudentTestId")%>' href="javascript:OpenIBEdit('<%#Eval("StudentTestId")%>')">IB-<%#Eval("Date") %></a>
                                                <a id='deleteib<%#Eval("StudentTestId")%>' href="javascript:IBDelete('<%#Eval("StudentTestId")%>')" class="delete">Delete</a>
                                            </li>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ul>
                                <%--For IB--%>
                                <div title="IB" id="divIB" style="display: none">
                                    <asp:Panel ID="pnlIB" runat="server" CssClass="ui_form">
                                        <asp:HiddenField ID="hndIBID" Value="0" runat="server" />
                                        <ul class="profile_form left_right_space">
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Subject :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtIBSubject" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator62" runat="server" ErrorMessage="Subject is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtIBSubject" ValidationGroup="IBValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator25" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtIBSubject"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="IBValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>

                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Score :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtIBScore" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator63" runat="server" ErrorMessage="Score is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtIBScore" ValidationGroup="IBValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator26" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtIBScore"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="IBValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span3">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtIBDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator64" runat="server" CssClass="field-validation-error" ControlToValidate="txtIBDate" ErrorMessage="Date is Required" ValidationGroup="IBValidation"></asp:RequiredFieldValidator>

                                                </div>
                                            </li>

                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label></label>
                                                </div>
                                                <div class="span6">
                                                    <asp:Button ID="btnIBSave" runat="server" Text="Save"
                                                        ValidationGroup="IBValidation" CssClass="small_button" />
                                                    <a href="javascript:CancelIB()" class="small_button">Cancel</a>
                                                </div>
                                            </li>
                                        </ul>
                                    </asp:Panel>

                                </div>
                            </div>
                        </div>

                        <div class="width100per">
                            <div class="shadowbox">
                                <div class="h2_heading">
                                    <h2>IELTS</h2>
                                    <a href="javascript:OpenIELTS()" class="add_btn fright">Add a score</a>
                                </div>
                                <ul id="ulIELTS" class="list_4">
                                    <asp:DataList ID="dlIELTS" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                        <ItemTemplate>
                                            <li>
                                                <a id='editielts<%#Eval("StudentTestId")%>' href="javascript:OpenIELTSEdit('<%#Eval("StudentTestId")%>')">IELTS-<%#Eval("Date") %></a>
                                                <a id='deleteielts<%#Eval("StudentTestId")%>' href="javascript:IELTSDelete('<%#Eval("StudentTestId")%>')" class="delete">Delete</a>
                                            </li>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ul>
                                <%--For IELTS--%>
                                <div title="IELTS" id="divIELTS" style="display: none">
                                    <asp:Panel ID="pnlIELTS" runat="server" CssClass="ui_form">
                                        <asp:HiddenField ID="hndIELTSID" Value="0" runat="server" />
                                        <ul class="profile_form left_right_space">
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Listening :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtIELTSListening" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator65" runat="server" ErrorMessage="Listening is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtIELTSListening" ValidationGroup="IELTSValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator27" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtIELTSListening"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="IELTSValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Reading :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtIELTSReading" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator66" runat="server" ErrorMessage="Reading is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtIELTSReading" ValidationGroup="IELTSValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator28" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtIELTSReading"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="IELTSValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Writing :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtIELTSWriting" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator67" runat="server" ErrorMessage="Writing is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtIELTSWriting" ValidationGroup="IELTSValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator29" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtIELTSWriting"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="IELTSValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Speaking :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtIELTSSpeaking" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator68" runat="server" ErrorMessage="Speaking is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtIELTSSpeaking" ValidationGroup="IELTSValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator30" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtIELTSSpeaking"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="IELTSValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span3">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtIELTSDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator69" runat="server" CssClass="field-validation-error" ControlToValidate="txtIELTSDate" ErrorMessage="Date is Required" ValidationGroup="IELTSValidation"></asp:RequiredFieldValidator>

                                                </div>
                                            </li>

                                            <li class="row-fluid">
                                                <div class="span3">
                                                </div>
                                                <div class="span6">
                                                    <asp:Button ID="btnIELTSSave" runat="server" Text="Save"
                                                        ValidationGroup="IELTSValidation" CssClass="small_button" />
                                                    <a href="javascript:CancelIELTS()" class="small_button">Cancel</a>
                                                </div>
                                            </li>
                                        </ul>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>

                        <div class="width100per">
                            <div class="shadowbox">
                                <div class="h2_heading">
                                    <h2>LSAT</h2>
                                    <a href="javascript:OpenLSAT()" class="add_btn fright">Add a score</a>
                                </div>
                                <ul id="ulLSAT" class="list_4">
                                    <asp:DataList ID="dlLSAT" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                        <ItemTemplate>
                                            <li>
                                                <a id='editlsat<%#Eval("StudentTestId")%>' href="javascript:OpenLSATEdit('<%#Eval("StudentTestId")%>')">LSAT-<%#Eval("Date") %></a>
                                                <a id='deletelsat<%#Eval("StudentTestId")%>' href="javascript:LSATDelete('<%#Eval("StudentTestId")%>')" class="delete">Delete</a>
                                            </li>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ul>
                                <%--For LSAT--%>
                                <div title="LSAT" id="divLSAT" style="display: none">
                                    <asp:Panel ID="pnlLSAT" runat="server" CssClass="ui_form">
                                        <asp:HiddenField ID="hndLSATID" Value="0" runat="server" />
                                        <ul class="profile_form left_right_space">
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Score :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtLSATScore" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator70" runat="server" ErrorMessage="Score is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtLSATScore" ValidationGroup="LSATValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator31" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtLSATScore"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="LSATValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span3">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtLSATDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator71" runat="server" CssClass="field-validation-error" ControlToValidate="txtLSATDate" ErrorMessage="Date is Required" ValidationGroup="LSATValidation"></asp:RequiredFieldValidator>

                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label></label>
                                                </div>
                                                <div class="span6">
                                                    <asp:Button ID="btnLSATSave" runat="server" Text="Save"
                                                        ValidationGroup="LSATValidation" CssClass="small_button" />
                                                    <a href="javascript:CancelLSAT()" class="small_button">Cancel</a>
                                                </div>
                                            </li>
                                        </ul>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>

                        <div class="width100per">
                            <div class="shadowbox">
                                <div class="h2_heading">
                                    <h2>MCAT</h2>
                                    <a href="javascript:OpenMCAT()" class="add_btn fright">Add a score</a>
                                </div>
                                <ul id="ulMCAT" class="list_4">
                                    <asp:DataList ID="dlMCAT" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                        <ItemTemplate>
                                            <li>
                                                <a id='editmcat<%#Eval("StudentTestId")%>' href="javascript:OpenMCATEdit('<%#Eval("StudentTestId")%>')">MCAT-<%#Eval("Date") %></a>
                                                <a id='deletemcat<%#Eval("StudentTestId")%>' href="javascript:MCATDelete('<%#Eval("StudentTestId")%>')" class="delete">Delete</a>
                                            </li>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ul>
                                <%--For MCAT--%>
                                <div title="MCAT" id="divMCAT" style="display: none">
                                    <asp:Panel ID="pnlMCAT" runat="server" CssClass="ui_form">
                                        <asp:HiddenField ID="hndMCATID" Value="0" runat="server" />
                                        <ul class="profile_form left_right_space">
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Biology :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtMCATBiology" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator72" runat="server" ErrorMessage="Biology is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtMCATBiology" ValidationGroup="MCATValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator32" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtMCATBiology"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="MCATValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Physics :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtMCATPhysics" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator73" runat="server" ErrorMessage="Physics is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtMCATPhysics" ValidationGroup="MCATValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator33" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtMCATPhysics"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="MCATValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Verbal :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtMCATVerbal" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator74" runat="server" ErrorMessage="Verbal is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtMCATVerbal" ValidationGroup="MCATValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator34" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtMCATVerbal"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="MCATValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Writing :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtMCATWriting" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator75" runat="server" ErrorMessage="Writing is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtMCATWriting" ValidationGroup="MCATValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator35" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtMCATWriting"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="MCATValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span3">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtMCATDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator76" runat="server" CssClass="field-validation-error" ControlToValidate="txtMCATDate" ErrorMessage="Date is Required" ValidationGroup="MCATValidation"></asp:RequiredFieldValidator>

                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                </div>
                                                <div class="span6">
                                                    <asp:Button ID="btnMCATSave" runat="server" Text="Save"
                                                        ValidationGroup="MCATValidation" CssClass="small_button" />
                                                    <a href="javascript:CancelMCAT()" class="small_button">Cancel</a>
                                                </div>
                                            </li>
                                        </ul>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>

                        <div class="width100per">
                            <div class="shadowbox">
                                <div class="h2_heading">
                                    <h2>PSAT</h2>
                                    <a href="javascript:OpenPSAT()" class="add_btn fright">Add a score</a>
                                </div>
                                <ul id="ulPSAT" class="list_4">
                                    <asp:DataList ID="dlPSAT" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                        <ItemTemplate>
                                            <li>
                                                <a id='editpsat<%#Eval("StudentTestId")%>' href="javascript:OpenPSATEdit('<%#Eval("StudentTestId")%>')">PSAT-<%#Eval("Date") %></a>
                                                <a id='deletepsat<%#Eval("StudentTestId")%>' href="javascript:PSATDelete('<%#Eval("StudentTestId")%>')" class="delete">Delete</a>
                                            </li>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ul>
                                <%--For PSAT--%>
                                <div title="PSAT" id="divPSAT" style="display: none">
                                    <asp:Panel ID="pnlPSAT" runat="server" CssClass="ui_form">
                                        <asp:HiddenField ID="hndPSATID" Value="0" runat="server" />
                                        <ul class="profile_form left_right_space">
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Reading :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtPSATReading" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator77" runat="server" ErrorMessage="Reading is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtPSATReading" ValidationGroup="PSATValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator36" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtPSATReading"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="PSATValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Math :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtPSATMath" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator78" runat="server" ErrorMessage="Math is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtPSATMath" ValidationGroup="PSATValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator37" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtPSATMath"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="PSATValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Writing :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtPSATWriting" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator79" runat="server" ErrorMessage="Writing is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtPSATWriting" ValidationGroup="PSATValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator38" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtPSATWriting"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="PSATValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span3">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtPSATDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator80" runat="server" CssClass="field-validation-error" ControlToValidate="txtPSATDate" ErrorMessage="Date is Required" ValidationGroup="PSATValidation"></asp:RequiredFieldValidator>

                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                </div>
                                                <div class="span6">
                                                    <asp:Button ID="btnPSATSave" runat="server" Text="Save"
                                                        ValidationGroup="PSATValidation" CssClass="small_button" />
                                                    <a href="javascript:CancelPSAT()" class="small_button">Cancel</a>
                                                </div>
                                            </li>
                                        </ul>
                                    </asp:Panel>

                                </div>
                            </div>
                        </div>

                        <div class="width100per">
                            <div class="shadowbox">
                                <div class="h2_heading">
                                    <h2>SAT-II</h2>
                                    <a href="javascript:OpenSAT2()" class="add_btn fright">Add a score</a>
                                </div>
                                <ul id="ulSAT2" class="list_4">
                                    <asp:DataList ID="dlSAT2" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                        <ItemTemplate>
                                            <li>
                                                <a id='editsat2<%#Eval("StudentTestId")%>' href="javascript:OpenSAT2Edit('<%#Eval("StudentTestId")%>')">SAT-II-<%#Eval("Date") %></a>
                                                <a id='deletesat2<%#Eval("StudentTestId")%>' href="javascript:SAT2Delete('<%#Eval("StudentTestId")%>')" class="delete">Delete</a>
                                            </li>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ul>
                                <%--For SAT-II--%>
                                <div title="SAT-II" id="divSAT2" style="display: none">
                                    <asp:Panel ID="pnlSAT2" runat="server" CssClass="ui_form">
                                        <asp:HiddenField ID="hndSAT2ID" Value="0" runat="server" />
                                        <ul class="profile_form left_right_space">
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Subject :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtSAT_IISubject" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator81" runat="server" ErrorMessage="Subject is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtSAT_IISubject" ValidationGroup="SAT2Validation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator39" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtSAT_IISubject"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="SAT2Validation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>

                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Score :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtSAT_IIScore" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator82" runat="server" ErrorMessage="Score is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtSAT_IIScore" ValidationGroup="SAT2Validation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator40" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtSAT_IIScore"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="SAT2Validation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span3">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtSAT_IIDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator83" runat="server" CssClass="field-validation-error" ControlToValidate="txtSAT_IIDate" ErrorMessage="Date is Required" ValidationGroup="SAT2Validation"></asp:RequiredFieldValidator>

                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                </div>
                                                <div class="span6">
                                                    <asp:Button ID="btnSAT_IISave" runat="server" Text="Save"
                                                        ValidationGroup="SAT2Validation" CssClass="small_button" />
                                                    <a href="javascript:CancelSAT2()" class="small_button">Cancel</a>
                                                </div>
                                            </li>
                                        </ul>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>

                        <div class="width100per">
                            <div class="shadowbox">
                                <div class="h2_heading">
                                    <h2>TOEFL Internet based</h2>
                                    <a href="javascript:OpenTOEFLInternetbased()" class="add_btn fright">Add a score</a>
                                </div>
                                <ul id="ulTOEFLInternetbased" class="list_4">
                                    <asp:DataList ID="dlTOEFLInternetbased" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                        <ItemTemplate>
                                            <li>
                                                <a id='edittoefli<%#Eval("StudentTestId")%>' href="javascript:OpenTOEFLInternetbasedEdit('<%#Eval("StudentTestId")%>')">TOEFLInternetbased-<%#Eval("Date") %></a>
                                                <a id='deletetoefli<%#Eval("StudentTestId")%>' href="javascript:TOEFLInternetbasedDelete('<%#Eval("StudentTestId")%>')" class="delete">Delete</a>
                                            </li>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ul>
                                <%--For TOEFL Internet based--%>
                                <div title="TOEFL Internet based" id="divTOEFLInternetbased" style="display: none">
                                    <asp:Panel ID="pnlTOEFLInternetbased" runat="server" CssClass="ui_form">
                                        <asp:HiddenField ID="hndTOEFLInternetbasedID" Value="0" runat="server" />
                                        <ul class="profile_form left_right_space">
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Reading :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtTOEFLInternetbasedReading" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator84" runat="server" ErrorMessage="Reading is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtTOEFLInternetbasedReading" ValidationGroup="TOEFLInternetbasedValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator41" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtTOEFLInternetbasedReading"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="TOEFLInternetbasedValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Listening :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtTOEFLInternetbasedListening" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator85" runat="server" ErrorMessage="Listening is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtTOEFLInternetbasedListening" ValidationGroup="TOEFLInternetbasedValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator42" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtTOEFLInternetbasedListening"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="TOEFLInternetbasedValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Speaking :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtTOEFLInternetbasedSpeaking" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator86" runat="server" ErrorMessage="Speaking is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtTOEFLInternetbasedSpeaking" ValidationGroup="TOEFLInternetbasedValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator43" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtTOEFLInternetbasedSpeaking"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="TOEFLInternetbasedValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Writing :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtTOEFLInternetbasedWriting" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator87" runat="server" ErrorMessage="Writing is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtTOEFLInternetbasedWriting" ValidationGroup="TOEFLInternetbasedValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator44" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtTOEFLInternetbasedWriting"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="TOEFLInternetbasedValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Total :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtTOEFLInternetbasedTotal" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator88" runat="server" ErrorMessage="Total is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtTOEFLInternetbasedTotal" ValidationGroup="TOEFLInternetbasedValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator45" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtTOEFLInternetbasedTotal"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="TOEFLInternetbasedValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span3">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtTOEFLInternetbasedDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator89" runat="server" CssClass="field-validation-error" ControlToValidate="txtTOEFLInternetbasedDate" ErrorMessage="Date is Required" ValidationGroup="TOEFLInternetbasedValidation"></asp:RequiredFieldValidator>

                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                </div>
                                                <div class="span6">
                                                    <asp:Button ID="btnTOEFLInternetbasedSave" runat="server" Text="Save"
                                                        ValidationGroup="TOEFLInternetbasedValidation" CssClass="small_button" />
                                                    <a href="javascript:CancelTOEFLInternetbased()" class="small_button">Cancel</a>
                                                </div>
                                            </li>
                                        </ul>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>

                        <div class="width100per">
                            <div class="shadowbox">
                                <div class="h2_heading">
                                    <h2>TOEFL Paper based</h2>
                                    <a href="javascript:OpenTOEFLPaperbased()" class="add_btn fright">Add a score</a>
                                </div>
                                <ul id="ulTOEFLPaperbased" class="list_4">
                                    <asp:DataList ID="dlTOEFLPaperbased" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                        <ItemTemplate>
                                            <li>
                                                <a id='edittoeflp<%#Eval("StudentTestId")%>' href="javascript:OpenTOEFLPaperbasedEdit('<%#Eval("StudentTestId")%>')">TOEFLPaperbased-<%#Eval("Date") %></a>
                                                <a id='deletetoeflp<%#Eval("StudentTestId")%>' href="javascript:TOEFLPaperbasedDelete('<%#Eval("StudentTestId")%>')" class="delete">Delete</a>
                                            </li>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ul>
                                <%--For TOEFL Paper based--%>
                                <div title="TOEFL Paper based" id="divTOEFLPaperbased" style="display: none">
                                    <asp:Panel ID="pnlTOEFLPaperbased" runat="server" CssClass="ui_form">
                                        <asp:HiddenField ID="hndTOEFLPaperbasedID" Value="0" runat="server" />
                                        <ul class="profile_form left_right_space">
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Reading :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtTOEFLPaperbasedReading" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator90" runat="server" ErrorMessage="Reading is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtTOEFLPaperbasedReading" ValidationGroup="TOEFLPaperbasedValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator46" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtTOEFLPaperbasedReading"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="TOEFLPaperbasedValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Listening :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtTOEFLPaperbasedListening" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator91" runat="server" ErrorMessage="Listening is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtTOEFLPaperbasedListening" ValidationGroup="TOEFLPaperbasedValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator47" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtTOEFLPaperbasedListening"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="TOEFLPaperbasedValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Speaking :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtTOEFLPaperbasedSpeaking" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator92" runat="server" ErrorMessage="Speaking is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtTOEFLPaperbasedSpeaking" ValidationGroup="TOEFLPaperbasedValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator48" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtTOEFLPaperbasedSpeaking"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="TOEFLPaperbasedValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Writing :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtTOEFLPaperbasedWriting" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator93" runat="server" ErrorMessage="Writing is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtTOEFLPaperbasedWriting" ValidationGroup="TOEFLPaperbasedValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator49" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtTOEFLPaperbasedWriting"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="TOEFLPaperbasedValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Total :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtTOEFLPaperbasedTotal" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator94" runat="server" ErrorMessage="Total is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtTOEFLPaperbasedTotal" ValidationGroup="TOEFLPaperbasedValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator50" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtTOEFLPaperbasedTotal"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="TOEFLPaperbasedValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span3">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtTOEFLPaperbasedDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator95" runat="server" CssClass="field-validation-error" ControlToValidate="txtTOEFLPaperbasedDate" ErrorMessage="Date is Required" ValidationGroup="TOEFLPaperbasedValidation"></asp:RequiredFieldValidator>

                                                </div>
                                            </li>
                                            <li class="row-fluid">
                                                <div class="span3">
                                                </div>
                                                <div class="span6">
                                                    <asp:Button ID="btnTOEFLPaperbasedSave" runat="server" Text="Save"
                                                        ValidationGroup="TOEFLPaperbasedValidation" CssClass="small_button" />
                                                    <a href="javascript:CancelTOEFLPaperbased()" class="small_button">Cancel</a>
                                                </div>
                                            </li>
                                        </ul>
                                    </asp:Panel>

                                </div>
                            </div>
                        </div>

                        <div>
                            <ul>
                                <li class="row-fluid">
                                    <div class="span3">
                                    </div>
                                    <div class="span4">
                                        <asp:Button ID="btnIntTestContinue" runat="server" OnClick="btnIntTestContinue_Click" Text="Continue" CssClass="large_button" />
                                    </div>
                                </li>
                            </ul>
                        </div>

                    </asp:Panel>
                </div>

            </div>
        </div>
    </div>

    <script type="text/javascript">
        //Check Duplicate Username
        $("#MainContent_txtEmail").change(function () {
            // alert("kk");
            if ($("#MainContent_txtEmail").val() != "") {
                if (Page_IsValid) {
                    var University = {
                        UserName: $('#MainContent_txtEmail').val(),
                    };
                    $.ajax({
                        type: "POST",
                        url: "AddNewStudent.aspx/CheckDuplicateUsername",
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
                }
            }
        });

        //Birth Date Validation
        $("#MainContent_btnSave").click(function () {
            var dd = document.getElementById("MainContent_ddlday");
            var dbday = dd.options[dd.selectedIndex].value;
            var ddlmonthlist = document.getElementById("MainContent_ddlmonth");
            var dbmonth = ddlmonthlist.options[ddlmonthlist.selectedIndex].value;
            var ddlyearlist = document.getElementById("MainContent_ddlyear");
            var dbyear = ddlyearlist.options[ddlyearlist.selectedIndex].value;


            dbmonth = parseInt(dbmonth, 10); dbday = parseInt(dbday, 10); dbyear = parseInt(dbyear, 10);
            if (dbmonth == 4 || dbmonth == 6 || dbmonth == 9 || dbmonth == 11) {
                if (dbday > 30) {
                    alert("The month you have selected cannot have more than 30 days");
                    dd.focus();
                    return false;
                }
            }
            if (dbmonth == 2) {
                if (((dbyear % 100 == 0) && (dbyear % 400 != 0)) && (dbday > 28)) {
                    alert("February for the selected year cannot have more than 28 days");
                    dd.focus();
                    return false;
                }
                else if ((dbyear % 4 == 0) && (dbday > 29)) {
                    alert("February for the selected year cannot have more than 29 days");
                    dd.focus();
                    return false;
                }
                else if ((dbyear % 4 != 0) && (dbday > 28)) {
                    alert("February for the selected year cannot have more than 28 days");
                    dd.focus();
                    return false;
                }
            }
            birth_date = new Date(dbyear, dbmonth - 1, dbday, 23, 59, 59);
            today_date = new Date();
            yearval = parseInt(today_date.getYear()) + 1900;
            if (birth_date > today_date) {
                alert("Entered Date of birth is either future or today's date");
                ddlyearlist.focus();
                return false;
            }
            return true;
        });


        //Start And End Date Validation
        $("#MainContent_btnSaveWorkHistory").click(function () {
            var dd = document.getElementById("MainContent_ddlstartday");
            var dbday = dd.options[dd.selectedIndex].value;
            var ddlmonthlist = document.getElementById("MainContent_ddlstartmonth");
            var dbmonth = ddlmonthlist.options[ddlmonthlist.selectedIndex].value;
            var ddlyearlist = document.getElementById("MainContent_ddlstartyear");
            var dbyear = ddlyearlist.options[ddlyearlist.selectedIndex].value;


            dbmonth = parseInt(dbmonth, 10); dbday = parseInt(dbday, 10); dbyear = parseInt(dbyear, 10);
            if (dbmonth == 4 || dbmonth == 6 || dbmonth == 9 || dbmonth == 11) {
                if (dbday > 30) {
                    alert("The month you have selected cannot have more than 30 days");
                    dd.focus();
                    return false;
                }
            }
            if (dbmonth == 2) {
                if (((dbyear % 100 == 0) && (dbyear % 400 != 0)) && (dbday > 28)) {
                    alert("February for the selected year cannot have more than 28 days");
                    dd.focus();
                    return false;
                }
                else if ((dbyear % 4 == 0) && (dbday > 29)) {
                    alert("February for the selected year cannot have more than 29 days");
                    dd.focus();
                    return false;
                }
                else if ((dbyear % 4 != 0) && (dbday > 28)) {
                    alert("February for the selected year cannot have more than 28 days");
                    dd.focus();
                    return false;
                }
            }
            Start_date = new Date(dbyear, dbmonth - 1, dbday, 23, 59, 59);
            today_date = new Date();
            yearval = parseInt(today_date.getYear()) + 1900;
            if (Start_date > today_date) {
                alert("Entered Date of Start is either future");
                ddlyearlist.focus();
                return false;
            }

            var dd1 = document.getElementById("MainContent_ddlendday");
            var dbday1 = dd1.options[dd1.selectedIndex].value;
            var ddlmonthlist1 = document.getElementById("MainContent_ddlendmonth");
            var dbmonth1 = ddlmonthlist1.options[ddlmonthlist1.selectedIndex].value;
            var ddlyearlist1 = document.getElementById("MainContent_ddlendyear");
            var dbyear1 = ddlyearlist1.options[ddlyearlist1.selectedIndex].value;

            dbmonth1 = parseInt(dbmonth1, 10); dbday1 = parseInt(dbday1, 10); dbyear1 = parseInt(dbyear1, 10);
            if (dbmonth1 == 4 || dbmonth1 == 6 || dbmonth1 == 9 || dbmonth1 == 11) {
                if (dbday1 > 30) {
                    alert("The month you have selected cannot have more than 30 days");
                    dd1.focus();
                    return false;
                }
            }
            if (dbmonth == 2) {
                if (((dbyear1 % 100 == 0) && (dbyear1 % 400 != 0)) && (dbday1 > 28)) {
                    alert("February for the selected year cannot have more than 28 days");
                    dd1.focus();
                    return false;
                }
                else if ((dbyear1 % 4 == 0) && (dbday1 > 29)) {
                    alert("February for the selected year cannot have more than 29 days");
                    dd1.focus();
                    return false;
                }
                else if ((dbyear1 % 4 != 0) && (dbday1 > 28)) {
                    alert("February for the selected year cannot have more than 28 days");
                    dd1.focus();
                    return false;
                }
            }
            End_date = new Date(dbyear1, dbmonth1 - 1, dbday1, 23, 59, 59);
            if (Start_date > End_date) {
                alert("Entered Start Date Is Greter Then End Date ");
                ddlyearlist1.focus();
                return false;
            }


            return true;
        });

        $("#MainContent_ddlStartINtYear").change(function () {
            var selyear = $(this).val();
            $('#MainContent_ddlGraduationyear').find('option').remove().end().append('<option value="0">Select Any</option>').val('whatever');


            while (selyear <= 2013) {
                $("#MainContent_ddlGraduationyear").append(new Option(selyear, selyear));
                selyear++;
            }
        });

        $("#MainContent_btnSaveEduPreference").click(function () {
            var LookupOptionList = document.getElementById("MainContent_ddlLookingForCountry");

            var lookupID = LookupOptionList.options[LookupOptionList.selectedIndex].value;
            if (lookupID == "233") {
                var sv = document.getElementById('MainContent_txtotherstudy').value;
                var lbl = document.getElementById("MainContent_lblotherstudy");
                if (sv == '' || sv == null) {
                    lbl.style.display = "";
                    return false;
                }
                else {
                    lbl.style.display = "none";
                }

            }
        });
        $(function () {
            $("#MainContent_txtBirthDate").datepicker();
            $("#MainContent_txtACTDate").datepicker();
            $("#MainContent_txtSATDate").datepicker();
            $("#MainContent_txtAPDate").datepicker();
            $("#MainContent_txtGREDate").datepicker();
            $("#MainContent_txtGMATDate").datepicker();
            $("#MainContent_txtIBDate").datepicker();
            $("#MainContent_txtIELTSDate").datepicker();
            $("#MainContent_txtLSATDate").datepicker();
            $("#MainContent_txtMCATDate").datepicker();
            $("#MainContent_txtPSATDate").datepicker();
            $("#MainContent_txtSAT_IIDate").datepicker();
            $("#MainContent_txtTOEFLInternetbasedDate").datepicker();
            $("#MainContent_txtTOEFLPaperbasedDate").datepicker();



            $("#MainContent_txtStartDate").datepicker({
                numberOfMonths: 1,
                onSelect: function (selected) {
                    $("#MainContent_txtEndDate").datepicker("option", "minDate", selected)
                }
            });
            $("#MainContent_txtEndDate").datepicker({
                numberOfMonths: 1,
                onSelect: function (selected) {
                    $("#MainContent_txtStartDate").datepicker("option", "maxDate", selected)
                }
            });

            $("#nav li").each(function () {
                $(this).removeClass('navi_active');
            });

            $("#Profile").addClass('navi_active');

            var currTab = $("#MainContent_hndCurrentTab").val();
            $("#tabNavigation li").each(function () {
                $(this).removeClass("list1_active");
            });
            if (currTab == "1") {
                $("#MainContent_BasicInformation").addClass("list1_active");
            }
            else if (currTab == "2") {
                $("#MainContent_lblcurrentacademic").addClass("list1_active");
            }
            else if (currTab == "3") {
                $("#MainContent_lblInternationalTest").addClass("list1_active");
            }
            else if (currTab == "4") {
                $("#MainContent_EducationalPreferences").addClass("list1_active");
            }
            else if (currTab == "5") {
                $("#MainContent_Photo").addClass("list1_active");
            }
            else if (currTab == "6") {
                $("#MainContent_lblworkhistory").addClass("list1_active");
            }
            else {
                $("#MainContent_ExtraCurricularActivities").addClass("list1_active");
            }
            if (document.getElementById("MainContent_ddlLookingForCountry") != null) {
                var LookupOptionList = document.getElementById("MainContent_ddlLookingForCountry");
                var lookupID = LookupOptionList.options[LookupOptionList.selectedIndex].value;
                if (lookupID == "233") {
                    document.getElementById('MainContent_pnlotherstudy').style.display = 'inline';
                }
                else {
                    document.getElementById('MainContent_pnlotherstudy').style.display = 'none';
                    document.getElementById('MainContent_txtotherstudy').value = '';

                }
            }
            $("#MainContent_ddlLookingForCountry").change(function () {
                var LookupOptionList = document.getElementById("MainContent_ddlLookingForCountry");
                var lookupID = LookupOptionList.options[LookupOptionList.selectedIndex].value;
                if (lookupID == "233") {
                    document.getElementById('MainContent_pnlotherstudy').style.display = 'inline';
                }
                else {
                    document.getElementById('MainContent_pnlotherstudy').style.display = 'none';
                    document.getElementById('MainContent_txtotherstudy').value = '';
                }
            });

            $("#MainContent_rdoDidYouGraduate li input").change(function () {
                var rdvlaue = $(this).attr('value');
                if (rdvlaue == 1) {
                    document.getElementById('MainContent_pnlGraduateDetail').style.display = 'inline';
                    document.getElementById('MainContent_pnlongoing').style.display = 'none';
                }
                else if (rdvlaue == 3) {
                    document.getElementById('MainContent_pnlongoing').style.display = 'inline';
                    document.getElementById('MainContent_pnlGraduateDetail').style.display = 'none';
                }
                else {
                    document.getElementById('MainContent_pnlGraduateDetail').style.display = 'none';
                    document.getElementById('MainContent_pnlongoing').style.display = 'none';
                }
            });
            var selectedvalue = document.getElementsByName('ctl00$MainContent$rdoDidYouGraduate');

            if (selectedvalue.item(0).checked) {
                document.getElementById('MainContent_pnlGraduateDetail').style.display = 'inline';
                document.getElementById('MainContent_pnlongoing').style.display = 'none';

            }
            else if (selectedvalue.item(2).checked) {
                document.getElementById('MainContent_pnlongoing').style.display = 'inline';
                document.getElementById('MainContent_pnlGraduateDetail').style.display = 'none';
            }
            else {

                document.getElementById('MainContent_pnlGraduateDetail').style.display = 'none';
                document.getElementById('MainContent_pnlongoing').style.display = 'none';
            }


        });


        $("#MainContent_btnSaveAcademics").click(function () {
            var selectedvalue = document.getElementsByName('ctl00$MainContent$rdoDidYouGraduate');
            if (selectedvalue.item(0).checked) {
                var sel = document.getElementById('MainContent_ddlYearOfGraduation');
                var sv = sel.options[sel.selectedIndex].value;
                var lbl = document.getElementById("MainContent_lblyeargradution");
                if (sv == '0') {
                    lbl.style.display = "";
                    return false;
                }
                else {
                    lbl.style.display = "none";
                }
                var sel1 = document.getElementById('MainContent_ddlLevelCompleted');
                var sv1 = sel1.options[sel1.selectedIndex].value;
                var lbl1 = document.getElementById("MainContent_lbllevelcompleted");
                if (sv1 == '0') {
                    lbl1.style.display = "";
                    return false;
                }
                else {
                    lbl1.style.display = "none";
                }
            }
            else if (selectedvalue.item(2).checked) {
                var sel = document.getElementById('MainContent_ddldegreepusued');
                var sv = sel.options[sel.selectedIndex].value;
                var lbl = document.getElementById("MainContent_lbldegreepusued");
                if (sv == '0') {
                    lbl.style.display = "";
                    return false;
                }
                else {
                    lbl.style.display = "none";
                }
                var sel1 = document.getElementById('MainContent_ddlexpectedgraduation');
                var sv1 = sel1.options[sel1.selectedIndex].value;
                var lbl1 = document.getElementById("MainContent_lblexpectedgraduation");
                if (sv1 == '0') {
                    lbl1.style.display = "";
                    return false;
                }
                else {
                    lbl1.style.display = "none";
                }
                var sel3 = document.getElementById('MainContent_txtfieldstudy').value;
                var lbl2 = document.getElementById("MainContent_lblfieldofstudy");
                if (sel3 == '' || sel3 == null) {
                    lbl2.style.display = "";
                    return false;
                }
                else {
                    lbl2.style.display = "none";
                }
            }
        });

    </script>

    <%--For International Test--%>
    <script type="text/javascript">
        function OpenHighScoolEnrollment() {
            $("#divHighSchoolEnrollment").show();
            $("#divACT").hide();
            $("#divSAT").hide();
            $("#divAP").hide();
            $("#divGRE").hide();
            $("#divGMAT").hide();
            $("#divIB").hide();
            $("#divIELTS").hide();
            $("#divLSAT").hide();
            $("#divMCAT").hide();
            $("#divPSAT").hide();
            $("#divSAT2").hide();
            $("#divTOEFLInternetbased").hide();
            $("#divTOEFLPaperbased").hide();
        };
        function OpenACT() {
            $("#divHighSchoolEnrollment").hide();
            $("#divACT").show();
            $("#divSAT").hide();
            $("#divAP").hide();
            $("#divGRE").hide();
            $("#divGMAT").hide();
            $("#divIB").hide();
            $("#divIELTS").hide();
            $("#divLSAT").hide();
            $("#divMCAT").hide();
            $("#divPSAT").hide();
            $("#divSAT2").hide();
            $("#divTOEFLInternetbased").hide();
            $("#divTOEFLPaperbased").hide();
        };
        function OpenSAT() {
            $("#divHighSchoolEnrollment").hide();
            $("#divACT").hide();
            $("#divSAT").show();
            $("#divAP").hide();
            $("#divGRE").hide();
            $("#divGMAT").hide();
            $("#divIB").hide();
            $("#divIELTS").hide();
            $("#divLSAT").hide();
            $("#divMCAT").hide();
            $("#divPSAT").hide();
            $("#divSAT2").hide();
            $("#divTOEFLInternetbased").hide();
            $("#divTOEFLPaperbased").hide();
        };
        function OpenAP() {
            $("#divHighSchoolEnrollment").hide();
            $("#divACT").hide();
            $("#divSAT").hide();
            $("#divAP").show();
            $("#divGRE").hide();
            $("#divGMAT").hide();
            $("#divIB").hide();
            $("#divIELTS").hide();
            $("#divLSAT").hide();
            $("#divMCAT").hide();
            $("#divPSAT").hide();
            $("#divSAT2").hide();
            $("#divTOEFLInternetbased").hide();
            $("#divTOEFLPaperbased").hide();
        };
        function OpenGRE() {
            $("#divHighSchoolEnrollment").hide();
            $("#divACT").hide();
            $("#divSAT").hide();
            $("#divAP").hide();
            $("#divGRE").show();
            $("#divGMAT").hide();
            $("#divIB").hide();
            $("#divIELTS").hide();
            $("#divLSAT").hide();
            $("#divMCAT").hide();
            $("#divPSAT").hide();
            $("#divSAT2").hide();
            $("#divTOEFLInternetbased").hide();
            $("#divTOEFLPaperbased").hide();
        };
        function OpenGMAT() {
            $("#divHighSchoolEnrollment").hide();
            $("#divACT").hide();
            $("#divSAT").hide();
            $("#divAP").hide();
            $("#divGRE").hide();
            $("#divGMAT").show();
            $("#divIB").hide();
            $("#divIELTS").hide();
            $("#divLSAT").hide();
            $("#divMCAT").hide();
            $("#divPSAT").hide();
            $("#divSAT2").hide();
            $("#divTOEFLInternetbased").hide();
            $("#divTOEFLPaperbased").hide();
        };
        function OpenIB() {
            $("#divHighSchoolEnrollment").hide();
            $("#divACT").hide();
            $("#divSAT").hide();
            $("#divAP").hide();
            $("#divGRE").hide();
            $("#divGMAT").hide();
            $("#divIB").show();
            $("#divIELTS").hide();
            $("#divLSAT").hide();
            $("#divMCAT").hide();
            $("#divPSAT").hide();
            $("#divSAT2").hide();
            $("#divTOEFLInternetbased").hide();
            $("#divTOEFLPaperbased").hide();
        };
        function OpenIELTS() {
            $("#divHighSchoolEnrollment").hide();
            $("#divACT").hide();
            $("#divSAT").hide();
            $("#divAP").hide();
            $("#divGRE").hide();
            $("#divGMAT").hide();
            $("#divIB").hide();
            $("#divIELTS").show();
            $("#divLSAT").hide();
            $("#divMCAT").hide();
            $("#divPSAT").hide();
            $("#divSAT2").hide();
            $("#divTOEFLInternetbased").hide();
            $("#divTOEFLPaperbased").hide();
        };
        function OpenLSAT() {
            $("#divHighSchoolEnrollment").hide();
            $("#divACT").hide();
            $("#divSAT").hide();
            $("#divAP").hide();
            $("#divGRE").hide();
            $("#divGMAT").hide();
            $("#divIB").hide();
            $("#divIELTS").hide();
            $("#divLSAT").show();
            $("#divMCAT").hide();
            $("#divPSAT").hide();
            $("#divSAT2").hide();
            $("#divTOEFLInternetbased").hide();
            $("#divTOEFLPaperbased").hide();
        };
        function OpenMCAT() {
            $("#divHighSchoolEnrollment").hide();
            $("#divACT").hide();
            $("#divSAT").hide();
            $("#divAP").hide();
            $("#divGRE").hide();
            $("#divGMAT").hide();
            $("#divIB").hide();
            $("#divIELTS").hide();
            $("#divLSAT").hide();
            $("#divMCAT").show();
            $("#divPSAT").hide();
            $("#divSAT2").hide();
            $("#divTOEFLInternetbased").hide();
            $("#divTOEFLPaperbased").hide();
        };
        function OpenPSAT() {
            $("#divHighSchoolEnrollment").hide();
            $("#divACT").hide();
            $("#divSAT").hide();
            $("#divAP").hide();
            $("#divGRE").hide();
            $("#divGMAT").hide();
            $("#divIB").hide();
            $("#divIELTS").hide();
            $("#divLSAT").hide();
            $("#divMCAT").hide();
            $("#divPSAT").show();
            $("#divSAT2").hide();
            $("#divTOEFLInternetbased").hide();
            $("#divTOEFLPaperbased").hide();
        };
        function OpenSAT2() {
            $("#divHighSchoolEnrollment").hide();
            $("#divACT").hide();
            $("#divSAT").hide();
            $("#divAP").hide();
            $("#divGRE").hide();
            $("#divGMAT").hide();
            $("#divIB").hide();
            $("#divIELTS").hide();
            $("#divLSAT").hide();
            $("#divMCAT").hide();
            $("#divPSAT").hide();
            $("#divSAT2").show();
            $("#divTOEFLInternetbased").hide();
            $("#divTOEFLPaperbased").hide();
        };
        function OpenTOEFLInternetbased() {
            $("#divHighSchoolEnrollment").hide();
            $("#divACT").hide();
            $("#divSAT").hide();
            $("#divAP").hide();
            $("#divGRE").hide();
            $("#divGMAT").hide();
            $("#divIB").hide();
            $("#divIELTS").hide();
            $("#divLSAT").hide();
            $("#divMCAT").hide();
            $("#divPSAT").hide();
            $("#divSAT2").hide();
            $("#divTOEFLInternetbased").show();
            $("#divTOEFLPaperbased").hide();
        };
        function OpenTOEFLPaperbased() {
            $("#divHighSchoolEnrollment").hide();
            $("#divACT").hide();
            $("#divSAT").hide();
            $("#divAP").hide();
            $("#divGRE").hide();
            $("#divGMAT").hide();
            $("#divIB").hide();
            $("#divIELTS").hide();
            $("#divLSAT").hide();
            $("#divMCAT").hide();
            $("#divPSAT").hide();
            $("#divSAT2").hide();
            $("#divTOEFLInternetbased").hide();
            $("#divTOEFLPaperbased").show();
        };

        function CancelHighScoolEnrollment() {
            $("#MainContent_txtHighSchoolEnrollmentSchoolName").val("");
            $("#MainContent_ddlStartINtYear").val("0");
            $("#MainContent_ddlGraduationyear").val("0");
            $("MainContent_hndStudentTestId").val("0");
            $("#divHighSchoolEnrollment").hide();
            $("#divHighSchoolEnrollment").dialog('close');

        };
        function CancelACT() {
            $("#MainContent_txtComposite").val("");
            $("#MainContent_txtEnglish").val("");
            $("#MainContent_txtMath").val("");
            $("#MainContent_txtReading").val("");
            $("#MainContent_txtScience").val("");
            $("#MainContent_txtWriting").val("");
            $("#MainContent_txtACTDate").val("");
            $("MainContent_hndACTId").val("0");
            $("#divACT").hide();
            $("#divACT").dialog('close');
        };
        function CancelSAT() {
            $("#MainContent_txtSATReading").val("");
            $("#MainContent_txtSATMath").val("");
            $("#MainContent_txtSATWriting").val("");
            $("#MainContent_txtSATComposite").val("");
            $("#MainContent_txtSATDate").val("");
            $("MainContent_hndSATId").val("0");
            $("#divSAT").hide();
            $("#divSAT").dialog('close');
        };
        function CancelAP() {
            $("#MainContent_txtAPDate").val("");
            $("#MainContent_txtAPScore").val("");
            $("#MainContent_txtAPSubject").val("");
            $("MainContent_hndAPId").val("0");
            $("#divAP").hide();
            $("#divAP").dialog('close');
        };
        function CancelGRE() {
            $("#MainContent_txtGREVerbalReasoning").val("");
            $("#MainContent_txtGREQuantitativeReasoning").val("");
            $("#MainContent_txtGREAnalyticalWriting").val("");
            $("#MainContent_txtGREDate").val("");
            $("MainContent_hndGREID").val("0");
            $("#divGRE").hide();
            $("#divGRE").dialog('close');
        };
        function CancelGMAT() {
            $("#MainContent_txtGMATVerbal").val("");
            $("#MainContent_txtGMATQuantitative").val("");
            $("#MainContent_txtGMATTotal").val("");
            $("#MainContent_txtGMATAnalyticalWriting").val("");
            $("#MainContent_txtGMATDate").val("");
            $("#MainContent_hndGMATID").val("0");
            $("#divGMAT").hide();
            $("#divGMAT").dialog('close');
        };
        function CancelIB() {
            $("#MainContent_txtIBSubject").val("");
            $("#MainContent_txtIBScore").val("");
            $("#MainContent_txtIBDate").val("");
            $("#MainContent_hndIBID").val("0");
            $("#divIB").hide();
            $("#divIB").dialog('close');
        };
        function CancelIELTS() {
            $("#MainContent_txtIELTSListening").val("");
            $("#MainContent_txtIELTSReading").val("");
            $("#MainContent_txtIELTSWriting").val("");
            $("#MainContent_txtIELTSSpeaking").val("");
            $("#MainContent_txtIELTSDate").val("");
            $("#MainContent_hndIELTSID").val("0");
            $("#divIELTS").hide();
            $("#divIELTS").dialog('close');
        };
        function CancelLSAT() {
            $("#MainContent_txtLSATScore").val("");
            $("#MainContent_txtLSATDate").val("");
            $("#MainContent_hndLSATID").val("0");
            $("#divLSAT").hide();
            $("#divLSAT").dialog('close');
        };
        function CancelMCAT() {
            $("#MainContent_txtMCATBiology").val("");
            $("#MainContent_txtMCATPhysics").val("");
            $("#MainContent_txtMCATVerbal").val("");
            $("#MainContent_txtMCATWriting").val("");
            $("#MainContent_txtMCATDate").val("");
            $("#MainContent_hndMCATID").val("0");
            $("#divMCAT").hide();
            $("#divMCAT").dialog('close');
        };
        function CancelPSAT() {
            $("#MainContent_txtPSATReading").val("");
            $("#MainContent_txtPSATMath").val("");
            $("#MainContent_txtPSATWriting").val("");
            $("#MainContent_txtPSATDate").val("");
            $("#MainContent_hndPSATID").val("0");
            $("#divPSAT").hide();
            $("#divPSAT").dialog('close');
        };
        function CancelSAT2() {
            $("#MainContent_txtSAT_IISubject").val("");
            $("#MainContent_txtSAT_IIScore").val("");
            $("#MainContent_txtSAT_IIDate").val("");
            $("#MainContent_hndSAT2ID").val("0");
            $("#divSAT2").hide();
            $("#divSAT2").dialog('close');
        };
        function CancelTOEFLInternetbased() {
            $("#MainContent_txtTOEFLInternetbasedReading").val("");
            $("#MainContent_txtTOEFLInternetbasedListening").val("");
            $("#MainContent_txtTOEFLInternetbasedSpeaking").val("");
            $("#MainContent_txtTOEFLInternetbasedWriting").val("");
            $("#MainContent_txtTOEFLInternetbasedTotal").val("");
            $("#MainContent_txtTOEFLInternetbasedDate").val("");
            $("#MainContent_hndTOEFLInternetbasedID").val("0");
            $("#divTOEFLInternetbased").hide();
            $("#divTOEFLInternetbased").dialog('close');
        };
        function CancelTOEFLPaperbased() {
            $("#MainContent_txtTOEFLPaperbasedReading").val("");
            $("#MainContent_txtTOEFLPaperbasedListening").val("");
            $("#MainContent_txtTOEFLPaperbasedSpeaking").val("");
            $("#MainContent_txtTOEFLPaperbasedWriting").val("");
            $("#MainContent_txtTOEFLPaperbasedTotal").val("");
            $("#MainContent_txtTOEFLPaperbasedDate").val("");
            $("#MainContent_hndTOEFLPaperbasedID").val("0");
            $("#divTOEFLPaperbased").hide();
            $("#divTOEFLPaperbased").dialog('close');
        };

        $("#MainContent_chkcurrentelyschool").change(function () {
            if (this.checked) {
                $("#MainContent_ddlGraduationyear").hide();
                $("#lbGraduationYear").hide();
                ValFalse();
            }
            else {
                $("#MainContent_ddlGraduationyear").show();
                $("#lbGraduationYear").show();
                ValTrue();
            }
            function ValFalse() {
                var myVal = document.getElementById('MainContent_RequiredFieldValidator37');
                ValidatorEnable(myVal, false);
            }
            function ValTrue() {
                var myVal = document.getElementById('MainContent_RequiredFieldValidator37');
                ValidatorEnable(myVal, true);
            }

        });


        /* ACT */
        $("#MainContent_btnACTSave").click(function () {
            var sid;
            if ($("#MainContent_hndNewStudentId").val() != 0) {
                sid = $("#MainContent_hndNewStudentId").val();
            }
            else {
                sid = $("#MainContent_hndStudentId").val();
            }
            if (Page_IsValid) {
                var act = {
                    TestId: "2",
                    Composite: $("#MainContent_txtComposite").val(),
                    English: $("#MainContent_txtEnglish").val(),
                    Math: $("#MainContent_txtMath").val(),
                    Reading: $("#MainContent_txtReading").val(),
                    Science: $("#MainContent_txtScience").val(),
                    Writing: $("#MainContent_txtWriting").val(),
                    Date: $("#MainContent_txtACTDate").val(),
                    StudentTestId: $("#MainContent_hndACTID").val(),
                    StudentId: sid
                };
                $.ajax({
                    type: "POST",
                    beforeSend: function () { $("#LoadingImage").show(); },
                    complete: function () { $("#LoadingImage").hide(); },
                    url: "AddNewStudent.aspx/ACTSave",
                    data: JSON.stringify(act),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var data = msg.d;

                        if (data[2] == "add") {

                            if (document.getElementById("MainContent_dlACT") != null) {
                                var v = '<br/><span><li><a id="editact' + data[0] + '" href="javascript:OpenACTEdit(' + data[0] + ')">ACT-' + data[1] + '</a> <a href="javascript:ACTDelete(' + data[0] + ')" id="deleteact' + data[0] + '"> Delete</a></li></span>';
                                $("#MainContent_dlACT").append(v);
                            }
                            else {
                                var v = '<span id="MainContent_dlACT"><span><li><a id="editact' + data[0] + '" href="javascript:OpenACTEdit(' + data[0] + ')">ACT-' + data[1] + '</a> <a href="javascript:ACTDelete(' + data[0] + ')" id="deleteact' + data[0] + '"> Delete</a></li></span></span>';
                                $("#ulACT").append(v);
                            }
                        }
                        else if (data[2] == "edit") {
                            $("#editact" + data[0]).text("ACT-" + data[1]);
                            window.location.href = "/Admin/AddNewStudent.aspx?sec=IntTest";

                        }
                        else { }
                        $("#LoadingImage").hide();
                        $("#MainContent_txtComposite").val("");
                        $("#MainContent_txtEnglish").val("");
                        $("#MainContent_txtMath").val("");
                        $("#MainContent_txtReading").val("");
                        $("#MainContent_txtScience").val("");
                        $("#MainContent_txtWriting").val("");
                        $("#MainContent_txtACTDate").val("");
                        $("#MainContent_hndACTID").val("0");
                        $("#MainContent_hndNewStudentId").val("0");
                        $("#divACT").hide();
                        $("#divACT").dialog('close');

                    }
                });
            }
            return false;
        });

        function OpenACTEdit(studentTestId) {
            var act = {
                StudentTestId: studentTestId
            };
            $.ajax({
                type: "POST",
                beforeSend: function () { $("#LoadingImage").show(); },
                complete: function () { $("#LoadingImage").hide(); },
                url: "AddNewStudent.aspx/GetACT",
                data: JSON.stringify(act),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = $.parseJSON(msg.d);
                    if (data != null) {
                        $("#MainContent_txtComposite").val(data.Composite);
                        $("#MainContent_txtEnglish").val(data.English);
                        $("#MainContent_txtMath").val(data.Math);
                        $("#MainContent_txtReading").val(data.Reading);
                        $("#MainContent_txtScience").val(data.Science);
                        $("#MainContent_txtWriting").val(data.Writing);
                        $("#MainContent_txtACTDate").val(data.Date);
                        $("#MainContent_hndACTID").val(data.StudentTestId);
                        $("#divACT").dialog({ modal: true, minWidth: 650, resizable: false, minHeight: 600, closeOnEscape: true, closeText: "" });
                    }
                },
                error: function (xhr, status, error) {
                    alert(xhr.statusText);
                }
            });

        }

        function ACTDelete(studentTestId) {
            $("#dialog-confirm").dialog({
                open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Yes": function () {
                        if (Page_IsValid) {
                            var HighSchool = {
                                StudentTestId: studentTestId
                            };
                            $.ajax({
                                type: "POST",
                                beforeSend: function () { $("#LoadingImage").show(); },
                                complete: function () { $("#LoadingImage").hide(); },
                                url: "AddNewStudent.aspx/ACTDelete",
                                data: JSON.stringify(HighSchool),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    $("#deleteact" + studentTestId).parent().parent().remove();
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

        $("#MainContent_btnSATSave").click(function () {
            var sid;
            if ($("#MainContent_hndNewStudentId").val() != 0) {
                sid = $("#MainContent_hndNewStudentId").val();
            }
            else {
                sid = $("#MainContent_hndStudentId").val();
            }
            if (Page_IsValid) {
                var sat = {
                    TestId: "3",
                    Reading: $("#MainContent_txtSATReading").val(),
                    Math: $("#MainContent_txtSATMath").val(),
                    Writing: $("#MainContent_txtSATWriting").val(),
                    Composite: $("#MainContent_txtSATComposite").val(),
                    Date: $("#MainContent_txtSATDate").val(),
                    StudentTestId: $("#MainContent_hndSATID").val(),
                    StudentId: sid
                }
                $.ajax({
                    type: "POST",
                    beforeSend: function () { $("#LoadingImage").show(); },
                    complete: function () { $("#LoadingImage").hide(); },
                    url: "AddNewStudent.aspx/SATSave",
                    data: JSON.stringify(sat),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //alert("Data Saved succesfully");

                        var data = msg.d;

                        if (data[2] == "add") {
                            if (document.getElementById("MainContent_dlSAT") != null) {
                                var v = '<br/><span><li><a id="editsat' + data[0] + '" href="javascript:OpenSATEdit(' + data[0] + ')">SAT-' + data[1] + '</a> <a href="javascript:SATDelete(' + data[0] + ')" id="deletesat' + data[0] + '"> Delete</a></li></span>';
                                $("#MainContent_dlSAT").append(v);
                            }
                            else {
                                var v = '<span id="MainContent_dlSAT"><span><li><a id="editsat' + data[0] + '" href="javascript:OpenSATEdit(' + data[0] + ')">SAT-' + data[1] + '</a> <a href="javascript:SATDelete(' + data[0] + ')" id="deletesat' + data[0] + '"> Delete</a></li></span></span>';
                                $("#ulSAT").append(v);
                            }
                        }
                        else if (data[2] == "edit") {
                            $("#editsat" + data[0]).text("SAT-" + data[1]);
                            window.location.href = "/Admin/AddNewStudent.aspx?sec=IntTest";

                        }
                        else { }
                        $("#LoadingImage").hide();
                        $("#MainContent_txtSATReading").val("");
                        $("#MainContent_txtSATMath").val("");
                        $("#MainContent_txtSATWriting").val("");
                        $("#MainContent_txtSATComposite").val("");
                        $("#MainContent_txtSATDate").val("");
                        $("#MainContent_hndSATId").val("0");
                        $("#MainContent_hndNewStudentId").val("0");
                        $("#divSAT").hide();
                        $("#divSAT").dialog('close');

                    }
                });
            }
            return false;
        });

        function OpenSATEdit(studentTestId) {
            var sat = {
                StudentTestId: studentTestId
            };
            $.ajax({
                type: "POST",
                beforeSend: function () { $("#LoadingImage").show(); },
                complete: function () { $("#LoadingImage").hide(); },
                url: "AddNewStudent.aspx/GetSAT",
                data: JSON.stringify(sat),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = $.parseJSON(msg.d);
                    if (data != null) {
                        $("#MainContent_txtSATReading").val(data.Reading);
                        $("#MainContent_txtSATMath").val(data.Math);
                        $("#MainContent_txtSATWriting").val(data.Writing);
                        $("#MainContent_txtSATComposite").val(data.Composite);
                        $("#MainContent_txtSATDate").val(data.Date);
                        $("#MainContent_hndSATID").val(data.StudentTestId);
                        $("#divSAT").dialog({ modal: true, minWidth: 650, resizable: false, minHeight: 600, closeOnEscape: true, closeText: "" });
                    }
                    // alert(data.StudentTestId);
                },
                error: function (xhr, status, error) {
                    alert(xhr.statusText);
                }
            });

        }

        function SATDelete(studentTestId) {
            $("#dialog-confirm").dialog({
                open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Yes": function () {
                        if (Page_IsValid) {
                            var HighSchool = {
                                StudentTestId: studentTestId
                            };
                            $.ajax({
                                type: "POST",
                                beforeSend: function () { $("#LoadingImage").show(); },
                                complete: function () { $("#LoadingImage").hide(); },
                                url: "AddNewStudent.aspx/SATDelete",
                                data: JSON.stringify(HighSchool),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    $("#deletesat" + studentTestId).parent().parent().remove();

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

        $("#MainContent_btnAPSave").click(function () {
            var sid;
            if ($("#MainContent_hndNewStudentId").val() != 0) {
                sid = $("#MainContent_hndNewStudentId").val();
            }
            else {
                sid = $("#MainContent_hndStudentId").val();
            }
            if (Page_IsValid) {
                var ap = {
                    TestId: "4",
                    Subject: $("#MainContent_txtAPSubject").val(),
                    Score: $("#MainContent_txtAPScore").val(),
                    Date: $("#MainContent_txtAPDate").val(),
                    StudentTestId: $("#MainContent_hndAPID").val(),
                    StudentId: sid
                };
                $.ajax({
                    type: "POST",
                    beforeSend: function () { $("#LoadingImage").show(); },
                    complete: function () { $("#LoadingImage").hide(); },
                    url: "AddNewStudent.aspx/APSave",
                    data: JSON.stringify(ap),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        // alert("Data Saved succesfully");

                        var data = msg.d;

                        if (data[2] == "add") {
                            if (document.getElementById("MainContent_dlAP") != null) {
                                var v = '<br/><span><li><a id="editap' + data[0] + '" href="javascript:OpenAPEdit(' + data[0] + ')">AP-' + data[1] + '</a> <a href="javascript:APDelete(' + data[0] + ')" id="deleteap' + data[0] + '"> Delete</a></li></span>';
                                $("#MainContent_dlAP").append(v);
                            } else {
                                var v = '<span id="MainContent_dlACT"><span><li><a id="editap' + data[0] + '" href="javascript:OpenAPEdit(' + data[0] + ')">AP-' + data[1] + '</a> <a href="javascript:APDelete(' + data[0] + ')" id="deleteap' + data[0] + '"> Delete</a></li></span></span>';
                                $("#ulAP").append(v);
                            }
                        }
                        else if (data[2] == "edit") {
                            $("#editap" + data[0]).text("AP-" + data[1]);
                            window.location.href = "/Admin/AddNewStudent.aspx?sec=IntTest";

                        }
                        else { }
                        $("#LoadingImage").hide();
                        $("#MainContent_txtAPSubject").val("");
                        $("#MainContent_txtAPScore").val("");
                        $("#MainContent_txtAPDate").val("");
                        $("#MainContent_hndAPID").val("0");
                        $("#MainContent_hndNewStudentId").val("0");
                        $("#divAP").hide();
                        $("#divAP").dialog('close');

                    }
                });
            }
            return false;
        });

        function OpenAPEdit(studentTestId) {
            var ap = {
                StudentTestId: studentTestId
            };
            $.ajax({
                type: "POST",
                beforeSend: function () { $("#LoadingImage").show(); },
                complete: function () { $("#LoadingImage").hide(); },
                url: "AddNewStudent.aspx/GetAP",
                data: JSON.stringify(ap),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = $.parseJSON(msg.d);
                    if (data != null) {
                        $("#MainContent_txtAPSubject").val(data.Subject);
                        $("#MainContent_txtAPScore").val(data.Score);
                        $("#MainContent_txtAPDate").val(data.Date);
                        $("#MainContent_hndAPID").val(data.StudentTestId);
                        $("#divAP").dialog({ modal: true, minWidth: 650, resizable: false, minHeight: 600, closeOnEscape: true, closeText: "" });
                    }
                },
                error: function (xhr, status, error) {
                    alert(xhr.statusText);
                }
            });

        }

        function APDelete(studentTestId) {
            $("#dialog-confirm").dialog({
                open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Yes": function () {
                        if (Page_IsValid) {
                            var ap = {
                                StudentTestId: studentTestId
                            };
                            $.ajax({
                                type: "POST",
                                beforeSend: function () { $("#LoadingImage").show(); },
                                complete: function () { $("#LoadingImage").hide(); },
                                url: "AddNewStudent.aspx/APDelete",
                                data: JSON.stringify(ap),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    $("#deleteap" + studentTestId).parent().parent().remove();

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

        $("#MainContent_btnGRESave").click(function () {
            var sid;
            if ($("#MainContent_hndNewStudentId").val() != 0) {
                sid = $("#MainContent_hndNewStudentId").val();
            }
            else {
                sid = $("#MainContent_hndStudentId").val();
            }
            if (Page_IsValid) {
                var gre = {
                    TestId: "5",
                    VerbalReasoning: $("#MainContent_txtGREVerbalReasoning").val(),
                    QuantitativeReasoning: $("#MainContent_txtGREQuantitativeReasoning").val(),
                    AnalyticalWriting: $("#MainContent_txtGREAnalyticalWriting").val(),
                    Date: $("#MainContent_txtGREDate").val(),
                    StudentTestId: $("#MainContent_hndGREID").val(),
                    StudentId: sid
                };
                $.ajax({
                    type: "POST",
                    beforeSend: function () { $("#LoadingImage").show(); },
                    complete: function () { $("#LoadingImage").hide(); },
                    url: "AddNewStudent.aspx/GRESave",
                    data: JSON.stringify(gre),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        // alert("Data Saved succesfully");

                        var data = msg.d;

                        if (data[2] == "add") {
                            if (document.getElementById("MainContent_dlGRE") != null) {
                                var v = '<br/><span><li><a id="editgre' + data[0] + '" href="javascript:OpenGREEdit(' + data[0] + ')">GRE-' + data[1] + '</a> <a href="javascript:GREDelete(' + data[0] + ')" id="deletegre' + data[0] + '"> Delete</a></li></span>';
                                $("#MainContent_dlGRE").append(v);
                            }
                            else {
                                var v = '<span id="ulGRE"><span><li><a id="editgre' + data[0] + '" href="javascript:OpenGREEdit(' + data[0] + ')">GRE-' + data[1] + '</a> <a href="javascript:GREDelete(' + data[0] + ')" id="deletegre' + data[0] + '"> Delete</a></li></span></span>';
                                $("#ulGRE").append(v);
                            }

                        }
                        else if (data[2] == "edit") {
                            $("#editgre" + data[0]).text("GRE-" + data[1]);
                            window.location.href = "/Admin/AddNewStudent.aspx?sec=IntTest";

                        }
                        else { }

                        $("#LoadingImage").hide();
                        $("#MainContent_txtGREVerbalReasoning").val("");
                        $("#MainContent_txtGREQuantitativeReasoning").val("");
                        $("#MainContent_txtGREAnalyticalWriting").val("");
                        $("#MainContent_txtGREDate").val("");
                        $("#MainContent_hndGREID").val("0");
                        $("#MainContent_hndNewStudentId").val("0");
                        $("#divGRE").hide();
                        $("#divGRE").dialog('close');

                    }
                });
            }
            return false;
        });

        function OpenGREEdit(studentTestId) {
            var gre = {
                StudentTestId: studentTestId
            };
            $.ajax({
                type: "POST",
                beforeSend: function () { $("#LoadingImage").show(); },
                complete: function () { $("#LoadingImage").hide(); },
                url: "AddNewStudent.aspx/GetAP",
                data: JSON.stringify(gre),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = $.parseJSON(msg.d);
                    if (data != null) {
                        $("#MainContent_txtGREVerbalReasoning").val(data.VerbalReasoning);
                        $("#MainContent_txtGREQuantitativeReasoning").val(data.QuantitativeReasoning);
                        $("#MainContent_txtGREAnalyticalWriting").val(data.AnalyticalWriting);
                        $("#MainContent_txtGREDate").val(data.Date);
                        $("#MainContent_hndGREID").val(data.StudentTestId);
                        $("#divGRE").dialog({ modal: true, minWidth: 650, resizable: false, minHeight: 600, closeOnEscape: true, closeText: "" });
                    }
                },
                error: function (xhr, status, error) {
                    alert(xhr.statusText);
                }
            });

        }

        function GREDelete(studentTestId) {
            $("#dialog-confirm").dialog({
                open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Yes": function () {
                        if (Page_IsValid) {
                            var gre = {
                                StudentTestId: studentTestId
                            };
                            $.ajax({
                                type: "POST",
                                beforeSend: function () { $("#LoadingImage").show(); },
                                complete: function () { $("#LoadingImage").hide(); },
                                url: "AddNewStudent.aspx/APDelete",
                                data: JSON.stringify(gre),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    $("#deletegre" + studentTestId).parent().parent().remove();

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

        $("#MainContent_btnGMATSave").click(function () {
            var sid;
            if ($("#MainContent_hndNewStudentId").val() != 0) {
                sid = $("#MainContent_hndNewStudentId").val();
            }
            else {
                sid = $("#MainContent_hndStudentId").val();
            }
            if (Page_IsValid) {
                var gmat = {
                    TestId: "6",
                    Verbal: $("#MainContent_txtGMATVerbal").val(),
                    QuantitativeReasoning: $("#MainContent_txtGMATQuantitative").val(),
                    Total: $("#MainContent_txtGMATTotal").val(),
                    AnalyticalWriting: $("#MainContent_txtGMATAnalyticalWriting").val(),
                    Date: $("#MainContent_txtGMATDate").val(),
                    StudentTestId: $("#MainContent_hndGMATID").val(),
                    StudentId: sid
                };
                $.ajax({
                    type: "POST",
                    beforeSend: function () { $("#LoadingImage").show(); },
                    complete: function () { $("#LoadingImage").hide(); },
                    url: "AddNewStudent.aspx/GMATSave",
                    data: JSON.stringify(gmat),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        // alert("Data Saved succesfully");

                        var data = msg.d;

                        if (data[2] == "add") {
                            if (document.getElementById("MainContent_dlGMAT") != null) {
                                var v = '<br/><span><li><a id="editgmat' + data[0] + '" href="javascript:OpenGMATEdit(' + data[0] + ')">GMAT-' + data[1] + '</a> <a href="javascript:GMATDelete(' + data[0] + ')" id="deletegmat' + data[0] + '"> Delete</a></li></span>';
                                $("#MainContent_dlGMAT").append(v);
                            }
                            else {
                                var v = '<span id="MainContent_dlGMAT"><span><li><a id="editgmat' + data[0] + '" href="javascript:OpenGMATEdit(' + data[0] + ')">GMAT-' + data[1] + '</a> <a href="javascript:GMATDelete(' + data[0] + ')" id="deletegmat' + data[0] + '"> Delete</a></li></span></span>';
                                $("#ulGMAT").append(v);
                            }
                        }
                        else if (data[2] == "edit") {
                            $("#editgmat" + data[0]).text("GMAT-" + data[1]);
                            window.location.href = "/Admin/AddNewStudent.aspx?sec=IntTest";

                        }
                        else { }
                        $("#LoadingImage").hide();
                        $("#MainContent_txtGMATVerbal").val("");
                        $("#MainContent_txtGMATQuantitative").val("");
                        $("#MainContent_txtGMATTotal").val("");
                        $("#MainContent_txtGMATAnalyticalWriting").val("");
                        $("#MainContent_txtGMATDate").val("");
                        $("#MainContent_hndGMATID").val("0");
                        $("#MainContent_hndNewStudentId").val("0");
                        $("#divGMAT").hide();
                        $("#divGMAT").dialog('close');

                    }
                });
            }
            return false;
        });

        function OpenGMATEdit(studentTestId) {
            var gmat = {
                StudentTestId: studentTestId
            };
            $.ajax({
                type: "POST",
                beforeSend: function () { $("#LoadingImage").show(); },
                complete: function () { $("#LoadingImage").hide(); },
                url: "AddNewStudent.aspx/GetAP",
                data: JSON.stringify(gmat),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = $.parseJSON(msg.d);
                    if (data != null) {
                        $("#MainContent_txtGMATVerbal").val(data.Verbal);
                        $("#MainContent_txtGMATQuantitative").val(data.QuantitativeReasoning);
                        $("#MainContent_txtGMATTotal").val(data.Total);
                        $("#MainContent_txtGMATAnalyticalWriting").val(data.AnalyticalWriting);
                        $("#MainContent_txtGMATDate").val(data.Date);
                        $("#MainContent_hndGMATID").val(data.StudentTestId);
                        $("#divGMAT").dialog({ modal: true, minWidth: 650, resizable: false, minHeight: 600, closeOnEscape: true, closeText: "" });
                    }
                },
                error: function (xhr, status, error) {
                    alert(xhr.statusText);
                }
            });

        }

        function GMATDelete(studentTestId) {
            $("#dialog-confirm").dialog({
                open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Yes": function () {
                        if (Page_IsValid) {
                            var gmat = {
                                StudentTestId: studentTestId
                            };
                            $.ajax({
                                type: "POST",
                                beforeSend: function () { $("#LoadingImage").show(); },
                                complete: function () { $("#LoadingImage").hide(); },
                                url: "AddNewStudent.aspx/APDelete",
                                data: JSON.stringify(gmat),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    $("#deletegmat" + studentTestId).parent().parent().remove();

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


        $("#MainContent_btnIBSave").click(function () {
            var sid;
            if ($("#MainContent_hndNewStudentId").val() != 0) {
                sid = $("#MainContent_hndNewStudentId").val();
            }
            else {
                sid = $("#MainContent_hndStudentId").val();
            }
            if (Page_IsValid) {
                var ib = {
                    TestId: "7",
                    Subject: $("#MainContent_txtIBSubject").val(),
                    Score: $("#MainContent_txtIBScore").val(),
                    Date: $("#MainContent_txtIBDate").val(),
                    StudentTestId: $("#MainContent_hndIBID").val(),
                    StudentId: sid
                };
                $.ajax({
                    type: "POST",
                    beforeSend: function () { $("#LoadingImage").show(); },
                    complete: function () { $("#LoadingImage").hide(); },
                    url: "AddNewStudent.aspx/IBSave",
                    data: JSON.stringify(ib),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //alert("Data Saved succesfully");

                        var data = msg.d;

                        if (data[2] == "add") {
                            if (document.getElementById("MainContent_dlIB") != null) {
                                var v = '<br/><span><li><a id="editib' + data[0] + '" href="javascript:OpenIBEdit(' + data[0] + ')">IB-' + data[1] + '</a> <a href="javascript:IBDelete(' + data[0] + ')" id="deleteib' + data[0] + '"> Delete</a></li></span>';
                                $("#MainContent_dlIB").append(v);
                            }
                            else {
                                var v = '<span id="MainContent_dlIB"><span><li><a id="editib' + data[0] + '" href="javascript:OpenIBEdit(' + data[0] + ')">IB-' + data[1] + '</a> <a href="javascript:IBDelete(' + data[0] + ')" id="deleteib' + data[0] + '"> Delete</a></li></span></span>';
                                $("#ulIB").append(v);
                            }
                        }
                        else if (data[2] == "edit") {
                            $("#editib" + data[0]).text("IB-" + data[1]);
                            window.location.href = "/Admin/AddNewStudent.aspx?sec=IntTest";

                        }
                        else { }
                        $("#LoadingImage").hide();
                        $("#MainContent_txtIBSubject").val("");
                        $("#MainContent_txtIBScore").val("");
                        $("#MainContent_txtIBDate").val("");
                        $("#MainContent_hndIBID").val("0");
                        $("#MainContent_hndNewStudentId").val("0");
                        $("#divIB").hide();
                        $("#divIB").dialog('close');

                    }
                });
            }
            return false;
        });

        function OpenIBEdit(studentTestId) {
            var ib = {
                StudentTestId: studentTestId
            };
            $.ajax({
                type: "POST",
                beforeSend: function () { $("#LoadingImage").show(); },
                complete: function () { $("#LoadingImage").hide(); },
                url: "AddNewStudent.aspx/GetAP",
                data: JSON.stringify(ib),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = $.parseJSON(msg.d);
                    if (data != null) {
                        $("#MainContent_txtIBSubject").val(data.Subject);
                        $("#MainContent_txtIBScore").val(data.Score);
                        $("#MainContent_txtIBDate").val(data.Date);
                        $("#MainContent_hndIBID").val(data.StudentTestId);
                        $("#divIB").dialog({ modal: true, minWidth: 650, resizable: false, minHeight: 600, closeOnEscape: true, closeText: "" });
                    }
                },
                error: function (xhr, status, error) {
                    alert(xhr.statusText);
                }
            });

        }

        function IBDelete(studentTestId) {
            $("#dialog-confirm").dialog({
                open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Yes": function () {
                        if (Page_IsValid) {
                            var ib = {
                                StudentTestId: studentTestId
                            };
                            $.ajax({
                                type: "POST",
                                beforeSend: function () { $("#LoadingImage").show(); },
                                complete: function () { $("#LoadingImage").hide(); },
                                url: "AddNewStudent.aspx/APDelete",
                                data: JSON.stringify(ib),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    $("#deleteib" + studentTestId).parent().parent().remove();

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

        $("#MainContent_btnIELTSSave").click(function () {
            var sid;
            if ($("#MainContent_hndNewStudentId").val() != 0) {
                sid = $("#MainContent_hndNewStudentId").val();
            }
            else {
                sid = $("#MainContent_hndStudentId").val();
            }
            if (Page_IsValid) {
                var ielts = {
                    TestId: "8",
                    Listening: $("#MainContent_txtIELTSListening").val(),
                    Reading: $("#MainContent_txtIELTSReading").val(),
                    Writing: $("#MainContent_txtIELTSWriting").val(),
                    Speaking: $("#MainContent_txtIELTSSpeaking").val(),
                    Date: $("#MainContent_txtIELTSDate").val(),
                    StudentTestId: $("#MainContent_hndIELTSID").val(),
                    StudentId: sid
                };
                $.ajax({
                    type: "POST",
                    beforeSend: function () { $("#LoadingImage").show(); },
                    complete: function () { $("#LoadingImage").hide(); },
                    url: "AddNewStudent.aspx/IELTSSave",
                    data: JSON.stringify(ielts),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        // alert("Data Saved succesfully");

                        var data = msg.d;

                        if (data[2] == "add") {
                            if (document.getElementById("MainContent_dlIELTS") != null) {
                                var v = '<br/><span><li><a id="editielts' + data[0] + '" href="javascript:OpenIELTSEdit(' + data[0] + ')">IELTS-' + data[1] + '</a> <a href="javascript:IELTSDelete(' + data[0] + ')" id="deleteielts' + data[0] + '"> Delete</a></li></span>';
                                $("#MainContent_dlIELTS").append(v);
                            }
                            else {
                                var v = '<span id="MainContent_dlIELTS"><span><li><a id="editielts' + data[0] + '" href="javascript:OpenIELTSEdit(' + data[0] + ')">IELTS-' + data[1] + '</a> <a href="javascript:IELTSDelete(' + data[0] + ')" id="deleteielts' + data[0] + '"> Delete</a></li></span></span>';
                                $("#ulIELTS").append(v);
                            }
                        }
                        else if (data[2] == "edit") {
                            $("#editielts" + data[0]).text("IB-" + data[1]);
                            window.location.href = "/Admin/AddNewStudent.aspx?sec=IntTest";

                        }
                        else { }
                        $("#LoadingImage").hide();
                        $("#MainContent_txtIELTSListening").val("");
                        $("#MainContent_txtIELTSReading").val("");
                        $("#MainContent_txtIELTSWriting").val("");
                        $("#MainContent_txtIELTSSpeaking").val("");
                        $("#MainContent_txtIELTSDate").val("");
                        $("#MainContent_hndIELTSID").val("0");
                        $("#MainContent_hndNewStudentId").val("0");
                        $("#divIELTS").hide();
                        $("#divIELTS").dialog('close');

                    }
                });
            }
            return false;
        });

        function OpenIELTSEdit(studentTestId) {
            var ielts = {
                StudentTestId: studentTestId
            };
            $.ajax({
                type: "POST",
                beforeSend: function () { $("#LoadingImage").show(); },
                complete: function () { $("#LoadingImage").hide(); },
                url: "AddNewStudent.aspx/GetAP",
                data: JSON.stringify(ielts),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = $.parseJSON(msg.d);
                    if (data != null) {

                        $("#MainContent_txtIELTSListening").val(data.Listening);
                        $("#MainContent_txtIELTSReading").val(data.Reading);
                        $("#MainContent_txtIELTSWriting").val(data.Writing);
                        $("#MainContent_txtIELTSSpeaking").val(data.Speaking);
                        $("#MainContent_txtIELTSDate").val(data.Date);
                        $("#MainContent_hndIELTSID").val(data.StudentTestId);
                        $("#divIELTS").dialog({ modal: true, minWidth: 650, resizable: false, minHeight: 600, closeOnEscape: true, closeText: "" });
                    }
                },
                error: function (xhr, status, error) {
                    alert(xhr.statusText);
                }
            });

        }

        function IELTSDelete(studentTestId) {
            $("#dialog-confirm").dialog({
                open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Yes": function () {
                        if (Page_IsValid) {
                            var ielts = {
                                StudentTestId: studentTestId
                            };
                            $.ajax({
                                type: "POST",
                                beforeSend: function () { $("#LoadingImage").show(); },
                                complete: function () { $("#LoadingImage").hide(); },
                                url: "AddNewStudent.aspx/APDelete",
                                data: JSON.stringify(ielts),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    $("#deleteielts" + studentTestId).parent().parent().remove();

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

        $("#MainContent_btnLSATSave").click(function () {
            var sid;
            if ($("#MainContent_hndNewStudentId").val() != 0) {
                sid = $("#MainContent_hndNewStudentId").val();
            }
            else {
                sid = $("#MainContent_hndStudentId").val();
            }
            if (Page_IsValid) {
                var lsat = {
                    TestId: "9",
                    Score: $("#MainContent_txtLSATScore").val(),
                    Date: $("#MainContent_txtLSATDate").val(),
                    StudentTestId: $("#MainContent_hndLSATID").val(),
                    StudentId: sid
                };
                $.ajax({
                    type: "POST",
                    beforeSend: function () { $("#LoadingImage").show(); },
                    complete: function () { $("#LoadingImage").hide(); },
                    url: "AddNewStudent.aspx/LSATSave",
                    data: JSON.stringify(lsat),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        // alert("Data Saved succesfully");
                        var data = msg.d;

                        if (data[2] == "add") {
                            if (document.getElementById("MainContent_dlLSAT") != null) {
                                var v = '<br/><span><li><a id="editlsat' + data[0] + '" href="javascript:OpenLSATEdit(' + data[0] + ')">LSAT-' + data[1] + '</a> <a href="javascript:LSATDelete(' + data[0] + ')" id="deletelsat' + data[0] + '"> Delete</a></li></span>';
                                $("#MainContent_dlLSAT").append(v);
                            }
                            else {
                                var v = '<span id="MainContent_dlLSAT"><span><li><a id="editlsat' + data[0] + '" href="javascript:OpenLSATEdit(' + data[0] + ')">LSAT-' + data[1] + '</a> <a href="javascript:LSATDelete(' + data[0] + ')" id="deletelsat' + data[0] + '"> Delete</a></li></span></span>';
                                $("#ulLSAT").append(v);
                            }
                        }
                        else if (data[2] == "edit") {
                            $("#editlsat" + data[0]).text("IB-" + data[1]);
                            window.location.href = "/Admin/AddNewStudent.aspx?sec=IntTest";

                        }
                        else { }
                        $("#LoadingImage").hide();
                        $("#MainContent_txtLSATScore").val("");
                        $("#MainContent_txtLSATDate").val("");
                        $("#MainContent_hndLSATID").val("0");
                        $("#MainContent_hndNewStudentId").val("0");
                        $("#divLSAT").hide();
                        $("#divLSAT").dialog('close');

                    }
                });
            }
            return false;
        });

        function OpenLSATEdit(studentTestId) {
            var lsat = {
                StudentTestId: studentTestId
            };
            $.ajax({
                type: "POST",
                beforeSend: function () { $("#LoadingImage").show(); },
                complete: function () { $("#LoadingImage").hide(); },
                url: "AddNewStudent.aspx/GetAP",
                data: JSON.stringify(lsat),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = $.parseJSON(msg.d);
                    if (data != null) {
                        $("#MainContent_txtLSATScore").val(data.Score);
                        $("#MainContent_txtLSATDate").val(data.Date);
                        $("#MainContent_hndLSATID").val(data.StudentTestId);
                        $("#divLSAT").dialog({ modal: true, minWidth: 650, resizable: false, minHeight: 600, closeOnEscape: true, closeText: "" });
                    }
                },
                error: function (xhr, status, error) {
                    alert(xhr.statusText);
                }
            });

        }

        function LSATDelete(studentTestId) {
            $("#dialog-confirm").dialog({
                open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Yes": function () {
                        if (Page_IsValid) {
                            var lsat = {
                                StudentTestId: studentTestId
                            };
                            $.ajax({
                                type: "POST",
                                beforeSend: function () { $("#LoadingImage").show(); },
                                complete: function () { $("#LoadingImage").hide(); },
                                url: "AddNewStudent.aspx/APDelete",
                                data: JSON.stringify(lsat),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    $("#deletelsat" + studentTestId).parent().parent().remove();

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

        $("#MainContent_btnMCATSave").click(function () {
            var sid;
            if ($("#MainContent_hndNewStudentId").val() != 0) {
                sid = $("#MainContent_hndNewStudentId").val();
            }
            else {
                sid = $("#MainContent_hndStudentId").val();
            }
            if (Page_IsValid) {
                var mcat = {
                    TestId: "10",
                    Biology: $("#MainContent_txtMCATBiology").val(),
                    Physics: $("#MainContent_txtMCATPhysics").val(),
                    Verbal: $("#MainContent_txtMCATVerbal").val(),
                    Writing: $("#MainContent_txtMCATWriting").val(),
                    Date: $("#MainContent_txtMCATDate").val(),
                    StudentTestId: $("#MainContent_hndMCATID").val(),
                    StudentId: sid
                };
                $.ajax({
                    type: "POST",
                    beforeSend: function () { $("#LoadingImage").show(); },
                    complete: function () { $("#LoadingImage").hide(); },
                    url: "AddNewStudent.aspx/MCATSave",
                    data: JSON.stringify(mcat),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        // alert("Data Saved succesfully");
                        var data = msg.d;

                        if (data[2] == "add") {
                            if (document.getElementById("MainContent_dlMCAT") != null) {
                                var v = '<br/><span><li><a id="editmcat' + data[0] + '" href="javascript:OpenLMCATEdit(' + data[0] + ')">MCAT-' + data[1] + '</a> <a href="javascriptMCATDelete(' + data[0] + ')" id="deletemcat' + data[0] + '"> Delete</a></li></span>';
                                $("#MainContent_dlMCAT").append(v);
                            }
                            else {
                                var v = '<span id="MainContent_dlMCAT"><span><li><a id="editmcat' + data[0] + '" href="javascript:OpenMCATEdit(' + data[0] + ')">MCAT-' + data[1] + '</a> <a href="javascript:MCATDelete(' + data[0] + ')" id="deletemcat' + data[0] + '"> Delete</a></li></span></span>';
                                $("#ulMCAT").append(v);
                            }
                        }
                        else if (data[2] == "edit") {
                            $("#editmcat" + data[0]).text("MCAT-" + data[1]);
                            window.location.href = "/Admin/AddNewStudent.aspx?sec=IntTest";

                        }
                        else { }
                        $("#LoadingImage").hide();
                        $("#MainContent_txtMCATBiology").val("");
                        $("#MainContent_txtMCATPhysics").val("");
                        $("#MainContent_txtMCATVerbal").val("");
                        $("#MainContent_txtMCATWriting").val("");
                        $("#MainContent_txtMCATDate").val("");
                        $("#MainContent_hndMCATID").val("0");
                        $("#MainContent_hndNewStudentId").val("0");
                        $("#divMCAT").hide();
                        $("#divMCAT").dialog('close');

                    }
                });
            }
            return false;
        });

        function OpenMCATEdit(studentTestId) {
            var mcat = {
                StudentTestId: studentTestId
            };
            $.ajax({
                type: "POST",
                beforeSend: function () { $("#LoadingImage").show(); },
                complete: function () { $("#LoadingImage").hide(); },
                url: "AddNewStudent.aspx/GetAP",
                data: JSON.stringify(mcat),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = $.parseJSON(msg.d);
                    if (data != null) {
                        $("#MainContent_txtMCATBiology").val(data.Biology);
                        $("#MainContent_txtMCATPhysics").val(data.Physics);
                        $("#MainContent_txtMCATVerbal").val(data.Verbal);
                        $("#MainContent_txtMCATWriting").val(data.Writing);
                        $("#MainContent_txtMCATDate").val(data.Date);
                        $("#MainContent_hndMCATID").val(data.StudentTestId);
                        $("#divMCAT").dialog({ modal: true, minWidth: 650, resizable: false, minHeight: 600, closeOnEscape: true, closeText: "" });
                    }
                },
                error: function (xhr, status, error) {
                    alert(xhr.statusText);
                }
            });

        }

        function MCATDelete(studentTestId) {
            $("#dialog-confirm").dialog({
                open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Yes": function () {
                        if (Page_IsValid) {
                            var mcat = {
                                StudentTestId: studentTestId
                            };
                            $.ajax({
                                type: "POST",
                                beforeSend: function () { $("#LoadingImage").show(); },
                                complete: function () { $("#LoadingImage").hide(); },
                                url: "AddNewStudent.aspx/APDelete",
                                data: JSON.stringify(mcat),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    $("#deletemcat" + studentTestId).parent().parent().remove();

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

        $("#MainContent_btnPSATSave").click(function () {
            var sid;
            if ($("#MainContent_hndNewStudentId").val() != 0) {
                sid = $("#MainContent_hndNewStudentId").val();
            }
            else {
                sid = $("#MainContent_hndStudentId").val();
            }
            if (Page_IsValid) {
                var psat = {
                    TestId: "11",
                    Reading: $("#MainContent_txtPSATReading").val(),
                    Math: $("#MainContent_txtPSATMath").val(),
                    Writing: $("#MainContent_txtPSATWriting").val(),
                    Date: $("#MainContent_txtPSATDate").val(),
                    StudentTestId: $("#MainContent_hndPSATID").val(),
                    StudentId: sid
                };
                $.ajax({
                    type: "POST",
                    beforeSend: function () { $("#LoadingImage").show(); },
                    complete: function () { $("#LoadingImage").hide(); },
                    url: "AddNewStudent.aspx/PSATSave",
                    data: JSON.stringify(psat),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        // alert("Data Saved succesfully");
                        var data = msg.d;

                        if (data[2] == "add") {
                            if (document.getElementById("MainContent_dlPSAT") != null) {
                                var v = '<br/><span><li><a id="editpsat' + data[0] + '" href="javascript:OpenPSATEdit(' + data[0] + ')">PSAT-' + data[1] + '</a> <a href="javascriptPSATDelete(' + data[0] + ')" id="deletepsat' + data[0] + '"> Delete</a></li></span>';
                                $("#MainContent_dlPSAT").append(v);
                            }
                            else {
                                var v = '<span id="MainContent_dlPSAT"><span><li><a id="editpsat' + data[0] + '" href="javascript:OpenPSATEdit(' + data[0] + ')">PSAT-' + data[1] + '</a> <a href="javascript:PSATDelete(' + data[0] + ')" id="deletepsat' + data[0] + '"> Delete</a></li></span></span>';
                                $("#ulPSAT").append(v);
                            }
                        }
                        else if (data[2] == "edit") {
                            $("#editpsat" + data[0]).text("PSAT-" + data[1]);
                            window.location.href = "/Admin/AddNewStudent.aspx?sec=IntTest";

                        }
                        else { }
                        $("#LoadingImage").hide();
                        $("#MainContent_txtPSATReading").val("");
                        $("#MainContent_txtPSATMath").val("");
                        $("#MainContent_txtPSATWriting").val("");
                        $("#MainContent_txtPSATDate").val("");
                        $("#MainContent_hndPSATID").val("0");
                        $("#MainContent_hndNewStudentId").val("0");
                        $("#divPSAT").hide();
                        $("#divPSAT").dialog('close');

                    }
                });
            }
            return false;
        });

        function OpenPSATEdit(studentTestId) {
            var psat = {
                StudentTestId: studentTestId
            };
            $.ajax({
                type: "POST",
                beforeSend: function () { $("#LoadingImage").show(); },
                complete: function () { $("#LoadingImage").hide(); },
                url: "AddNewStudent.aspx/GetAP",
                data: JSON.stringify(psat),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = $.parseJSON(msg.d);
                    if (data != null) {
                        $("#MainContent_txtPSATReading").val(data.Reading);
                        $("#MainContent_txtPSATMath").val(data.Math);
                        $("#MainContent_txtPSATWriting").val(data.Writing);
                        $("#MainContent_txtPSATDate").val(data.Date);
                        $("#MainContent_hndPSATID").val(data.StudentTestId);
                        $("#divPSAT").dialog({ modal: true, minWidth: 650, resizable: false, minHeight: 600, closeOnEscape: true, closeText: "" });
                    }
                },
                error: function (xhr, status, error) {
                    alert(xhr.statusText);
                }
            });

        }

        function PSATDelete(studentTestId) {
            $("#dialog-confirm").dialog({
                open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Yes": function () {
                        if (Page_IsValid) {
                            var psat = {
                                StudentTestId: studentTestId
                            };
                            $.ajax({
                                type: "POST",
                                beforeSend: function () { $("#LoadingImage").show(); },
                                complete: function () { $("#LoadingImage").hide(); },
                                url: "AddNewStudent.aspx/APDelete",
                                data: JSON.stringify(psat),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    $("#deletepsat" + studentTestId).parent().parent().remove();

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

        $("#MainContent_btnSAT_IISave").click(function () {
            var sid;
            if ($("#MainContent_hndNewStudentId").val() != 0) {
                sid = $("#MainContent_hndNewStudentId").val();
            }
            else {
                sid = $("#MainContent_hndStudentId").val();
            }
            if (Page_IsValid) {
                var sat2 = {
                    TestId: "12",
                    Subject: $("#MainContent_txtSAT_IISubject").val(),
                    Score: $("#MainContent_txtSAT_IIScore").val(),
                    Date: $("#MainContent_txtSAT_IIDate").val(),
                    StudentTestId: $("#MainContent_hndSAT2ID").val(),
                    StudentId: sid
                };
                $.ajax({
                    type: "POST",
                    beforeSend: function () { $("#LoadingImage").show(); },
                    complete: function () { $("#LoadingImage").hide(); },
                    url: "EditProfile.aspx/SAT2Save",
                    data: JSON.stringify(sat2),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        // alert("Data Saved succesfully");

                        var data = msg.d;

                        if (data[2] == "add") {
                            if (document.getElementById("MainContent_dlSAT2") != null) {
                                var v = '<br/><span><li><a id="editsat2' + data[0] + '" href="javascript:OpenSAT2Edit(' + data[0] + ')">SAT-II-' + data[1] + '</a> <a href="javascript:SAT2Delete(' + data[0] + ')" id="deletesat2' + data[0] + '"> Delete</a></li></span>';
                                $("#MainContent_dlSAT2").append(v);
                            }
                            else {
                                var v = '<span id="MainContent_dlSAT2"><span><li><a id="editsat2' + data[0] + '" href="javascript:OpenSAT2Edit(' + data[0] + ')">SAT-II-' + data[1] + '</a> <a href="javascript:SAT2Delete(' + data[0] + ')" id="deletesat2' + data[0] + '"> Delete</a></li></span></span>';
                                $("#ulSAT2").append(v);
                            }
                        }
                        else if (data[2] == "edit") {
                            $("#editsat2" + data[0]).text("SAT-II-" + data[1]);
                            window.location.href = "/Admin/AddNewStudent.aspx?sec=IntTest";

                        }
                        else { }
                        $("#LoadingImage").hide();
                        $("#MainContent_txtSAT_IISubject").val("");
                        $("#MainContent_txtSAT_IIScore").val("");
                        $("#MainContent_txtSAT_IIDate").val("");
                        $("#MainContent_hndSAT2ID").val("0");
                        $("#MainContent_hndNewStudentId").val("0");
                        $("#divSAT2").hide();
                        $("#divSAT2").dialog('close');

                    }
                });
            }
            return false;
        });

        function OpenSAT2Edit(studentTestId) {
            var sat2 = {
                StudentTestId: studentTestId
            };
            $.ajax({
                type: "POST",
                beforeSend: function () { $("#LoadingImage").show(); },
                complete: function () { $("#LoadingImage").hide(); },
                url: "AddNewStudent.aspx/GetAP",
                data: JSON.stringify(sat2),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = $.parseJSON(msg.d);
                    if (data != null) {
                        $("#MainContent_txtSAT_IISubject").val(data.Subject);
                        $("#MainContent_txtSAT_IIScore").val(data.Score);
                        $("#MainContent_txtSAT_IIDate").val(data.Date);
                        $("#MainContent_hndSAT2ID").val(data.StudentTestId);
                        $("#divSAT2").dialog({ modal: true, minWidth: 650, resizable: false, minHeight: 600, closeOnEscape: true, closeText: "" });
                    }
                },
                error: function (xhr, status, error) {
                    alert(xhr.statusText);
                }
            });

        }

        function SAT2Delete(studentTestId) {
            $("#dialog-confirm").dialog({
                open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Yes": function () {
                        if (Page_IsValid) {
                            var sat2 = {
                                StudentTestId: studentTestId
                            };
                            $.ajax({
                                type: "POST",
                                beforeSend: function () { $("#LoadingImage").show(); },
                                complete: function () { $("#LoadingImage").hide(); },
                                url: "AddNewStudent.aspx/APDelete",
                                data: JSON.stringify(sat2),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    $("#deletesat2" + studentTestId).parent().parent().remove();

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

        $("#MainContent_btnTOEFLInternetbasedSave").click(function () {
            var sid;
            if ($("#MainContent_hndNewStudentId").val() != 0) {
                sid = $("#MainContent_hndNewStudentId").val();
            }
            else {
                sid = $("#MainContent_hndStudentId").val();
            }
            if (Page_IsValid) {
                var toeflinternet = {
                    TestId: "13",
                    Reading: $("#MainContent_txtTOEFLInternetbasedReading").val(),
                    Listening: $("#MainContent_txtTOEFLInternetbasedListening").val(),
                    Speaking: $("#MainContent_txtTOEFLInternetbasedSpeaking").val(),
                    Writing: $("#MainContent_txtTOEFLInternetbasedWriting").val(),
                    Total: $("#MainContent_txtTOEFLInternetbasedTotal").val(),
                    Date: $("#MainContent_txtTOEFLInternetbasedDate").val(),
                    StudentTestId: $("#MainContent_hndTOEFLInternetbasedID").val(),
                    StudentId: sid
                };
                $.ajax({
                    type: "POST",
                    beforeSend: function () { $("#LoadingImage").show(); },
                    complete: function () { $("#LoadingImage").hide(); },
                    url: "AddNewStudent.aspx/TOEFLInternetbasedSave",
                    data: JSON.stringify(toeflinternet),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        // alert("Data Saved succesfully");

                        var data = msg.d;

                        if (data[2] == "add") {
                            if (document.getElementById("MainContent_dlTOEFLInternetbased") != null) {
                                var v = '<br/><span><li><a id="edittoefli' + data[0] + '" href="javascript:OpenTOEFLInternetbasedEdit(' + data[0] + ')">TOEFLInternetbased-' + data[1] + '</a> <a href="javascript:TOEFLInternetbasedDelete(' + data[0] + ')" id="deletetoefli' + data[0] + '"> Delete</a></li></span>';
                                $("#MainContent_dlTOEFLInternetbased").append(v);
                            }
                            else {
                                var v = '<span id="MainContent_dlTOEFLInternetbased"><span><li><a id="edittoefli' + data[0] + '" href="javascript:OpenTOEFLInternetbasedEdit(' + data[0] + ')">TOEFLInternetbased-' + data[1] + '</a> <a href="javascript:TOEFLInternetbasedDelete(' + data[0] + ')" id="deletetoefli' + data[0] + '"> Delete</a></li></span></span>';
                                $("#ulTOEFLInternetbased").append(v);
                            }
                        }
                        else if (data[2] == "edit") {
                            $("#edittoefli" + data[0]).text("TOEFLInternetbased-" + data[1]);
                            window.location.href = "/Admin/AddNewStudent.aspx?sec=IntTest";

                        }
                        else { }
                        $("#LoadingImage").hide();
                        $("#MainContent_txtTOEFLInternetbasedReading").val("");
                        $("#MainContent_txtTOEFLInternetbasedListening").val("");
                        $("#MainContent_txtTOEFLInternetbasedSpeaking").val("");
                        $("#MainContent_txtTOEFLInternetbasedWriting").val("");
                        $("#MainContent_txtTOEFLInternetbasedTotal").val("");
                        $("#MainContent_txtTOEFLInternetbasedDate").val("");
                        $("#MainContent_hndTOEFLInternetbasedID").val("0");
                        $("#MainContent_hndNewStudentId").val("0");
                        $("#divTOEFLInternetbased").hide();
                        $("#divTOEFLInternetbased").dialog('close');

                    }
                });
            }
            return false;
        });

        function OpenTOEFLInternetbasedEdit(studentTestId) {
            var toeflinternet = {
                StudentTestId: studentTestId
            };
            $.ajax({
                type: "POST",
                beforeSend: function () { $("#LoadingImage").show(); },
                complete: function () { $("#LoadingImage").hide(); },
                url: "AddNewStudent.aspx/GetAP",
                data: JSON.stringify(toeflinternet),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = $.parseJSON(msg.d);
                    if (data != null) {
                        $("#MainContent_txtTOEFLInternetbasedReading").val(data.Reading);
                        $("#MainContent_txtTOEFLInternetbasedListening").val(data.Listening);
                        $("#MainContent_txtTOEFLInternetbasedSpeaking").val(data.Speaking);
                        $("#MainContent_txtTOEFLInternetbasedWriting").val(data.Writing);
                        $("#MainContent_txtTOEFLInternetbasedTotal").val(data.Total);
                        $("#MainContent_txtTOEFLInternetbasedDate").val(data.Date);
                        $("#MainContent_hndTOEFLInternetbasedID").val(data.StudentTestId);
                        $("#divTOEFLInternetbased").dialog({ modal: true, minWidth: 650, resizable: false, minHeight: 600, closeOnEscape: true, closeText: "" });
                    }
                },
                error: function (xhr, status, error) {
                    alert(xhr.statusText);
                }
            });

        }

        function TOEFLInternetbasedDelete(studentTestId) {
            $("#dialog-confirm").dialog({
                open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Yes": function () {
                        if (Page_IsValid) {
                            var toeflinternet = {
                                StudentTestId: studentTestId
                            };
                            $.ajax({
                                type: "POST",
                                beforeSend: function () { $("#LoadingImage").show(); },
                                complete: function () { $("#LoadingImage").hide(); },
                                url: "AddNewStudent.aspx/APDelete",
                                data: JSON.stringify(toeflinternet),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    $("#deletetoefli" + studentTestId).parent().parent().remove();

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

        $("#MainContent_btnTOEFLPaperbasedSave").click(function () {
            var sid;
            if ($("#MainContent_hndNewStudentId").val() != 0) {
                sid = $("#MainContent_hndNewStudentId").val();
            }
            else {
                sid = $("#MainContent_hndStudentId").val();
            }
            if (Page_IsValid) {
                var toeflpaper = {
                    TestId: "14",
                    Reading: $("#MainContent_txtTOEFLPaperbasedReading").val(),
                    Listening: $("#MainContent_txtTOEFLPaperbasedListening").val(),
                    Speaking: $("#MainContent_txtTOEFLPaperbasedSpeaking").val(),
                    Writing: $("#MainContent_txtTOEFLPaperbasedWriting").val(),
                    Total: $("#MainContent_txtTOEFLPaperbasedTotal").val(),
                    Date: $("#MainContent_txtTOEFLPaperbasedDate").val(),
                    StudentTestId: $("#MainContent_hndTOEFLPaperbasedID").val(),
                    StudentId: sid
                };
                $.ajax({
                    type: "POST",
                    beforeSend: function () { $("#LoadingImage").show(); },
                    complete: function () { $("#LoadingImage").hide(); },
                    url: "AddNewStudent.aspx/TOEFLPaperbasedSave",
                    data: JSON.stringify(toeflpaper),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        // alert("Data Saved succesfully");

                        var data = msg.d;

                        if (data[2] == "add") {
                            if (document.getElementById("MainContent_dlTOEFLPaperbased") != null) {
                                var v = '<br/><span><li><a id="edittoeflp' + data[0] + '" href="javascript:OpenTOEFLPaperbasedEdit(' + data[0] + ')">TOEFLPaperbased-' + data[1] + '</a> <a href="javascript:TOEFLTOEFLPaperbasedDelete(' + data[0] + ')" id="deletetoeflp' + data[0] + '"> Delete</a></li></span>';
                                $("#MainContent_dlTOEFLPaperbased").append(v);
                            }
                            else {
                                var v = '<span id="MainContent_dlTOEFLPaperbased"><span><li><a id="edittoeflp' + data[0] + '" href="javascript:OpenTOEFLPaperbasedEdit(' + data[0] + ')">TOEFLPaperbased-' + data[1] + '</a> <a href="javascript:TOEFLPaperbasedDelete(' + data[0] + ')" id="deletetoeflp' + data[0] + '"> Delete</a></li></span></span>';
                                $("#ulTOEFLPaperbased").append(v);
                            }
                        }
                        else if (data[2] == "edit") {
                            $("#edittoeflp" + data[0]).text("TOEFLPaperbased-" + data[1]);
                            window.location.href = "/Admin/AddNewStudent.aspx?sec=IntTest";

                        }
                        else { }
                        $("#LoadingImage").hide();
                        $("#MainContent_txtTOEFLPaperbasedReading").val("");
                        $("#MainContent_txtTOEFLPaperbasedListening").val("");
                        $("#MainContent_txtTOEFLPaperbasedSpeaking").val("");
                        $("#MainContent_txtTOEFLPaperbasedWriting").val("");
                        $("#MainContent_txtTOEFLPaperbasedTotal").val("");
                        $("#MainContent_txtTOEFLPaperbasedDate").val("");
                        $("#MainContent_hndTOEFLPaperbasedID").val("0");
                        $("#MainContent_hndNewStudentId").val("0");
                        $("#divTOEFLPaperbased").hide();
                        $("#divTOEFLPaperbased").dialog('close');

                    }
                });
            }
            return false;
        });

        function OpenTOEFLPaperbasedEdit(studentTestId) {
            var toeflpaper = {
                StudentTestId: studentTestId
            };
            $.ajax({
                type: "POST",
                beforeSend: function () { $("#LoadingImage").show(); },
                complete: function () { $("#LoadingImage").hide(); },
                url: "AddNewStudent.aspx/GetAP",
                data: JSON.stringify(toeflpaper),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = $.parseJSON(msg.d);
                    if (data != null) {
                        $("#MainContent_txtTOEFLPaperbasedReading").val(data.Reading);
                        $("#MainContent_txtTOEFLPaperbasedListening").val(data.Listening);
                        $("#MainContent_txtTOEFLPaperbasedSpeaking").val(data.Speaking);
                        $("#MainContent_txtTOEFLPaperbasedWriting").val(data.Writing);
                        $("#MainContent_txtTOEFLPaperbasedTotal").val(data.Total);
                        $("#MainContent_txtTOEFLPaperbasedDate").val(data.Date);
                        $("#MainContent_hndTOEFLPaperbasedID").val(data.StudentTestId);
                        $("#divTOEFLPaperbased").dialog({ modal: true, minWidth: 650, resizable: false, minHeight: 600, closeOnEscape: true, closeText: "" });
                    }
                },
                error: function (xhr, status, error) {
                    alert(xhr.statusText);
                }
            });

        }

        function TOEFLPaperbasedDelete(studentTestId) {
            $("#dialog-confirm").dialog({
                open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Yes": function () {
                        if (Page_IsValid) {
                            var toeflpaper = {
                                StudentTestId: studentTestId
                            };
                            $.ajax({
                                type: "POST",
                                beforeSend: function () { $("#LoadingImage").show(); },
                                complete: function () { $("#LoadingImage").hide(); },
                                url: "AddNewStudent.aspx/APDelete",
                                data: JSON.stringify(toeflpaper),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    $("#deletetoeflp" + studentTestId).parent().parent().remove();

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

        function confirmDelete() {
            //if (confirm("Are you sure you want to delete this?") == true)
            //    return true;
            //else
            //    return false;
            //alert("k");
            $("#dialog-confirm").dialog({
                open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Yes": function () {
                        return true;
                        $(this).dialog("close");
                    },
                    Cancel: function () {
                        return false;
                        $(this).dialog("close");
                    }
                }
            });
        }
    </script>

</asp:Content>
