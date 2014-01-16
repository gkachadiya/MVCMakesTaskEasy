<%@ Page Title="UniversityInfo" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="UniversityInformation.aspx.cs" Inherits="SpotCollege.Admin.UniversityInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <div class="pattern_box">
        <div class="row-fluid">
            <div class="msg_replay_hide">

                <div class="span3">
                    <div class="searh_box">
                        <div class="shadowbox">
                            <div class="h2_heading">
                                <h2>Search</h2>
                            </div>
                            <ul>
                                <li>
                                    <span>From Country: </span>
                                    <asp:DropDownList ID="ddlFromCountry" runat="server"></asp:DropDownList>
                                </li>
                            </ul>
                            <ul class="checkbox_list">
                                <li>
                                    <asp:CheckBox runat="server" ID="chkIncompCost" Text="Incomplete cost of International students" />
                                </li>
                                <li>
                                    <asp:CheckBox runat="server" ID="chnIncompEnrollNo" Text="Incomplete Enrollment numbers" />
                                </li>
                                <li>
                                    <asp:CheckBox runat="server" ID="chkNoLogo" Text="No logo image" />
                                </li>
                                <li>
                                    <asp:CheckBox runat="server" ID="chkNoLargeImg" Text="No large image" />
                                </li>
                                <li>
                                    <asp:CheckBox runat="server" ID="chkIsActive" Text="Is active" />
                                </li>
                                <li>
                                    <asp:CheckBox runat="server" ID="chkIsNotActive" Text="Is not active" />
                                </li>
                                <li>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="button" />
                                </li>
                            </ul>

                        </div>
                    </div>
                </div>

                <div class="span9">

                    <div id="LoadingImage" style="display: none">
                        <asp:Image ID="ImgLoader" runat="server" ImageUrl="~/Images/ajaxloader.gif" />
                    </div>

                    <div class="shadowbox">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <asp:HiddenField ID="hndUserName" Value="" runat="server" />
                        <asp:HiddenField ID="hndUniversityId" Value="0" runat="server" />

                        <asp:Panel ID="pnlUniversityInfo" runat="server">
                            <div id="UniversityInfo" runat="server" class="width100per">
                                <div class="h2_heading">
                                    <h2>University Information</h2>
                                    <asp:LinkButton ID="lnkbtnAddNeWUniversity" runat="server" OnClick="lnkbtnAddNeWUniversity_Click" Text="Add New University" class="edit fright"></asp:LinkButton>
                                </div>
                                <div class="box">
                                    <asp:Label runat="server" ID="lblNoOfResult"></asp:Label><br />
                                </div>

                                <asp:GridView ID="GrvUniversityInfo" CssClass="table box pagination" runat="server" AutoGenerateColumns="false" DataKeyNames="UniversityId" EmptyDataText="No University Detail Found...!" AllowPaging="true" PageSize="10" OnPageIndexChanging="GrvUniversityInfo_PageIndexChanging" AllowSorting="true" OnSorting="GrvUniversityInfo_Sorting">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="4%" ItemStyle-Width="4%">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkUni" />
                                                <asp:HiddenField ID="hrnUserName" runat="server" Value='<%#Eval("UserName") %>'></asp:HiddenField>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UserName" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="lnkBtnShowDetails" runat="server" Value='<%#Eval("UserName") %>'></asp:HiddenField>
                                                <a id='<%#Eval("UserName")%>' class="anUniversityDetail" href="javascript:Openpopup('<%#Eval("UserName")%>')"><%#Eval("UserName") %></a>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="University Name" HeaderStyle-Width="12%" ItemStyle-Width="10%" SortExpression="University Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUniversityName" runat="server" Text='<%# Eval("UniversityName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="Address" HeaderText="Address" HeaderStyle-Width="10%" ItemStyle-Width="10%"></asp:BoundField>--%>
                                        <asp:BoundField HeaderText="Administrator Name" DataField="AdminName" HeaderStyle-Width="10%" ItemStyle-Width="10%" />
                                        <%--<asp:BoundField HeaderText="EstablishedYear" DataField="EstablishedYear" HeaderStyle-Width="11%" ItemStyle-Width="10%" />--%>
                                        <asp:TemplateField HeaderText="Is Active" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkStatus" runat="server" AutoPostBack="true" OnCheckedChanged="chkStatus_CheckedChanged"
                                                    Checked='<%# Convert.ToBoolean(Eval("User.IsActive")) %>' Text='<%# Eval("User.IsActive").ToString().Equals("True") ? " Active " : " InActive" %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBtnEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("UserName") %>' OnClick="lnkBtnEdit_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Student" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                            <ItemTemplate>
                                                <a id="lnkMessage" onclick="javascript:openStudentDetail('<%#Eval("UserName")%>')" style="cursor: pointer">Student</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Student message" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                            <ItemTemplate>
                                                <a id="lnkMessage" href="javascript:OpenMsgPopup('<%#Eval("UserName")%>')">Message</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="6%" ItemStyle-Width="6%">
                                            <ItemTemplate>
                                                <a id='lnkBtnDelete<%#Eval("UniversityId") %>' href="javascript:UniversityDelete('<%#Eval("UniversityId")%>')">Delete</a>
                                                <%--<asp:LinkButton ID="lnkBtnDelete" runat="server" Text="Delete" CommandArgument='<%#Eval("UniversityId") %>' OnClick="lnkBtnDelete_Click" OnClientClick="return confirmDelete();"></asp:LinkButton>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:HiddenField ID="hdnAllRecipient" runat="server" Value="none" />
                                <br />
                                <div class="box">
                                    <asp:CheckBox ID="chkAll" runat="server" Text="&nbsp;&nbsp;Check All Search Result" />
                                    <asp:Button ID="btnSend" Text="Send Message" CssClass="button" OnClientClick="javascript:return false;" runat="server" />
                                    <asp:Button ID="btnDelete" Text="Delete" CssClass="button" runat="server" OnClientClick="javascript:return isRefresh();" OnClick="btnDelete_Click" />
                                </div>
                            </div>
                        </asp:Panel>

                        <div class="span8">
                            <asp:Panel ID="pnlEditUniverSityInfo" runat="server" Visible="false">
                                <ul class="profile_form">
                                    <li class="row">
                                        <div class="span2">
                                            <label>University Name :</label>
                                        </div>
                                        <div class="span4">
                                            <asp:TextBox ID="txtUniversityName" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="field-validation-error" ErrorMessage="Enter University Name" ControlToValidate="txtUniversityName" ValidationGroup="UniversityRegistrationValidation"></asp:RequiredFieldValidator>
                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>University Address :</label>
                                        </div>
                                        <div class="span4">
                                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="field-validation-error" ErrorMessage="Enter University Address" ControlToValidate="txtAddress" ValidationGroup="UniversityRegistrationValidation"></asp:RequiredFieldValidator>
                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>Administrator Email Id :</label>
                                        </div>
                                        <div class="span4">
                                            <asp:TextBox runat="server" ID="txtUserName" CausesValidation="True" Enabled="false" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName" CssClass="field-validation-error" ErrorMessage="Enter Administrator Email Id " ValidationGroup="UniversityRegistrationValidation" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" CssClass="field-validation-error" ControlToValidate="txtUserName" ValidationGroup="UniversityRegistrationValidation" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid Email"></asp:RegularExpressionValidator>
                                            <span id="lblDuplicateuser" style="display: none" class="field-validation-error">UserName already exist in Record.</span>
                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>Administrator Name :</label>
                                        </div>
                                        <div class="span4">
                                            <asp:TextBox runat="server" ID="txtAdministratorName" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAdministratorName" CssClass="field-validation-error" ErrorMessage="Enter Administrator Name" ValidationGroup="UniversityRegistrationValidation" />
                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>Password :</label>
                                        </div>
                                        <div class="span4">
                                            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPassword" CssClass="field-validation-error" ErrorMessage="Enter Administrator Password" ValidationGroup="UniversityRegistrationValidation" />
                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                Confirm Password :</label>
                                        </div>
                                        <div class="span4">
                                            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtConfirmPassword" CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The confirm password field is required." ValidationGroup="UniversityRegistrationValidation" />
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." ValidationGroup="UniversityRegistrationValidation" />
                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>Degree Level Offred :</label>
                                        </div>
                                        <div class="span4">
                                            <asp:CheckBoxList ID="chkDegreeOffredList" runat="server" RepeatLayout="UnorderedList" CssClass="list_6"></asp:CheckBoxList>
                                        </div>
                                    </li>

                                    <li class="row">
                                        <div class="span2">
                                            <label>Courses Offred :</label>
                                        </div>
                                        <div class="span4">
                                            <asp:CheckBoxList ID="chkCoursesOfferedList" runat="server" RepeatLayout="UnorderedList" CssClass="list_6"></asp:CheckBoxList>
                                        </div>
                                    </li>

                                    <li class="row">
                                        <div class="span2">
                                            <label>City :</label>
                                        </div>
                                        <div class="span4">
                                            <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="field-validation-error" runat="server" ErrorMessage="Enter City Name" ControlToValidate="txtCity" ValidationGroup="UniversityRegistrationValidation"></asp:RequiredFieldValidator>
                                        </div>
                                    </li>

                                    <li class="row">
                                        <div class="span2">
                                            <label>Country :</label>
                                        </div>
                                        <div class="span4">
                                            <asp:DropDownList ID="ddlCountry" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" CssClass="field-validation-error" ControlToValidate="ddlCountry" InitialValue="0" ErrorMessage="Country is Required" ValidationGroup="UniversityRegistrationValidation"></asp:RequiredFieldValidator>
                                            <%--<asp:Label ID="lblCountryValidation" CssClass="field-validation-error" runat="server" Text="Select Any" Visible="false"></asp:Label>--%>
                                        </div>
                                    </li>

                                    <li class="row">
                                        <div class="span2">
                                            <label>Established Year :</label>
                                        </div>
                                        <%--<div class="span4">
                                    <asp:TextBox ID="txtEstablishedYear" runat="server" MaxLength="4"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" CssClass="field-validation-error" runat="server" ErrorMessage="Enter Established Year" ControlToValidate="txtEstablishedYear" ValidationGroup="UniversityRegistrationValidation"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="\d{0,15}" CssClass="field-validation-error" ErrorMessage="Invalid Year" ControlToValidate="txtEstablishedYear" ValidationGroup="UniversityRegistrationValidation"></asp:RegularExpressionValidator>
                                </div>--%>
                                        <div class="span4">
                                            <asp:DropDownList ID="ddlEstablishYear" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" CssClass="field-validation-error" runat="server" ErrorMessage="Select Any Year" InitialValue="0" ControlToValidate="ddlEstablishYear" ValidationGroup="EditBasicInfoUniversity"></asp:RequiredFieldValidator>
                                        </div>
                                    </li>

                                    <li class="row">
                                        <div class="span2">
                                            <label>Description :</label>
                                        </div>
                                        <div class="span4">
                                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" CssClass="field-validation-error" runat="server" ErrorMessage="Enter Description" ControlToValidate="txtDescription" ValidationGroup="UniversityRegistrationValidation"></asp:RequiredFieldValidator>
                                        </div>
                                    </li>

                                    <li class="row">
                                        <div class="span2">
                                            <label>Under Graduate Fee :</label>
                                        </div>
                                        <div class="span4">
                                            <asp:TextBox ID="txtUnderGraduateFee" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" CssClass="field-validation-error" runat="server" ErrorMessage="Enter Under Graduate Fee" ControlToValidate="txtUnderGraduateFee" ValidationGroup="UniversityRegistrationValidation"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationExpression="\d{0,15}.\d{0,2}" CssClass="field-validation-error" ErrorMessage="Invalid UnderGraduateFee" ControlToValidate="txtUnderGraduateFee" ValidationGroup="UniversityRegistrationValidation"></asp:RegularExpressionValidator>
                                        </div>
                                    </li>

                                    <li class="row">
                                        <div class="span2">
                                            <label>Graduate Fee :</label>
                                        </div>
                                        <div class="span4">
                                            <asp:TextBox ID="txtGraduateFee" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" CssClass="field-validation-error" ErrorMessage="Enter Graduate Fee" ControlToValidate="txtGraduateFee" ValidationGroup="UniversityRegistrationValidation"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ValidationExpression="\d{0,15}.\d{0,2}" CssClass="field-validation-error" ErrorMessage="Invalid GraduateFee" ControlToValidate="txtGraduateFee" ValidationGroup="UniversityRegistrationValidation"></asp:RegularExpressionValidator>
                                        </div>
                                    </li>

                                    <li class="row">
                                        <div class="span2">
                                            <label>Book Fee :</label>
                                        </div>
                                        <div class="span4">
                                            <asp:TextBox ID="txtBookFee" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" CssClass="field-validation-error" ErrorMessage="Enter Book Fee" ControlToValidate="txtBookFee" ValidationGroup="UniversityRegistrationValidation"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ValidationExpression="\d{0,15}.\d{0,2}" CssClass="field-validation-error" ErrorMessage="Invalid BookFee" ControlToValidate="txtBookFee" ValidationGroup="UniversityRegistrationValidation"></asp:RegularExpressionValidator>
                                        </div>
                                    </li>

                                    <li class="row">
                                        <div class="span2">
                                            <label>Board Fee :</label>
                                        </div>
                                        <div class="span4">
                                            <asp:TextBox ID="txtBoardFee" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" CssClass="field-validation-error" ErrorMessage="Enter Board Fee" ControlToValidate="txtBoardFee" ValidationGroup="UniversityRegistrationValidation"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ValidationExpression="\d{0,15}.\d{0,2}" CssClass="field-validation-error" ErrorMessage="Invalid BoardFee" ControlToValidate="txtBoardFee" ValidationGroup="UniversityRegistrationValidation"></asp:RegularExpressionValidator>
                                        </div>
                                    </li>

                                    <li class="row">
                                        <div class="span2">
                                            <label>No. Of Graduates :</label>
                                        </div>
                                        <div class="span4">
                                            <asp:TextBox ID="txtGraduates" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" CssClass="field-validation-error" ErrorMessage="Enter Graduates" ControlToValidate="txtGraduates" ValidationGroup="UniversityRegistrationValidation"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ValidationExpression="\d{0,15}" CssClass="field-validation-error" ErrorMessage="Invalid Graduates" ControlToValidate="txtGraduates" ValidationGroup="UniversityRegistrationValidation"></asp:RegularExpressionValidator>
                                        </div>
                                    </li>

                                    <li class="row">
                                        <div class="span2">
                                            <label>No. Of Under Graduates :</label>

                                        </div>
                                        <div class="span4">
                                            <asp:TextBox ID="txtUnderGraduates" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" CssClass="field-validation-error" ErrorMessage="Enter Under Graduates" ControlToValidate="txtUnderGraduates" ValidationGroup="UniversityRegistrationValidation"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ValidationExpression="\d{0,15}" CssClass="field-validation-error" ErrorMessage="Invalid UnderGraduates" ControlToValidate="txtUnderGraduates" ValidationGroup="UniversityRegistrationValidation"></asp:RegularExpressionValidator>
                                        </div>
                                    </li>


                                    <li class="row">
                                        <div class="span2">
                                            <label>No. Of InterNational Graduates :</label>

                                        </div>
                                        <div class="span4">
                                            <asp:TextBox ID="txtInterNationalGraduates" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" CssClass="field-validation-error" ErrorMessage="Enter InterNational Graduates" ControlToValidate="txtInterNationalGraduates" ValidationGroup="UniversityRegistrationValidation"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ValidationExpression="\d{0,15}" CssClass="field-validation-error" ErrorMessage="Invalid InterNationalGraduates" ControlToValidate="txtInterNationalGraduates" ValidationGroup="UniversityRegistrationValidation"></asp:RegularExpressionValidator>
                                        </div>
                                    </li>

                                    <li class="row">
                                        <div class="span2">
                                            <label>Image :</label>
                                        </div>
                                        <div class="span4">
                                            <asp:FileUpload ID="fluploadImage" runat="server" />
                                            <asp:RequiredFieldValidator ID="ReqFldValImage" runat="server" CssClass="field-validation-error" ErrorMessage="Please Upload Image" ControlToValidate="fluploadImage" ValidationGroup="UniversityRegistrationValidation"></asp:RequiredFieldValidator>
                                            <img id="imgProfileImage" runat="server" visible="false" height="40" width="40" src="" />
                                        </div>
                                    </li>

                                    <div class="row">
                                        <div class="span2"></div>
                                        <div class="span4">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="UniversityRegistrationValidation" OnClick="btnSubmit_Click" CausesValidation="true" class="large_button" />
                                            <asp:Button ID="btnReset" runat="server" Text="Cancel" OnClick="btnReset_Click" class="large_button" />
                                        </div>
                                    </div>
                                </ul>
                            </asp:Panel>
                        </div>



                        <%--Interested Students List--%>
                        <asp:Panel ID="pnlInterestedStudent" runat="server" Visible="false">
                            <div id="divInterestedStudents" runat="server" class="width100per">
                                <div class="h2_heading">
                                    <h2>Interested Students List</h2>
                                    <asp:LinkButton ID="lnkbtnstudBack" runat="server" OnClick="lnkbtnstudBack_Click" class="back">Back</asp:LinkButton>
                                </div>
                                <b class="studentid">
                                    <asp:Label ID="lblUniversityId" runat="server"></asp:Label></b>
                                <asp:HiddenField ID="hnduniUsername" runat="server" />
                                <asp:GridView ID="grvInterestedStudents" CssClass="table box" runat="server" AutoGenerateColumns="false" OnRowDataBound="grvInterestedStudents_RowDataBound" EmptyDataText="No Student Record Found...!" AllowPaging="true" PageSize="20" OnPageIndexChanging="grvInterestedStudents_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Students Name" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HndstudentUserName" runat="server" Value='<%#Eval("User.UserName") %>' />
                                                <asp:LinkButton ID="lnkBtnStudentEdit" runat="server" Text="" CommandArgument='<%#Eval("StudentInterestId") %>' OnClick="lnkBtnStudentEdit_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStudentStatus" runat="server" Text='<%#Eval("Approved") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBtnStudentDelete" runat="server" Text="Delete" CommandArgument='<%#Eval("StudentInterestId") %>' OnClick="lnkBtnStudentDelete_Click" OnClientClick="return confirm('Are you sure want to delete Student?');"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>

                        <div class="span8">
                            <asp:Panel ID="pnlInterestedStudentEdit" runat="server" Visible="false">
                                <ul class="profile_form">
                                    <asp:HiddenField ID="hndStudentUserName" runat="server" />
                                    <li class="row">
                                        <div class="span2">
                                            <label>Change Status :</label>
                                        </div>
                                        <div class="span4">
                                            <asp:DropDownList ID="ddlstudentStatus" runat="server"></asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" CssClass="field-validation-error" ControlToValidate="ddlCountry" InitialValue="0" ErrorMessage="Country is Required" ValidationGroup="UniversityRegistrationValidation"></asp:RequiredFieldValidator>--%>
                                            <%--<asp:Label ID="lblCountryValidation" CssClass="field-validation-error" runat="server" Text="Select Any" Visible="false"></asp:Label>--%>
                                        </div>
                                    </li>
                                    <div class="row">
                                        <div class="span2"></div>
                                        <div class="span4">
                                            <asp:Button ID="BtnSaveStudent" runat="server" Text="Submit" ValidationGroup="UniversityRegistrationValidation" OnClick="BtnSaveStudent_Click" class="large_button" />
                                            <asp:Button ID="BtnCancelstudent" runat="server" Text="Cancel" OnClick="BtnCancelstudent_Click" class="large_button" />
                                        </div>
                                    </div>
                                </ul>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            <div class="msg_replay_show messagereplayfrom">
                <div class="span5">
                    <span class="msg_replay_close">Close</span>
                    <div class="trsnsparentnone_box">
                        <div class="message_right shadowbox">
                            <asp:GridView ID="GrvUniversityInfo2" CssClass="table box pagination" runat="server" AutoGenerateColumns="false" DataKeyNames="UniversityId" EmptyDataText="No University Detail Found...!" AllowPaging="true" PageSize="10" OnPageIndexChanging="GrvUniversityInfo_PageIndexChanging" AllowSorting="true" OnSorting="GrvUniversityInfo_Sorting">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-Width="4%" ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkUni" />
                                            <asp:HiddenField ID="hrnUserName" runat="server" Value='<%#Eval("UserName") %>'></asp:HiddenField>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UserName" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="lnkBtnShowDetails" runat="server" Value='<%#Eval("UserName") %>'></asp:HiddenField>
                                            <a id='<%#Eval("UserName")%>' class="anUniversityDetail" href="javascript:Openpopup('<%#Eval("UserName")%>')"><%#Eval("UserName") %></a>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="University Name" HeaderStyle-Width="12%" ItemStyle-Width="10%" SortExpression="University Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUniversityName" runat="server" Text='<%# Eval("UniversityName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="Address" HeaderText="Address" HeaderStyle-Width="10%" ItemStyle-Width="10%"></asp:BoundField>--%>
                                    <asp:BoundField HeaderText="Administrator Name" DataField="AdminName" HeaderStyle-Width="10%" ItemStyle-Width="10%" />
                                    <%--<asp:BoundField HeaderText="EstablishedYear" DataField="EstablishedYear" HeaderStyle-Width="11%" ItemStyle-Width="10%" />--%>
                                    <asp:TemplateField HeaderText="Is Active" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkStatus" runat="server" AutoPostBack="true" OnCheckedChanged="chkStatus_CheckedChanged"
                                                Checked='<%# Convert.ToBoolean(Eval("User.IsActive")) %>' Text='<%# Eval("User.IsActive").ToString().Equals("True") ? " Active " : " InActive" %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="span7">
                    <div class="row-fluid">
                        <div class="span8">
                            <div id="messageList" class="student_msg message_right shadowbox">
                            </div>
                            <%--Message Box--%>
                            <div id="messagebox" class="student_msg message_right shadowbox" style="display: none" title="Message Box">
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="message_right">
                                            <ul class="profile_form">
                                                <li class="row-fluid">
                                                    <div class="span4">
                                                        <label>Send to :</label>
                                                    </div>
                                                    <div class="span5">
                                                        <asp:TextBox runat="server" ID="txtSendToUserName" Enabled="false" />
                                                    </div>
                                                </li>
                                                <li class="row-fluid">
                                                    <div class="span4">
                                                        <label>Subject :</label>
                                                    </div>
                                                    <div class="span5">
                                                        <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" CssClass="field-validation-error" ErrorMessage="Enter Subject" ControlToValidate="txtSubject" ValidationGroup="sendmessage"></asp:RequiredFieldValidator>
                                                    </div>
                                                </li>
                                                <li class="row-fluid">
                                                    <div class="span4">
                                                        <label>Message :</label>
                                                    </div>
                                                    <div class="span6">
                                                        <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" Display="Dynamic" CssClass="field-validation-error" ErrorMessage="Enter Message" ControlToValidate="txtMessage" ValidationGroup="sendmessage"></asp:RequiredFieldValidator>
                                                    </div>
                                                </li>
                                                <li class="row-fluid">
                                                    <div class="span4"></div>
                                                    <div class="span6">
                                                        <asp:Button ID="btnMsgSend" runat="server" Text="Send" CssClass="large_button" OnClientClick="return false" ValidationGroup="sendmessage" />
                                                        <asp:Button ID="btnMsgCancel" runat="server" class="large_button" Text="Cancel" OnClientClick="return false" />
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--End--%>

                            <div id="divLoader" class="student_msg message_right shadowbox">
                                please wait...<br />
                                <img src="../Images/ajaxloader.gif" />
                            </div>
                        </div>
                        <div class="span4">
                            <ul class="student_right_block" id="ulMessageBetween">
                                <li></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="dialog-confirm" title="Are You Sure?" style="display: none;">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Are you sure?</p>
    </div>
    <div id="Dialog-Send" title="Message Send" style="display: none;">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Message Send Successfully</p>
    </div>
    <%--For popup, opened by click on student link--%>
    <div id="studentsConversation" class="studentsconversation" style="display: none" title="Students Conversation">
        <div class="row-fluid">
            <div class="span3">
                <div class="shadowbox" id="studentList">
                    <div class="over_hidden">
                        <div class="pre-scrollable">
                            <ul class="user_msg_box" id="ulStudentList">
                                <li>
                                    <div class="user_img">
                                        <asp:Image ID="ImgBtnPhoto1" runat="server" ImageUrl="../Images/no_image.jpg" />
                                    </div>
                                    <div class="user_name">
                                        <label>
                                            <a href="#" class="msg_conversation_open_click" id='<%#Eval("MessageId") %>'>
                                                <asp:Label ID="lblStudentname" runat="server">Jayesh G</asp:Label>
                                            </a>
                                        </label>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>

                    <div id="noMsgAlert" style="display: none" class="left_right_space">
                        No More Students Found
                    </div>
                    <div id="waitDiv" style="display: none" class="left_right_space">
                        please wait...<br />
                        <img src="../Images/ajaxloader.gif" />
                    </div>
                </div>
            </div>
            <div class="span9">
                <div class="shadowbox">
                    <ul id="ulDisplayConversation" class="user_msg_box">
                    </ul>
                </div>
            </div>
        </div>
    </div>

    <%-- MessageBox --%>
    <div id="groupMsgBox" class="student_msg message_right shadowbox" style="display: none; margin: 0" title="Group Message Box">
        <div class="row-fluid">
            <div class="span12">
                <div class="message_right">
                    <ul class="profile_form">
                        <li class="row-fluid">
                            <div class="span4">
                                <label>Send to :</label>
                            </div>
                            <div class="span4">
                                <asp:TextBox runat="server" ID="txtGrpSendTo" Enabled="false" />
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span4">
                                <label>Subject :</label>
                            </div>
                            <div class="span4">
                                <asp:TextBox ID="txtGrpSubject" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" Display="Dynamic" runat="server" CssClass="field-validation-error" ErrorMessage="Enter Subject" ControlToValidate="txtSubject" ValidationGroup="sendmessage"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span4">
                                <label>Message :</label>
                            </div>
                            <div class="span6">
                                <asp:TextBox ID="txtGrpDesc" runat="server" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" Display="Dynamic" CssClass="field-validation-error" ErrorMessage="Enter Message" ControlToValidate="txtMessage" ValidationGroup="sendmessage"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span4"></div>
                            <div class="span6">
                                <asp:Button ID="btnSendGroupMessage" runat="server" Text="Send" CssClass="large_button" OnClientClick="return false" ValidationGroup="sendmessage" />
                                <asp:Button ID="btnGrpCancel" runat="server" class="large_button" Text="Cancel" OnClientClick="return false" />
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>

    <%--Full Information Popup --%>
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

    <div id="Dialog-Alert" title="" style="display: none;">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Please Enter Message Text</p>
    </div>
     <div id="Dialog-SelectAlert" title="" style="display: none;">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Please Select University</p>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#MainContent_txtUserName").change(function () {
                // alert("kk");
                if ($("#MainContent_txtUserName").val() != "") {
                    if (Page_IsValid) {
                        var University = {
                            UserName: $('#MainContent_txtUserName').val(),
                        };
                        $.ajax({
                            type: "POST",
                            url: "UniversityInformation.aspx/ChechUniversityDuplicate",
                            data: JSON.stringify(University),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                if (msg.d != 'true') {
                                    $('#MainContent_btnSubmit').show();
                                    $('#lblDuplicateuser').hide();
                                }
                                else {

                                    $('#MainContent_btnSubmit').hide();
                                    $('#lblDuplicateuser').show();
                                }
                            }
                        });
                    }
                }
            });



            $("#nav li").each(function () {
                $(this).removeClass('navi_active');
            });
            $("#lnkCollege").addClass("navi_active");
        });

        function openStudentDetail(username) {
            $("#studentsConversation").dialog({ modal: true, minWidth: 1170, resizable: false, minHeight: 600, closeOnEscape: true, closeText: "" });
            $("#ulStudentList").html("<li>loading Students</li>");
            $("#ulDisplayConversation").html("Click On UsreName to Retrive Conversation.")
            var user = {
                university: username,
            };
            $.ajax({
                type: "POST",
                url: "UniversityInformation.aspx/retriveStudentList",
                data: JSON.stringify(user),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == "no")
                        $("#ulStudentList").html("<li>No Data Found</li>");
                    else {
                        $("#ulStudentList").html(msg.d);
                        activate(username);
                    }
                },
                error: function (e1, e2, e3, e4) { alert(JSON.stringify(e2)); }
            });
        }
        function activate(university) {
            var university = university;
            $("#ulDisplayConversation").html("Click On UsreName to Retrive Conversation.");

            $(".msg_conversation_open_click").click(function () {
                $("#ulDisplayConversation").html("<li>Loading Conversation...</li>");
                var id = this.id;
                var user = {
                    studentusername: id,
                    university: university,
                };
                $.ajax({
                    type: "POST",
                    url: "UniversityInformation.aspx/retriveConversationBetweenUni",
                    data: JSON.stringify(user),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {

                        if (msg.d == "")
                            $("#ulDisplayConversation").html("<li>No Conversation Found</li>");
                        else {
                            $("#ulDisplayConversation").html(msg.d);
                        }
                    },
                    error: function (e1, e2, e3, e4) { alert(JSON.stringify(e2)); }
                });
            });
        }

        $("#MainContent_btnMsgCancel").click(function () {
            $("#MainContent_txtSubject").val("");
            $("#MainContent_txtMessage").val("");
            flag_showpopup = false;
            $(".msg_replay_show").hide(1000);
            $(".msg_replay_hide").show(1000);
        });
        $("#MainContent_btnGrpCancel").click(function () {
            $("#MainContent_txtGrpSubject").val("");
            $("#MainContent_txtGrpDesc").val("");
            $("#groupMsgBox").dialog("close");
        });

        var nScrollHight = 0; // rolling distance Total length (note not the length of the scroll bar)
        var nScrollTop = 0; // Scroll to the current position
        var nDivHight = $("#studentList").height();

        var flag = true;
        $("#studentList").scroll(function () {
            nScrollHight = $(this)[0].scrollHeight;
            nScrollTop = $(this)[0].scrollTop;
            if (nScrollTop + nDivHight >= nScrollHight) {
                if (flag) {
                    $("#waitdiv").show();
                    flag = false
                    $.ajax({
                        type: "POST",
                        beforeSend: function () { $("#waitdiv").show(); },
                        complete: function () { $("#waitdiv").hide(); },
                        url: "UniversityInformation.aspx/AppendAndDisplayStudent",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d == "no")
                                $("#noMsgAlert").show();
                            else {
                                $("#noMsgAlert").hide();
                                flag = true;
                                $("#ulStudentList").append(data.d);
                            }
                            $("#waitdiv").hide();
                        }
                    });
                }
            }

        });

        function UniversityDelete(universityId) {
            $("#dialog-confirm").dialog({
                open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Yes": function () {
                        if (Page_IsValid) {
                            var Uni = {
                                UniversityId: universityId
                            };

                            $.ajax({
                                type: "POST",
                                beforeSend: function () { $("#LoadingImage").show(); },
                                complete: function () { $("#LoadingImage").hide(); },
                                url: "UniversityInformation.aspx/UniversityDelete",
                                data: JSON.stringify(Uni),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    $("#lnkBtnDelete" + universityId).parent().parent().remove();
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

        function isRefresh() {
            if (confirm("Are You Sure?"))
                return true;
            else
                return false;
        }

        function confirmDelete() {
            $("#dialog-confirm").dialog({
                open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Yes": function () {
                        $(this).dialog("close");
                        return true;

                    },
                    Cancel: function () {
                        $(this).dialog("close");
                        return false;

                    }
                }
            });
        }

        function getAllCheckedItem() {
            var username = Array();
            var chk = $("input[id^=MainContent_GrvUniversityInfo_chkUni]");
            for (i = 0; i < chk.length; i++) {
                if (chk[i].checked) {
                    var tmp = chk[i].id;
                    tmp = tmp.substr(tmp.lastIndexOf('_') + 1);
                    username.push($("#MainContent_GrvUniversityInfo_hrnUserName_" + tmp).val());
                }
            }
            return username.join(',');
        }

        $(function () {
            $(".msg_replay_show").hide();
        });
        $(".msg_replay_close").click(function () {
            flag_showpopup = false;
            $(".msg_replay_show").hide(1000);
            $(".msg_replay_hide").show(1000);
        });


        $("#MainContent_btnSend").click(function () {
            var user = '';
            if ($("#MainContent_chkAll").attr('checked'))
                user = $("#MainContent_hdnAllRecipient").val();
            else
                user = getAllCheckedItem();

            if (user != '') {
                $("#groupMsgBox").dialog({ modal: true, minWidth: 650, resizable: false, closeOnEscape: true, closeText: "" });

                $("#MainContent_txtGrpSendTo").val(user);
                $("#MainContent_btnSendGroupMessage").show();
                $("#MainContent_btnMsgSend").hide();
                $("#liLoader").hide();

            }
            else {
                //alert('Please Select University!');
                $("#Dialog-SelectAlert").dialog({
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
        });

        //For Check All the checkbox
        $("#MainContent_chkAll").change(function () {

            if (this.checked) {
                var arr = $("input[id^=MainContent_GrvUniversityInfo_chkUni]");
                for (i = 0; i < arr.length; i++) {
                    arr[i].checked = true;
                }
            }
            else {
                var arr = $("input[id^=MainContent_GrvUniversityInfo_chkUni]");
                for (i = 0; i < arr.length; i++) {
                    arr[i].checked = false;
                }
            }
        });

        $("#MainContent_btnSendGroupMessage").click(function () {
            var str = $("#MainContent_txtGrpSendTo").val();

            $("#liLoader").show();

            var user = {
                users: str,
                Title: $("#MainContent_txtGrpSubject").val(),
                Description: $("#MainContent_txtGrpDesc").val()
            };
            if ($("#MainContent_txtGrpSubject").val() != "" && $("#MainContent_txtGrpDesc").val() != "") {
                $.ajax({
                    type: "POST",
                    beforeSend: function () { $("#LoadingImage").show(); },
                    complete: function () { $("#LoadingImage").hide(); },
                    url: "StudentInfo.aspx/sendGroupMessage",
                    data: JSON.stringify(user),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var data = msg.d;
                        $("#liLoader").hide();
                        $("#MainContent_txtGrpSubject").val("");
                        $("#MainContent_txtGrpSendTo").val("");
                        $("#MainContent_txtGrpDesc").val("");
                        $("#groupMsgBox").dialog("close");
                        // alert("Message Successfully Sent");
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
                    },
                    error: function (xhr, status, error) {
                        $("#liLoader").hide();
                        $("#MainContent_txtSubject").val("");
                        $("#MainContent_txtMessage").val("");
                        alert(xhr.statusText);
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

        });

        function retriveConversation(parentId) {
            var msgId = {
                messageId: parentId
            };
            $("#ulMessageBetween").html("");
            $.ajax({
                type: "POST",
                url: "StudentInfo.aspx/GetAllConversation",
                data: JSON.stringify(msgId),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;
                    $("#messageList").html(data[0]);
                    $("#ulMessageBetween").html(data[1]);
                    $("#messagebox").hide();
                    $("#messageList").show();
                    $("#divLoader").hide();

                    bindToDisplayRemainMessage();
                },
                fail: function (e1, e2, e3, e4) { alert(JSON.stringify(e2)); }
            });

        }
        function bindToDisplayRemainMessage() {
            var str = $("#openAll").text();
            var status = true;
            $("#openAll").click(function () {
                $(".remainMsg").toggle(1000);

                if (status) {
                    $(this).text("hide Messages");
                    status = false;
                }
                else {
                    $(this).text(str);
                    status = true;
                }
            });
        }


        function MsgDelete(messageId) {
            $("#dialog-confirm").dialog({
                open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Yes": function () {
                        if (Page_IsValid) {
                            var msg = {
                                MessageId: messageId
                            };
                            $.ajax({
                                type: "POST",
                                beforeSend: function () { $("#LoadingImage").show(); },
                                complete: function () { $("#LoadingImage").hide(); },
                                url: "AdminMessage.aspx/MsgDelete",
                                data: JSON.stringify(msg),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                   
                                        $("#msgdel" + messageId).parent().parent().parent().remove();
                                   
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

        function MsgDeleteAll(parentMsg) {
            $("#dialog-confirm").dialog({
                open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Yes": function () {
                        if (Page_IsValid) {
                            var msg = {
                                parentId: parentMsg
                            };
                            $.ajax({
                                type: "POST",
                                beforeSend: function () { $("#LoadingImage").show(); },
                                complete: function () { $("#LoadingImage").hide(); },
                                url: "StudentInfo.aspx/DeleteAllConversation",
                                data: JSON.stringify(msg),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    flag_showpopup = false;
                                    $(".msg_replay_show").hide(1000);
                                    $(".msg_replay_hide").show(1000);
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


        function SaveMessageReply(parentMsgId, title, fromuser, touser) {
            var msgdata = {
                parentMsgId: parentMsgId,
                msgDesc: $('#txtReplyDesc').val(),
                msgTitle: title,
                fromusername: fromuser,
                tousername: touser
            };
            if ($('#txtReplyDesc').val() != "") {
                $.ajax({
                    type: "POST",
                    url: "StudentInfo.aspx/SaveMessageReply",
                    data: JSON.stringify(msgdata),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $("#txtReplyDesc").val("");
                        $("#ulMessage").html(msg.d + $("#ulMessage").html());

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


        //For Open Massegebox
        function OpenMsgPopup(sendToUserName) {
            $(".msg_replay_hide").hide(1000);
            $(".msg_replay_show").show(1000);
            $("#divLoader").show();
            $("#messagebox").hide();
            $("#messageList").hide();
            $("#ulMessageBetween").html("");
            var user = {
                ToUserName: sendToUserName
            };
            $.ajax({
                type: "POST",
                beforeSend: function () { $("#LoadingImage").show(); },
                complete: function () { $("#LoadingImage").hide(); },
                url: "StudentInfo.aspx/Getstatus",
                data: JSON.stringify(user),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;
                    if (data != "") {
                        retriveConversation(data);
                        //window.location.href = "AdminMessage.aspx?id=" + data;
                    }
                    else {
                        $("#MainContent_txtSendToUserName").val(sendToUserName);
                        $("#MainContent_btnSendGroupMessage").hide();
                        $("#MainContent_btnMsgSend").show();
                        $("#MainContent_txtSendToUserName").val(sendToUserName);
                        $("#messagebox").show();
                        $("#messageList").hide();
                        $("#divLoader").hide();
                    }
                },
                error: function (xhr, status, error) {
                    alert(xhr.statusText);
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
                        url: "UniversityInformation.aspx/SendMessage",
                        data: JSON.stringify(Message),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                           
                            $("#divLoader").hide();
                            $("#MainContent_txtSubject").val("");
                            $("#MainContent_txtMessage").val("");
                            $("#messagebox").hide();
                            $("#divLoader").show();
                            retriveConversation(msg.d);
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
            flag_showpopup = false;
            $(".msg_replay_show").hide(1000);
            $(".msg_replay_hide").show(1000);
        });

        //For Full details of University
        function Openpopup(universityUserName) {
            var University = {
                UniversityUserName: universityUserName,
            };
            $.ajax({
                type: "POST",
                beforeSend: function () { $("#LoadingImage").show(); },
                complete: function () { $("#LoadingImage").hide(); },
                url: "UniversityInformation.aspx/GetUnivercityData",
                data: JSON.stringify(University),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;
                    document.getElementById("MainContent_lbladdress").innerHTML = data[0] + " " + data[1] + " " + data[2];
                    if (data[3] != null)
                        document.getElementById("MainContent_lblgradustudent").innerHTML = data[3];
                    else
                        document.getElementById("MainContent_lblgradustudent").innerHTML = "Not Available";
                    if (data[4] != null)
                        document.getElementById("MainContent_lblundergradute").innerHTML = data[4];
                    else
                        document.getElementById("MainContent_lblundergradute").innerHTML = "Not Available";
                    if (data[5] != null) {
                        document.getElementById("MainContent_lblinternational").innerHTML = data[5]; MainContent_imgprofile;
                    }
                    else
                        document.getElementById("MainContent_lblinternational").innerHTML = "Not Available";

                    if (data[6] == null)
                        document.getElementById("MainContent_imgprofile").src = "/Images/no_image.jpg";
                    else
                        document.getElementById("MainContent_imgprofile").src = "/Images/Profile/" + data[6];

                    document.getElementById("MainContent_lblDescription").innerHTML = data[7];

                    if (data[8] != null)
                        document.getElementById("MainContent_lblundergradutefee").innerHTML = data[8];
                    else
                        document.getElementById("MainContent_lblundergradutefee").innerHTML = "Not Available";

                    if (data[9] != null)
                        document.getElementById("MainContent_lblgraduatefee").innerHTML = data[9];
                    else
                        document.getElementById("MainContent_lblgraduatefee").innerHTML = "Not Available";

                    if (data[10] != null)
                        document.getElementById("MainContent_lblbookfee").innerHTML = data[10];
                    else
                        document.getElementById("MainContent_lblbookfee").innerHTML = "Not Available";

                    if (data[11] != null)
                        document.getElementById("MainContent_lblroom").innerHTML = data[11];
                    else
                        document.getElementById("MainContent_lblroom").innerHTML = "Not Available";

                    if (data[12] != "")
                        document.getElementById("MainContent_lbldegreelvloffred").innerHTML = data[12];
                    else
                        document.getElementById("MainContent_lbldegreelvloffred").innerHTML = "Not Available";

                    if (data[13] != "")
                        document.getElementById("MainContent_lblcourceoffered").innerHTML = data[13];
                    else
                        document.getElementById("MainContent_lblcourceoffered").innerHTML = "Not Available";

                    document.getElementById("MainContent_lblusername").innerHTML = data[14];

                    $("#myModal").dialog({ modal: true, minWidth: 700, resizable: false, minHeight: 300, closeOnEscape: true, closeText: "" });

                },
                error: function (xhr, textStatus, errorThrown) {
                    alert('request failed' + JSON.stringify(xhr) + " " + JSON.stringify(textStatus) + " " + JSON.stringify(errorThrown));
                }
            });
        }
    </script>
</asp:Content>

