<%@ Page Title="International Test" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="InternationalTest.aspx.cs" Inherits="SpotCollege.Account.InternationalTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="span12">
            <div class="pattern_box box_space">
                <%-- <div class="h2_heading">
                    <h2>International Test</h2>
                </div>
                <ul>
                    <asp:DataList ID="dlInternationalTest" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" OnItemDataBound="">
                        <ItemTemplate>
                            <li>
                                <asp:Label ID="lblInternationalTestList" runat="server" Text=""></asp:Label><br />
                                <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
                            </li>
                        </ItemTemplate>
                    </asp:DataList>
                </ul>--%>

                <div class="span8" id="divInternationalTest">
                    <asp:Panel ID="pnlInternationalTest" runat="server">
                        <%--<asp:HiddenField ID="hndStudentWorkHistoryId" Value="0" runat="server" />--%>
                        <ul class="profile_form">
                            <li class="row">
                                <div class="span2">
                                    <label>
                                        High School Enrollment</label><br />

                                    <asp:LinkButton ID="lnkBtnAddHighSchool" runat="server" OnClick="lnkBtnAddHighSchool_Click">Add a HighSchool</asp:LinkButton>
                                </div>
                                <%--For High School Enrollment--%>
                                <div class="span8" id="divHighSchoolEnrollment">
                                    <asp:Panel ID="pnlHighSchoolEnrollment" runat="server" Visible="false">
                                        <%--<asp:HiddenField ID="hndStudentWorkHistoryId" Value="0" runat="server" />--%>
                                        <ul class="profile_form">
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Sector :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:RadioButtonList ID="rdBtnSector" CssClass="list_6" runat="server" RepeatLayout="UnorderedList">
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="rdBtnSector"
                                                        CssClass="field-validation-error" ErrorMessage="Please Select Any"
                                                        ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        School Name :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtSchoolName" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="School Name is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtSchoolName" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <asp:CheckBox ID="chkcurrentelyschool" Text="I currently go to school here" runat="server" />
                                                </div>

                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Start :</label>
                                                </div>
                                                <div class="span2">
                                                    <asp:DropDownList ID="ddlstartyear" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="ddlstartyear"
                                                        InitialValue="0" CssClass="field-validation-error" ErrorMessage="Please Select Year"
                                                        ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Graduation Year :</label>
                                                </div>
                                                <div class="span2">
                                                    <asp:DropDownList ID="ddlGraduationyear" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="ddlGraduationyear"
                                                        InitialValue="0" CssClass="field-validation-error" ErrorMessage="Please Select Year"
                                                        ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                </div>

                                            </li>
                                            <div class="row">
                                                <div class="span2">
                                                </div>
                                                <div class="span6">
                                                    <asp:Button ID="btnHighSchoolEnrollmentSave" runat="server" Text="Save" OnClick="btnHighSchoolEnrollmentSave_Click"
                                                        ValidationGroup="WorkHistoryValidation" CssClass="large_button" />
                                                    <asp:Button ID="btnHighSchoolEnrollmentCancel" runat="server" Text="Cancel"
                                                        OnClick="btnHighSchoolEnrollmentCancel_Click" CssClass="large_button" />
                                                </div>
                                            </div>
                                        </ul>
                                    </asp:Panel>
                                </div>
                            </li>
                            <li class="row">
                                <div class="span2">
                                    <label>
                                        ACT</label>
                                    <br />
                                    <asp:LinkButton ID="lnkBtnAddACTScore" runat="server" OnClick="lnkBtnAddACTScore_Click">Add a score</asp:LinkButton>
                                </div>
                                <%--For ACT--%>
                                <div class="span8" id="divACT">
                                    <asp:Panel ID="pnlACT" runat="server" Visible="false">
                                        <%--<asp:HiddenField ID="HiddenField1" Value="0" runat="server" />--%>
                                        <ul class="profile_form">
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Composite :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtComposite" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Composite is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtComposite" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtComposite"
                                                        ErrorMessage="Invalid Value" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        English :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtEnglish" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="English is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtEnglish" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtEnglish"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Math :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtMath" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Math is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtMath" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtMath"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Reading :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtReading" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Reading is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtReading" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtReading"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Science :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtScience" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Science is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtScience" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtScience"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Writing :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtWriting" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Writing is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtWriting" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtWriting"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span2">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="field-validation-error" ControlToValidate="txtDate" ErrorMessage="Date is Required" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="row">
                                                    <div class="span2">
                                                    </div>
                                                    <div class="span6">
                                                        <asp:Button ID="btnACTSave" runat="server" Text="Save" OnClick="btnACTSave_Click"
                                                            ValidationGroup="WorkHistoryValidation" CssClass="large_button" />
                                                        <asp:Button ID="btnACTCancel" runat="server" Text="Cancel"
                                                            OnClick="btnACTCancel_Click" CssClass="large_button" />
                                                    </div>
                                                </div>
                                        </ul>
                                    </asp:Panel>

                                </div>
                            </li>
                            <li class="row">
                                <div class="span2">
                                    <label>
                                        SAT</label>
                                    <br />
                                    <asp:LinkButton ID="lnkBtnAddSATScore" runat="server" OnClick="lnkBtnAddSATScore_Click">Add a score</asp:LinkButton>
                                </div>
                                <%--For SAT--%>
                                <div class="span8" id="divSAT">
                                    <asp:Panel ID="pnlSAT" runat="server" Visible="false">
                                        <%--<asp:HiddenField ID="HiddenField1" Value="0" runat="server" />--%>
                                        <ul class="profile_form">
                                            <li class="row">
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Reading :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtSATReading" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Reading is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtSATReading" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtSATReading"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>

                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Math :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtSATMath" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Math is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtSATMath" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtSATMath"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Writing :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtSATWriting" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Writing is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtSATWriting" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtSATWriting"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Composite :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtSATComposite" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Composite is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtSATComposite" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtSATComposite"
                                                        ErrorMessage="Invalid Value" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span2">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtSATDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" CssClass="field-validation-error" ControlToValidate="txtSATDate" ErrorMessage="Date is Required" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="row">
                                                    <div class="span2">
                                                    </div>
                                                    <div class="span6">
                                                        <asp:Button ID="btnSATSave" runat="server" Text="Save" OnClick="btnSATSave_Click"
                                                            ValidationGroup="WorkHistoryValidation" CssClass="large_button" />
                                                        <asp:Button ID="btnSATCancel" runat="server" Text="Cancel"
                                                            OnClick="btnSATCancel_Click" CssClass="large_button" />
                                                    </div>
                                                </div>
                                        </ul>
                                    </asp:Panel>

                                </div>
                            </li>
                            <li class="row">
                                <div class="span2">
                                    <label>
                                        AP</label>
                                    <br />
                                    <asp:LinkButton ID="lnkBtnAddAPScore" runat="server" OnClick="lnkBtnAddAPScore_Click">Add a score</asp:LinkButton>
                                </div>
                                <%--For AP--%>
                                <div class="span8" id="divAP">
                                    <asp:Panel ID="pnlAP" runat="server" Visible="false">
                                        <%--<asp:HiddenField ID="HiddenField1" Value="0" runat="server" />--%>
                                        <ul class="profile_form">
                                            <li class="row">
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Subject :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtAPSubject" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Subject is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtAPSubject" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtAPSubject"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>

                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Score :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtAPScore" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Score is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtAPScore" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtAPScore"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span2">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" CssClass="field-validation-error" ControlToValidate="txtSATDate" ErrorMessage="Date is Required" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="row">
                                                    <div class="span2">
                                                    </div>
                                                    <div class="span6">
                                                        <asp:Button ID="btnAPSave" runat="server" Text="Save" OnClick="btnAPSave_Click"
                                                            ValidationGroup="WorkHistoryValidation" CssClass="large_button" />
                                                        <asp:Button ID="btnAPCancel" runat="server" Text="Cancel"
                                                            OnClick="btnAPCancel_Click" CssClass="large_button" />
                                                    </div>
                                                </div>
                                        </ul>
                                    </asp:Panel>

                                </div>
                            </li>
                            <li class="row">
                                <div class="span2">
                                    <label>
                                        GRE</label>
                                    <br />
                                    <asp:LinkButton ID="lnkBtnAddGREScore" runat="server" OnClick="lnkBtnAddGREScore_Click">Add a score</asp:LinkButton>
                                </div>
                                <%--For GRE--%>
                                <div class="span8" id="divGRE">
                                    <asp:Panel ID="pnlGRE" runat="server" Visible="false">
                                        <%--<asp:HiddenField ID="HiddenField1" Value="0" runat="server" />--%>
                                        <ul class="profile_form">
                                            <li class="row">
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Verbal Reasoning  :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtGREVerbalReasoning" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="Verbal Reasoning is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtGREVerbalReasoning" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtGREVerbalReasoning"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Quantitative Reasoning  :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtGREQuantitativeReasoning" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="Quantitative Reasoning is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtGREQuantitativeReasoning" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtGREQuantitativeReasoning"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Analytical Writing :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtGREAnalyticalWriting" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="Analytical Writing is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtGREAnalyticalWriting" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator15" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtGREAnalyticalWriting"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span2">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtGREDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" CssClass="field-validation-error" ControlToValidate="txtGREDate" ErrorMessage="Date is Required" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="row">
                                                    <div class="span2">
                                                    </div>
                                                    <div class="span6">
                                                        <asp:Button ID="btnGRESave" runat="server" Text="Save" OnClick="btnGRESave_Click"
                                                            ValidationGroup="WorkHistoryValidation" CssClass="large_button" />
                                                        <asp:Button ID="btnGRECancel" runat="server" Text="Cancel"
                                                            OnClick="btnGRECancel_Click" CssClass="large_button" />
                                                    </div>
                                                </div>
                                        </ul>
                                    </asp:Panel>

                                </div>
                            </li>
                            <li class="row">
                                <div class="span2">
                                    <label>
                                        GMAT</label>
                                    <br />
                                    <asp:LinkButton ID="lnkBtnAddGMATScore" runat="server" OnClick="lnkBtnAddGMATScore_Click">Add a score</asp:LinkButton>
                                </div>
                                <%--For GMAT--%>
                                <div class="span8" id="divGMAT">
                                    <asp:Panel ID="pnlGMAT" runat="server" Visible="false">
                                        <%--<asp:HiddenField ID="HiddenField1" Value="0" runat="server" />--%>
                                        <ul class="profile_form">
                                            <li class="row">
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Verbal :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtGMATVerbal" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="Verbal is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtGMATVerbal" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator16" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtGMATVerbal"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Quantitative :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtGMATQuantitative" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="Quantitative is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtGMATQuantitative" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator17" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtGMATQuantitative"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Total :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtGMATTotal" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ErrorMessage="Total is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtGMATTotal" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator19" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtGMATTotal"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Analytical Writing :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtGMATAnalyticalWriting" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="Analytical Writing is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtGMATAnalyticalWriting" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator18" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtGMATAnalyticalWriting"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span2">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtGMATDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" CssClass="field-validation-error" ControlToValidate="txtGMATDate" ErrorMessage="Date is Required" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="row">
                                                    <div class="span2">
                                                    </div>
                                                    <div class="span6">
                                                        <asp:Button ID="btnGMATSave" runat="server" Text="Save" OnClick="btnGMATSave_Click"
                                                            ValidationGroup="WorkHistoryValidation" CssClass="large_button" />
                                                        <asp:Button ID="btnGMATCancel" runat="server" Text="Cancel"
                                                            OnClick="btnGMATCancel_Click" CssClass="large_button" />
                                                    </div>
                                                </div>
                                        </ul>
                                    </asp:Panel>

                                </div>
                            </li>
                            <li class="row">
                                <div class="span2">
                                    <label>
                                        IB</label>
                                    <br />
                                    <asp:LinkButton ID="lnkBtnAddIBScore" runat="server" OnClick="lnkBtnAddIBScore_Click">Add a score</asp:LinkButton>
                                </div>
                                <%--For IB--%>
                                <div class="span8" id="divIB">
                                    <asp:Panel ID="pnlIB" runat="server" Visible="false">
                                        <%--<asp:HiddenField ID="HiddenField1" Value="0" runat="server" />--%>
                                        <ul class="profile_form">
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Subject :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtIBSubject" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ErrorMessage="Subject is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtIBSubject" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator20" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtIBSubject"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>

                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Score :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtIBScore" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ErrorMessage="Score is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtIBScore" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator21" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtIBScore"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span2">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtIBDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" CssClass="field-validation-error" ControlToValidate="txtIBDate" ErrorMessage="Date is Required" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="row">
                                                    <div class="span2">
                                                    </div>
                                                    <div class="span6">
                                                        <asp:Button ID="btnIBSave" runat="server" Text="Save" OnClick="btnIBSave_Click"
                                                            ValidationGroup="WorkHistoryValidation" CssClass="large_button" />
                                                        <asp:Button ID="btnIBCancel" runat="server" Text="Cancel"
                                                            OnClick="btnIBCancel_Click" CssClass="large_button" />
                                                    </div>
                                                </div>
                                        </ul>
                                    </asp:Panel>

                                </div>
                            </li>
                            <li class="row">
                                <div class="span2">
                                    <label>
                                        IELTS</label>
                                    <br />
                                    <asp:LinkButton ID="lnkBtnAddIELTSScore" runat="server" OnClick="lnkBtnAddIELTSScore_Click">Add a score</asp:LinkButton>
                                </div>
                                <%--For IELTS--%>
                                <div class="span8" id="divIELTS">
                                    <asp:Panel ID="pnlIELTS" runat="server" Visible="false">
                                        <%--<asp:HiddenField ID="HiddenField1" Value="0" runat="server" />--%>
                                        <ul class="profile_form">
                                            <li class="row">
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Listening :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtIELTSListening" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ErrorMessage="Listening is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtIELTSListening" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator22" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtIELTSListening"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Reading :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtIELTSReading" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ErrorMessage="Reading is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtIELTSReading" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator23" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtIELTSReading"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Writing :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtIELTSWriting" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ErrorMessage="Writing is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtIELTSWriting" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator24" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtIELTSWriting"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Speaking :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtIELTSSpeaking" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ErrorMessage="Speaking is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtIELTSSpeaking" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator25" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtIELTSSpeaking"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span2">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtIELTSDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" CssClass="field-validation-error" ControlToValidate="txtIELTSDate" ErrorMessage="Date is Required" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="row">
                                                    <div class="span2">
                                                    </div>
                                                    <div class="span6">
                                                        <asp:Button ID="btnIELTSSave" runat="server" Text="Save" OnClick="btnIELTSSave_Click"
                                                            ValidationGroup="WorkHistoryValidation" CssClass="large_button" />
                                                        <asp:Button ID="btnIELTSCancel" runat="server" Text="Cancel"
                                                            OnClick="btnIELTSCancel_Click" CssClass="large_button" />
                                                    </div>
                                                </div>
                                        </ul>
                                    </asp:Panel>

                                </div>
                            </li>
                            <li class="row">
                                <div class="span2">
                                    <label>
                                        LSAT</label>
                                    <br />
                                    <asp:LinkButton ID="LinlnkBtnAddLSATScore" runat="server" OnClick="LinlnkBtnAddLSATScore_Click">Add a score</asp:LinkButton>
                                </div>

                                <%--For LSAT--%>
                                <div class="span8" id="divLSAT">
                                    <asp:Panel ID="pnlLSAT" runat="server" Visible="false">
                                        <%--<asp:HiddenField ID="HiddenField1" Value="0" runat="server" />--%>
                                        <ul class="profile_form">
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Score :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtLSATScore" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ErrorMessage="Score is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtLSATScore" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator27" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtLSATScore"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span2">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtLSATDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" CssClass="field-validation-error" ControlToValidate="txtLSATDate" ErrorMessage="Date is Required" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="row">
                                                    <div class="span2">
                                                    </div>
                                                    <div class="span6">
                                                        <asp:Button ID="btnLSATSave" runat="server" Text="Save" OnClick="btnLSATSave_Click"
                                                            ValidationGroup="WorkHistoryValidation" CssClass="large_button" />
                                                        <asp:Button ID="btnLSATCancel" runat="server" Text="Cancel"
                                                            OnClick="btnLSATCancel_Click" CssClass="large_button" />
                                                    </div>
                                                </div>
                                        </ul>
                                    </asp:Panel>

                                </div>
                            </li>
                            <li class="row">
                                <div class="span2">
                                    <label>
                                        MCAT</label>
                                    <br />
                                    <asp:LinkButton ID="LinlnkBtnAddMCATScore" runat="server" OnClick="LinlnkBtnAddMCATScore_Click">Add a score</asp:LinkButton>
                                </div>
                                <%--For MCAT--%>
                                <div class="span8" id="divMCAT">
                                    <asp:Panel ID="pnlMCAT" runat="server" Visible="false">
                                        <%--<asp:HiddenField ID="HiddenField1" Value="0" runat="server" />--%>
                                        <ul class="profile_form">
                                            <li class="row">
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Biology :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtMCATBiology" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ErrorMessage="Biology is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtMCATBiology" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator26" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtMCATBiology"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Physics :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtMCATPhysics" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ErrorMessage="Physics is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtMCATPhysics" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator28" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtMCATPhysics"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Verbal :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtMCATVerbal" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ErrorMessage="Verbal is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtMCATVerbal" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator29" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtMCATVerbal"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Writing :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtMCATWriting" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ErrorMessage="Writing is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtMCATWriting" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator30" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtMCATWriting"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span2">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtMCATDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" CssClass="field-validation-error" ControlToValidate="txtMCATDate" ErrorMessage="Date is Required" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>

                                                </div>
                                            </li>
                                            <div class="row">
                                                <div class="span2">
                                                </div>
                                                <div class="span6">
                                                    <asp:Button ID="btnMCATSave" runat="server" Text="Save" OnClick="btnMCATSave_Click"
                                                        ValidationGroup="WorkHistoryValidation" CssClass="large_button" />
                                                    <asp:Button ID="btnMCATCancel" runat="server" Text="Cancel"
                                                        OnClick="btnMCATCancel_Click" CssClass="large_button" />
                                                </div>
                                            </div>
                                        </ul>
                                    </asp:Panel>

                                </div>
                            </li>
                            <li class="row">
                                <div class="span2">
                                    <label>
                                        PSAT</label>
                                    <br />
                                    <asp:LinkButton ID="LinlnkBtnAddPSATScore" runat="server" OnClick="LinlnkBtnAddPSATScore_Click">Add a score</asp:LinkButton>
                                </div>
                                <%--For PSAT--%>
                                <div class="span8" id="divPSAT">
                                    <asp:Panel ID="pnlPSAT" runat="server" Visible="false">
                                        <%--<asp:HiddenField ID="HiddenField1" Value="0" runat="server" />--%>
                                        <ul class="profile_form">
                                            <li class="row">
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Reading :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtPSATReading" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" ErrorMessage="Reading is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtPSATReading" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator31" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtPSATReading"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Math :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtPSATMath" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator45" runat="server" ErrorMessage="Math is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtPSATMath" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator32" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtPSATMath"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Writing :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtPSATWriting" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator46" runat="server" ErrorMessage="Writing is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtPSATWriting" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator33" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtPSATWriting"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span2">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtPSATDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator48" runat="server" CssClass="field-validation-error" ControlToValidate="txtPSATDate" ErrorMessage="Date is Required" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>

                                                </div>
                                            </li>
                                            <div class="row">
                                                <div class="span2">
                                                </div>
                                                <div class="span6">
                                                    <asp:Button ID="btnPSATSave" runat="server" Text="Save" OnClick="btnPSATSave_Click"
                                                        ValidationGroup="WorkHistoryValidation" CssClass="large_button" />
                                                    <asp:Button ID="btnPSATCancel" runat="server" Text="Cancel"
                                                        OnClick="btnPSATCancel_Click" CssClass="large_button" />
                                                </div>
                                            </div>

                                        </ul>
                                    </asp:Panel>

                                </div>
                            </li>
                            <li class="row">
                                <div class="span2">
                                    <label>
                                        SAT-II</label>
                                    <br />
                                    <asp:LinkButton ID="LinlnkBtnAddSAT_2Score" runat="server" OnClick="LinlnkBtnAddSAT_2Score_Click">Add a score</asp:LinkButton>
                                </div>

                                <%--For SAT-II--%>
                                <div class="span8" id="divSAT2">
                                    <asp:Panel ID="pnlSAT2" runat="server" Visible="false">
                                        <%--<asp:HiddenField ID="HiddenField1" Value="0" runat="server" />--%>
                                        <ul class="profile_form">
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Subject :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtSAT_IISubject" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator47" runat="server" ErrorMessage="Subject is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtSAT_IISubject" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator34" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtSAT_IISubject"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>

                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Score :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtSAT_IIScore" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator49" runat="server" ErrorMessage="Score is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtSAT_IIScore" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator35" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtSAT_IIScore"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span3">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtSAT_IIDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator50" runat="server" CssClass="field-validation-error" ControlToValidate="txtSAT_IIDate" ErrorMessage="Date is Required" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>

                                                </div>
                                            </li>
                                            <div class="row">
                                                <div class="span2">
                                                </div>
                                                <div class="span6">
                                                    <asp:Button ID="btnSAT_IISave" runat="server" Text="Save" OnClick="btnSAT_IISave_Click"
                                                        ValidationGroup="WorkHistoryValidation" CssClass="large_button" />
                                                    <asp:Button ID="btnSAT_IICancel" runat="server" Text="Cancel"
                                                        OnClick="btnSAT_IICancel_Click" CssClass="large_button" />
                                                </div>
                                            </div>

                                        </ul>
                                    </asp:Panel>

                                </div>
                            </li>
                            <li class="row">
                                <div class="span2">
                                    <label>
                                        TOEFL Internet based</label>
                                    <br />
                                    <asp:LinkButton ID="LinlnkBtnAddTOEFLInternetbasedScore" runat="server" OnClick="LinlnkBtnAddTOEFLInternetbasedScore_Click">Add a score</asp:LinkButton>
                                </div>
                                <%--For TOEFL Internet based--%>
                                <div class="span8" id="divTOEFLInternetbased">
                                    <asp:Panel ID="pnlTOEFLInternetbased" runat="server" Visible="false">
                                        <%--<asp:HiddenField ID="HiddenField1" Value="0" runat="server" />--%>
                                        <ul class="profile_form">
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Reading :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtTOEFLInternetbasedReading" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator52" runat="server" ErrorMessage="Reading is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtTOEFLInternetbasedReading" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator37" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtTOEFLInternetbasedReading"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Listening :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtTOEFLInternetbasedListening" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator51" runat="server" ErrorMessage="Listening is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtTOEFLInternetbasedListening" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator36" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtTOEFLInternetbasedListening"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Speaking :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtTOEFLInternetbasedSpeaking" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator54" runat="server" ErrorMessage="Speaking is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtTOEFLInternetbasedSpeaking" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator39" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtTOEFLInternetbasedSpeaking"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Writing :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtTOEFLInternetbasedWriting" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator53" runat="server" ErrorMessage="Writing is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtTOEFLInternetbasedWriting" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator38" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtTOEFLInternetbasedWriting"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Total :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtTOEFLInternetbasedTotal" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator56" runat="server" ErrorMessage="Total is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtTOEFLInternetbasedTotal" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator40" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtTOEFLInternetbasedTotal"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span2">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtTOEFLInternetbasedDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" CssClass="field-validation-error" ControlToValidate="txtTOEFLInternetbasedDate" ErrorMessage="Date is Required" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>

                                                </div>
                                            </li>
                                            <div class="row">
                                                <div class="span2">
                                                </div>
                                                <div class="span6">
                                                    <asp:Button ID="btnTOEFLInternetbasedSave" runat="server" Text="Save" OnClick="btnTOEFLInternetbasedSave_Click"
                                                        ValidationGroup="WorkHistoryValidation" CssClass="large_button" />
                                                    <asp:Button ID="btnTOEFLInternetbasedCancel" runat="server" Text="Cancel"
                                                        OnClick="btnTOEFLInternetbasedCancel_Click" CssClass="large_button" />
                                                </div>
                                            </div>
                                        </ul>
                                    </asp:Panel>

                                </div>
                            </li>
                            <li class="row">
                                <div class="span2">
                                    <label>
                                        TOEFL Paper based</label>
                                    <br />
                                    <asp:LinkButton ID="LinlnkBtnAddTOEFLPaperbasedScore" runat="server" OnClick="LinlnkBtnAddTOEFLPaperbasedScore_Click">Add a score</asp:LinkButton>
                                </div>
                                <%--For TOEFL Paper based--%>
                                <div class="span8" id="divTOEFLPaperbased">
                                    <asp:Panel ID="pnlTOEFLPaperbased" runat="server" Visible="false">
                                        <%--<asp:HiddenField ID="HiddenField1" Value="0" runat="server" />--%>
                                        <ul class="profile_form">
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Reading :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtTOEFLPaperbasedReading" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" ErrorMessage="Reading is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtTOEFLPaperbasedReading" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator41" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtTOEFLPaperbasedReading"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Listening :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtTOEFLPaperbasedListening" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator58" runat="server" ErrorMessage="Listening is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtTOEFLPaperbasedListening" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator42" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtTOEFLPaperbasedListening"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Speaking :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtTOEFLPaperbasedSpeaking" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator59" runat="server" ErrorMessage="Speaking is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtTOEFLPaperbasedSpeaking" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator43" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtTOEFLPaperbasedSpeaking"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Writing :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtTOEFLPaperbasedWriting" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator60" runat="server" ErrorMessage="Writing is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtTOEFLPaperbasedWriting" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator44" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtTOEFLPaperbasedWriting"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Total :</label>
                                                </div>
                                                <div class="span3">
                                                    <asp:TextBox ID="txtTOEFLPaperbasedTotal" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator61" runat="server" ErrorMessage="Total is Required"
                                                        CssClass="field-validation-error" ControlToValidate="txtTOEFLPaperbasedTotal" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator45" CssClass="field-validation-error"
                                                        ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtTOEFLPaperbasedTotal"
                                                        ErrorMessage="Invalid Ranking" ValidationGroup="WorkHistoryValidation"></asp:RegularExpressionValidator>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Date :</label>
                                                </div>
                                                <div class="span2">
                                                    <span class="datepickericon"></span>
                                                    <asp:TextBox ID="txtTOEFLPaperbasedDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator62" runat="server" CssClass="field-validation-error" ControlToValidate="txtTOEFLPaperbasedDate" ErrorMessage="Date is Required" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>

                                                </div>
                                            </li>
                                            <div class="row">
                                                <div class="span2">
                                                </div>
                                                <div class="span6">
                                                    <asp:Button ID="btnTOEFLPaperbasedSave" runat="server" Text="Save" OnClick="btnTOEFLPaperbasedSave_Click"
                                                        ValidationGroup="WorkHistoryValidation" CssClass="large_button" />
                                                    <asp:Button ID="btnTOEFLPaperbasedCancel" runat="server" Text="Cancel"
                                                        OnClick="btnTOEFLPaperbasedCancel_Click" CssClass="large_button" />
                                                </div>
                                            </div>
                                        </ul>
                                    </asp:Panel>

                                </div>
                            </li>


                        </ul>
                    </asp:Panel>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
