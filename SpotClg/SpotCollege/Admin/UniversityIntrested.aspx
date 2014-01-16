<%@ Page Title="Intrested University" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="UniversityIntrested.aspx.cs" Inherits="SpotCollege.Admin.UniversityIntrested" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="pattern_box">
    <div class="row-fluid">
        <div class="span12">
            <div class="shadowbox box_space">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <asp:HiddenField ID="hndUserName" Value="" runat="server" />
                <asp:HiddenField ID="hndUniversityId" Value="0" runat="server" />

                <asp:Panel ID="pnlUniversityInfo" runat="server">
                    <div id="UniversityInfo" runat="server" class="width100per">
                        <div class="h2_heading">
                            <h2>Intrested University Information</h2>
                        </div>
                        <asp:GridView ID="GrvUniversityInfo" CssClass="table box" runat="server" AutoGenerateColumns="false" DataKeyNames="UniversityId" EmptyDataText="No University Detail Found...!">
                            <Columns>
                                <asp:TemplateField HeaderText="UserName" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lnkBtnEdit" runat="server" Text='<%#Eval("UserName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="University Name" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUniversityName" runat="server" Text='<%# Eval("UniversityName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Address" HeaderText="Address" HeaderStyle-Width="10%" ItemStyle-Width="10%"></asp:BoundField>
                                <asp:BoundField HeaderText="City" DataField="City" HeaderStyle-Width="10%" ItemStyle-Width="10%" />
                                <asp:BoundField HeaderText="Country" DataField="Country" HeaderStyle-Width="10%" ItemStyle-Width="10%" />
                                <asp:TemplateField HeaderText="Is Active" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkStatus" runat="server" AutoPostBack="true" OnCheckedChanged="chkStatus_CheckedChanged"
                                            Checked='<%# Convert.ToBoolean(Eval("User.IsActive")) %>' Text='<%# Eval("User.IsActive").ToString().Equals("True") ? " Active " : " InActive" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkBtnDelete" runat="server" Text="Delete" CommandArgument='<%#Eval("UniversityId") %>' OnClick="lnkBtnDelete_Click" OnClientClick="return confirmDelete();"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
        </div>

    <div id="dialog-confirm" title="Are You Sure?" style="display: none;">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Are you sure?</p>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#nav li").each(function () {
                $(this).removeClass('navi_active');
            });
            $("#lnkintrestUniversity").addClass("navi_active");

        });
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
    </script>
</asp:Content>


