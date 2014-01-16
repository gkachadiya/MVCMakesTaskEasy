<%@ Page Title="StudentInfo" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="StudentInfo.aspx.cs" Inherits="SpotCollege.Admin.StudentInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="pattern_box">
        <div class="row-fluid">
            <div class="msg_replay_hide">
                <div id="Div1" title="Are You Sure?" style="display: none;">
                    <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Are you sure?</p>
                </div>
                <div class="span3 first">
                    <div class="searh_box">
                        <div class="shadowbox box_space">
                            <div class="h2_heading">
                                <h2>Student Search</h2>
                            </div>
                            <ul>
                                <li><span>Student From : </span>
                                    <asp:DropDownList ID="dllStudentCountry" runat="server"></asp:DropDownList>
                                </li>
                            </ul>
                            <ul>
                                <li class="month_date">
                                    <span>From Date :</span>
                                    <asp:TextBox ID="txtFromMonth" runat="server" MaxLength="2" placeholder="Month" CssClass="span3"></asp:TextBox>
                                    <asp:TextBox ID="txtFromYear" runat="server" MaxLength="4" placeholder="Year" CssClass="span3"></asp:TextBox>
                                </li>
                                <li class="month_date">
                                    <span>To Date :</span>
                                    <asp:TextBox ID="txtToMonth" runat="server" MaxLength="2" placeholder="Month" CssClass="span3"></asp:TextBox>
                                    <asp:TextBox ID="txtToYear" runat="server" MaxLength="4" placeholder="Year" CssClass="span3"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:Label ID="msgErr" runat="server" ForeColor="Red"></asp:Label>
                                </li>
                            </ul>
                            <ul>
                                <li>
                                    <span>Desired Country : </span>
                                    <asp:DropDownList ID="ddlDesiredCountry" runat="server"></asp:DropDownList>
                                </li>
                                <li>
                                    <span>Desired Term of Starting : </span>
                                    <asp:DropDownList ID="ddlTerms" runat="server"></asp:DropDownList>
                                </li>
                                <li>
                                    <span>Desired Level : </span>
                                    <asp:DropDownList ID="ddlSearchByLookingFor" runat="server"></asp:DropDownList>
                                </li>
                                <li>
                                    <span>Desired Program : </span>
                                    <asp:DropDownList ID="ddlPrograms" runat="server"></asp:DropDownList>
                                </li>
                            </ul>
                            <ul class="checkbox_list">
                                <li>
                                    <asp:CheckBox runat="server" ID="chkActive" Text="Is Active" />
                                </li>
                                <li>
                                    <asp:CheckBox runat="server" ID="chnEduInfo" Text="Not entered any education information" />
                                </li>
                                <li>
                                    <asp:CheckBox runat="server" ID="chkCertiImg" Text="Not uploaded any certificate images" />
                                </li>
                                <li>
                                    <asp:CheckBox runat="server" ID="chkSelfImg" Text="Not uploaded self photo" />
                                </li>
                                <li>
                                    <asp:CheckBox runat="server" ID="chkEduPref" Text="Not completed educational prefrences" />
                                </li>
                                <li>
                                    <asp:CheckBox runat="server" ID="chkIntScore" Text="Not entered any International test scores" />
                                </li>
                                <li>
                                    <asp:CheckBox runat="server" ID="chnNotActive" Text="Is not active" />
                                </li>
                            </ul>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="button" />
                            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnSearch_Click" CssClass="button" />
                        </div>
                    </div>
                </div>
                <div class="span9">

                    <div id="LoadingImage" style="display: none">
                        <asp:Image ID="ImgLoader" runat="server" ImageUrl="~/Images/ajaxloader.gif" />
                    </div>

                    <div class="shadowbox box_space">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <asp:HiddenField ID="hndUserName" Value="" runat="server" />
                        <asp:HiddenField ID="hndStudentId" Value="0" runat="server" />

                        <%--Student Basic Info--%>
                        <asp:Panel ID="pnlBasicStudentInfo" runat="server">
                            <div id="studentInfo" runat="server" class="width100per">
                                <div class="h2_heading">
                                    <h2>Students Information</h2>
                                    <asp:LinkButton ID="lnkbtnAddNewStudent" runat="server" OnClick="lnkbtnAddNewStudent_Click" Text="Add New Student" class="edit fright"></asp:LinkButton>
                                </div>
                                <asp:Label runat="server" ID="lblTotalResult"></asp:Label>
                                Results Found<br />
                                <asp:HiddenField ID="hdnAllRecipients" runat="server" Value="none" />
                                <asp:GridView ID="GrvBasicStudentInfo" runat="server" CssClass="table pagination" AutoGenerateColumns="false" DataKeyNames="StudentId" EmptyDataText="No Student Detail Found...!" Width="100%" AllowPaging="true" PageSize="10" OnPageIndexChanging="GrvBasicStudentInfo_PageIndexChanging" AllowSorting="true" OnSorting="GrvBasicStudentInfo_Sorting">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="4%" ItemStyle-Width="4%">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("StudentId") %>' />
                                                <asp:HiddenField ID="hdnUserName" runat="server" Value='<%#Eval("UserName") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="UserName" HeaderStyle-Width="12%" ItemStyle-Width="12%">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("UserName") %>' Style="display: none"></asp:Label>
                                                <a id='<%#Eval("UserName")%>' href="javascript:OpenFullInfoPopup('<%#Eval("UserName")%>')"><%#Eval("UserName") %></a>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Student Name" HeaderStyle-Width="10%" ItemStyle-Width="10%" SortExpression="Student Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("FirstName") +" "+ Eval("LastName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBtnEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("StudentId") %>' OnClick="lnkBtnEdit_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <%--<asp:BoundField DataField="Address1" HeaderText="Address" HeaderStyle-Width="10%" ItemStyle-Width="10%"></asp:BoundField>
                                <asp:BoundField DataField="City" HeaderText="City" HeaderStyle-Width="5%" ItemStyle-Width="5%"></asp:BoundField>
                                <asp:BoundField DataField="BirthDate" HeaderText="Birth Date" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Is Active" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkStatus" runat="server" AutoPostBack="true" OnCheckedChanged="chkStatus_CheckedChanged"
                                                    Checked='<%# Convert.ToBoolean(Eval("User.IsActive")) %>' Text='<%# Eval("User.IsActive").ToString().Equals("True") ? " Active " : " InActive" %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="More" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkBtnViewMore" runat="server" Text="More" CommandArgument='<%#Eval("UserName") %>' OnClick="lnkBtnViewMore_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                        <%-- <asp:TemplateField HeaderText="Academics Details" HeaderStyle-Width="12%" ItemStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkBtnAcademicsDetails" runat="server" Text="Academics Details" CommandArgument='<%#Eval("StudentId") %>' OnClick="lnkBtnAcademicsDetails_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                        <%-- <asp:TemplateField HeaderText="Work History" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkBtnWorkHistory" runat="server" Text="Work History" CommandArgument='<%#Eval("StudentId") %>' OnClick="lnkBtnWorkHistory_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Message" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <a id="lnkMessage" href="javascript:OpenMsgPopup('<%#Eval("UserName")%>')">Message</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <a id='lnkBtnDelete<%#Eval("StudentId") %>' href="javascript:StudentDelete('<%#Eval("StudentId")%>')">Delete</a>
                                                <%--<asp:LinkButton ID="lnkBtnDelete" runat="server" Text="Delete" CommandArgument='<%#Eval("StudentId") %>' OnClick="lnkBtnDelete_Click" OnClientClick="return confirmDelete();"></asp:LinkButton>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <div id="noUniversityMsg" style="display: none">
                                    <br />
                                    <br />
                                    No More Student
                                </div>
                                <div id="waitdiv" style="display: none">
                                    please wait...
                                </div>

                                <div>
                                    <div style="width: 20px; float: left">&nbsp;</div>
                                    <asp:CheckBox ID="chkAll" runat="server" Text="&nbsp;&nbsp;Check All Search Result" />&nbsp;&nbsp;
                            <asp:Button ID="btnSend" Text="Send Message" CssClass="large_button" OnClientClick="javascript:return false;" runat="server" />
                                    <asp:Button ID="btnDelete" Text="Delete" CssClass="large_button" runat="server" OnClientClick="javascript:return isRefresh();" OnClick="btnDelete_Click" />
                                </div>

                            </div>
                        </asp:Panel>

                        <%--student Edit Information--%>
                        <div class="span8">

                            <asp:Panel ID="pnlEditBasicStudentInfo" runat="server" Visible="false">
                                <ul class="profile_form">
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                Email :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:TextBox ID="txtEmail" runat="server" Enabled="false"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtEmail"
                                                CssClass="field-validation-error" ErrorMessage="The Email field is required." ValidationGroup="EditProfileValidation" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" CssClass="field-validation-error" ControlToValidate="txtEmail" ValidationGroup="EditProfileValidation" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid Email"></asp:RegularExpressionValidator>
                                            <span id="lblDuplicateuser" style="display: none" class="field-validation-error">UserName already exist in Record.</span>

                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                Password :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtPassword"
                                                CssClass="field-validation-error" ErrorMessage="The Password field is required." ValidationGroup="EditProfileValidation" />
                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                Confirm Password :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtConfirmPassword" CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The confirm password field is required." ValidationGroup="EditProfileValidation" />
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." ValidationGroup="EditProfileValidation" />
                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                First Name :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="field-validation-error" ControlToValidate="txtFirstName" ErrorMessage="First Name is Required" ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                Middle Name :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:TextBox ID="txtMiddleName" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="field-validation-error" ControlToValidate="txtMiddleName" ErrorMessage="Middle Name is Required" ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                Last Name :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="field-validation-error" ControlToValidate="txtLastName" ErrorMessage="Last Name is Required" ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                Address1 :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:TextBox ID="txtAddress1" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="field-validation-error" ControlToValidate="txtAddress1" ErrorMessage="Address1 is Required" ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                Address 2 :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:TextBox ID="txtAddress2" runat="server"></asp:TextBox>
                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                City :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="field-validation-error" ControlToValidate="txtCity" ErrorMessage="City is Required" ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                Country :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:DropDownList ID="ddlCountry" runat="server"></asp:DropDownList>
                                            <asp:Label ID="lblCountryValidation" runat="server" Text="Please Select Any" Visible="false" CssClass="field-validation-error"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" CssClass="field-validation-error" ControlToValidate="ddlCountry" InitialValue="0" ErrorMessage="Country is Required" ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>

                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                Primary Number :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:DropDownList ID="ddlPhoneType" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" CssClass="field-validation-error" ControlToValidate="ddlPhoneType" InitialValue="0" ErrorMessage="PhoneType is Required" ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                                            <asp:Label ID="lblPrimaryPhoneTypeValidation" runat="server" Text="Please Select Any" Visible="false" CssClass="field-validation-error"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="field-validation-error" ControlToValidate="txtPrimaryNumber" ErrorMessage="Primary Number is Required" ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="field-validation-error" ValidationExpression="\d{0,15}" runat="server" ControlToValidate="txtPrimaryNumber" ErrorMessage="Invalid Number" ValidationGroup="EditProfileValidation"></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="span2">
                                            <asp:TextBox ID="txtPrimaryNumber" runat="server" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                Secondary Number :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:DropDownList ID="ddlSecondaryPhoneType" runat="server"></asp:DropDownList>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" CssClass="field-validation-error" ValidationExpression="\d{0,15}" runat="server" ControlToValidate="txtSecondaryNumber" ErrorMessage="Invalid Number" ValidationGroup="EditProfileValidation"></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="span2">
                                            <asp:TextBox ID="txtSecondaryNumber" runat="server" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                Primary Email :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:TextBox ID="txtPrimaryEmail" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="field-validation-error" ControlToValidate="txtPrimaryEmail" ErrorMessage="Primary Email is Required" ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" CssClass="field-validation-error" ControlToValidate="txtPrimaryEmail" ValidationGroup="EditProfileValidation" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid Email"></asp:RegularExpressionValidator>
                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                Birth Date :</label>
                                        </div>
                                        <div class="span2">
                                            <span class="datepickericon"></span>
                                            <asp:TextBox ID="txtBirthDate" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="field-validation-error" ControlToValidate="txtBirthDate" ErrorMessage="Birth Date is Required" ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>

                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                Country of Citizenship :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:DropDownList ID="ddlCountryOfCitizenship" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" CssClass="field-validation-error" ControlToValidate="ddlCountryOfCitizenship" InitialValue="0" ErrorMessage="CountryOfCitizenship is Required" ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>
                                            <asp:Label ID="lblCountryOfCitizenshipCountryValidation" runat="server" Text="Please Select Any" Visible="false" CssClass="field-validation-error"></asp:Label>

                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                I am currently in  :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:DropDownList ID="ddlCurrentlyIn" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" CssClass="field-validation-error" ControlToValidate="ddlCurrentlyIn" InitialValue="0" ErrorMessage="CurrentlyIn is Required" ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>

                                            <asp:Label ID="lblCurrentlyInValidation" runat="server" Text="Please Select Any" Visible="false" CssClass="field-validation-error"></asp:Label>

                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                program looking for in  :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:DropDownList ID="ddlLookingFor" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" CssClass="field-validation-error" ControlToValidate="ddlLookingFor" InitialValue="0" ErrorMessage="LookingFor is Required" ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>

                                            <asp:Label ID="lblLookingForValidation" runat="server" Text="Please Select Any" Visible="false" CssClass="field-validation-error"></asp:Label>

                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                I prefer to study in  :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:DropDownList ID="ddlLookingForCountry" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" CssClass="field-validation-error" ControlToValidate="ddlLookingForCountry" InitialValue="0" ErrorMessage="LookingForCountry is Required" ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>

                                            <asp:Label ID="lblLookingForCountryValidation" runat="server" Text="Please Select Any" Visible="false" CssClass="field-validation-error"></asp:Label>

                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                Expected Start Date    :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:DropDownList ID="ddlStartDate" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" CssClass="field-validation-error" ControlToValidate="ddlStartDate" InitialValue="0" ErrorMessage="Expected Start Date is Required" ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>

                                            <asp:Label ID="lblStartDateValidation" runat="server" Text="Please Select Any" Visible="false" CssClass="field-validation-error"></asp:Label>

                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                I can afford to pay    :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:DropDownList ID="ddlPayout" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" CssClass="field-validation-error" ControlToValidate="ddlPayout" InitialValue="0" ErrorMessage="I can afford to pay  is Required" ValidationGroup="EditProfileValidation"></asp:RequiredFieldValidator>

                                            <asp:Label ID="lblPayoutValidation" runat="server" Text="Please Select Any" Visible="false" CssClass="field-validation-error"></asp:Label>

                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                Sports :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:TextBox ID="txtSports" runat="server" TextMode="MultiLine"></asp:TextBox>

                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                Leadership :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:TextBox ID="txtLeadership" runat="server" TextMode="MultiLine"></asp:TextBox>

                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                Other Activities :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:TextBox ID="txtOtherActivities" runat="server" TextMode="MultiLine"></asp:TextBox>

                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                Upload Image :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:FileUpload ID="fuProfileImage" runat="server" />
                                            <img id="imgProfileImage" runat="server" visible="false" height="40" width="40" src="" />

                                        </div>
                                    </li>
                                    <div class="row">
                                        <div class="span2">
                                        </div>
                                        <div class="span3">
                                            <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update" ValidationGroup="EditProfileValidation" class="large_button" />
                                            <asp:Button ID="btnCancelEditUser" runat="server" Text="Cancel" OnClick="btnCancelEditUser_Click" class="large_button" />

                                        </div>
                                    </div>
                                </ul>
                            </asp:Panel>
                        </div>

                        <%--student edit Academics details--%>
                        <div class="span8">
                            <asp:Panel ID="pnlCurrentAcademics" Visible="false" runat="server">
                                <asp:HiddenField ID="hndStudentAcademicsId" Value="0" runat="server" />
                                <ul class="profile_form">
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                School name :</label>
                                        </div>

                                        <div class="span2">
                                            <asp:TextBox ID="txtSchoolName" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="School Name is Required" CssClass="field-validation-error" ControlToValidate="txtSchoolName" ValidationGroup="AcademicsValidation"></asp:RequiredFieldValidator>
                                        </div>
                                    </li>

                                    <li class="row">
                                        <div class="span2">
                                            <label>School City :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:TextBox ID="txtSchoolCity" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="School City is Required" CssClass="field-validation-error" ControlToValidate="txtSchoolCity" ValidationGroup="AcademicsValidation"></asp:RequiredFieldValidator>
                                        </div>
                                    </li>

                                    <li class="row">
                                        <div class="span2">
                                            <label>School Country :</label>
                                        </div>
                                        <div class="span2">

                                            <asp:DropDownList ID="ddlSchoolCountry" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" CssClass="field-validation-error" ControlToValidate="ddlSchoolCountry" InitialValue="0" ErrorMessage="School Country is Required" ValidationGroup="AcademicsValidation"></asp:RequiredFieldValidator>
                                            <asp:Label ID="lblSchoolCountryValidation" runat="server" Text="Please Select Any" Visible="false" CssClass="field-validation-error"></asp:Label>
                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>Did you graduate  :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:RadioButtonList ID="rdoDidYouGraduate" runat="server" onclick="GetRadioButtonListSelectedValue(this);" CssClass="list_6" RepeatLayout="UnorderedList"></asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" CssClass="field-validation-error" ControlToValidate="rdoDidYouGraduate" ErrorMessage="Please Select Any" ValidationGroup="AcademicsValidation"></asp:RequiredFieldValidator>
                                        </div>
                                    </li>
                                    <asp:Panel ID="pnlGraduateDetail" runat="server" Style="display: none;">
                                        <ul class="profile_form">
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Year of graudation :</label>
                                                </div>
                                                <div class="span2">
                                                    <asp:DropDownList ID="ddlYearOfGraduation" runat="server"></asp:DropDownList>
                                                    <asp:Label ID="lblyeargradution" runat="server" Text="Select Year Of Garduation" Style="display: none;" CssClass="field-validation-error"></asp:Label>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" CssClass="field-validation-error" ControlToValidate="ddlYearOfGraduation" InitialValue="0" ErrorMessage="YearOfGraduation is Required" ValidationGroup="AcademicsValidation"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </li>
                                            <li class="row">
                                                <div class="span2">
                                                    <label>
                                                        Level completed :</label>
                                                </div>
                                                <div class="span2">
                                                    <asp:DropDownList ID="ddlLevelCompleted" runat="server"></asp:DropDownList>
                                                    <asp:Label ID="lbllevelcompleted" runat="server" Text="Select level Completed" Style="display: none;" CssClass="field-validation-error"></asp:Label>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" CssClass="field-validation-error" ControlToValidate="ddlLevelCompleted" InitialValue="0" ErrorMessage="LevelCompleted is Required" ValidationGroup="AcademicsValidation"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </li>
                                        </ul>
                                    </asp:Panel>

                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                GPA or Ranking in class :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:TextBox ID="txtRankingInClass" runat="server"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" CssClass="field-validation-error" ValidationExpression="\d{0,2}.\d{0,2}" runat="server" ControlToValidate="txtRankingInClass" ErrorMessage="Invalid Ranking" ValidationGroup="AcademicsValidation"></asp:RegularExpressionValidator>
                                        </div>
                                    </li>

                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                Certificate :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:FileUpload ID="fuCertificate" runat="server" />
                                            <asp:RequiredFieldValidator ID="ReqFldValImage" runat="server" CssClass="field-validation-error" ErrorMessage="Please Upload Certificate" ControlToValidate="fuCertificate" ValidationGroup="AcademicsValidation"></asp:RequiredFieldValidator>
                                            <asp:Label ID="lblImageValidation" runat="server" Text="Please Upload Image" Visible="false" CssClass="field-validation-error"></asp:Label>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="fuCertificate" runat="server" ErrorMessage="Upload Certificate" CssClass="field-validation-error" ValidationGroup="AcademicsValidation"></asp:RequiredFieldValidator>--%>
                                            <img id="imgCertificate" runat="server" visible="false" height="50" width="50" />
                                        </div>
                                    </li>


                                    <div class="row">
                                        <div class="span2">
                                        </div>
                                        <div class="span5">
                                            <asp:Button ID="btnSaveAcademics" runat="server" Text="Save" OnClick="btnSaveAcademics_Click" ValidationGroup="AcademicsValidation" class="large_button" />
                                            <asp:Button ID="btnCancelAcademic" runat="server" Text="Cancel" OnClick="btnCancelAcademic_Click" class="large_button" />

                                            <%--<asp:Button ID="btnSubmitAcademics" runat="server" Text="Save & Continue" OnClick="btnSubmitAcademics_Click" ValidationGroup="AcademicsValidation" />--%>
                                        </div>
                                    </div>
                                </ul>
                            </asp:Panel>
                        </div>

                        <%--student edit work history--%>
                        <div class="span8">
                            <asp:Panel ID="pnlEditWorkHistory" runat="server" Visible="false">
                                <asp:HiddenField ID="hndStudentWorkHistoryId" Value="0" runat="server" />
                                <ul class="profile_form">
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                Company Name :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Company Name is Required" CssClass="field-validation-error" ControlToValidate="txtCompanyName" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                Position :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:TextBox ID="txtWorkPosition" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Position Name is Required" CssClass="field-validation-error" ControlToValidate="txtWorkPosition" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">

                                            <label>
                                                Start Date :</label>
                                        </div>
                                        <div class="span2">
                                            <span class="datepickericon"></span>
                                            <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Start Date Name is Required" CssClass="field-validation-error" ControlToValidate="txtStartDate" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                End Date :</label>
                                        </div>
                                        <div class="span2">
                                            <span class="datepickericon"></span>

                                            <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="End Date Name is Required" CssClass="field-validation-error" ControlToValidate="txtEndDate" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                        </div>
                                    </li>
                                    <li class="row">
                                        <div class="span2">
                                            <label>
                                                Resposibilities :</label>
                                        </div>
                                        <div class="span2">
                                            <asp:TextBox ID="txtResposibilities" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="Resposibilities is Required" CssClass="field-validation-error" ControlToValidate="txtResposibilities" ValidationGroup="WorkHistoryValidation"></asp:RequiredFieldValidator>
                                        </div>
                                    </li>
                                    <div class="row">
                                        <div class="span2">
                                        </div>
                                        <div class="span6">
                                            <asp:Button ID="btnSaveWorkHistory" runat="server" Text="Save" OnClick="btnSaveWorkHistory_Click" ValidationGroup="WorkHistoryValidation" class="large_button" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" class="large_button" />

                                        </div>
                                    </div>
                                </ul>
                            </asp:Panel>
                        </div>

                        <%--Student Full Details--%>
                        <asp:Panel ID="pnlStudentFullDetails" runat="server" Visible="false">
                            <%--<div id="BasicOverview" runat="server">
                        <div class="h2_heading">
                            <h2>Students Full Information</h2>
                            <asp:LinkButton ID="lnkbtnFullinfoBack" runat="server" OnClick="lnkbtnFullinfoBack_Click" class="back">Back</asp:LinkButton>
                        </div>
                        <ul class="list_4">
                            <li>
                                <asp:Label ID="lblFirstName" runat="server"></asp:Label></li>
                            <li>
                                <asp:Label ID="lblMiddleName" runat="server"></asp:Label></li>
                            <li>
                                <asp:Label ID="lblLastName" runat="server"></asp:Label></li>
                            <li>
                                <asp:Label ID="lblAddress1" runat="server"></asp:Label></li>
                            <li>
                                <asp:Label ID="lblAddress2" runat="server"></asp:Label></li>
                            <li>
                                <asp:Label ID="lblCity" runat="server"></asp:Label></li>
                            <li>
                                <asp:Label ID="lblCountry" runat="server"></asp:Label></li>
                            <li>
                                <asp:Label ID="lblPrimaryNumber" runat="server"></asp:Label></li>
                            <li>
                                <asp:Label ID="lblSecondaryNumber" runat="server"></asp:Label></li>
                            <li>
                                <asp:Label ID="lblPrimaryEmail" runat="server"></asp:Label></li>
                            <li>
                                <asp:Label ID="lblBirthDate" runat="server"></asp:Label></li>
                            <li>
                                <asp:Label ID="lblCountryOfCitizenship" runat="server"></asp:Label></li>
                        </ul>
                    </div>--%>
                        </asp:Panel>

                        <%--Student Academics Details--%>
                        <asp:Panel ID="pnlAcademicsDetail" runat="server" Visible="false">
                            <div id="CurrentAcademics" runat="server">
                                <div class="h2_heading">
                                    <h2>Students Academics Details</h2>
                                    <asp:LinkButton ID="lnkbtnBackAcademicsdetail" runat="server" OnClick="lnkbtnBackAcademicsdetail_Click" class="back">Back</asp:LinkButton>
                                    <asp:LinkButton ID="lnkBtnAddNewAcademicDetail" runat="server" OnClick="lnkBtnAddNewAcademicDetail_Click" class="edit fright">Add New</asp:LinkButton>
                                </div>
                                <b class="studentid">
                                    <asp:Label ID="lblStudentName" runat="server"></asp:Label></b>
                                <asp:GridView ID="grvAcademicDetails" CssClass="table box" runat="server" AutoGenerateColumns="false" DataKeyNames="StudentAcademicsId" EmptyDataText="No Academic Record Found...!" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="School Name" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBtnAcademicEdit" runat="server" Text='<%# Eval("SchoolName")%>' CommandArgument='<%#Eval("StudentAcademicsId") %>' OnClick="lnkBtnAcademicEdit_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SchoolCity" HeaderText="School City" HeaderStyle-Width="8%" ItemStyle-Width="8%"></asp:BoundField>
                                        <asp:BoundField DataField="SchoolCountry" HeaderText="School Country" HeaderStyle-Width="10%" ItemStyle-Width="10%"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Graduate?" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGraduateStatus" runat="server" Text='<%# ((SpotCollege.Common.GraduateStatus)(Eval("Graduate"))).ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="GraduationYear" HeaderText="Graduation Year" HeaderStyle-Width="10%" ItemStyle-Width="10%"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Graduation Level" HeaderStyle-Width="13%" ItemStyle-Width="13%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGraduateLevel" runat="server" Text='<%# Eval("GraduationLevel")==null?"":((SpotCollege.Common.LevelCompleted)(Eval("GraduationLevel"))).ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Rank" HeaderText="Rank" HeaderStyle-Width="5%" ItemStyle-Width="5%"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Certificate" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <img id="imgCertificate" src='<%# "..\\Images\\Certificate\\"+Eval("CertificatePath") %>' height="30" width="30" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBtnAcademicDelete" runat="server" Text="Delete" CommandArgument='<%#Eval("StudentAcademicsId") %>' OnClick="lnkBtnAcademicDelete_Click" OnClientClick="return confirmDelete();"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>

                        <%--Student Work History--%>
                        <asp:Panel ID="pnlWorkHistory" runat="server" Visible="false">
                            <div id="WorkHistory" runat="server">
                                <div class="h2_heading">
                                    <h2>Work history</h2>
                                    <asp:LinkButton ID="lnkbtnBackworkhistory" runat="server" OnClick="lnkbtnBackworkhistory_Click" class="back">Back</asp:LinkButton>
                                    <asp:LinkButton ID="LnkbtnAddnewWorkHistory" runat="server" OnClick="LnkbtnAddnewWorkHistory_Click" class="edit fright">Add New</asp:LinkButton>
                                </div>
                                <b class="studentid">
                                    <asp:Label ID="lblstdname" runat="server"></asp:Label></b>
                                <asp:GridView ID="grvWorkHistory" runat="server" CssClass="table box" AutoGenerateColumns="false" DataKeyNames="StudentWorkHistoryId" EmptyDataText="No History Record Found..!" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Company Name" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEditWorkHistory" runat="server" Text='<%# Eval("CompanyName")%>' OnClick="lnkEditWorkHistory_Click" CommandArgument='<%# Eval("StudentWorkHistoryId") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Position" HeaderText="Position" HeaderStyle-Width="10%" ItemStyle-Width="10%"></asp:BoundField>
                                        <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-Width="5%" ItemStyle-Width="5%"></asp:BoundField>
                                        <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-Width="5%" ItemStyle-Width="5%"></asp:BoundField>
                                        <asp:BoundField DataField="Responsibilities" HeaderText="Responsibilities" HeaderStyle-Width="10%" ItemStyle-Width="10%"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDeleteWorkHistory" runat="server" Text="Delete" OnClick="lnkDeleteWorkHistory_Click" CommandArgument='<%# Eval("StudentWorkHistoryId") %>' OnClientClick="return confirmDelete();"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>

                    </div>
                </div>
            </div>


            <div class="msg_replay_show messagereplayfrom">
                <div class="span5">
                    <span class="msg_replay_close">Close</span>
                    <div class="trsnsparentnone_box">
                        <div class="message_right shadowbox">
                            <asp:GridView ID="GrvBasicStudentInfo2" runat="server" CssClass="table pagination" AutoGenerateColumns="false" DataKeyNames="StudentId" EmptyDataText="No Student Detail Found...!" Width="100%" AllowPaging="true" PageSize="10">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-Width="4%" ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                            <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("StudentId") %>' />
                                            <asp:HiddenField ID="hdnUserName" runat="server" Value='<%#Eval("UserName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="UserName" HeaderStyle-Width="12%" ItemStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("UserName") %>' Style="display: none"></asp:Label>
                                            <a id='<%#Eval("UserName")%>' href="javascript:OpenFullInfoPopup('<%#Eval("UserName")%>')"><%#Eval("UserName") %></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Student Name" HeaderStyle-Width="10%" ItemStyle-Width="10%" SortExpression="Student Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("FirstName") +" "+ Eval("LastName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBtnEdit" runat="server" Text="Edit" CommandArgument='<%#Eval("UserName") %>' OnClick="lnkBtnEdit_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" Display="Dynamic" runat="server" CssClass="field-validation-error" ErrorMessage="Enter Subject" ControlToValidate="txtSubject" ValidationGroup="sendmessage"></asp:RequiredFieldValidator>
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
                                <img src="Images/ajaxloader.gif" />
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
    <%--For Full information of Student--%>
    <div id="FullInfo" style="display: none" title="Student Information">
        <div class="row-fluid">
            <div class="span6">
                <div id="BasicOverview" runat="server">
                    <div class="shadowbox">
                        <div class="h2_heading">
                            <div class="accordion-header">
                                <h2>Basics Information</h2>
                            </div>
                        </div>

                        <div class="accordion-content">
                            <ul class="list_4">
                                <li>
                                    <label>
                                        First Name :</label>
                                    <asp:Label ID="lblFirstName" runat="server"></asp:Label>

                                </li>
                                <li>
                                    <label>
                                        Middle Name :</label>
                                    <asp:Label ID="lblMiddleName" runat="server"></asp:Label>
                                </li>
                                <li>
                                    <label>
                                        Last Name :</label>
                                    <asp:Label ID="lblLastName" runat="server"></asp:Label>
                                </li>
                                <li>
                                    <label>
                                        Country of Citizenship :</label>
                                    <asp:Label ID="lblCountryOfCitizenship" runat="server"></asp:Label>
                                </li>
                                <li>
                                    <label>
                                        Address 1 :</label>
                                    <asp:Label ID="lblAddress1" runat="server"></asp:Label>

                                </li>
                                <li>
                                    <label>
                                        Address 2 :</label>
                                    <asp:Label ID="lblAddress2" runat="server"></asp:Label>
                                </li>
                                <li>
                                    <label>
                                        Zip Code :</label>
                                    <asp:Label ID="lblzipcode" runat="server"></asp:Label>
                                </li>
                                <li>
                                    <label>
                                        Primary Phone :</label>
                                    <asp:Label ID="lblPrimaryNumber" runat="server"></asp:Label>
                                </li>
                                <li>
                                    <label>
                                        Secondary Phone :</label>
                                    <asp:Label ID="lblSecondaryNumber" runat="server"></asp:Label>
                                </li>
                                <li>
                                    <label>
                                        Birth Date :</label>
                                    <asp:Label ID="lblBirthDate" runat="server"></asp:Label>
                                </li>
                                <li>
                                    <label>
                                        City :</label>
                                    <asp:Label ID="lblCity" runat="server"></asp:Label>

                                </li>
                                <li>
                                    <label>
                                        Country :</label>
                                    <asp:Label ID="lblCountry" runat="server"></asp:Label>

                                </li>
                                <li>
                                    <label>
                                        Primary Email :</label>
                                    <asp:Label ID="lblPrimaryEmail" runat="server"></asp:Label>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <br />
                <div id="EducationPreferences" runat="server">
                    <div class="shadowbox">
                        <div class="h2_heading">
                            <div class="accordion-header">
                                <h2>Education Preferences</h2>
                            </div>
                        </div>

                        <div class="accordion-content">
                            <ul class="list_4">
                                <li>
                                    <label>
                                        I am currently in :</label>
                                    <asp:Label ID="lblCurrentlyIn" runat="server"></asp:Label>
                                </li>
                                <li>
                                    <label>
                                        program looking for :</label>
                                    <asp:Label ID="lblLookingFor" runat="server"></asp:Label>
                                </li>
                                <li>
                                    <label>
                                        I prefer to study in :</label>

                                    <asp:Label ID="lblPreferStudyIn" runat="server"></asp:Label>
                                </li>
                                <li>
                                    <label>
                                        Expected Start Date :</label>
                                    <asp:Label ID="lblExpectedStartDate" runat="server"></asp:Label>
                                </li>
                                <li>
                                    <label>
                                        I can afford to pay :</label>
                                    <asp:Label ID="lblPayout" runat="server"></asp:Label>
                                </li>
                                <li>
                                    <label>
                                        Desired Field of Study:</label>
                                    <asp:Label ID="lbldesiredfieldstudy" runat="server"></asp:Label>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <br />
                <div class="shadowbox right_space">
                    <div class="h2_heading">
                        <div class="accordion-header">
                            <h2>Photo :</h2>
                        </div>
                    </div>
                    <div class="accordion-content">
                        <ul class="list_4">

                            <li>
                                <label>
                                    profile Image :</label>
                                <span>
                                    <%--<a href="" runat="server" id="hoverimg" class="preview"></a>--%>
                                    <img id="imgStudent" class="large_images" runat="server" src="" /></span> </li>

                        </ul>
                    </div>
                </div>
                <br />
            </div>

            <div class="span6">
                <div id="Div2" runat="server">
                    <div class="shadowbox right_space">
                        <div class="h2_heading">
                            <div class="accordion-header">
                                <h2>Current Academics</h2>
                            </div>
                        </div>
                        <div class="accordion-content" id="tblCurrentAcademics">
                        </div>
                    </div>
                </div>
                <br />
                <div id="internationalTest" runat="server">
                    <div class="shadowbox right_space">
                        <div class="h2_heading">
                            <div class="accordion-header">
                                <h2>International Test</h2>
                            </div>
                        </div>
                        <div class="accordion-content" id="tblInternationalTest">
                        </div>
                    </div>
                </div>
                <br />

                <div id="Div3" runat="server">
                    <div class="shadowbox right_space">
                        <div class="h2_heading">
                            <div class="accordion-header">
                                <h2>Work history</h2>
                            </div>
                        </div>
                        <div class="accordion-content" id="tblWorkHistory">
                        </div>
                    </div>
                </div>
                <br />

                <div id="CurricularActivities" runat="server">
                    <div class="shadowbox right_space">
                        <div class="h2_heading">
                            <div class="accordion-header">
                                <h2>Extra Curricular Activies</h2>
                            </div>
                        </div>
                        <div class="accordion-content">
                            <ul class="list_4">
                                <li>
                                    <label>Sports :</label>
                                    <span id="lblSports"></span>
                                </li>
                                <li>
                                    <label>Leadership :</label>
                                    <span id="lblLeadership"></span>
                                </li>
                                <li>
                                    <label>Activites :</label>
                                    <span id="lblActivity"></span>
                                </li>
                            </ul>
                        </div>
                    </div>
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
                            <div class="span5">
                                <asp:TextBox runat="server" ID="txtGrpSendTo" Enabled="false" />
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span4">
                                <label>Subject :</label>
                            </div>
                            <div class="span5">
                                <asp:TextBox ID="txtGrpSubject" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" Display="Dynamic" runat="server" CssClass="field-validation-error" ErrorMessage="Enter Subject" ControlToValidate="txtSubject" ValidationGroup="sendmessage"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="row-fluid">
                            <div class="span4">
                                <label>Message :</label>
                            </div>
                            <div class="span6">
                                <asp:TextBox ID="txtGrpDesc" runat="server" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" Display="Dynamic" CssClass="field-validation-error" ErrorMessage="Enter Message" ControlToValidate="txtMessage" ValidationGroup="sendmessage"></asp:RequiredFieldValidator>
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

    <div id="Dialog-Send" title="Message Send" style="display: none;">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Message Send Successfully</p>
    </div>
    <div id="Dialog-Alert" title="" style="display: none;">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Please Enter Message Text</p>
    </div>
    <div id="Dialog-SelectAlert" title="" style="display: none;">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Please Select Student</p>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#MainContent_txtBirthDate").datepicker();

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

            $("#MainContent_txtEmail").change(function () {
                // alert("kk");
                if ($("#MainContent_txtEmail").val() != "") {
                    if (Page_IsValid) {
                        var University = {
                            UserName: $('#MainContent_txtEmail').val(),
                        };
                        $.ajax({
                            type: "POST",
                            url: "StudentInfo.aspx/ChechUniversityDuplicate",
                            data: JSON.stringify(University),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                if (msg.d != 'true') {
                                    $('#MainContent_btnUpdate').show();
                                    $('#lblDuplicateuser').hide();
                                }
                                else {
                                    $('#MainContent_btnUpdate').hide();
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
            $("#lnkStudent").addClass("navi_active");

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
            });

            var selectedvalue = document.getElementsByName('ctl00$MainContent$rdoDidYouGraduate');
            try {
                if (selectedvalue.item(0).checked) {
                    document.getElementById('MainContent_pnlGraduateDetail').style.display = 'inline';
                }
                else {
                    document.getElementById('MainContent_pnlGraduateDetail').style.display = 'none';
                }
            } catch (ex) { }
        });

        $(function () {
            $(".msg_replay_show").hide();
        });
        $(".msg_replay_close").click(function () {
            flag_showpopup = false;
            $(".msg_replay_show").hide(1000);
            $(".msg_replay_hide").show(1000);
        });




        function GetRadioButtonListSelectedValue(rdoDidYouGraduate) {
            //alert("kk");
            for (var i = 0; i < rdoDidYouGraduate.rows.length; ++i) {

                if (rdoDidYouGraduate.rows[i].cells[0].firstChild.checked) {

                    //alert(rdoDidYouGraduate.rows[i].cells[0].firstChild.value);
                    var rdvlaue = rdoDidYouGraduate.rows[i].cells[0].firstChild.value;
                    if (rdvlaue == 1) {
                        //alert("kk");
                        document.getElementById('MainContent_pnlGraduateDetail').style.display = 'inline';
                    }
                    else {
                        document.getElementById('MainContent_pnlGraduateDetail').style.display = 'none';
                    }
                }

            }

        }

        function confirmDelete() {
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

        //For Open Massegebox
        function OpenMsgPopup(sendToUserName) {
            // alert("kk");
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
                        url: "StudentInfo.aspx/SendMessage",
                        data: JSON.stringify(Message),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            // alert("Message send succesfully");
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
                                    if (msg.d) {
                                        $("#msgdel" + messageId).parent().parent().parent().remove();
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

        function StudentDelete(studentId) {
            $("#dialog-confirm").dialog({
                open: function (event, ui) { $(".ui-dialog-titlebar-close .ui-button-text").hide(); },
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "Yes": function () {
                        if (Page_IsValid) {
                            var Std = {
                                StudentId: studentId
                            };

                            $.ajax({
                                type: "POST",
                                beforeSend: function () { $("#LoadingImage").show(); },
                                complete: function () { $("#LoadingImage").hide(); },
                                url: "StudentInfo.aspx/StudentDelete",
                                data: JSON.stringify(Std),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    $("#lnkBtnDelete" + studentId).parent().parent().remove();
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

        function getAllCheckedItem() {
            var username = Array();

            var chk = $("input[id^=MainContent_GrvBasicStudentInfo_chkSelect]");

            for (i = 0; i < chk.length; i++) {
                if (chk[i].checked) {
                    var tmp = chk[i].id;
                    tmp = tmp.substr(tmp.lastIndexOf('_') + 1);
                    username.push($("#MainContent_GrvBasicStudentInfo_hdnUserName_" + tmp).val());
                }
            }
            return username.join(',');
        }

        $("#MainContent_btnSend").click(function () {
            var user = '';

            if ($("#MainContent_chkAll").attr('checked'))
                user = $("#MainContent_hdnAllRecipients").val();
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
                //alert('Please Select Students!');
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
                var arr = $("input[id^=MainContent_GrvBasicStudentInfo_chkSelect]");
                for (i = 0; i < arr.length; i++) {
                    arr[i].checked = true;
                }
            }
            else {
                var arr = $("input[id^=MainContent_GrvBasicStudentInfo_chkSelect]");
                for (i = 0; i < arr.length; i++) {
                    arr[i].checked = false;
                }
            }
        });

        //For Send Message


        // for Cancel Message
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

        //for Full information of Student
        function OpenFullInfoPopup(userName) {
            // alert("kk");
            var student = {
                UserName: userName,
            };
            $.ajax({
                type: "POST",
                beforeSend: function () { $("#LoadingImage").show(); },
                complete: function () { $("#LoadingImage").hide(); },
                url: "StudentInfo.aspx/GetStudentData",
                data: JSON.stringify(student),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = msg.d;
                    document.getElementById("MainContent_lblFirstName").innerHTML = data[0];
                    document.getElementById("MainContent_lblMiddleName").innerHTML = data[1];
                    document.getElementById("MainContent_lblLastName").innerHTML = data[2];
                    document.getElementById("MainContent_lblCountryOfCitizenship").innerHTML = data[3];
                    document.getElementById("MainContent_lblAddress1").innerHTML = data[4];
                    if (data[5] == null)
                        document.getElementById("MainContent_imgStudent").src = "/Images/no_image.jpg";
                    else
                        document.getElementById("MainContent_imgStudent").src = "/Images/Profile/" + data[5];
                    document.getElementById("MainContent_lblAddress2").innerHTML = data[6];
                    document.getElementById("MainContent_lblzipcode").innerHTML = data[7];
                    document.getElementById("MainContent_lblPrimaryNumber").innerHTML = data[8];
                    document.getElementById("MainContent_lblBirthDate").innerHTML = data[9];
                    document.getElementById("MainContent_lblCity").innerHTML = data[10];
                    document.getElementById("MainContent_lblCountry").innerHTML = data[11];
                    document.getElementById("MainContent_lblPrimaryEmail").innerHTML = data[12];
                    if (data[13] != null && data[13] != 0)
                        document.getElementById("MainContent_lblCurrentlyIn").innerHTML = data[13];
                    else
                        document.getElementById("MainContent_lblCurrentlyIn").innerHTML = "Not Available";
                    if (data[14] != null && data[14] != 0)
                        document.getElementById("MainContent_lblLookingFor").innerHTML = data[14];
                    else
                        document.getElementById("MainContent_lblLookingFor").innerHTML = "Not Available";
                    if (data[15] != null && data[15] != "")
                        document.getElementById("MainContent_lblPreferStudyIn").innerHTML = data[15];
                    else
                        document.getElementById("MainContent_lblPreferStudyIn").innerHTML = "Not Available";
                    if (data[16] != null && data[16] != 0)
                        document.getElementById("MainContent_lblExpectedStartDate").innerHTML = data[16];
                    else
                        document.getElementById("MainContent_lblExpectedStartDate").innerHTML = "Not Available";
                    if (data[17] != null && data[17] != "")
                        document.getElementById("MainContent_lblPayout").innerHTML = data[17];
                    else
                        document.getElementById("MainContent_lblPayout").innerHTML = "Not Available";
                    if (data[18] != null && data[18] != "")
                        document.getElementById("MainContent_lbldesiredfieldstudy").innerHTML = data[18];
                    else
                        document.getElementById("MainContent_lbldesiredfieldstudy").innerHTML = "Not Available";

                    if (data[19] != null && data[19] != "")
                        document.getElementById("tblCurrentAcademics").innerHTML = data[19];
                    else
                        document.getElementById("tblCurrentAcademics").innerHTML = "Not Available";

                    if (data[20] != null && data[20] != "")
                        document.getElementById("tblInternationalTest").innerHTML = data[20];
                    else
                        document.getElementById("tblInternationalTest").innerHTML = "Not Available";

                    if (data[21] != null && data[21] != "")
                        document.getElementById("tblWorkHistory").innerHTML = data[21];
                    else
                        document.getElementById("tblWorkHistory").innerHTML = "Not Available";

                    if (data[22] != null && data[22] != "")
                        document.getElementById("lblSports").innerHTML = data[22];
                    else
                        document.getElementById("lblSports").innerHTML = "Not Available";

                    if (data[23] != null && data[23] != "")
                        document.getElementById("lblLeadership").innerHTML = data[23];
                    else
                        document.getElementById("lblLeadership").innerHTML = "Not Available";

                    if (data[24] != null && data[24] != "")
                        document.getElementById("lblActivity").innerHTML = data[24];
                    else
                        document.getElementById("lblActivity").innerHTML = "Not Available";


                    $("#FullInfo").dialog({ modal: true, minWidth: 1000, resizable: false, minHeight: 700, closeOnEscape: true, closeText: "" });

                },
                error: function (xhr, status, error) {
                    alert(xhr.statusText);
                }
            });

        }

        //For Scrolling
        //var flag = true;
        //$(window).scroll(function () {
        //    if ($(window).scrollTop() + $(window).height() == $(document).height()) {
        //        if (flag) {
        //            $("#waitdiv").show();
        //            flag = false
        //            $.ajax({
        //                type: "POST",
        //                beforeSend: function () { $("#waitdiv").show(); },
        //                complete: function () { $("#waitdiv").hide(); },
        //                url: "StudentInfo.aspx/AppendAndDisplayStudentList",
        //                contentType: "application/json; charset=utf-8",
        //                success: function (data) {
        //                    if (data.d == "no")
        //                        $("#noUniversityMsg").show();
        //                    else {
        //                        $("#noUniversityMsg").hide();
        //                        flag = true;
        //                        $("#MainContent_GrvBasicStudentInfo").append(data.d);
        //                    }
        //                    $("#waitdiv").hide();
        //                }
        //            });
        //        }
        //        else {

        //        }
        //    }
        //});

    </script>
</asp:Content>
